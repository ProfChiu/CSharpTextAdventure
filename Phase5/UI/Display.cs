using Phase5.Characters;
using Phase5.World;

namespace Phase5.UI;

static class Display
{
    public static void Welcome()
    {
        Console.WriteLine("=======================================================");
        Console.WriteLine("  THE CARAVAN & THE GUARDIAN'S RIDDLE — Chapter 5");
        Console.WriteLine("  1271 AD. You have tracked Marco Polo's caravan to its");
        Console.WriteLine("  camp at a moonlit oasis. Make your way to its heart —");
        Console.WriteLine("  but the last road is guarded by more than men.");
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
            Console.WriteLine("You see here: " + string.Join(", ", room.Items.Select(i => i.Name)) + ".");
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

    // --- The guardian encounter ---

    public static void Goddess()
    {
        Console.WriteLine();
        Console.WriteLine("        .  *  .   *  .  *");
        Console.WriteLine("         \\   .  |  .   /");
        Console.WriteLine("          .--' '-' '--.");
        Console.WriteLine("          |   ( o o )  |");
        Console.WriteLine("          |    \\ ^ /   |");
        Console.WriteLine("         /| .--'~'--. |\\");
        Console.WriteLine("        ( |/  GODDESS \\| )");
        Console.WriteLine("         \\|___________|/");
        Console.WriteLine();
        Console.WriteLine("The shrine fills with pale light. Not bandits — a goddess, radiant and");
        Console.WriteLine("calm, guardian of the road. She does not reach for a weapon. She smiles.");
        Console.WriteLine("\"No one passes to the caravan,\" she says, \"who cannot answer me three.");
        Console.WriteLine("Answer with words, traveler. You may try each as often as you like.\"");
        Console.WriteLine("(Type your answer. You may type QUIT to step back from her.)");
    }

    public static void RiddleQuestion(int number, string question)
    {
        Console.WriteLine();
        Console.WriteLine($"Riddle {number} of 3:");
        Console.WriteLine($"  \"{question}\"");
    }

    public static void RiddleCorrect()
    {
        Console.WriteLine("The goddess inclines her head. \"Yes. That is so.\"");
    }

    public static void RiddleWrong(string hint)
    {
        Console.WriteLine("She shakes her head gently. \"Not so. Think again.\"");
        Console.WriteLine($"  Hint: {hint}");
    }

    public static void GoddessBlessing()
    {
        Console.WriteLine();
        Console.WriteLine("\"Three of three,\" the goddess says. \"Wisdom, not iron, opens this road.\"");
        Console.WriteLine("She steps aside and the light gathers into a path leading north.");
        Console.WriteLine("\"Go. They are waiting for you.\"");
    }

    public static void Win()
    {
        Console.WriteLine();
        Console.WriteLine("*******************************************************");
        Console.WriteLine("       ___    welcome home    ___");
        Console.WriteLine("      /   \\  /\\  /\\  /\\  /\\  /   \\");
        Console.WriteLine("     | tent |/  \\/  \\/  \\/  \\| fire |");
        Console.WriteLine("*******************************************************");
        Console.WriteLine();
        Console.WriteLine("  You walk into the heart of the caravan. A familiar");
        Console.WriteLine("  figure turns at the firelight — Marco Polo himself,");
        Console.WriteLine("  older and dustier than you remember, breaking into a");
        Console.WriteLine("  grin. \"We had given you up,\" he says, clasping your");
        Console.WriteLine("  arm. You show him the seal; he laughs aloud.");
        Console.WriteLine();
        Console.WriteLine("  At dawn the bells ring and the caravan moves east");
        Console.WriteLine("  toward Cathay — and you ride with it, at last.");
        Console.WriteLine();
        Console.WriteLine("  THE END — You have rejoined Marco Polo's caravan.");
        Console.WriteLine("*******************************************************");
        Console.WriteLine();
    }
}
