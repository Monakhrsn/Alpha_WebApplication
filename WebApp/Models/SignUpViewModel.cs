using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class SignUpViewModel
{
    [Required]
    [Display(Name = "First Name", Prompt = "Enter first name")]
    [DataType(DataType.Text)]
    public string FirstName { get; set; } = null!; 
    
    [Required]
    [Display(Name = "Last Name", Prompt = "Enter last name")]
    [DataType(DataType.Text)]
    public string LastName { get; set; } = null!; 
    
    [Required]
    [EmailAddress]
    [Display(Name = "Email", Prompt = "Enter e-mail address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!; 

    [Required(ErrorMessage = "{0} is required.")]
    [StringLength(100, ErrorMessage = "{0} must be at least {2} characters and at most {1} long, include a special character and a capital letter .", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter Password")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "{0} must contain at least one uppercase letter and one number.")]
    public string Password { get; set; } = null!; 
    
    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
    [Display(Name = "ConfirmPassword", Prompt = "Confirm password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!; 
    
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions to use this site.")]
    public bool TermsAndConditions { get; set; }
}