using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class EditProjectViewModel
{
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
    public string Description { get; set; } = null!;
    
    [DataType(DataType.Date)]
    [Display(Name = "Start date")]
    [Required(ErrorMessage = "Required")]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "End date")]
    public DateTime? EndDate { get; set; }
    
    [DataType(DataType.Currency)]
    [Display(Name = "Budget")]
    public decimal? Budget { get; set; }
    
    [Display(Name = "Status")]
    public int StatusId { get; set; }
}