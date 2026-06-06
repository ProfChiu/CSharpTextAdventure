namespace Phase2;

static class GameRunner
{
    public static void Start()
    {
        // Geographic leg 2: the trail up the mountain, ending at the Hidden Pass.
        // Caravanserai Gate -> Foothill Trail -> Old Shrine -> Wind Ridge -> Hidden Pass,
        // with the Ice Cave as a side branch off the Foothill Trail.
        Room gate = new Room(
            "Caravanserai Gate",
            "Dawn at the inn gate. The Polo Satchel is slung over your shoulder and the\ncaravan is a day ahead. The trail climbs north into the mountains.",
            "   /\\  /\\\n  /__\\/__\\\n  |  ||  |\n  |[]||[]|"
        );

        Room trail = new Room(
            "Foothill Trail",
            "A stony switchback path. Loose scree slides underfoot. The trail bends north,\nand a dark opening yawns to the east.",
            "    /\\\n   /  \\__\n  /      \\\n_/        \\_"
        );

        Room iceCave = new Room(
            "Ice Cave",
            "A frozen hollow in the rock. Your breath fogs. Something heavy and warm is\nfolded on a ledge. The only way out is back west.",
            "  __________\n /  *  ..  * \\\n|  .  ICE  . |\n \\__________/"
        );

        Room shrine = new Room(
            "Old Shrine",
            "A weathered roadside shrine, half-buried in snow. Travelers before you left\nofferings. The path continues north.",
            "    _i_\n   |___|\n   |   |\n  _|   |_"
        );

        Room ridge = new Room(
            "Wind Ridge",
            "A knife-edge ridge scoured by howling wind. The cold cuts to the bone. A narrow\ngap leads north toward the pass — but the wind would tear you off the edge.",
            "  /\\    /\\\n /  \\/\\/  \\\n/   wind   \\"
        );

        Room hiddenPass = new Room(
            "Hidden Pass",
            "A sheltered cleft between the peaks — the way through the mountains.",
            "   ____________\n  /  the pass  \\\n /______________\\"
        );

        // Wire the world (N/S/E/W fields, Phase-2 style).
        gate.North   = trail;
        trail.South   = gate;
        trail.North   = shrine;
        trail.East    = iceCave;
        iceCave.West  = trail;
        shrine.South  = trail;
        shrine.North  = ridge;
        ridge.South   = shrine;
        ridge.North   = hiddenPass;
        hiddenPass.South = ridge;

        // Scatter supplies along the trail. The Fur Cloak hides in the side cave,
        // so the player must explore east before they can cross the ridge.
        gate.Items.Add(new Item("Dried Figs"));
        trail.Items.Add(new Item("Oil Lantern"));
        iceCave.Items.Add(new Item("Fur Cloak"));
        shrine.Items.Add(new Item("Flint"));
        ridge.Items.Add(new Item("Rope"));

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
        Console.WriteLine("=== Chapter 2: The Mountain Pass ===");
        Console.WriteLine("A storm has sealed the high road. Gather what you need and cross the pass.");
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

        // Locked crossing: Wind Ridge -> Hidden Pass needs BOTH the Fur Cloak (warmth)
        // and the Rope (to lash yourself against the wind). Hard-coded by room name,
        // Phase-2 style. "What if there were 20 locked doors?" -> see later chapters.
        if (_player.CurrentRoom.Name == "Wind Ridge" && direction == "north")
        {
            if (!CarryingItem("Fur Cloak") || !CarryingItem("Rope"))
            {
                Console.WriteLine("The wind shrieks across the gap. Without the Fur Cloak to keep warm");
                Console.WriteLine("and the Rope to anchor yourself, crossing here would be death.");
                return;
            }
            Console.WriteLine("Wrapped in the Fur Cloak and lashed to the rock by the Rope, you inch");
            Console.WriteLine("across the gap into the shelter of the pass.");
        }

        _player.CurrentRoom = next;
        PrintRoom();

        // Win: you've reached the Hidden Pass and broken through the mountains.
        if (_player.CurrentRoom.Name == "Hidden Pass")
        {
            Console.WriteLine();
            Console.WriteLine("You stagger into the Hidden Pass. The wind falls away behind you.");
            Console.WriteLine("Below, the mountains open onto the green of an oasis town —");
            Console.WriteLine("and somewhere down there, the caravan's trail grows warm again.");
            Console.WriteLine("CONGRATULATIONS! On to Chapter 3: The Oasis Bazaar.");
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
