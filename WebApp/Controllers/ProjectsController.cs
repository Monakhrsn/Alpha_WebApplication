using System.Security.Claims;
using Business.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApp.Models;

namespace WebApp.Controllers;

[Authorize]
[Route("admin/[controller]")]
public class ProjectsController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    public async Task<IActionResult> Index()
    {
         var result = await _projectService.GetProjectsAsync();
         var viewModel = new ProjectsViewModel
         {
             Projects = result.Result ?? new List<Project>(),
             AddProjectFormData = new AddProjectViewModel(),
             EditProjectFormData = new EditProjectViewModel()
         };
             
         return View(viewModel);
    }
    
    [HttpPost("add")]
     public async Task<IActionResult> Add(AddProjectViewModel model)
     {
         if (!ModelState.IsValid)
             return BadRequest(new { error = "Invalid project data submitted." });
         
         var addProjectFormData = model.MapTo<AddProjectFormData>();
         
         var result = await _projectService.CreateProjectAsync(addProjectFormData);
         // //  Get the current user ID
         // var userId = User?.Identity?.IsAuthenticated == true 
         //     ? User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type.Contains("nameIdentifier"))?.Value 
         //     : null;
         //
         // if (userId == null)
         //     return BadRequest("User is not authenticated");

         // Assign the user ID
         // addProjectFormData.UserId = userId;
         
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