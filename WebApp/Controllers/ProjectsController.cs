using Business.Interfaces;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class ProjectsController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    public async Task<IActionResult> Index()
    {
        var model = new ProjectsViewModel
        {
            Projects = await _projectService.GetProjectsAsync(),
        };
        
        return View(model);
    }
    
    [HttpPost]
     public async Task<IActionResult> Add(AddProjectViewModel model)
     {
         var addProjectFormData = model.MapTo<AddProjectFormData>();
         
         var result = await _projectService.CreateProjectAsync(addProjectFormData);
         return Json(new {});
     }
     
    [HttpPost]
    public async Task<IActionResult> Update(EditProjectViewModel model)
    {
        return Json(new {});
    }
     
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        return Json(new {});
    }
}