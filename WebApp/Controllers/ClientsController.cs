
using Business.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize]
[Route("admin/[controller]")]
public class ClientsController : Controller
{
   private readonly IClientService _clientService;

   public ClientsController(IClientService clientService)
   {
       _clientService = clientService;
   }

   [HttpGet]
   public IActionResult Index()
   {
       return View();
   }
   
   [HttpPost]
    public async Task<IActionResult> Add(AddClientForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );
            return BadRequest(new { success = false, errors });
        }
            
        // Send data to client service
        var result = await _clientService.CreateAsync(form);
        if (result.Succeeded)
        {
            return Ok(new { success = true });
        }
        
        return Problem("Unable to submit data.");

    }
    
    [HttpPut]
    public IActionResult Edit(EditClientForm form)
    {
        if(!ModelState.IsValid)
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                    );
                return BadRequest(new { success = false, errors });
            }
            
        // Send data to client service
        return Ok(new { success = true });
    } 
}