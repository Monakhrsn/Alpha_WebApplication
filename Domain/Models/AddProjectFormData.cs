using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.Models;
//AddFormDto 
public class AddProjectFormData
{
    [DataType(DataType.Upload)]
    [Display(Name = "Project Image" , Prompt = "Select an image")]
    public IFormFile? Image { get; set; }
    
    [DataType(DataType.Text)]
    [Display(Name = "Project Name" , Prompt = "Enter project name")]
    [Required(ErrorMessage = "Required")]
    public string ProjectName { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "Description" , Prompt = "Enter description")]
    public string? Description { get; set; }
    
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Required")]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }
    
    [DataType(DataType.Currency)]
    [Display(Name = "Budget" , Prompt = "Enter budget if it is available")]
    public decimal? Budget { get; set; }
    
    public string ClientId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public int StatusId { get; set; }
}