using Business.Models;
using Data.Entities;

namespace Data.Interfaces;

public interface IMemberRepository : IBaseRepository<MemberEntity, Member>
{
    
}