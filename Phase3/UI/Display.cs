using Phase3.Characters;
using Phase3.World;

namespace Phase3.UI;

static class Display
{
    public static void Welcome()
    {
        Console.WriteLine("=======================================================");
        Console.WriteLine("  THE CARAVANSERAI — A Silk Road Text Adventure");
        Console.WriteLine("  1271 AD. You have become separated from Marco Polo's");
        Console.WriteLine("  caravan. Find your documents and rejoin it by dawn.");
        Console.WriteLine("=======================================================");
        Console.WriteLine("  Type HELP for a list of commands.");
        Console.WriteLine();
    }

    public static void Room(Room room)
    {
        Console.WriteLine();
        if (room.Art != "") Console.WriteLine(room.Art);
        Console.WriteLine($"[ {room.Name.ToUpper()} ]");
        Console.WriteLine(room.Description);

        if (room.Items.Count > 0)
        {
            var visible = room.Items.Where(i => i.CanPickUp || i.Name != "Brass Key").ToList();
            if (visible.Count > 0)
            {
                Console.WriteLine("You see here: " + string.Join(", ", visible.Select(i => i.Name)) + ".");
            }
        }

        Console.WriteLine($"Exits: {room.GetExitsList()}.");
    }

    public static void Prompt()
    {
        Console.Write("> ");
    }

    public static void Message(string text)
    {
        Console.WriteLine(text);
    }

    public static void Help()
    {
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("  go <direction>   / n s e w   — move to an adjacent room");
        Console.WriteLine("  look             / l         — describe the current room");
        Console.WriteLine("  examine <item>   / x <item>  — inspect an item");
        Console.WriteLine("  take <item>      / get       — pick up an item");
        Console.WriteLine("  drop <item>                  — drop an item");
        Console.WriteLine("  inventory        / i / inv   — list carried items");
        Console.WriteLine("  unlock <dir>                 — unlock an exit (key auto-used)");
        Console.WriteLine("  help             / ?         — show this list");
        Console.WriteLine("  quit             / exit / q  — exit the game");
        Console.WriteLine();
    }

    public static void Win()
    {
        Console.WriteLine();
        Console.WriteLine("*******************************************************");
        Console.WriteLine("  You step back into the courtyard clutching the");
        Console.WriteLine("  Polo Satchel. The first grey light of dawn touches");
        Console.WriteLine("  the eastern walls. In the distance, you hear the");
        Console.WriteLine("  bells of Marco Polo's caravan — you made it!");
        Console.WriteLine();
        Console.WriteLine("  CONGRATULATIONS — You have rejoined the caravan.");
        Console.WriteLine("*******************************************************");
        Console.WriteLine();
    }
}
