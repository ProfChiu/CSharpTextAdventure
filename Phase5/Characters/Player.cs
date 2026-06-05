using Phase5.World;

namespace Phase5.Characters;

class Player
{
    public Room      CurrentRoom  { get; set; }
    public Inventory Inventory    { get; set; }
    public List<string> VisitedRooms { get; set; }

    public Player(Room startingRoom)
    {
        CurrentRoom  = startingRoom;
        Inventory    = new Inventory();
        VisitedRooms = new List<string>();
    }

    public void MoveTo(Room room)
    {
        CurrentRoom = room;
        if (!HasVisited(room))
            VisitedRooms.Add(room.Name);
        room.HasBeenVisited = true;
    }

    public bool HasVisited(Room room)
    {
        return VisitedRooms.Contains(room.Name);
    }
}
