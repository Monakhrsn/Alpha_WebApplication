using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AuthController(IAuthService _authService) : Controller
{
    public IActionResult Login()
    {
        ViewBag.ErrorMessage = null;
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(MemberLoginForm form, string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = null;
        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(form);
            
            if (result)
            {
                return Redirect(returnUrl);  
            }
        }
        
        ViewBag.ErrorMessage = "Incorrect email or password.";
        return View(form);
    }
}