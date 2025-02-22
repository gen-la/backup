using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cybergames.Models;

namespace Cybergames.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CartService _cartService; // Inject CartService

        // List of games
        public List<Game> Games { get; set; } = new()
        {
            new Game { 
                ID = 1, 
                Title = "Cyberpunk 2077", 
                Price = 59.99m, 
                Description = "A futuristic RPG game.",
                Category = "RPG",
                Cover = "https://cdn.thegamesdb.net/images/original/boxart/front/14517-1.jpg",
                Img1 = "https://example.com/img1.jpg",
                Img2 = "https://example.com/img2.jpg"
            },
            new Game { 
    ID = 2, 
    Title = "The Witcher 3: Wild Hunt", 
    Price = 39.99m, 
    Description = "An open-world fantasy RPG.",
    Category = "Fantasy",
    Cover = "https://cdn.thegamesdb.net/images/original/boxart/front/17251-1.jpg",
    Img1 = "https://example.com/img3.jpg",
    Img2 = "https://example.com/img4.jpg"
},    
new Game { 
    ID = 3, 
    Title = "Skyrim V", 
    Price = 49.99m, 
    Description = "RPG set in the open world of Tamriel.",
    Category = "Fantasy",
    Cover = "https://cdn.thegamesdb.net/images/original/boxart/front/2511-1.jpg",
    Img1 = "https://example.com/img5.jpg",
    Img2 = "https://example.com/img6.jpg"
},          
new Game { 
    ID = 4, 
    Title = "Red Dead Redemption 2", 
    Price = 59.99m, 
    Description = "An open-world action-adventure game set in the Wild West.",
    Category = "Action",
    Cover = "https://cdn.thegamesdb.net/images/original/boxart/front/66595-1.jpg",
    Img1 = "https://example.com/img7.jpg",
    Img2 = "https://example.com/img8.jpg"
},     
new Game { 
    ID = 5, 
    Title = "Horizon Zero Dawn", 
    Price = 49.99m, 
    Description = "A post-apocalyptic open-world RPG with robot creatures.",
    Category = "Action",
    Cover = "https://cdn.thegamesdb.net/images/original/boxart/front/130245-1.jpg",
    Img1 = "https://example.com/img9.jpg",
    Img2 = "https://example.com/img10.jpg"
},     
new Game { 
    ID = 6, 
    Title = "Ghost of Tsushima", 
    Price = 59.99m, 
    Description = "A samurai action game set in feudal Japan.",
    Category = "Action",
    Cover = "https://cdn.thegamesdb.net/images/original/boxart/front/74666-1.jpg",
    Img1 = "https://example.com/img11.jpg",
    Img2 = "https://example.com/img12.jpg"
},     
new Game { 
    ID = 7, 
    Title = "Assassin's Creed Valhalla", 
    Price = 59.99m, 
    Description = "An open-world RPG set in the Viking Age.",
    Category = "RPG",
    Cover = "https://cdn.thegamesdb.net/images/original/boxart/front/79982-1.jpg",
    Img1 = "https://example.com/img13.jpg",
    Img2 = "https://example.com/img14.jpg"
},     
new Game { 
    ID = 8, 
    Title = "The Legend of Zelda: Breath of the Wild", 
    Price = 59.99m, 
    Description = "An action-adventure game set in an open world.",
    Category = "Adventure",
    Cover = "https://cdn.thegamesdb.net/images/original/boxart/front/123898-1.jpg",
    Img1 = "https://example.com/img15.jpg",
    Img2 = "https://example.com/img16.jpg"
}

        };

        public IndexModel(ILogger<IndexModel> logger, CartService cartService)
        {
            _logger = logger;
            _cartService = cartService; // Inject CartService
        }

        public void OnGet()
        {
            var cart = _cartService.GetCart();
            ViewData["CartItemCount"] = cart.Items.Sum(item => item.Quantity);        }

        // Handle adding a game to the cart
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

            return RedirectToPage(); // Refresh the page
        }
    }
}