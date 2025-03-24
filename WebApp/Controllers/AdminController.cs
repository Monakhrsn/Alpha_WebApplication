using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("admin")]
public class AdminController : Controller
{
    [Route("clients")]
    public IActionResult Clients()
    {
        return View();
    }
    
    [Route("members")]
    public IActionResult Members()
    {
        return View();
    }
}