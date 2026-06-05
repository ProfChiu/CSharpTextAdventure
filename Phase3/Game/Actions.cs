using Phase3.Characters;
using Phase3.Items;
using Phase3.UI;
using Phase3.World;

namespace Phase3.Game;

static class Actions
{
    public static bool Go(string direction, Player player)
    {
        var room = player.CurrentRoom;

        if (!room.Exits.TryGetValue(direction, out var exit))
        {
            Display.Message("You can't go that way.");
            return false;
        }

        if (exit.IsLocked)
        {
            // Auto-unlock if the player carries the right key.
            if (exit.RequiredKey != null && HasMatchingKey(player, exit.RequiredKey))
            {
                exit.IsLocked = false;
                Display.Message("The Brass Key turns smoothly in the lock. The door swings open.");
            }
            else
            {
                Display.Message("That way is locked. You'll need a key.");
                return false;
            }
        }

        player.MoveTo(exit.Destination);
        Display.Room(player.CurrentRoom);
        return true;
    }

    public static void Look(Player player)
    {
        Display.Room(player.CurrentRoom);
    }

    public static void Examine(string noun, Player player)
    {
        var room = player.CurrentRoom;

        // Check room items first, then inventory.
        var item = FindItem(noun, room) ?? player.Inventory.Find(noun);

        if (item == null)
        {
            Display.Message($"You don't see any '{noun}' here.");
            return;
        }

        Display.Message(item.Description);

        // Examining the loose brick reveals the brass key.
        if (item.Name.Equals("Loose Brick", StringComparison.OrdinalIgnoreCase))
        {
            var key = room.Items.FirstOrDefault(i => i.Name == "Brass Key");
            if (key != null && !key.CanPickUp)
            {
                key.CanPickUp = true;
                Display.Message("The Brass Key is now visible — you can take it.");
            }
        }
    }

    public static void Take(string noun, Player player)
    {
        var room = player.CurrentRoom;
        var item = FindItem(noun, room);

        if (item == null)
        {
            Display.Message($"You don't see any '{noun}' here.");
            return;
        }

        if (!item.CanPickUp)
        {
            Display.Message($"You can't pick up the {item.Name}.");
            return;
        }

        room.Items.Remove(item);
        player.Inventory.Add(item);
        Display.Message($"You take the {item.Name}.");
    }

    public static void Drop(string noun, Player player)
    {
        var item = player.Inventory.Find(noun);

        if (item == null)
        {
            Display.Message($"You aren't carrying any '{noun}'.");
            return;
        }

        player.Inventory.Remove(item);
        player.CurrentRoom.AddItem(item);
        Display.Message($"You drop the {item.Name}.");
    }

    public static void ShowInventory(Player player)
    {
        player.Inventory.Display();
    }

    public static void Unlock(string direction, Player player)
    {
        var room = player.CurrentRoom;

        if (!room.Exits.TryGetValue(direction, out var exit))
        {
            Display.Message("There's no exit in that direction.");
            return;
        }

        if (!exit.IsLocked)
        {
            Display.Message("That exit is already unlocked.");
            return;
        }

        if (exit.RequiredKey != null && HasMatchingKey(player, exit.RequiredKey))
        {
            exit.IsLocked = false;
            Display.Message("The lock clicks open.");
        }
        else
        {
            Display.Message("You don't have the right key for this lock.");
        }
    }

    // --- helpers ---

    private static Item? FindItem(string noun, Room room)
    {
        return room.Items.FirstOrDefault(i => i.Name.Contains(noun, StringComparison.OrdinalIgnoreCase));
    }

    private static bool HasMatchingKey(Player player, string exitId)
    {
        return player.Inventory.GetAll().OfType<KeyItem>().Any(k => k.UnlocksExitId == exitId);
    }
}
