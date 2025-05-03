using Domain.Models;

namespace WebApp.ViewModels;

public class MembersViewModel
{
    public AddMemberFormData NewMemberForm { get; set; } = new();
    public IEnumerable<Member> Members { get; set; } = new List<Member>();
}
 