using Domain.Models;

namespace WebApp.Models;

public class ClientsViewModel
{
    public IEnumerable<Client> Clients { get; set; } = [];

    public AddClientFormData AddClientFormData{ get; set; } = new();
    public EditClientFormData EditProjectFormData { get; set; } = new();
}