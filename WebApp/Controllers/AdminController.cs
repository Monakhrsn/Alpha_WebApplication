using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Members()
    {
        return View();
    }
}