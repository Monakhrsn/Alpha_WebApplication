using Business.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
         var viewModel = new ProjectsViewModel()
         {
             Projects = SetProjects(),
                 
             AddProjectFormData = new AddProjectViewModel(),
             EditProjectFormData = new EditProjectViewModel()
         };
             
         return View(viewModel);
    }
    
    private IEnumerable<ProjectViewModel> SetProjects()
    {
        var projects = new List<ProjectViewModel>

        {
            new() {
                Id = Guid.NewGuid().ToString(),
                ProjectName = "Website Redesign",
                ProjectImage = "/images/projects/project-image.svg",
                ClientName = "GitLab Inc.",
                Description = "<p>It is <strong>necessary</strong> to develop a website redesign in a corporate style.</p>",
                TimeLeft = "1 week left",
                Members = ["/images/avatars/avatar-template.svg"]
            }
        };

        return projects;
    }

    
    [HttpPost("add")]
     public async Task<IActionResult> Add(AddProjectViewModel model)
     {
         if (!ModelState.IsValid)
             return BadRequest(new { error = "Invalid project data submitted." });
         
         var addProjectFormData = model.MapTo<AddProjectFormData>();
         
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