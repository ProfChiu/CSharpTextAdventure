namespace Phase4;

// NEW in Chapter 4: inheritance. A KeyItem IS an Item (it has a Name, Description,
// and CanPickUp) and adds ONE thing of its own: which locked exit it opens.
//
// `: Item` means "KeyItem extends Item".
// `: base(...)` hands the name/description straight up to the Item constructor,
// so we don't repeat that work here. Keys are always carryable, so we pass
// canPickUp: true for the player.
class KeyItem : Item
{
    public string UnlocksExitId { get; set; }

    public KeyItem(string name, string description, string unlocksExitId)
        : base(name, description, canPickUp: true)
    {
        UnlocksExitId = unlocksExitId;
    }
}
