using Business.Models;
using Domain.Models;

namespace Business.Interfaces;

public interface IMemberService 
{
    Task<MemberResult> GetMembersAsync();
}