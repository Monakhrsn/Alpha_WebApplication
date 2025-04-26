using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;
    
    public async Task<ClientResult> CreateAsync(AddClientForm addClientForm)
    {
        var clientEntity = addClientForm.MapTo<ClientEntity>();
        var result = await _clientRepository.AddAsync(clientEntity);
        
        return result.Succeeded
            ? new ClientResult() { Succeeded = true, StatusCode = 201 }
            : new ClientResult() { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }

    public async Task<ClientResult> GetClientsAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        return result.MapTo<ClientResult>();
    }
}






