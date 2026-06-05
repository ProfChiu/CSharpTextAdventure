namespace Phase1;

static class GameRunner
{
    public static void Start()
    {
        Room courtyard = new Room(
            "Courtyard",
            "A dusty open square under a darkening sky. A heavy wooden door leads south.",
            "     ___\n    /   \\\n   (  ~  )\n    \\___/\n  __|   |__"
        );

        Room tradeHall = new Room(
            "Trade Hall",
            "A long vaulted hall lit by a single oil lamp. Overturned tables litter the floor. Exits: north, south.",
            "      |\n    -----\n   /     \\\n  O       O\n    scale"
        );

        Room vault = new Room(
            "Merchant's Vault",
            "A small cedar-shelved chamber. A leather satchel stamped with the Polo crest sits on the table.",
            "  .-------.\n  |[lock] |\n  |_______|\n  |  $ $  |\n  `-------'"
        );

        courtyard.South = tradeHall;
        tradeHall.North = courtyard;
        tradeHall.South = vault;
        vault.North     = tradeHall;

        Player player = new Player(courtyard);
        new Game(player).Run();
    }
}

class Game
{
    private Player _player;

    public Game(Player player)
    {
        _player = player;
    }

    public void Run()
    {
        Console.WriteLine("=== Chapter 1: Rooms & Movement ===");
        Console.WriteLine("Commands: go north / go south / go east / go west / quit");
        PrintRoom();

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (input == null) break;
            HandleInput(input.ToLower().Trim());
        }
    }

    private void HandleInput(string input)
    {
        if (input == "go north" || input == "north" || input == "n")
            TryMove("north");
        else if (input == "go south" || input == "south" || input == "s")
            TryMove("south");
        else if (input == "go east" || input == "east" || input == "e")
            TryMove("east");
        else if (input == "go west" || input == "west" || input == "w")
            TryMove("west");
        else if (input == "quit" || input == "exit" || input == "q")
            Environment.Exit(0);
        else
            Console.WriteLine("Try: go north / go south / go east / go west / quit");
    }

    private void TryMove(string direction)
    {
        Room? next = null;

        if (direction == "north") next = _player.CurrentRoom.North;
        if (direction == "south") next = _player.CurrentRoom.South;
        if (direction == "east")  next = _player.CurrentRoom.East;
        if (direction == "west")  next = _player.CurrentRoom.West;

        if (next == null)
            Console.WriteLine("You can't go that way.");
        else
        {
            _player.CurrentRoom = next;
            PrintRoom();
        }
    }

    private void PrintRoom()
    {
        Room room = _player.CurrentRoom;
        Console.WriteLine();
        if (room.Art != "") Console.WriteLine(room.Art);
        Console.WriteLine($"[ {room.Name.ToUpper()} ]");
        Console.WriteLine(room.Description);
        Console.Write("Exits: ");
        if (room.North != null) Console.Write("north ");
        if (room.South != null) Console.Write("south ");
        if (room.East  != null) Console.Write("east ");
        if (room.West  != null) Console.Write("west ");
        Console.WriteLine();
    }
}
