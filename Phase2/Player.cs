namespace Phase2;

class Player
{
    public Room       CurrentRoom { get; set; }
    public List<Item> Inventory   { get; set; }

    public Player(Room startingRoom)
    {
        CurrentRoom = startingRoom;
        Inventory   = new List<Item>();
    }
}
