using Cybergames.Data;
using Cybergames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Cybergames.Pages
{
    [Authorize]

public class PurchasedGamesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PurchasedGamesModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Game> PurchasedGames { get; set; } = new List<Game>();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // Get the games purchased by the user
                PurchasedGames = await _context.UserGames
                    .Where(ug => ug.UserId == user.Id)
                    .Select(ug => ug.Game)
                    .ToListAsync();
            }
        }

        // Handler for removing a game
        public async Task<IActionResult> OnPostRemoveGameAsync(int gameId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login"); // Redirect to login if the user is not authenticated
            }

            // Find the game in the user's account
            var userGame = await _context.UserGames
                .FirstOrDefaultAsync(ug => ug.UserId == user.Id && ug.GameId == gameId);

            if (userGame != null)
            {
                // Remove the game from the user's account
                _context.UserGames.Remove(userGame);
                await _context.SaveChangesAsync();
            }

            // Redirect back to the Purchased Games page
            return RedirectToPage();
        }
    }
}