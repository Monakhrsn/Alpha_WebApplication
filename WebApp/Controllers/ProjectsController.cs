using System.Security.Claims;
using Business.Interfaces;
using Business.Models;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

[Authorize]
[Route("admin/[controller]")]
public class ProjectsController(IProjectService projectService, IClientService clientService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    private readonly IClientService _clientService = clientService;

    
    // Communicated with chatgpt: This Index which is an asynchronous controller action method builds up all the data needed for a "Projects" page
    // It accepts an optional query string parameter status, which defaults to "all" if not provided,  
    // and returns an IActionResult, which usually means it will render a View or return a result (like a redirect, JSON, etc.).
    public async Task<IActionResult> Index([FromQuery] string status = "all")
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        
        ProjectResult<IEnumerable<Project>>? result;
        if(status.ToLower().Equals("completed") )
            result = await _projectService.GetProjectsAsync(userId, true);
        else if (status.ToLower().Equals("started"))
            result = await _projectService.GetProjectsAsync(userId, false);
        else
            result = await _projectService.GetAllProjectsAsync(userId);
        
        var clientResult = await _clientService.GetClientsAsync();
        ViewBag.Clients = clientResult.Result;

        var projectCount = await _projectService.GetProjectsCountAsync(userId);
        var completedProjectCount = await _projectService.GetProjectsCountAsync(userId, true);
        var startedProjectCount = await _projectService.GetProjectsCountAsync(userId, false);
        
        var viewModel = new ProjectsViewModel()
        {
         Projects = result.Result!,
             
         AddProjectFormData = new AddProjectViewModel(),
         EditProjectFormData = new EditProjectViewModel(),
         AllProjectCount = projectCount.Result!,
         CompletedProjectCount = completedProjectCount.Result!,
         StartedProjectCount = startedProjectCount.Result!,
        };
         
        return View(viewModel);
    }
    
    [HttpPost("add")]
     public async Task<IActionResult> Add(AddProjectViewModel model)
     {
         if (!ModelState.IsValid)
         {
             var errors = ModelState
                 .Where(x => x.Value?.Errors.Count > 0)
                 .ToDictionary(
                     kvp => kvp.Key,
                     kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                 );
             return BadRequest(new { success = false, errors });
         }
         
         var addProjectFormData = model.MapTo<AddProjectFormData>();
         addProjectFormData.StartDate = model.StartDate!.Value;
         addProjectFormData.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
         
         var result = await _projectService.CreateProjectAsync(addProjectFormData);
         
         if (!result.Succeeded)
         {
             // Log to console and return error
             Console.WriteLine($"[ADD ERROR] {result.Error}");
             return BadRequest(new { result.Error });
         }

         return Json(new { message = "Project created successfully." });
     }

    [HttpPost("update")]
    public async Task<IActionResult> Update(EditProjectViewModel model)
    {
        if (!ModelState.IsValid)
        {
             var errors = ModelState
                 .Where(x => x.Value?.Errors.Count > 0)
                 .ToDictionary(
                     kvp => kvp.Key,
                     kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                 );
             return BadRequest(new { success = false, errors });
        }

        var updateData = model.MapTo<EditProjectFormData>();
        updateData.StartDate = model.StartDate!.Value;
        var result = await _projectService.UpdateProjectAsync(updateData);

        if (!result.Succeeded)
        {
            Console.WriteLine($"[UPDATE ERROR] {result.Error}");
            return BadRequest(new { error = result.Error });
        }
        return Json(new { message = "Project updated successfully." });
    }
     
    [HttpPost("delete")]
    public IActionResult Delete(string id)
    {
        return Json(new {});
    }
}