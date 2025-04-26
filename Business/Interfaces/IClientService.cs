using Business.Models;
using Domain.Models;

namespace Business.Interfaces;

public interface IClientService
{
    Task<ClientResult> CreateAsync(AddClientForm addClientForm);   
    Task<ClientResult> GetClientsAsync();
}