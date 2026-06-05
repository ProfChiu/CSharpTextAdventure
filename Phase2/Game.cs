namespace Phase2;

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
            "A long vaulted hall lit by a single oil lamp. Exits: north, south.",
            "      |\n    -----\n   /     \\\n  O       O\n    scale"
        );

        Room vault = new Room(
            "Merchant's Vault",
            "A small cedar-shelved chamber. A leather satchel sits on the table.",
            "  .-------.\n  |[lock] |\n  |_______|\n  |  $ $  |\n  `-------'"
        );

        courtyard.South = tradeHall;
        tradeHall.North = courtyard;
        tradeHall.South = vault;
        vault.North     = tradeHall;

        // Place items in rooms
        Item brassKey    = new Item("Brass Key");
        Item poloSatchel = new Item("Polo Satchel");
        tradeHall.Items.Add(brassKey);
        vault.Items.Add(poloSatchel);

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
        Console.WriteLine("=== Chapter 2: Items & Inventory ===");
        Console.WriteLine("Commands: go <dir> / take <item> / drop <item> / inventory / look / quit");
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
        else if (input.StartsWith("take "))
            TakeItem(input.Substring(5));
        else if (input.StartsWith("drop "))
            DropItem(input.Substring(5));
        else if (input == "inventory" || input == "i" || input == "inv")
            ShowInventory();
        else if (input == "look" || input == "l")
            PrintRoom();
        else if (input == "quit" || input == "exit" || input == "q")
            Environment.Exit(0);
        else
            Console.WriteLine("Try: go <dir> / take <item> / drop <item> / inventory / look / quit");
    }

    private void TryMove(string direction)
    {
        Room? next = null;

        if (direction == "north") next = _player.CurrentRoom.North;
        if (direction == "south") next = _player.CurrentRoom.South;
        if (direction == "east")  next = _player.CurrentRoom.East;
        if (direction == "west")  next = _player.CurrentRoom.West;

        if (next == null)
        {
            Console.WriteLine("You can't go that way.");
            return;
        }

        // Locked door: Trade Hall -> Vault requires the Brass Key
        if (_player.CurrentRoom.Name == "Trade Hall" && direction == "south")
        {
            bool hasKey = false;
            foreach (Item item in _player.Inventory)
            {
                if (item.Name == "Brass Key")
                {
                    hasKey = true;
                    break;
                }
            }

            if (!hasKey)
            {
                Console.WriteLine("The vault door is locked. You need a key.");
                return;
            }
            Console.WriteLine("You use the Brass Key. The vault door swings open.");
        }

        _player.CurrentRoom = next;
        PrintRoom();

        // Win: back in Courtyard carrying the Polo Satchel
        if (_player.CurrentRoom.Name == "Courtyard" && CarryingItem("Polo Satchel"))
        {
            Console.WriteLine();
            Console.WriteLine("You step into the courtyard clutching the Polo Satchel.");
            Console.WriteLine("The caravan bells ring. You made it!");
            Console.WriteLine("CONGRATULATIONS!");
            Environment.Exit(0);
        }
    }

    private void TakeItem(string itemName)
    {
        Item? found = null;
        foreach (Item item in _player.CurrentRoom.Items)
        {
            if (item.Name.ToLower() == itemName)
            {
                found = item;
                break;
            }
        }

        if (found == null)
            Console.WriteLine($"You don't see any '{itemName}' here.");
        else
        {
            _player.CurrentRoom.Items.Remove(found);
            _player.Inventory.Add(found);
            Console.WriteLine($"You take the {found.Name}.");
        }
    }

    private void DropItem(string itemName)
    {
        Item? found = null;
        foreach (Item item in _player.Inventory)
        {
            if (item.Name.ToLower() == itemName)
            {
                found = item;
                break;
            }
        }

        if (found == null)
            Console.WriteLine($"You aren't carrying any '{itemName}'.");
        else
        {
            _player.Inventory.Remove(found);
            _player.CurrentRoom.Items.Add(found);
            Console.WriteLine($"You drop the {found.Name}.");
        }
    }

    private void ShowInventory()
    {
        if (_player.Inventory.Count == 0)
            Console.WriteLine("You are carrying nothing.");
        else
        {
            Console.WriteLine("You are carrying:");
            foreach (Item item in _player.Inventory)
                Console.WriteLine($"  {item.Name}");
        }
    }

    private bool CarryingItem(string itemName)
    {
        foreach (Item item in _player.Inventory)
        {
            if (item.Name == itemName)
                return true;
        }
        return false;
    }

    private void PrintRoom()
    {
        Room room = _player.CurrentRoom;
        Console.WriteLine();
        if (room.Art != "") Console.WriteLine(room.Art);
        Console.WriteLine($"[ {room.Name.ToUpper()} ]");
        Console.WriteLine(room.Description);

        if (room.Items.Count > 0)
        {
            Console.Write("You see here: ");
            foreach (Item item in room.Items)
                Console.Write(item.Name + "  ");
            Console.WriteLine();
        }

        Console.Write("Exits: ");
        if (room.North != null) Console.Write("north ");
        if (room.South != null) Console.Write("south ");
        if (room.East  != null) Console.Write("east ");
        if (room.West  != null) Console.Write("west ");
        Console.WriteLine();
    }
}
