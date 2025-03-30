using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ProjectsController : Controller
{ 
    /*[Route("")] */
    [HttpPost]
    public IActionResult Projects()
    {
        return View();
    }
    
  /*  [Route("add")]
    public IActionResult AddProjects()
    {
        return View();
    }
    */
}