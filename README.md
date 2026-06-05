# The Caravanserai — A C# Text Adventure

> **The Silk Road, 1271 AD.** You are a young Venetian merchant's apprentice, separated
> from Marco Polo's caravan at a fortified roadside inn. Explore the compound, recover your
> missing travel documents, and rejoin the caravan before it departs at dawn.

A small, fully working **text adventure game** built in C# / .NET. It is a **teaching project**
for an introductory/intermediate programming class, designed to demonstrate core
**object-oriented programming** concepts — classes, objects, fields, properties, constructors,
methods, collections, and inheritance — through code you can read end to end.

There is no combat. The challenge is exploration: find items, solve a simple puzzle, and unlock
your way to the goal.

---

## How This Project Is Organized (Read This First)

The code is split into **three chapters (phases)** that build on each other. When you run the
program, a menu lets you choose which chapter to play:

```
Choose a chapter:
  1 - Chapter 1: Rooms & Movement      (small, ~140 lines)
  2 - Chapter 2: Items & Inventory     (medium, ~275 lines)
  3 - Chapter 3: Full Adventure        (full architecture)
```

Each chapter is a **complete, runnable game** — just a little bigger and better organized than
the one before it. The idea is to study them **in order** and watch how the same program grows
from a few files into a clean, multi-folder design.

| Folder | Chapter | What it teaches |
|---|---|---|
| [`Phase1/`](Phase1/) | 1 — Rooms & Movement | Your first classes (`Room`, `Player`), a `Dictionary` of exits, and a basic game loop |
| [`Phase2/`](Phase2/) | 2 — Items & Inventory | Adding an `Item` class, a `List` of items, and picking things up |
| [`Phase3/`](Phase3/) | 3 — Full Adventure | A full design split across folders: parser, world builder, inheritance (`KeyItem : Item`), and separated UI |

---

## How to Study This as a Class Demo

You don't need to understand all 1,000 lines at once. Work through it like this:

1. **Play it first.** Run the program (see below), pick **Chapter 3**, and finish the game so
   you know what it *does* before reading how it works.
2. **Read Chapter 1.** Open every file in [`Phase1/`](Phase1/). It is small enough to read in
   full. Ask: *What is a class? What is an object? How does `Player` "know" which `Room` it is in?*
3. **Compare Chapter 2 to Chapter 1.** What was added to support items? Notice the new `Item`
   class and the `List<Item>` it lives in.
4. **Read Chapter 3 folder by folder.** This is the "real" architecture. Notice how each class
   has one job and lives in its own file:
   - `World/` — `Room`, `Exit`, and `WorldBuilder` (which *constructs and wires together* every room)
   - `Items/` — `Item` and `KeyItem` (an example of **inheritance**)
   - `Characters/` — `Player` and `Inventory`
   - `Engine/` — `Game` (the main loop), `Parser` (reads your commands), and `Actions`
   - `UI/` — `Display` (every line of console output lives here, in one place)
5. **Make a change.** Add a room, a new item, or a new command. (See *Suggested Exercises* below.)

> 💡 **Tip:** [`WorldBuilder.cs`](Phase3/World/WorldBuilder.cs) is the best place to *see* objects
> referencing each other. It creates the rooms and connects them — a concrete picture of how
> object instances link up.

---

## How to Build and Run

You need the **.NET SDK** installed. Check with:

```bash
dotnet --version
```

If that errors, install it from <https://dotnet.microsoft.com/download>.

Then, from the project folder:

```bash
dotnet run
```

Pick `1`, `2`, or `3` at the menu to launch that chapter.

> **Running in VS Code:** open the integrated terminal (`` Ctrl+` ``) and run `dotnet run`.
> Because this game reads keyboard input, the terminal works better than the F5 debugger for
> actually *playing* it.

---

## How to Play

Type short commands at the `>` prompt.

| Command | Aliases | What it does |
|---|---|---|
| `go <direction>` | `n`, `s`, `e`, `w` | Move to an adjacent room |
| `look` | `l` | Describe the current room |
| `examine <item>` | `x` | Show an item's description |
| `take <item>` | `get` | Pick up an item |
| `drop <item>` | | Drop an item in the current room |
| `inventory` | `i`, `inv` | List what you are carrying |
| `unlock <direction>` | | Unlock an exit using a key you carry |
| `help` | `?` | Show the command list |
| `quit` | `exit`, `q` | Leave the game |

### The Map (Chapter 3)

```
[ COURTYARD ]   <-- start and finish here
      | south
      v
[ TRADE HALL ]  <-- puzzle: find the hidden key
      | south
      v
[ MERCHANT'S VAULT ]  <-- grab the Polo Satchel, then return north to win
```

### Walkthrough (in case you get stuck)

1. From the **Courtyard**, `go south` into the Trade Hall.
2. `examine brick` — a loose brick reveals a hidden **Brass Key**.
3. `take brass key`.
4. `go south` — carrying the key unlocks the **Vault** door.
5. `take polo satchel`.
6. `go north` twice to return to the **Courtyard** with the satchel — you win!

---

## Key Concepts Illustrated

- **Classes & objects** — `Room`, `Player`, `Item` are blueprints; the game creates many objects from them.
- **Properties** — explicit `public string Name { get; set; }` instead of bare fields.
- **Constructors** — every class sets up its starting state when created.
- **Collections** — `Dictionary<string, Exit>` for exits, `List<Item>` for items and inventory.
- **Object references** — a `Player` holds a `Room`; a `Room` holds `Exit`s that point to other `Room`s.
- **Inheritance** — `KeyItem : Item` reuses `Item` and adds the ability to unlock a door.
- **Separation of concerns** — input parsing, game rules, and screen output each live in their own class.

---

## Suggested Exercises

Pick one and try it. Start in Chapter 1 if you're new — it's the smallest.

- **Easy:** Change a room's description, or add a new takeable item to a room.
- **Easy:** Add a new command alias (e.g. make `grab` work like `take`).
- **Medium:** Add a fourth room connected to the map (e.g. a *Stable* east of the Courtyard).
- **Medium:** Add a new `look`-style command that lists only the items in the current room.
- **Harder:** Add a second locked door with its own key, reusing the `KeyItem` pattern.
- **Harder:** Add a simple "score" or "moves taken" counter shown when you win.

---

## Project Layout

```
CSharpTextAdventure/
  CSharpTextAdventure.csproj
  Program.cs              -- Entry point: shows the chapter menu

  Phase1/                 -- Chapter 1: Rooms & Movement
  Phase2/                 -- Chapter 2: Items & Inventory
  Phase3/                 -- Chapter 3: Full Adventure
    Engine/   (Game, Parser, Actions)
    World/    (Room, Exit, WorldBuilder)
    Items/    (Item, KeyItem)
    Characters/ (Player, Inventory)
    UI/       (Display)
```

---

## For Instructors

- Each class is kept to roughly one screen so students can read it in full.
- The three phases let you pace a course: teach classes and a loop first, add collections and
  items next, then introduce multi-file architecture and inheritance.
- Comments explain *why*, not *what* — encourage students to read the code, not just the comments.
- Code clarity is favored over architectural sophistication on purpose.

Happy exploring. The caravan leaves at dawn. 🐫
