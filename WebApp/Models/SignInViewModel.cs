using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class SignInViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email", Prompt = "Enter e-mail address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!; 

    [Required(ErrorMessage = "{0} is required.")]
    [StringLength(100, ErrorMessage = "{0} must be at least {2} characters and at most {1} long.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter Password")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "{0} must contain at least one uppercase letter and one number.")]
    public string Password { get; set; } = null!; 
    
    public bool IsPersistent { get; set; }
}