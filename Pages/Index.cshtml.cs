using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cybergames.Models;
using Cybergames.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Cybergames.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public List<Game> Games { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        private const int PageSize = 12; // Number of games per page

        public IndexModel(
            ILogger<IndexModel> logger, 
            CartService cartService, 
            ApplicationDbContext context,
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _cartService = cartService;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            CurrentPage = pageIndex;
            int totalGames = await _context.Games.CountAsync();
            TotalPages = (int)Math.Ceiling(totalGames / (double)PageSize);

            Games = await _context.Games
                .Skip((pageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            if (_signInManager.IsSignedIn(User))
            {
                var cart = _cartService.GetCart();
                ViewData["CartItemCount"] = cart.Items.Sum(item => item.Quantity);
            }
            else
            {
                ViewData["CartItemCount"] = 0;
            }
        }

        [Authorize]
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

            return RedirectToPage(new { pageIndex = CurrentPage }); // Stay on the same page
        }
    }
}