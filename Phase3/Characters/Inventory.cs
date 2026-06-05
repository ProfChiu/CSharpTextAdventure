using Phase3.Items;

namespace Phase3.Characters;

class Inventory
{
    private List<Item> _items = new List<Item>();

    public void Add(Item item)
    {
        _items.Add(item);
    }

    public bool Remove(Item item)
    {
        return _items.Remove(item);
    }

    public Item? Find(string name)
    {
        return _items.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public bool Contains(string name)
    {
        return Find(name) != null;
    }

    public void Display()
    {
        if (_items.Count == 0)
        {
            Console.WriteLine("You are carrying nothing.");
            return;
        }
        Console.WriteLine("You are carrying:");
        foreach (var item in _items)
            Console.WriteLine($"  {item.Name}");
    }

    public List<Item> GetAll() => _items;
}
