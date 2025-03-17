// Models/CartItem.cs
public class CartItem
{
    public int Id { get; set; } // Game ID
    public string Name { get; set; } // Game Title
    public decimal Price { get; set; } // Game Price
    public int Quantity { get; set; } // Quantity
}

// Models/ShoppingCart.cs
// Models/ShoppingCart.cs
public class ShoppingCart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public void AddItem(CartItem item)
    {
        var existingItem = Items.FirstOrDefault(i => i.Id == item.Id);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            Items.Add(item);
        }
    }

    public void RemoveItem(int itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            Items.Remove(item);
        }
    }

    public void Clear()
    {
        Items.Clear(); // Töm varukorgen
    }

    public decimal CalculateTotal()
    {
        return Items.Sum(item => item.Price * item.Quantity);
    }
}