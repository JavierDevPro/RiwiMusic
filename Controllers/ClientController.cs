using RiwiMusic.Models;
using RiwiMusic.Services;

namespace RiwiMusic.Controllers;

/// shows a menu and routes to actions to register, list, and delete clients.
/// Console messages remain in Spanish.
public class ClientController
{
    private ClientService clientService = new ClientService();
    
    /// Displays the client management menu and handles user input.
    public void Menu()
    {
        int option;
        do
        {
            Console.WriteLine("\n=== Gestión de Clientes ===");
            Console.WriteLine("1. Registrar cliente");
            Console.WriteLine("2. Listar clientes");
            Console.WriteLine("3. Eliminar cliente");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("Elige una opción: ");
            
            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1: RegisterClient(); break;
                    case 2: ListClients(); break;
                    case 3: DeleteClient(); break;
                    case 0: Console.WriteLine("Volviendo al menú principal..."); break;
                    default: Console.WriteLine("Opción inválida. Intenta de nuevo."); break;
                }
            }
            else
            {
                Console.WriteLine("Por favor ingresa un número válido.");
                option = -1; // Para que no salga del bucle
            }
        } while (option != 0);
    }
    
    /// Prompts the user for client data and registers a new client if the ID is unique.
    private void RegisterClient()
    {
        Console.WriteLine("\n--- Registrar Cliente ---");
        
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        // Verificar si ya existe un cliente con ese ID
        if (clientService.GetClientById(id) != null)
        {
            Console.WriteLine("Ya existe un cliente con ese ID.");
            return;
        }

        Console.Write("Nombre: ");
        string name = Console.ReadLine() ?? "";
        
        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";
        
        Console.Write("Edad: ");
        if (!int.TryParse(Console.ReadLine(), out int age))
        {
            Console.WriteLine("Edad inválida.");
            return;
        }
        
        Console.Write("DNI: ");
        string dni = Console.ReadLine() ?? "";
        
        Console.Write("Teléfono: ");
        string phone = Console.ReadLine() ?? "";
        
        Console.Write("Género: ");
        string gender = Console.ReadLine() ?? "";

        var client = new Client
        {
            idPerson = id,
            name = name,
            email = email,
            age = age,
            dni = dni,
            phone = phone,
            gender = gender
        };

        clientService.AddClient(client);
        Console.WriteLine("Cliente registrado exitosamente.");
    }
    
    /// Lists all clients via the service.
    private void ListClients()
    {
        Console.WriteLine("\n--- Lista de Clientes ---");
        clientService.ListClients();
    }
    
    /// Deletes a client by ID after user confirmation.
    private void DeleteClient()
    {
        Console.WriteLine("\n--- Eliminar Cliente ---");
        
        Console.Write("Ingresa el ID del cliente a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var client = clientService.GetClientById(id);
        if (client == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }

        Console.WriteLine($"Cliente encontrado: {client.name} ({client.email})");
        Console.Write("¿Estás seguro de eliminarlo? (s/n): ");
        string confirmation = Console.ReadLine()?.ToLower() ?? "";
        
        if (confirmation == "s" || confirmation == "si")
        {
            clientService.RemoveClient(client);
            Console.WriteLine("Cliente eliminado exitosamente.");
        }
        else
        {
            Console.WriteLine("Eliminación cancelada.");
        }
    }
}