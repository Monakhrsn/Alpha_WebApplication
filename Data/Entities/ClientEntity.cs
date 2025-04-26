using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class ClientEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string? Phone { get; set; }
    
    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}

