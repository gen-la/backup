// Services/CartService.cs
using Cybergames.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

public class CartService
{
    private const string CartSessionKey = "ShoppingCart";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ShoppingCart GetCart()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var cart = session.GetObject<ShoppingCart>(CartSessionKey);
        if (cart == null)
        {
            cart = new ShoppingCart();
            session.SetObject(CartSessionKey, cart);
        }
        return cart;
    }

    public void SaveCart(ShoppingCart cart)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        session.SetObject(CartSessionKey, cart);
    }
}

// Extension methods for session serialization
public static class SessionExtensions
{
    public static void SetObject(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}