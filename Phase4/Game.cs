namespace Phase4;

static class GameRunner
{
    public static void Start()
    {
        // Geographic leg 4: a customs fortress on the desert road. A central spine
        // (Gatehouse -> Customs Hall -> Inner Courtyard -> Sally Port) is barred by
        // three locked doors; the keys/passes hide in side rooms off the spine.
        Room gatehouse = new Room(
            "Gatehouse",
            "A massive arched gate of mortared stone. Guards eye you from the shadows.\nThe inner fortress lies north; a barracks opens east.",
            "  |^^^^^^^^^^|\n  | []  []   |\n  | GATEHOUSE |"
        );

        Room barracks = new Room(
            "Guard Barracks",
            "Rows of empty cots and a rack of spears. An iron gate key hangs on a peg by\nthe door. The gatehouse is back to the west.",
            "  |=|=|=|=|=|\n   cots & spears"
        );

        Room customsHall = new Room(
            "Customs Hall",
            "A high hall where tariffs are weighed. A heavy door leads north into the\ncourtyard; an archive opens east and a cistern west.",
            "   ___________\n  | $ scales $ |\n  | CUSTOMS    |"
        );

        Room archive = new Room(
            "Archive",
            "Shelves of dusty scrolls and tax ledgers. A wax customs seal lies among them.\nThe hall is back to the west.",
            "  |≡≡|≡≡|≡≡|\n   scrolls"
        );

        Room cistern = new Room(
            "Cistern",
            "A cool underground reservoir. A lantern rests on the steps. The hall is east.",
            "  ~~~~~~~~~~~\n  ( water )\n  ~~~~~~~~~~~"
        );

        Room courtyard = new Room(
            "Inner Courtyard",
            "An open courtyard baked by the sun. The sally port — the fortress's back gate\nto the desert — lies north. A watchtower stair climbs east.",
            "   [] [] [] []\n   courtyard\n   [] [] [] []"
        );

        Room watchtower = new Room(
            "Watchtower",
            "The top of the tower. The captain's pass, signed and sealed, is pinned to a\nmap of the road ahead. The courtyard is back to the west.",
            "    /\\\n   /  \\\n   |[]|\n   tower"
        );

        Room sallyPort = new Room(
            "Sally Port",
            "A low desert gate standing open to the dunes — and the caravan's road beyond.",
            "  ____________\n  | ||  || ||\n  | SALLY PORT"
        );

        // Wire the world.
        gatehouse.North   = customsHall;
        gatehouse.East    = barracks;
        barracks.West     = gatehouse;
        customsHall.South = gatehouse;
        customsHall.North = courtyard;
        customsHall.East  = archive;
        customsHall.West  = cistern;
        archive.West      = customsHall;
        cistern.East      = customsHall;
        courtyard.South   = customsHall;
        courtyard.North   = sallyPort;
        courtyard.East    = watchtower;
        watchtower.West   = courtyard;
        sallyPort.South   = courtyard;

        // Three locked doors along the spine. Each names an exit id; the matching
        // KeyItem (below) shares that id. Adding a fourth locked door would need NO
        // new code in Game — just a LockExit + a KeyItem with the same id.
        gatehouse.LockExit("north", "iron-gate");
        customsHall.LockExit("north", "customs-seal");
        courtyard.LockExit("north", "captains-pass");

        // The keys/passes are KeyItems (Item subclass). Each opens one exit id.
        barracks.Items.Add(new KeyItem(
            "Iron Gate Key",
            "A heavy iron key, cold to the touch. It fits the inner gatehouse door.",
            "iron-gate"));

        archive.Items.Add(new KeyItem(
            "Customs Seal",
            "A disk of red wax stamped with the fortress crest — proof your goods are cleared.",
            "customs-seal"));

        watchtower.Items.Add(new KeyItem(
            "Captain's Pass",
            "A signed pass granting passage through the sally port to the desert road.",
            "captains-pass"));

        // Plain Items: a clue and a bit of flavor.
        archive.Items.Add(new Item(
            "Ledger",
            "An entry reads: \"None pass the inner gate without the iron key, the customs seal at the hall, and the captain's pass at the port.\"",
            canPickUp: true));

        cistern.Items.Add(new Item(
            "Lantern",
            "A brass lantern with a stub of candle — useful in the fortress's dark corners.",
            canPickUp: true));

        Player player = new Player(gatehouse);
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
        Display.Intro();
        Display.ShowRoom(_player.CurrentRoom);

        while (true)
        {
            Display.Prompt();
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
            Display.ShowRoom(_player.CurrentRoom);
        else if (input == "quit" || input == "exit" || input == "q")
            Environment.Exit(0);
        else
            Display.Line("Try: go <dir> / take <item> / drop <item> / examine <item> / inventory / look / quit");
    }

    private void TryMove(string direction)
    {
        Room here = _player.CurrentRoom;
        Room? next = null;

        if (direction == "north") next = here.North;
        if (direction == "south") next = here.South;
        if (direction == "east")  next = here.East;
        if (direction == "west")  next = here.West;

        if (next == null)
        {
            Display.Line("You can't go that way.");
            return;
        }

        // Generic locked-door check: if this exit is locked, look for ANY KeyItem
        // in the pack whose UnlocksExitId matches. No room-name strings here.
        if (here.LockedDirection == direction && here.LockedExitId != null)
        {
            KeyItem? key = FindKeyFor(here.LockedExitId);
            if (key == null)
            {
                Display.Line("The way is locked. You need the right key or pass to get through.");
                return;
            }

            Display.Line($"You present the {key.Name}. The way opens.");
            here.LockedDirection = null;   // it stays open from now on
        }

        _player.CurrentRoom = next;
        Display.ShowRoom(_player.CurrentRoom);

        // Win: out the sally port and onto the caravan road.
        if (_player.CurrentRoom.Name == "Sally Port")
        {
            Display.Blank();
            Display.Line("You walk out the sally port into the open desert. The fortress falls");
            Display.Line("behind you, and ahead the caravan's tracks run on toward the horizon.");
            Display.Line("CONGRATULATIONS! On to Chapter 5: The Caravan & the Guardian's Riddle.");
            Environment.Exit(0);
        }
    }

    // Scan the inventory for a KeyItem matching this exit id. This is where
    // inheritance pays off: we ask each Item whether it `is KeyItem`, and only
    // those carry an UnlocksExitId.
    private KeyItem? FindKeyFor(string exitId)
    {
        foreach (Item item in _player.Inventory.GetAll())
        {
            if (item is KeyItem)
            {
                KeyItem key = (KeyItem)item;
                if (key.UnlocksExitId == exitId)
                    return key;
            }
        }
        return null;
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
            Display.Line($"You don't see any '{itemName}' here.");
            return;
        }

        if (!found.CanPickUp)
        {
            Display.Line($"You can't carry the {found.Name}.");
            return;
        }

        _player.CurrentRoom.Items.Remove(found);
        _player.Inventory.Add(found);
        Display.Line($"You take the {found.Name}.");
    }

    private void DropItem(string itemName)
    {
        Item? found = _player.Inventory.Find(itemName);

        if (found == null)
        {
            Display.Line($"You aren't carrying any '{itemName}'.");
            return;
        }

        _player.Inventory.Remove(found);
        _player.CurrentRoom.Items.Add(found);
        Display.Line($"You drop the {found.Name}.");
    }

    private void ExamineItem(string itemName)
    {
        foreach (Item item in _player.CurrentRoom.Items)
        {
            if (item.Name.ToLower() == itemName)
            {
                Display.Line(item.Description);
                return;
            }
        }

        Item? carried = _player.Inventory.Find(itemName);
        if (carried != null)
        {
            Display.Line(carried.Description);
            return;
        }

        Display.Line($"You don't see any '{itemName}' to examine.");
    }
}
