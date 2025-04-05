using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class MemberSignUpForm
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
    [Display(Name = "Email", Prompt = "Enter e-mail address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!; 

    [Required]
    [Display(Name = "Password", Prompt = "Enter password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!; 
    
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
    [Display(Name = "ConfirmPassword", Prompt = "Confirm password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!; 
    
    [Display(Name = "Phone", Prompt = "Enter phone number")]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }
    
    [Display(Name = "Terms And Conditions", Prompt = "I accept the term and conditions.")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions to use this site.")]
    public bool TermsAndConditions { get; set; }
}