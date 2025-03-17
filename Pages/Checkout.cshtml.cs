using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Cybergames.Models;

public class CheckoutModel : PageModel
{
    private readonly CartService _cartService;
    private readonly GameService _gameService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ShoppingCart Cart { get; set; } // Property for the cart

    [BindProperty]
    public bool ShowConfirmationMessage { get; set; } = false;

    public CheckoutModel(
        CartService cartService,
        GameService gameService,
        UserManager<ApplicationUser> userManager)
    {
        _cartService = cartService;
        _gameService = gameService;
        _userManager = userManager;
    }

    public void OnGet()
    {
        Cart = _cartService.GetCart(); // Get the cart when the page loads
        ViewData["CartItemCount"] = Cart.Items.Sum(item => item.Quantity); // Send the number of games to the view
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Get the current user
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login"); // Redirect to login if the user is not authenticated
        }

        // Get the cart
        var cart = _cartService.GetCart();

        // Add each game in the cart to the user's account
        foreach (var item in cart.Items)
        {
            await _gameService.AddGameToUserAsync(item.Id, User); // Use item.Id (or item.GameId if you renamed it)
        }

        // Clear the cart
        cart.Clear(); // Empty the cart
        _cartService.SaveCart(cart); // Save the updated (empty) cart

        // Show the confirmation message
        ShowConfirmationMessage = true;

        // Update the cart to show 0 games after it has been cleared
        Cart = _cartService.GetCart();
        ViewData["CartItemCount"] = Cart.Items.Sum(item => item.Quantity);

        // Return the same page to show the message
        return Page();
    }
}