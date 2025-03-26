using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("admin")]
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
    
    [Route("members")]
    public IActionResult Members()
    {
        return View();
    }
    
    [Route("clients")]
    public IActionResult Clients()
    {
        return View();
    }
}