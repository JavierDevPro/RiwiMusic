using RiwiMusic.Models;
using RiwiMusic.Services;

namespace RiwiMusic.Controllers;

public class ConcertController
{
     private ConcertService concertService = new ConcertService();

    public void Menu()
    {
        int option;
        do
        {
            Console.WriteLine("\n=== Gestión de Conciertos ===");
            Console.WriteLine("1. Registrar concierto");
            Console.WriteLine("2. Listar conciertos");
            Console.WriteLine("3. Editar concierto");
            Console.WriteLine("4. Eliminar concierto");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("Elige una opción: ");
            
            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1: RegisterConcert(); break;
                    case 2: ListConcerts(); break;
                    case 3: EditConcert(); break;
                    case 4: DeleteConcert(); break;
                    case 0: Console.WriteLine("Volviendo al menú principal..."); break;
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

    private void RegisterConcert()
    {
        Console.WriteLine("\n--- Registrar Concierto ---");
        
        Console.Write("ID del concierto: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine(" ID inválido.");
            return;
        }

        if (concertService.GetConcertById(id) != null)
        {
            Console.WriteLine(" Ya existe un concierto con ese ID.");
            return;
        }

        Console.Write("Nombre del concierto: ");
        string name = Console.ReadLine() ?? "";
        
        Console.Write("Ciudad: ");
        string city = Console.ReadLine() ?? "";
        
        Console.Write("Fecha (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            Console.WriteLine(" Fecha inválida. Usa el formato dd/MM/yyyy");
            return;
        }
        
        Console.Write("Precio del tiquete: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine(" Precio inválido.");
            return;
        }
        
        Console.Write("Capacidad del venue: ");
        if (!int.TryParse(Console.ReadLine(), out int capacity))
        {
            Console.WriteLine(" Capacidad inválida.");
            return;
        }

        var concert = new Concert
        {
            idConcert = id,
            name = name,
            city = city,
            date = date,
        };

        concertService.AddConcert(concert);
        Console.WriteLine("Concierto registrado exitosamente.");
    }

    private void ListConcerts()
    {
        Console.WriteLine("\n--- Lista de Conciertos ---");
        concertService.ListConcerts();
    }

    private void EditConcert()
    {
        Console.WriteLine("\n--- Editar Concierto ---");
        
        Console.Write("ID del concierto a editar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine(" ID inválido.");
            return;
        }

        var existingConcert = concertService.GetConcertById(id);
        if (existingConcert == null)
        {
            Console.WriteLine(" Concierto no encontrado.");
            return;
        }

        Console.WriteLine($"Concierto encontrado: {existingConcert.name}");
        Console.WriteLine("Deja en blanco para mantener el valor actual:");

        Console.Write($"Nuevo nombre ({existingConcert.name}): ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) name = existingConcert.name;

        Console.Write($"Nueva ciudad ({existingConcert.city}): ");
        string city = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(city)) city = existingConcert.city;

        Console.Write($"Nueva fecha ({existingConcert.date:dd/MM/yyyy}) [dd/MM/yyyy]: ");
        DateTime date = existingConcert.date;
        string dateInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(dateInput))
        {
            if (!DateTime.TryParseExact(dateInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out date))
            {
                Console.WriteLine(" Fecha inválida, manteniendo la actual.");
                date = existingConcert.date;
            }
        }

        var updatedConcert = new Concert
        {
            idConcert = id,
            name = name,
            city = city,
            date = date,
        };

        if (concertService.UpdateConcert(id, updatedConcert))
        {
            Console.WriteLine("Concierto actualizado exitosamente.");
        }
        else
        {
            Console.WriteLine(" Error al actualizar el concierto.");
        }
    }

    private void DeleteConcert()
    {
        Console.WriteLine("\n--- Eliminar Concierto ---");
        
        Console.Write("ID del concierto a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine(" ID inválido.");
            return;
        }

        var concert = concertService.GetConcertById(id);
        if (concert == null)
        {
            Console.WriteLine(" Concierto no encontrado.");
            return;
        }

        Console.WriteLine($"Concierto encontrado: {concert.name} en {concert.city}");
        Console.Write("¿Estás seguro de eliminarlo? (s/n): ");
        string confirmation = Console.ReadLine()?.ToLower() ?? "";
        
        if (confirmation == "s" || confirmation == "si")
        {
            concertService.RemoveConcert(concert);
            Console.WriteLine("Concierto eliminado exitosamente.");
        }
        else
        {
            Console.WriteLine("Eliminación cancelada.");
        }
    } 
}
