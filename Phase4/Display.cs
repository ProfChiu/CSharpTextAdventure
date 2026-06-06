namespace Phase4;

// NEW in Chapter 4: separation of concerns. Every Console call in this chapter
// lives in this one STATIC class — a static class is never instantiated; you call
// its methods directly (Display.Line("...")). If we want to change how the game
// looks, we change it here and nowhere else. Compare earlier chapters, where Game
// sprinkled Console.WriteLine everywhere.
static class Display
{
    public static void Line(string text)
    {
        Console.WriteLine(text);
    }

    public static void Blank()
    {
        Console.WriteLine();
    }

    public static void Prompt()
    {
        Console.Write("> ");
    }

    public static void Intro()
    {
        Line("=== Chapter 4: The Desert Fortress ===");
        Line("A customs fortress bars the route. Find the keys and passes to slip through.");
        Line("Commands: go <dir> / take <item> / drop <item> / examine <item> / inventory / look / quit");
    }

    public static void ShowRoom(Room room)
    {
        Blank();
        if (room.Art != "") Line(room.Art);
        Line($"[ {room.Name.ToUpper()} ]");
        Line(room.Description);

        if (room.Items.Count > 0)
        {
            Console.Write("You see here: ");
            foreach (Item item in room.Items)
                Console.Write(item.Name + "  ");
            Blank();
        }

        Console.Write("Exits: ");
        if (room.North != null) Console.Write("north ");
        if (room.South != null) Console.Write("south ");
        if (room.East  != null) Console.Write("east ");
        if (room.West  != null) Console.Write("west ");
        Blank();
    }
}
