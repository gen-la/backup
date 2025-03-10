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
            existingItem.Quantity += item.Quantity; // Update quantity if item already exists
        }
        else
        {
            Items.Add(item); // Add new item to the cart
        }
    }

    public void RemoveItem(int itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            Items.Remove(item); // Remove item from the cart
        }
    }

    public void Clear()
    {
        Items.Clear(); // Remove all items from the cart
    }

    public decimal CalculateTotal()
    {
        return Items.Sum(i => i.Price * i.Quantity); // Calculate total price
    }
}