using Business.Models;
using Data.Entities;

namespace Data.Interfaces;

public interface IUserRepository : IBaseRepository<UserEntity, User>
{
    
}