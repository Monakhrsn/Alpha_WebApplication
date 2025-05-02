

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class AddClientFormData
{
    [DataType(DataType.Upload)]
    [Display(Name = "Client Image" , Prompt = "Select an image")]

    public IFormFile? ClientImage { get; set; }
    
    [DataType(DataType.Text)]
    [Display(Name = "Client Name" , Prompt = "Enter client name")]
    [Required(ErrorMessage = "Required")]
    public string Name { get; set; } = null!;
    
    [Display(Name = "Email Address" , Prompt = "Enter email")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;
    
    [Display(Name = "Location" , Prompt = "Enter location")]
    [DataType(DataType.Text)]
    public string? Location { get; set; }
    
    [Display(Name = "Phone Number" , Prompt = "Enter phone number")]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; } 
}