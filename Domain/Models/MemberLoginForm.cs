using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class MemberLoginForm
{
     [Required]
     [Display(Name = "Email", Prompt = "Enter e-mail address")]
     [DataType(DataType.EmailAddress)]
     public string Email { get; set; } = null!; 

     [Required]
     [Display(Name = "Password", Prompt = "Enter password")]
     [DataType(DataType.Password)]
     public string Password { get; set; } = null!; 
}