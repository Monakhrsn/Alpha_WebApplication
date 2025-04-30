using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class ProjectViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Required(ErrorMessage = "Project image is required.")]
    public string ProjectImage { get; set; } = null!;
    
    [Required(ErrorMessage = "Project name is required.")]
    [StringLength(100, ErrorMessage = "Project name cannot be more than 100 characters.")]
    public string ProjectName { get; set; } = null!;
    
    [Required(ErrorMessage = "Client name is required.")]
    public string ClientName { get; set; } = null!;
    
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = null!;
    
    public string TimeLeft { get; set; } = null!;
    public IEnumerable<string> Members { get; set; } = [];
}