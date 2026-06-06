namespace Phase3;

// The Player now holds an Inventory OBJECT rather than a bare List<Item>.
// All the list bookkeeping lives behind Inventory's methods.
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
