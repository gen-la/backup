using Cybergames.Models;

public class UserGame
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int GameId { get; set; }
    public Game Game { get; set; }

    public DateTime PurchaseDate { get; set; } // Add this property
}