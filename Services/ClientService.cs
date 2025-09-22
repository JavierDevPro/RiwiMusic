using RiwiMusic.Models;
using System.Linq;
namespace RiwiMusic.Services;

public class ClientService
{
    // In-memory client management service: add, list, remove, update, and fetch by ID
    private static List<Client> clients = new List<Client>  {
        new Client { idPerson = 1, name = "Juan Perez", email = "juan@email.com", age = 25, dni = "12345678", phone = "300-123-4567", gender = "Masculino" },
        new Client { idPerson = 2, name = "Ana Garcia", email = "ana@email.com", age = 30, dni = "87654321", phone = "300-987-6543", gender = "Femenino" },
        new Client { idPerson = 3, name = "Carlos Lopez", email = "carlos@email.com", age = 28, dni = "11223344", phone = "300-555-1234", gender = "Masculino" },
        new Client { idPerson = 4, name = "María Rodrguez", email = "maria@email.com", age = 22, dni = "44332211", phone = "300-444-5555", gender = "Femenino" },
        new Client { idPerson = 5, name = "Luis Martines", email = "luis@email.com", age = 35, dni = "55667788", phone = "300-777-8888", gender = "Masculino" }
    };

    // Register client
    public void AddClient(Client client)
    {
        clients.Add(client);
    }

    // List clients
    public void ListClients()
    {
        foreach (var client in clients)
        {
            Console.WriteLine($"ID: {client.idPerson}, Nombre: {client.name}, Email: {client.email}, Edad: {client.age}, DNI: {client.dni}, Teléfono: {client.phone}, Género: {client.gender}");
        }
    }

    // Remove client
    public void RemoveClient(Client client)
    {
        clients.Remove(client);
    }

    // Update an existing client by ID using values from updatedClient (ID is not modified)
    public bool UpdateClient(int id, Client updatedClient)
    {
        var existingClient = GetClientById(id);
        if (existingClient == null)
        {
            return false;
        }

        // Update properties individually (safer than replacing)
        existingClient.name = updatedClient.name;
        existingClient.email = updatedClient.email;
        existingClient.age = updatedClient.age;
        existingClient.dni = updatedClient.dni;
        existingClient.phone = updatedClient.phone;
        existingClient.gender = updatedClient.gender;
        // Do not change the ID
    
        return true;
    }
    
    // Find client by ID (for controllers or validations)
    public Client? GetClientById(int id)
    {
        return clients.FirstOrDefault(c => c.idPerson == id);
    }
}