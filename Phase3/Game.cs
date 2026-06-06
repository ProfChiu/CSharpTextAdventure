namespace Phase3;

static class GameRunner
{
    public static void Start()
    {
        // Geographic leg 3: down from the pass into a Silk Road oasis town.
        // Market Square is the hub; stalls branch off it.
        Room gate = new Room(
            "Town Gate",
            "A mud-brick gate into a bustling oasis town. Palm trees throw shade over the\nstreet. A notice board is nailed beside the arch. The market lies north.",
            "  _____________\n  | |       | |\n  | |  TOWN | |\n  | |  GATE | |"
        );

        Room market = new Room(
            "Market Square",
            "A crowded square full of haggling voices and the smell of dust and saffron.\nStalls open east and west; a well stands to the north.",
            "  ___   ___   ___\n  |o|   |o|   |o|\n  market stalls"
        );

        Room spiceStall = new Room(
            "Spice Stall",
            "Sacks of pepper, cumin, and saffron in little mountains. An old brass lamp\nsits forgotten on the counter. The square is back to the west.",
            "  (~) (~) (~)\n  spice  sacks\n  (~) (~) (~)"
        );

        Room carpetWeaver = new Room(
            "Carpet Weaver",
            "A dim shop hung with rugs in deep reds and blues. A fine bolt of silk is\nstacked among them. The square is back to the east.",
            "  |XXX|XXX|XXX|\n  |XXX|XXX|XXX|\n  woven  carpets"
        );

        Room well = new Room(
            "Public Well",
            "A stone well ringed by women filling jars. The water is cool and clear.\nThe square is south; the stable lies north.",
            "   .-\"\"\"-.\n   | o o |\n   |  W  |\n   '-----'"
        );

        Room stable = new Room(
            "Stable",
            "A shaded stable smelling of camels and straw. A silver coin glints in the\ndirt where a traveler dropped it. The guide's house is just east.",
            "   /\\_/\\\n  ( camel )\n   \\___/  |"
        );

        Room guideHouse = new Room(
            "Guide's House",
            "A low house of packed earth. The desert guide sits cross-legged on a rug,\nwaiting to hear what you can offer for the crossing.",
            "   _______\n  /  guide \\\n /  [ @ ]  \\"
        );

        // Wire the world (N/S/E/W fields, Phase-2/3 style).
        gate.North         = market;
        market.South       = gate;
        market.East        = spiceStall;
        spiceStall.West    = market;
        market.West        = carpetWeaver;
        carpetWeaver.East  = market;
        market.North       = well;
        well.South         = market;
        well.North         = stable;
        stable.South       = well;
        stable.East        = guideHouse;
        guideHouse.West    = stable;

        // Items. The last argument is CanPickUp: fixtures set it to false so `take`
        // refuses them, but `examine` still reads their Description.
        gate.Items.Add(new Item(
            "Notice Board",
            "Charcoal scrawl: \"Desert guide for hire. I take only a bolt of silk and a silver coin.\"",
            canPickUp: false));

        spiceStall.Items.Add(new Item(
            "Brass Lamp",
            "Tarnished, but you can read an engraving: \"Silk for the cloth, silver for the coin — the guide trades for nothing else.\"",
            canPickUp: true));

        carpetWeaver.Items.Add(new Item(
            "Bolt of Silk",
            "A heavy bolt of shimmering Venetian silk — worth a small fortune out here.",
            canPickUp: true));

        well.Items.Add(new Item(
            "Public Well",
            "An ancient stone well. Far too heavy to carry, but the water is good.",
            canPickUp: false));

        well.Items.Add(new Item(
            "Water Skin",
            "A goatskin flask, filled cold and full at the well. Handy for the desert ahead.",
            canPickUp: true));

        stable.Items.Add(new Item(
            "Silver Coin",
            "A Venetian silver grosso, stamped with the doge's likeness.",
            canPickUp: true));

        Player player = new Player(gate);
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
        Console.WriteLine("=== Chapter 3: The Oasis Bazaar ===");
        Console.WriteLine("Hire a desert guide. Barter what the bazaar offers to win the crossing.");
        Console.WriteLine("Commands: go <dir> / take <item> / drop <item> / examine <item> / inventory / look / quit");
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
        else if (input.StartsWith("examine "))
            ExamineItem(input.Substring(8));
        else if (input == "inventory" || input == "i" || input == "inv")
            _player.Inventory.Display();
        else if (input == "look" || input == "l")
            PrintRoom();
        else if (input == "quit" || input == "exit" || input == "q")
            Environment.Exit(0);
        else
            Console.WriteLine("Try: go <dir> / take <item> / drop <item> / examine <item> / inventory / look / quit");
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

        _player.CurrentRoom = next;
        PrintRoom();

        // Win: reaching the guide carrying BOTH the silk and the coin hires him.
        // The Inventory's Find() does the lookup for us (encapsulation in action).
        if (_player.CurrentRoom.Name == "Guide's House")
        {
            Item? silk = _player.Inventory.Find("Bolt of Silk");
            Item? coin = _player.Inventory.Find("Silver Coin");

            if (silk == null || coin == null)
            {
                Console.WriteLine();
                Console.WriteLine("The guide shakes his head. \"A bolt of silk and a silver coin — that is");
                Console.WriteLine("my price for the desert. Come back when you carry both.\"");
                return;
            }

            // Pay the guide: hand over both items, then win.
            _player.Inventory.Remove(silk);
            _player.Inventory.Remove(coin);

            Console.WriteLine();
            Console.WriteLine("The guide weighs the silk, bites the coin, and grins.");
            Console.WriteLine("\"Done. I will take you across the sands.\"");
            Console.WriteLine("With a guide at your side, the desert road opens ahead.");
            Console.WriteLine("CONGRATULATIONS! On to Chapter 4: The Desert Fortress.");
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
        {
            Console.WriteLine($"You don't see any '{itemName}' here.");
            return;
        }

        // CanPickUp gates what `take` will accept — fixtures stay put.
        if (!found.CanPickUp)
        {
            Console.WriteLine($"You can't carry the {found.Name}.");
            return;
        }

        _player.CurrentRoom.Items.Remove(found);
        _player.Inventory.Add(found);
        Console.WriteLine($"You take the {found.Name}.");
    }

    private void DropItem(string itemName)
    {
        Item? found = _player.Inventory.Find(itemName);

        if (found == null)
        {
            Console.WriteLine($"You aren't carrying any '{itemName}'.");
            return;
        }

        _player.Inventory.Remove(found);
        _player.CurrentRoom.Items.Add(found);
        Console.WriteLine($"You drop the {found.Name}.");
    }

    // NEW command: read an item's Description, whether it's here or in your pack.
    private void ExamineItem(string itemName)
    {
        // First check the room, then your inventory.
        foreach (Item item in _player.CurrentRoom.Items)
        {
            if (item.Name.ToLower() == itemName)
            {
                Console.WriteLine(item.Description);
                return;
            }
        }

        Item? carried = _player.Inventory.Find(itemName);
        if (carried != null)
        {
            Console.WriteLine(carried.Description);
            return;
        }

        Console.WriteLine($"You don't see any '{itemName}' to examine.");
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
