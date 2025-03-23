using Microsoft.AspNetCore.Mvc;
using Business.Models;

namespace WebApp.Controllers;

[Route("clients")]
public class ClientsController : Controller
{
    [HttpPost("add")]
    public IActionResult AddClient(AddClientForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                );
            return BadRequest(new { success = false, errors });
        }
            
        // Send data to client service
        return RedirectToAction("Clients", "Admin");
    }
    
    [HttpPut("edit")]
    public IActionResult EditClient(EditClientForm form)
    {
        if(!ModelState.IsValid)
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                    );
                return BadRequest(new { success = false, errors });
            }
            
        // Send data to client service
        return RedirectToAction("Clients", "Admin");
    }
}