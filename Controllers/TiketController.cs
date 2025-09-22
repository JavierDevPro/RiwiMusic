using RiwiMusic.Services;
using RiwiMusic.Models;
using System.Linq;

namespace RiwiMusic.Controllers;
/// Controller for ticket operations: menu navigation and actions to register, list, edit, delete,
/// and run LINQ-based queries. Console messages remain in Spanish.

public class TiketController
{
    private TiketService tiketService = new TiketService();
    
    /// Displays the menu and routes user selections to corresponding actions.
    public void Menu()
    {
        int option;
        do
        {
            Console.WriteLine("\n=== Gestión de Tiquetes ===");

            Console.WriteLine("1. Registrar compra de tiquete");
            Console.WriteLine("2. Listar tiquetes vendidos");
            Console.WriteLine("3. Editar compra");
            Console.WriteLine("4. Eliminar compra");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("Elige una opción: ");
            
            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1: RegisterTicket(); break;
                    case 2: ListTickets(); break;
                    case 3: EditTicket(); break;
                    case 4: DeleteTicket(); break;
                    case 0: Console.WriteLine("Volviendo al menú principal..."); break;
                    case 5: ConcertWithMostTickets(); break;
                    case 6: TicketsByClient(); break;
                    case 7: TotalIncomeByConcert(); break;
                    default: Console.WriteLine(" Opción inválida. Intenta de nuevo."); break;
                }
            }
            else
            {
                Console.WriteLine(" Por favor ingresa un número válido.");
                option = -1;
            }
        } while (option != 0);
    }
    
    /// Prompts the user for ticket data and registers a new purchase.
    private void RegisterTicket()
    {
        Console.WriteLine("\n--- Registrar Compra de Tiquete ---");
        
        Console.Write("ID del tiquete: ");
        if (!int.TryParse(Console.ReadLine(), out int ticketId))
        {
            Console.WriteLine(" ID inválido.");
            return;
        }

        if (tiketService.GetTicketById(ticketId) != null)
        {
            Console.WriteLine(" Ya existe un tiquete con ese ID.");
            return;
        }

        Console.Write("ID del cliente: ");
        if (!int.TryParse(Console.ReadLine(), out int clientId))
        {
            Console.WriteLine(" ID de cliente inválido.");
            return;
        }

        Console.Write("ID del concierto: ");
        if (!int.TryParse(Console.ReadLine(), out int concertId))
        {
            Console.WriteLine(" ID de concierto inválido.");
            return;
        }

        Console.Write("Precio: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine(" Precio inválido.");
            return;
        }

        var tiket = new Tiket
        {
            idTiket = ticketId,
            idClient = clientId,
            idConcert = concertId,
            price = price,
            purchaseDate = DateTime.Now
        };

        tiketService.AddTicket(tiket);
        Console.WriteLine("Tiquete registrado exitosamente.");
    }
    
    /// Lists all registered tickets.
    private void ListTickets()
    {
        Console.WriteLine("\n--- Lista de Tiquetes Vendidos ---");
        tiketService.ListTikets();
    }
    
    /// Edits an existing ticket, allowing the user to keep current values.
    private void EditTicket()
    {
        Console.WriteLine("\n--- Editar Compra ---");
        
        Console.Write("ID del tiquete a editar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine(" ID inválido.");
            return;
        }

        var existingTicket = tiketService.GetTicketById(id);
        if (existingTicket == null)
        {
            Console.WriteLine(" Tiquete no encontrado.");
            return;
        }

        Console.WriteLine($"Tiquete encontrado - Cliente: {existingTicket.idClient}, Concierto: {existingTicket.idConcert}");
        Console.WriteLine("Deja en blanco para mantener el valor actual:");

        Console.Write($"Nuevo ID Cliente ({existingTicket.idClient}): ");
        string clientInput = Console.ReadLine();
        int clientId = existingTicket.idClient;
        if (!string.IsNullOrWhiteSpace(clientInput) && int.TryParse(clientInput, out int newClientId))
        {
            clientId = newClientId;
        }

        Console.Write($"Nuevo ID Concierto ({existingTicket.idConcert}): ");
        string concertInput = Console.ReadLine();
        int concertId = existingTicket.idConcert;
        if (!string.IsNullOrWhiteSpace(concertInput) && int.TryParse(concertInput, out int newConcertId))
        {
            concertId = newConcertId;
        }

        Console.Write($"Nuevo precio (${existingTicket.price}): ");
        string priceInput = Console.ReadLine();
        decimal price = existingTicket.price;
        if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal newPrice))
        {
            price = newPrice;
        }

        var updatedTicket = new Tiket
        {
            idTiket = id,
            idClient = clientId,
            idConcert = concertId,
            price = price,
            purchaseDate = existingTicket.purchaseDate
        };

        if (tiketService.UpdateTicket(id, updatedTicket))
        {
            Console.WriteLine("Tiquete actualizado exitosamente.");
        }
        else
        {
            Console.WriteLine("Error al actualizar el tiquete. ");
        }
    }

    
    /// Deletes an existing ticket after confirmation.
    
    private void DeleteTicket()
    {
        Console.WriteLine("\n--- Eliminar Compra ---");
        
        Console.Write("ID del tiquete a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine(" ID inválido.");
            return;
        }

        var tiket = tiketService.GetTicketById(id);
        if (tiket == null)
        {
            Console.WriteLine(" Tiquete no encontrado.");
            return;
        }

        Console.WriteLine($"Tiquete encontrado - Precio: ${tiket.price}, Fecha: {tiket.purchaseDate:dd/MM/yyyy}");
        Console.Write("¿Estás seguro de eliminarlo? (s/n): ");
        string confirmation = Console.ReadLine()?.ToLower() ?? "";
        
        if (confirmation == "s" || confirmation == "si")
        {
            tiketService.RemoveTicket(tiket);
            Console.WriteLine("Tiquete eliminado exitosamente.");
        }
        else
        {
            Console.WriteLine("Eliminación cancelada.");
        }
    }
    
    // MÉTODOS PARA LAS CONSULTAS LINQ
    
    /// Shows the concert ID with the highest number of tickets sold.
    private void ConcertWithMostTickets()
    {
        Console.WriteLine("\n--- Concierto con más tiquetes vendidos ---");
        
        int concertId = tiketService.GetConcertWithMostTickets();
        
        if (concertId == 0)
        {
            Console.WriteLine("No hay tiquetes vendidos aún.");
            return;
        }
        
        Console.WriteLine($"El concierto con más tiquetes vendidos tiene ID: {concertId}");
    }
    
    
    /// Shows all purchases made by a given client ID and the total spent.
    private void TicketsByClient()
    {
        Console.WriteLine("\n--- Compras por cliente ---");
        
        Console.Write("ID del cliente: ");
        if (!int.TryParse(Console.ReadLine(), out int clientId))
        {
            Console.WriteLine(" ID de cliente inválido.");
            return;
        }
        
        var clientTickets = tiketService.GetTicketsByClient(clientId);
        
        if (clientTickets.Count == 0)
        {
            Console.WriteLine($" No se encontraron compras para el cliente ID: {clientId}");
            return;
        }
        
        Console.WriteLine($"Compras del cliente ID {clientId}:");
        foreach (var ticket in clientTickets)
        {
            Console.WriteLine($"  - Tiquete ID: {ticket.idTiket}, Concierto: {ticket.idConcert}, Precio: ${ticket.price}, Fecha: {ticket.purchaseDate:dd/MM/yyyy}");
        }
        
        decimal totalSpent = clientTickets.Sum(t => t.price);
        Console.WriteLine($"Total gastado: ${totalSpent}");
    }
    
    /// Shows the total income for a given concert ID.
    private void TotalIncomeByConcert()
    {
        Console.WriteLine("\n--- Ingresos totales de concierto ---");
        
        Console.Write("ID del concierto: ");
        if (!int.TryParse(Console.ReadLine(), out int concertId))
        {
            Console.WriteLine(" ID de concierto inválido.");
            return;
        }
        
        decimal totalIncome = tiketService.GetTotalIncomeByConcert(concertId);
        
        if (totalIncome == 0)
        {
            Console.WriteLine($"Nohay tiquetes vendidos para el concierto ID: {concertId}");
            return;
        }
        
        Console.WriteLine($"Ingresos totales del concierto ID {concertId}: ${totalIncome}");
    }
}