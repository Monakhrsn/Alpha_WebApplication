using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize]
[Route("admin/[controller]")]
public class OverviewController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}