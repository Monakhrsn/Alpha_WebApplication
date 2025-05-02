using Domain.Models;

namespace WebApp.Models;

public class ClientsViewModel
{
    public IEnumerable<Client> Clients { get; set; } = [];

    public AddClientFormData AddClientFormData{ get; set; } = new();
    public EditClientForm EditProjectFormData { get; set; } = new();
}