using Business.Models;

namespace WebApp.ViewModels;

public class MembersWrapper
{
    public AddMemberForm NewMember { get; set; } = new();
    public IEnumerable<Member> Members { get; set; } = new List<Member>();
}
