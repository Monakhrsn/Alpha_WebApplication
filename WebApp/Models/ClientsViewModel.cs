using Domain.Models;

namespace WebApp.Models;

public class ClientsViewModel
{
    public IEnumerable<Client> Clients { get; set; } = [];

    public AddClientForm AddClientForm{ get; set; } = new();
    public EditClientForm EditProjectFormData { get; set; } = new();
}