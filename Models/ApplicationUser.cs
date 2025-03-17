using Microsoft.AspNetCore.Identity;

namespace Cybergames.Models;

public class ApplicationUser :IdentityUser
{
    public string Firstname { get; set; } = "";
    public string Lastname { get; set; } = "";
    public string Adress { get; set; } = "";
    public DateTime CreatedAt { get; set; } 
    
    public ICollection<UserGame> UserGames { get; set; }

}

