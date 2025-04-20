using Domain.Models;

namespace WebApp.ViewModels;

public class MembersViewModel
{
    public AddMemberForm NewMemberForm { get; set; } = new();
    public IEnumerable<Member> Members { get; set; } = new List<Member>();
}
 