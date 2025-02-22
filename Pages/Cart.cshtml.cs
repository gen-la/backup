// Pages/Cart.cshtml.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        ViewData["CartItemCount"] = Cart.Items.Sum(item => item.Quantity); // Pass item cou
    }

    public IActionResult OnPostAddToCart(int id, string name, decimal price, int quantity)
    {
        var cart = _cartService.GetCart();
        cart.AddItem(new CartItem { Id = id, Name = name, Price = price, Quantity = quantity });
        _cartService.SaveCart(cart);
        return RedirectToPage();
    }

    public IActionResult OnPostRemoveFromCart(int itemId)
    {
        var cart = _cartService.GetCart();
        cart.RemoveItem(itemId);
        _cartService.SaveCart(cart);
        return RedirectToPage();
    }
}