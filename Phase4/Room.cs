namespace Phase4;

// Same N/S/E/W shape as before, plus a tiny bit of lock data so a door can be
// locked WITHOUT hard-coding room names in Game (compare Phase 2). A locked room
// records which direction is barred and an exit id; a KeyItem whose UnlocksExitId
// matches that id will open it.
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

    // null = nothing on this room is locked. Otherwise LockedDirection is one of
    // "north"/"south"/"east"/"west" and LockedExitId names the matching key.
    public string? LockedDirection { get; set; }
    public string? LockedExitId    { get; set; }

    public Room(string name, string description, string art = "")
    {
        Name        = name;
        Description = description;
        Art         = art;
        Items       = new List<Item>();
    }

    // Mark one exit as locked behind a given exit id.
    public void LockExit(string direction, string exitId)
    {
        LockedDirection = direction;
        LockedExitId    = exitId;
    }
}
