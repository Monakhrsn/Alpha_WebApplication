using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult Login()
    {
        return LocalRedirect("/projects");
        //return View();
    }
}