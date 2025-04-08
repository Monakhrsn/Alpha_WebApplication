using Business.Models;
using Data.Entities;

namespace Data.Interfaces;

public interface IClientRepository : IBaseRepository<ClientEntity, Client>
{
    
}