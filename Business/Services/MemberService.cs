using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Domain.Extensions;

namespace Business.Services;

public class MemberService(IMemberRepository memberRepository) : IMemberService
{
    private readonly IMemberRepository _memberRepository = memberRepository;

    public async Task<MemberResult> GetMembersAsync()
    {
        var result = await _memberRepository.GetAllAsync();
        return result.MapTo<MemberResult>();
    }
}






