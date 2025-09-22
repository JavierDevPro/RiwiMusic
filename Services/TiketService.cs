using RiwiMusic.Models;

namespace RiwiMusic.Services;

public class TiketService
{
    private static List<Tiket> tickets = new List<Tiket>{
        // Cliente 1 (Juan) -Compras múltiples maximo
        new Tiket { idTiket = 1, idClient = 1, idConcert = 1, price = 50000, purchaseDate = new DateTime(2024, 10, 01) },
        new Tiket { idTiket = 2, idClient = 1, idConcert = 2, price = 45000, purchaseDate = new DateTime(2024, 10, 05) },
        new Tiket { idTiket = 3, idClient = 1, idConcert = 3, price = 40000, purchaseDate = new DateTime(2024, 10, 10) },
        
        // Cliente 2 (Ana) - Fan del Rock
        new Tiket { idTiket = 4, idClient = 2, idConcert = 1, price = 50000, purchaseDate = new DateTime(2024, 10, 02) },
        new Tiket { idTiket = 5, idClient = 2, idConcert = 1, price = 50000, purchaseDate = new DateTime(2024, 10, 03) },
        
        // Cliente 3 (Carlos)
        new Tiket { idTiket = 6, idClient = 3, idConcert = 4, price = 60000, purchaseDate = new DateTime(2024, 10, 15) },
        
        // Cliente 4 (María) - Fan del Pop
        new Tiket { idTiket = 7, idClient = 4, idConcert = 3, price = 40000, purchaseDate = new DateTime(2024, 10, 12) },
        new Tiket { idTiket = 8, idClient = 4, idConcert = 3, price = 40000, purchaseDate = new DateTime(2024, 10, 13) },
        new Tiket { idTiket = 9, idClient = 4, idConcert = 3, price = 40000, purchaseDate = new DateTime(2024, 10, 14) },
        
        // Cliente 5 (Luis)
        new Tiket { idTiket = 10, idClient = 5, idConcert = 2, price = 45000, purchaseDate = new DateTime(2024, 10, 08) },
        new Tiket { idTiket = 11, idClient = 5, idConcert = 5, price = 55000, purchaseDate = new DateTime(2024, 10, 20) }
    };

    // CRUD básico
    public void AddTicket(Tiket ticket)
    {
        tickets.Add(ticket);
    }

    public void ListTikets()
    {
        foreach (var ticket in tickets)
        {
            Console.WriteLine($"ID: {ticket.idTiket}, Cliente: {ticket.idClient}, Concierto: {ticket.idConcert}, Precio: ${ticket.price}, Fecha: {ticket.purchaseDate:dd/MM/yyyy}");
        }
    }

    public void RemoveTicket(Tiket ticket)
    {
        tickets.Remove(ticket);
    }

    public Tiket? GetTicketById(int id)
    {
        return tickets.FirstOrDefault(t => t.idTiket == id);
    }

    public bool UpdateTicket(int id, Tiket updatedTicket)
    {
        var existingTicket = GetTicketById(id);
        if (existingTicket == null)
        {
            return false;
        }

        existingTicket.idClient = updatedTicket.idClient;
        existingTicket.idConcert = updatedTicket.idConcert;
        existingTicket.price = updatedTicket.price;
        existingTicket.purchaseDate = updatedTicket.purchaseDate;
        
        return true;
    }

    // CONSULTAS LINQ ESPECÍFICAS
    
    // 1. Consultar el concierto con más tiquetes vendidos
    public int GetConcertWithMostTickets()
    {
        return tickets.GroupBy(t => t.idConcert)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault()?.Key ?? 0;
    }

    // 2. Mostrar todas las compras realizadas por un usuario determinado
    public List<Tiket> GetTicketsByClient(int clientId)
    {
        return tickets.Where(t => t.idClient == clientId).ToList();
    }

    // 3. Consultar ingresos totales de un concierto
    public decimal GetTotalIncomeByConcert(int concertId)
    {
        return tickets.Where(t => t.idConcert == concertId).Sum(t => t.price);
    }
}