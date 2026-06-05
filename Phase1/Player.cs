namespace Phase1;

class Player
{
    public Room CurrentRoom { get; set; }

    public Player(Room startingRoom)
    {
        CurrentRoom = startingRoom;
    }
}
