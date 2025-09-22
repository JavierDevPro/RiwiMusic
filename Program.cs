using RiwiMusic.Controllers;
namespace RiwiMusic;

public class Program
{
    public static void Main(string[] args)
    {
         var clientController = new ClientController();
        var concertController = new ConcertController();
        var tiketController = new TiketController();

        int option;
        do
        {
            Console.Clear();
            Console.WriteLine("         RIWI MUSIC     ");
            Console.WriteLine("     Sistema de Gestión          ");
            Console.WriteLine();
            Console.WriteLine("1. Gestión de Clientes");
            Console.WriteLine("2. Gestión de Conciertos");
            Console.WriteLine("3. Gestión de Tiquetes");
            Console.WriteLine("0. Salir");
            Console.WriteLine();
            Console.Write("Elige una opción: ");
            
            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1: 
                        clientController.Menu(); 
                        break;
                    case 2: 
                        concertController.Menu(); 
                        break;
                    case 3: 
                        tiketController.Menu(); 
                        break;
                    case 0: 
                        Console.WriteLine();
                        Console.WriteLine("¡Gracias por usar RiwiMusic!");
                        Console.WriteLine("¡Hasta pronto!");
                        break;
                    default: 
                        Console.WriteLine(" Opción inválida. Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine(" Por favor ingresa un número válido. Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                option = -1; // Para que no salga del bucle
            }
        } while (option != 0);
    }
}