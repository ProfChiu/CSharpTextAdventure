namespace Phase1;

class Room
{
    public string Name        { get; set; }
    public string Description { get; set; }
    public string Art         { get; set; }

    // Each direction holds a reference to another Room.
    // null means there is no exit in that direction.
    public Room? North { get; set; }
    public Room? South { get; set; }
    public Room? East  { get; set; }
    public Room? West  { get; set; }

    public Room(string name, string description, string art = "")
    {
        Name        = name;
        Description = description;
        Art         = art;
    }
}
