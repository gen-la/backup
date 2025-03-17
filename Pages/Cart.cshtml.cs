using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize] // Kräv inloggning för att komma åt denna sida
public class CartModel : PageModel
{
    private readonly CartService _cartService;

    public ShoppingCart Cart { get; set; }

    public CartModel(CartService cartService)
    {
        _cartService = cartService;
    }

    public void OnGet()
    {
        Cart = _cartService.GetCart();
        ViewData["CartItemCount"] = Cart.Items.Sum(item => item.Quantity);
    }

    [Authorize] // Kräv inloggning för att lägga till spel
    public IActionResult OnPostAddToCart(int id, string name, decimal price, int quantity)
    {
        var cart = _cartService.GetCart();
        cart.AddItem(new CartItem { Id = id, Name = name, Price = price, Quantity = quantity });
        _cartService.SaveCart(cart);
        return RedirectToPage();
    }

    [Authorize] // Kräv inloggning för att ta bort spel
    public IActionResult OnPostRemoveFromCart(int itemId)
    {
        var cart = _cartService.GetCart();
        cart.RemoveItem(itemId);
        _cartService.SaveCart(cart);
        return RedirectToPage();
    }
}