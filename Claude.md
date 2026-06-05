# A text adventure game made with C# dot net.

## Purpose

This is a **teaching project** for an introductory/intermediate C# course. The goal is to demonstrate core object-oriented programming concepts through a small, fully working game that students can play and read side by side. Code simplicity and clarity take priority over architectural sophistication.

---

## Setting & Premise

**The Silk Road, 1271 AD — A Caravanserai at Dusk**

The player is a young Venetian merchant's apprentice who has become separated from Marco Polo's caravan. Stranded at an ancient fortified roadside inn (a caravanserai), they must explore the compound, find their missing travel documents and supplies, and rejoin the caravan before it departs at dawn.

There is no combat. Tension comes from exploration, finding items, and unlocking areas.

---

## Phased Learning Structure

The project has three chapters that all live in the same repo and compile together as one program. At startup the player picks a chapter. Students can compare source code across chapters to see exactly what was added.

```
dotnet run
→ Choose a chapter: 1, 2, or 3
```

### Chapter 1 — Rooms & Movement  (`Phase1/`, ~110 lines, 4 files)

**Concepts taught:** classes, properties, constructors, object references (Room? holding a Room), while loop, if/else, null checks, method decomposition.

```
Phase1/
  Room.cs      — Name, Description, Art, North/South/East/West? properties
  Player.cs    — CurrentRoom property only
  Game.cs      — GameRunner.Start() wires the world; Game class has Run/HandleInput/TryMove/PrintRoom
```

### Chapter 2 — Items & Inventory  (`Phase2/`, ~210 lines, 5 files)

**Concepts taught:** `List<T>`, `foreach`, using a bool flag inside a loop, conditionals guarding state transitions, win condition.

```
Phase2/
  Room.cs      — Phase 1 Room + List<Item> Items
  Player.cs    — Phase 1 Player + List<Item> Inventory
  Item.cs      — Name only (simplest class)
  Game.cs      — Phase 1 Game + TakeItem/DropItem/ShowInventory/locked-door check
```

The locked-door check (Trade Hall → Vault requires "Brass Key") is intentionally hard-coded by room name. "What if there were 20 locked doors?" is the Chapter 3 lesson.

### Chapter 3 — Full Adventure  (`Phase3/`, 11 files, full architecture)

**Concepts taught:** inheritance (`KeyItem : Item`), static helper classes, `Dictionary<string, T>`, LINQ, separation of concerns across multiple files and namespaces.

```
Phase3/
  GameRunner.cs
  Game/
    Game.cs    — main loop
    Parser.cs  — switch dispatch
    Actions.cs — static action handlers
  World/
    Room.cs, Exit.cs, WorldBuilder.cs
  Characters/
    Player.cs, Inventory.cs
  Items/
    Item.cs, KeyItem.cs
  UI/
    Display.cs
```

---

## ASCII Art

Every `Room` has an `Art` property (string) that is printed before the room description when the player enters. Art is a 5-line ASCII scene representing a prop or landmark in the room. Students can replace the art as a creative exercise.

---

## World Map (same in all chapters)

```
[COURTYARD]  <-- start and finish here
      | south
      v
[TRADE HALL]  <-- Chapter 2+: find the Brass Key
      | south (locked in Chapter 2+)
      v
[MERCHANT'S VAULT]  <-- retrieve the Polo Satchel, then return north to win
```

**Win flow (Chapter 2 & 3):**
1. Go south into the Trade Hall.
2. Pick up the Brass Key.
3. Go south — vault door requires the key.
4. Take the Polo Satchel from the vault.
5. Return north twice to the Courtyard — win!

---

## Build & Run

```bash
dotnet build
dotnet run
```

Project requires:
```xml
<Nullable>enable</Nullable>
<ImplicitUsings>enable</ImplicitUsings>
<LangVersion>latest</LangVersion>
```

---

## Teaching Guidelines

- Each chapter's folder is self-contained — students study one folder at a time
- `GameRunner.Start()` in each chapter is the "wiring" method — best place to show how objects connect
- Phase 1 Room is the first class students read — keep it under 20 lines with no imports
- Comments should explain *why* something is done, not restate what the code already says
- Students compare Phase 1 vs Phase 2 gameplay to motivate learning `List<T>` and `foreach`
- Chapter 3's `WorldBuilder.cs` demonstrates the difference between hard-coding and a general mechanism
