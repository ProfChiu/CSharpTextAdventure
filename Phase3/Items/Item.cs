namespace Phase3.Items;

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
