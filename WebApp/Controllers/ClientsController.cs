using Microsoft.AspNetCore.Mvc;
using Business.Models;

namespace WebApp.Controllers;

public class ClientsController : Controller
{
   /* private readonly ClientService _clientService; */
    [HttpPost]
    public IActionResult Add(AddClientForm form)
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
     /*   var result = await _clientService.AddClientAsync(form);
        if (result)
        {
            return Ok(new { success = true });
        }
        else
        {
            return Problem("Unable to submit data.")
        } */
        
        return Ok(new { success = true });
    }
    
    [HttpPost]
    public IActionResult Edit(EditClientForm form)
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
        return Ok(new { success = true });
    } 
}