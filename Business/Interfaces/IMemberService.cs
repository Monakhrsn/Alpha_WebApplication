using Business.Models;

namespace Business.Interfaces;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetAllMembers();
}