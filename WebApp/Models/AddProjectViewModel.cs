using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AddProjectViewModel
{
    [DataType(DataType.Upload)]
    [Display(Name = "Project Image" , Prompt = "Select an image")]

    public IFormFile? ProjectImage { get; set; }
    
    [DataType(DataType.Text)]
    [Display(Name = "Project Name" , Prompt = "Project Name")]
    [Required(ErrorMessage = "Required")]
    public string ProjectName { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "Client Name" , Prompt = "Client Name")]
    [Required(ErrorMessage = "Required")]
    public string ClientName { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "Description" , Prompt = "Describe about the project")]
    [Required(ErrorMessage = "Required")]
    public string Description { get; set; } = null!;
    
    
    [DataType(DataType.DateTime)]
    [Display(Name = "Start date" , Prompt = "Start date")]
    [Required(ErrorMessage = "Required")]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.DateTime)]
    [Display(Name = "End date" , Prompt = "End date")]
    [Required(ErrorMessage = "Required")]
    public DateTime? EndDate { get; set; }
    
    /*[DataType(DataType.decimal)]*/
    [Display(Name = "Budget" , Prompt = "Budget")]
    [Required(ErrorMessage = "Required")]
    public decimal? Budget { get; set; }
}

