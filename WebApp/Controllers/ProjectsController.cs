using System.Security.Claims;
using Business.Interfaces;
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

    public async Task<IActionResult> Index()
    {
         var result = await _projectService.GetProjectsAsync();
         var clientResult = await _clientService.GetClientsAsync();
         ViewBag.Clients = clientResult.Result; 
         var viewModel = new ProjectsViewModel()
         {
             Projects = result.Result!,
                 
             AddProjectFormData = new AddProjectViewModel(),
             EditProjectFormData = new EditProjectViewModel(),
         };
             
         return View(viewModel);
    }
    
    [HttpPost("add")]
     public async Task<IActionResult> Add(AddProjectViewModel model)
     {
         if (!ModelState.IsValid)
             return BadRequest(new { error = "Invalid project data submitted." });
         
         var addProjectFormData = model.MapTo<AddProjectFormData>();
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
    public IActionResult Update(int model)
    {
        return Json(new {});
    }
     
    [HttpPost("delete")]
    public IActionResult Delete(string id)
    {
        return Json(new {});
    }
}