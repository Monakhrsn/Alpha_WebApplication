using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]
/*[Route("admin")] */
public class AdminController(IMemberService memberService) : Controller
{
    private readonly IMemberService _memberService = memberService;

    /*  [Route("/")] */
    public IActionResult Index()
    {
        return View();
    }
    
   /* [Route("projects")] */
    public IActionResult Projects()
    {
        return View();
    } 
    
   /* [Authorize(Roles = "Admin")] */
   /* [Route("members")] */
    public async Task<IActionResult> Members()
    {
        var members = await _memberService.GetAllMembers();
        var membersWrapper = new MembersWrapper
        {
            NewMember = new AddMemberForm(), // An empty form
            Members = members // List of members
        };
        
        return View(membersWrapper);
    } 
    
   /* [Authorize(Roles = "Admin")] */
   /* [Route("clients")] */
    public IActionResult Clients()
    {
        return View();
    } 
}