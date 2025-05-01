using System.ComponentModel.DataAnnotations;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class AddProjectViewModel
{
    [DataType(DataType.Upload)]
    [Display(Name = "Project Image" , Prompt = "Select an image")]
    public IFormFile? Image { get; set; }
    
    [DataType(DataType.Text)]
    [Display(Name = "Project Name" , Prompt = "Project Name")]
    [Required(ErrorMessage = "Required")]
    public string ProjectName { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "Client Name" , Prompt = "Client Name")]
    [Required(ErrorMessage = "Required")]
    public string ClientId { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "Description" , Prompt = "Describe about the project")]
    [Required(ErrorMessage = "Required")]
    public string Description { get; set; } = null!;


    [DataType(DataType.Date)]
    [Display(Name = "Start Date", Prompt = "Start date")]
    [Required(ErrorMessage = "Required")]
    public DateTime? StartDate { get; set; } = null;
    
    [DataType(DataType.Date)]
    [Display(Name = "End Date" , Prompt = "End date")]
    public DateTime? EndDate { get; set; }
    
    [DataType(DataType.Currency)]
    [Display(Name = "Budget" , Prompt = "Budget")]
    [Required(ErrorMessage = "Required")]
    public decimal? Budget { get; set; }
}

