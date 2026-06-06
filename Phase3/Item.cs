namespace Phase3;

// Chapter 3 grows the Item: besides a Name it now carries a Description (shown by
// the new `examine` command) and a CanPickUp flag (fixtures like the Public Well
// and the Notice Board set this to false so `take` refuses them).
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
