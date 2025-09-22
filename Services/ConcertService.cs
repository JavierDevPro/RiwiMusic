using RiwiMusic.Models;
namespace RiwiMusic.Services;

public class ConcertService
{
    private static List<Concert> concerts = new List<Concert>{
        new Concert { idConcert = 1, name = "Rock", city = "Medellin", date = new DateTime(2024, 12, 15) },
        new Concert { idConcert = 2, name = "Jazz", city = "Bogota", date = new DateTime(2024, 11, 20) },
        new Concert { idConcert = 3, name = "Pop-Festival", city = "Cali", date = new DateTime(2024, 10, 25) },
        new Concert { idConcert = 4, name = "Reggaeton", city = "Medellin", date = new DateTime(2024, 12, 31) },
        new Concert { idConcert = 5, name = "Salsa", city = "Cartagena", date = new DateTime(2025, 01, 10) }
    };

    // Registrar concierto
    public void AddConcert(Concert concert)
    {
        concerts.Add(concert);
    }

    // Listar conciertos
    public void ListConcerts()
    {
        foreach (var concert in concerts)
        {
            Console.WriteLine($"ID: {concert.idConcert}, Nombre: {concert.name}, Ciudad: {concert.city}, Fecha: {concert.date:dd/MM/yyyy}");
        }
    }

    // Eliminar concierto
    public void RemoveConcert(Concert concert)
    {
        concerts.Remove(concert);
    }

    // Buscar concierto por ID
    public Concert? GetConcertById(int id)
    {
        return concerts.FirstOrDefault(c => c.idConcert == id);
    }

    // Actualizar concierto (MÉTODO MEJORADO)
    public bool UpdateConcert(int id, Concert updatedConcert)
    {
        var existingConcert = GetConcertById(id);
        if (existingConcert == null)
        {
            return false;
        }

        // Actualizar propiedades individualmente
        existingConcert.name = updatedConcert.name;
        existingConcert.city = updatedConcert.city;
        existingConcert.date = updatedConcert.date;
        //Note: ticketsSold no se actualiza aquí, se maneja en TicketService
        
        return true;
    }

    // Obtener todos los conciertos (útil para LINQ)
    public List<Concert> GetAllConcerts()
    {
        return concerts;
    }
}