namespace Phase4;

// Carried over from Chapter 3. The list stays private; callers go through these
// methods. GetAll() is added so Game can scan for a KeyItem by TYPE (see how Game
// uses `is KeyItem` to spot keys) without exposing the list for editing.
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

    public Item? Find(string name)
    {
        foreach (Item item in _items)
        {
            if (item.Name.ToLower() == name.ToLower())
                return item;
        }
        return null;
    }

    public List<Item> GetAll()
    {
        return _items;
    }

    // Carried over verbatim from Chapter 3. (It keeps its own Console calls rather
    // than routing through the static Display class, because a method named Display
    // here would otherwise collide with that class's name.)
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
