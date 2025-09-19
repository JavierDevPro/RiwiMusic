using RiwiMusic.Models;
namespace RiwiMusic.Services;

public class ClientService
{
    private List<Client> clients = new List<Client>();

    // Register a Client!
    public void addClient(Client client)
    {
        clients.Add(client);
    }

    // Listar Clientes
    public void listClients()
    {
        foreach (var client in clients)
        {
            Console.WriteLine($"Cliente: ");
        }
    }
}