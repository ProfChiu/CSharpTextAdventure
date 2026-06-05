namespace Phase3.World;

class Exit
{
    public Room Destination { get; set; }
    public bool IsLocked { get; set; }

    // The id of the KeyItem whose UnlocksExitId matches this exit.
    public string? RequiredKey { get; set; }

    public Exit(Room destination, bool isLocked = false, string? requiredKey = null)
    {
        Destination = destination;
        IsLocked    = isLocked;
        RequiredKey = requiredKey;
    }
}
