using Microsoft.AspNetCore.Identity;

namespace Data.Entities;
// it seems that it is memberEntity
public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string? FirstName { get; set; }
    
    [ProtectedPersonalData]
    public string? LastName { get; set; }
    
    [ProtectedPersonalData]
    public string? JobTitle { get; set; }
    
    public virtual MemberAddressEntity? Address { get; set; }
    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
} 
