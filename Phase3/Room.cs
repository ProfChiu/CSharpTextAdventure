namespace Phase3;

// Same shape as the Phase 2 Room: properties for the four compass exits plus a
// list of items lying on the ground here.
class Room
{
    public string Name        { get; set; }
    public string Description { get; set; }
    public string Art         { get; set; }

    public Room? North { get; set; }
    public Room? South { get; set; }
    public Room? East  { get; set; }
    public Room? West  { get; set; }

    public List<Item> Items { get; set; }

    public Room(string name, string description, string art = "")
    {
        Name        = name;
        Description = description;
        Art         = art;
        Items       = new List<Item>();
    }
}
