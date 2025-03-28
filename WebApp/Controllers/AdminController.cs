using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("admin")]
[Authorize]
public class AdminController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }
    
    [Route("projects")]
    public IActionResult Projects()
    {
        return View();
    }
    
   /* [Authorize(Roles = "Admin")] */
    [Route("members")]
    public IActionResult Members()
    {
        return View();
    }
    
   /* [Authorize(Roles = "Admin")] */
    [Route("clients")]
    public IActionResult Clients()
    {
        return View();
    }
}