namespace Phase3;

// NEW in Chapter 3: instead of the Player holding a raw List<Item>, the list is
// hidden inside this class. Callers can only touch it through these methods —
// that is "encapsulation". Compare with Phase 2, where Game reached straight into
// the player's list.
class Inventory
{
    private List<Item> _items = new List<Item>();

    public int Count => _items.Count;

    public void Add(Item item)
    {
        _items.Add(item);
    }

    public void Remove(Item item)
    {
        _items.Remove(item);
    }

    // Look up an item by name, ignoring case. Returns null if we aren't carrying it.
    public Item? Find(string name)
    {
        foreach (Item item in _items)
        {
            if (item.Name.ToLower() == name.ToLower())
                return item;
        }
        return null;
    }

    public void Display()
    {
        if (_items.Count == 0)
        {
            Console.WriteLine("You are carrying nothing.");
            return;
        }

        Console.WriteLine("You are carrying:");
        foreach (Item item in _items)
            Console.WriteLine($"  {item.Name}");
    }
}
