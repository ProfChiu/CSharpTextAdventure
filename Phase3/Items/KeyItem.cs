namespace Phase3.Items;

// A key item unlocks a specific exit when the player carries it.
class KeyItem : Item
{
    public string UnlocksExitId { get; set; }

    public KeyItem(string name, string description, string unlocksExitId)
        : base(name, description, canPickUp: true)
    {
        UnlocksExitId = unlocksExitId;
    }
}
