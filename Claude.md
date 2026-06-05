# A text adventure game made with C# dot net.

## Purpose

This is a **teaching project** for an introductory/intermediate C# course. The goal is to demonstrate core object-oriented programming concepts — primarily **classes and objects** (fields, properties, constructors, methods) — through a small, readable, fully working game. Code simplicity and clarity take priority over architectural sophistication.

---

## Setting & Premise

**The Silk Road, 1271 AD — A Caravanserai at Dusk**

The player is a young Venetian merchant's apprentice who has become separated from Marco Polo's caravan. Stranded at an ancient fortified roadside inn (a caravanserai), they must explore the compound, find their missing travel documents and supplies, and rejoin the caravan before it departs at dawn.

There is no combat. Tension comes from exploration, finding items, and unlocking areas.

---

## Project Structure

```
CSharpTextAdventure/
  CSharpTextAdventure.csproj
  Program.cs              -- Entry point: creates Game and calls Run()

  Game/
    Game.cs               -- Main game loop: reads input, calls Parser, updates state
    Parser.cs             -- Splits input, uses a switch to call the right action

  World/
    Room.cs               -- Class: Name, Description, Exits (Dictionary), Items (List)
    Exit.cs               -- Class: Destination room, IsLocked, RequiredKey
    WorldBuilder.cs       -- Creates and connects all rooms; returns the starting room

  Items/
    Item.cs               -- Class: Name, Description, CanPickUp
    KeyItem.cs            -- Subclass of Item; adds UnlocksExitId property

  Characters/
    Player.cs             -- Class: CurrentRoom, Inventory, VisitedRooms
    Inventory.cs          -- Class: holds List<Item>, has Add/Remove/Find/Display methods

  UI/
    Display.cs            -- All Console.Write calls go here (one place to change output)
```

---

## Key Classes

### Item
```csharp
class Item {
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CanPickUp { get; set; }

    public Item(string name, string description, bool canPickUp) { ... }
}
```

### KeyItem (inherits from Item)
```csharp
class KeyItem : Item {
    public string UnlocksExitId { get; set; }

    public KeyItem(string name, string description, string unlocksExitId)
        : base(name, description, canPickUp: true) { ... }
}
```

### Room
```csharp
class Room {
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string, Exit> Exits { get; set; }
    public List<Item> Items { get; set; }
    public bool HasBeenVisited { get; set; }

    public Room(string name, string description) { ... }
    public void AddExit(string direction, Exit exit) { ... }
    public void AddItem(Item item) { ... }
    public string GetExitsList() { ... }
}
```

### Player
```csharp
class Player {
    public Room CurrentRoom { get; set; }
    public Inventory Inventory { get; set; }
    public List<string> VisitedRooms { get; set; }

    public Player(Room startingRoom) { ... }
    public void MoveTo(Room room) { ... }
    public bool HasVisited(Room room) { ... }
}
```

### Parser (simple switch — no interface needed)
```csharp
class Parser {
    public void Parse(string input, Player player) {
        string[] words = input.ToLower().Trim().Split(' ');
        string verb = words[0];
        string noun = words.Length > 1 ? words[1] : "";

        switch (verb) {
            case "go": case "n": case "s": case "e": case "w":
                Actions.Go(noun, player); break;
            case "take": case "get":
                Actions.Take(noun, player); break;
            case "look": case "l":
                Actions.Look(player); break;
            // ...
        }
    }
}
```

---

## Commands

| Input | Aliases | Action |
|---|---|---|
| `go <direction>` | `n`, `s`, `e`, `w` | Move to adjacent room |
| `look` | `l` | Describe current room |
| `examine <item>` | `x` | Show item description |
| `take <item>` | `get` | Pick up item into inventory |
| `drop <item>` | | Drop item into current room |
| `inventory` | `i`, `inv` | List carried items |
| `unlock <direction>` | | Unlock an exit using a key in inventory |
| `help` | `?` | Show command list |
| `quit` | `exit`, `q` | Exit the game |

---

## World Map

```
[OUTER GATE]
      | south
      v
[COURTYARD] ---east---> [STABLES]  <-- Brass Key is here
      | south
      v
[TRADE HALL] ---east---> [MERCHANT'S VAULT]  <-- WIN
             (locked: requires Brass Key)
```

**Win condition:** The player finds the Brass Key in the Stables, unlocks the east door of the Trade Hall, enters the Merchant's Vault, and takes or examines the Polo Satchel.

### Room Descriptions

- **Outer Gate** — Crumbling mudbrick archway; the northern entrance to the compound. No items.
- **Courtyard** — Wide stone square with a dry fountain. Contains a Worn Map (takeable) and Copper Coins (takeable, flavor only).
- **Stables** — Dark, hay-scented. Contains a Saddle Bag (examine only) and the Brass Key (takeable).
- **Trade Hall** — Long vaulted hall with overturned tables. Contains a Clay Lamp (flavor). East exit is locked.
- **Merchant's Vault** — Cedar shelves, bolts of silk. Contains the Polo Satchel — taking or examining it triggers the win.

---

## Build & Run

```bash
# Initialize project (first time only)
dotnet new console -n CSharpTextAdventure --output .

# Build
dotnet build

# Run
dotnet run
```

Add to `<PropertyGroup>` in `CSharpTextAdventure.csproj`:
```xml
<Nullable>enable</Nullable>
<ImplicitUsings>enable</ImplicitUsings>
<LangVersion>latest</LangVersion>
```

---

## Implementation Order

1. `Item.cs` + `KeyItem.cs` — simplest classes, good first lesson on properties and constructors
2. `Room.cs` + `Exit.cs` — classes that use collections (List, Dictionary) as properties
3. `Player.cs` + `Inventory.cs` — classes that reference other classes
4. `WorldBuilder.cs` — demonstrates constructing and wiring objects together
5. `Display.cs` — console output helpers
6. `Parser.cs` — string parsing and switch dispatch
7. `Game.cs` — the main game loop
8. `Program.cs` — entry point (just a few lines)

---

## Teaching Guidelines

- Each class should fit on one screen (~50 lines max) so students can read it in full
- Use explicit `public string Name { get; set; }` properties — not bare fields — to show the pattern
- `WorldBuilder.cs` is the best place to show how object instances reference each other
- `KeyItem : Item` demonstrates inheritance in a concrete, memorable context
- Comments should explain *why* something is done, not restate what the code already says
