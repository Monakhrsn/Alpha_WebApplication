using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class EditProjectViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [DataType(DataType.Upload)]
    [Display(Name = "Project Image")]
    public IFormFile? Image { get; set; }
    
    [DataType(DataType.Text)]
    [Display(Name = "Project Name")]
    [Required(ErrorMessage = "Required")]
    public string ProjectName { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "Client Name")]
    [Required(ErrorMessage = "Required")]
    public string ClientId { get; set; } = null!;
    
    
    [DataType(DataType.Text)]
    [Display(Name = "Description")]
    [Required(ErrorMessage = "Required")]
    public string? Description { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Start date")]
    [Required(ErrorMessage = "Required")]
    public DateTime? StartDate { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "End date")]
    public DateTime? EndDate { get; set; }
    
    [DataType(DataType.Currency)]
    [Display(Name = "Budget")]
    [Required(ErrorMessage = "Required")]
    public decimal? Budget { get; set; }
}