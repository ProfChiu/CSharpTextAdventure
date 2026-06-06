namespace Phase4;

// Carried over from Chapter 3: the Player holds an Inventory object.
class Player
{
    public Room      CurrentRoom { get; set; }
    public Inventory Inventory   { get; set; }

    public Player(Room startingRoom)
    {
        CurrentRoom = startingRoom;
        Inventory   = new Inventory();
    }
}
