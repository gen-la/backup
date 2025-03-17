using Cybergames.Data;
using Cybergames.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class GameService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public GameService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task AddGameToUserAsync(int gameId, ClaimsPrincipal userPrincipal)
    {
        var user = await _userManager.GetUserAsync(userPrincipal);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var game = await _context.Games.FindAsync(gameId);
        if (game == null)
        {
            throw new Exception("Game not found");
        }

        // Check if the user already owns the game
        var userGame = await _context.UserGames
            .FirstOrDefaultAsync(ug => ug.UserId == user.Id && ug.GameId == gameId);

        if (userGame == null)
        {
            // Add the game to the user's account
            userGame = new UserGame
            {
                UserId = user.Id,
                GameId = gameId,
                PurchaseDate = DateTime.UtcNow // Set the purchase date
            };

            _context.UserGames.Add(userGame);
            await _context.SaveChangesAsync();
        }
    }
}