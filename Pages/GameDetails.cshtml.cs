using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cybergames.Models;
using Cybergames.Data;
using Microsoft.EntityFrameworkCore;

namespace Cybergames.Pages
{
    public class GameDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService; // Inject CartService

        public Game Game { get; set; }

        public GameDetailsModel(ApplicationDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService; // Inject CartService
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game = await _context.Games.FirstOrDefaultAsync(g => g.ID == id);

            if (Game == null)
            {
                return NotFound();
            }

            // 🛒 Uppdatera CartItemCount för badgen
            var cart = _cartService.GetCart();
            ViewData["CartItemCount"] = cart.Items.Sum(item => item.Quantity);

            return Page();
        }

        // 🛒 Lägg till spelet i kundvagnen och uppdatera CartItemCount
        public IActionResult OnPostAddToCart(int id, string title, decimal price, int quantity)
        {
            var cart = _cartService.GetCart();
            cart.AddItem(new CartItem
            {
                Id = id,
                Name = title,
                Price = price,
                Quantity = quantity
            });
            _cartService.SaveCart(cart);

            // 🛒 Uppdatera CartItemCount efter att ett spel lagts till
            ViewData["CartItemCount"] = cart.Items.Sum(item => item.Quantity);

            return RedirectToPage(new { id = id }); // Håll kvar på sidan
        }
    }
}