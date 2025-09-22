namespace RiwiMusic.Models;

public class Tiket
{
    public int idTiket;
    public int idClient;        // Relación con Client
    public int idConcert;       // Relación con Concert
    public decimal price;   // Precio unitario
    public DateTime purchaseDate;
}