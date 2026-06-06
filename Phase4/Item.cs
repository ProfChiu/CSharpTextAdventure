namespace Phase4;

// Carried over from Chapter 3: Name + Description (read by `examine`) + CanPickUp.
// In this chapter it also becomes a BASE class — see KeyItem, which extends it.
class Item
{
    public string Name        { get; set; }
    public string Description { get; set; }
    public bool   CanPickUp   { get; set; }

    public Item(string name, string description, bool canPickUp)
    {
        Name        = name;
        Description = description;
        CanPickUp   = canPickUp;
    }
}
