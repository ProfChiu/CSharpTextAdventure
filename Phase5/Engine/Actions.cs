using Phase5.Characters;
using Phase5.Items;
using Phase5.UI;
using Phase5.World;

namespace Phase5.Engine;

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
            KeyItem? key = exit.RequiredKey != null ? FindMatchingKey(player, exit.RequiredKey) : null;

            if (key != null)
            {
                // Auto-unlock if the player carries the right key.
                exit.IsLocked = false;
                Display.Message($"The {key.Name} fits the lock. The way opens.");
            }
            else if (exit.RequiredKey == "guardian")
            {
                // No key opens this one — only the goddess's riddles do.
                Display.Message("The goddess bars the way. \"Answer my riddles, traveler, and pass.\"");
                return false;
            }
            else
            {
                Display.Message("That way is locked. You'll need a key.");
                return false;
            }
        }

        player.MoveTo(exit.Destination);
        Display.Room(player.CurrentRoom);

        // Entering the Hidden Shrine triggers the guardian encounter. The gate north
        // to the Caravan Heart stays locked ("guardian") until the riddles are solved.
        if (player.CurrentRoom.Name == "Hidden Shrine"
            && player.CurrentRoom.Exits.TryGetValue("north", out var onward)
            && onward.IsLocked && onward.RequiredKey == "guardian")
        {
            if (RiddleEncounter.Run())
                onward.IsLocked = false;
        }

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
        return FindMatchingKey(player, exitId) != null;
    }

    // Return the carried KeyItem that opens this exit id, or null if we have none.
    private static KeyItem? FindMatchingKey(Player player, string exitId)
    {
        return player.Inventory.GetAll().OfType<KeyItem>().FirstOrDefault(k => k.UnlocksExitId == exitId);
    }
}
