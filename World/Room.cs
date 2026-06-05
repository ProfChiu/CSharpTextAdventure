using CSharpTextAdventure.Items;

namespace CSharpTextAdventure.World;

class Room
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string, Exit> Exits { get; set; }
    public List<Item> Items { get; set; }
    public bool HasBeenVisited { get; set; }

    public Room(string name, string description)
    {
        Name = name;
        Description = description;
        Exits = new Dictionary<string, Exit>();
        Items = new List<Item>();
        HasBeenVisited = false;
    }

    public void AddExit(string direction, Exit exit)
    {
        Exits[direction.ToLower()] = exit;
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public string GetExitsList()
    {
        return string.Join(", ", Exits.Keys);
    }
}
