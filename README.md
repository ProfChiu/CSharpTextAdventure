# The Silk Road — A C# Text Adventure

> **The Silk Road, 1271 AD.** You are a young Venetian merchant's apprentice racing to rejoin
> Marco Polo's caravan. Your journey runs from a roadside inn, over a mountain pass, through an
> oasis town and a desert fortress, to the caravan itself — where a surprise waits on the last
> stretch of road.

A small, fully working **text adventure game** built in C# / .NET. It is a **teaching project**
for an introductory/intermediate programming class, designed to demonstrate core
**object-oriented programming** concepts — classes, objects, fields, properties, constructors,
methods, collections, encapsulation, and inheritance — through code you can read end to end.

There is **no combat**. The challenge is exploration: find items, solve small puzzles, unlock
your way forward — and, at the very end, answer a riddle with words rather than weapons.

---

## The Journey (Five Chapters)

The game is split into **five chapters**. Each one is a **complete, runnable game** that adds
**exactly one new programming idea** on top of the last. The story advances geographically with
each chapter, but every chapter is **self-contained and playable in any order** — there is no
inventory carry-over, so you can jump straight into whichever one you're studying.

| Ch. | Story leg | Folder | Headline concept (what's NEW) |
|---|---|---|---|
| 1 | The Caravanserai | [`Phase1/`](Phase1/) | Classes & objects + the game loop |
| 2 | The Mountain Pass | [`Phase2/`](Phase2/) | Collections — `List<Item>`, take/drop |
| 3 | The Oasis Bazaar | [`Phase3/`](Phase3/) | Richer items + an `Inventory` class (encapsulation), `examine` |
| 4 | The Desert Fortress | [`Phase4/`](Phase4/) | Inheritance (`KeyItem : Item`) + a `static Display` class |
| 5 | The Caravan & the Guardian's Riddle | [`Phase5/`](Phase5/) | Full architecture + the goddess riddle + end-game scene |

When you run the program, a menu lets you choose a chapter:

```
  1 - The Caravanserai          classes & objects + the game loop
  2 - The Mountain Pass         collections: List<Item>, take/drop
  3 - The Oasis Bazaar          richer items + an Inventory class, examine
  4 - The Desert Fortress       inheritance (KeyItem) + static Display
  5 - The Caravan & the Riddle  full architecture + the guardian's riddle
```

---

## How to Study This as a Class Demo

The whole point is to **compare adjacent chapters** and see what each new idea buys you.

1. **Play a chapter first** so you know what it *does* before reading how it works.
2. **Read Chapter 1** ([`Phase1/`](Phase1/)) in full — it's the smallest. Ask: *What is a class?
   What is an object? How does `Player` "know" which `Room` it is in?*
3. **Compare each chapter to the one before it:**
   - **2 vs 1** — a `List<Item>` appears so rooms and the player can hold things (`take`/`drop`).
   - **3 vs 2** — the raw list is hidden inside an `Inventory` class (encapsulation); items grow a
     `Description` and a `CanPickUp` flag, and a new `examine` command reads them.
   - **4 vs 3** — `KeyItem : Item` introduces **inheritance**, and every line of console output
     moves into one `static Display` class. Locked doors now check for a *key type*, not a
     hard-coded room name.
   - **5 vs 4** — the full professional layout: folders & namespaces, a `Parser` + `Actions`
     split, a `Dictionary<string,Exit>` of `Exit` objects that carry their own lock state, an
     `out` parameter — plus the climactic **riddle encounter** and **end-game scene**.
4. **Make a change.** Add a room, an item, or a command. (See *Suggested Exercises* below.)

> 💡 **Tip:** Each chapter's `GameRunner.Start()` (or [`WorldBuilder.cs`](Phase5/World/WorldBuilder.cs)
> in Chapter 5) is the "wiring" code — the best place to *see* objects referencing each other as
> the world is built and connected.

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

Pick `1`–`5` at the menu to launch that chapter.

> **Running in VS Code:** open the integrated terminal (`` Ctrl+` ``) and run `dotnet run`.
> Because this game reads keyboard input, the terminal works better than the F5 debugger for
> actually *playing* it.

---

## How to Play

Type short commands at the `>` prompt. Chapter 5 supports the full set below; earlier chapters
support a subset (always `go`/`take`/`drop`/`inventory`/`look`/`quit`, with `examine` added from
Chapter 3 on).

| Command | Aliases | What it does |
|---|---|---|
| `go <direction>` | `n`, `s`, `e`, `w` | Move to an adjacent room |
| `look` | `l` | Describe the current room |
| `examine <item>` | `x` | Show an item's description |
| `take <item>` | `get` | Pick up an item |
| `drop <item>` | | Drop an item in the current room |
| `inventory` | `i`, `inv` | List what you are carrying |
| `unlock <direction>` | | Unlock an exit using a key you carry (Chapter 5) |
| `help` | `?` | Show the command list (Chapter 5) |
| `quit` | `exit`, `q` | Leave the game |

### Where each chapter ends (if you get stuck)

- **1 — The Caravanserai:** the simplest chapter — just walk between the three inn rooms with
  `go north`/`go south`. It teaches rooms and movement, so there is no item to collect yet.
- **2 — The Mountain Pass:** carry the **Fur Cloak** *and* the **Rope**, then cross from the
  Wind Ridge into the Hidden Pass. (The cloak hides in the Ice Cave to the east.)
- **3 — The Oasis Bazaar:** bring a **Bolt of Silk** and a **Silver Coin** to the Guide's House.
- **4 — The Desert Fortress:** collect the **Iron Gate Key**, **Customs Seal**, and **Captain's
  Pass** from the side rooms to open the spine and exit the Sally Port.
- **5 — The Caravan & the Riddle:** use the **Tent Key** and **Shrine Token** to reach the Hidden
  Shrine, then answer the goddess's three riddles to pass to the Caravan Heart. Wrong answers just
  give a hint and let you try again — there is no losing.

---

## Key Concepts Illustrated

- **Classes & objects** — `Room`, `Player`, `Item` are blueprints; the game creates many objects from them.
- **Properties & constructors** — each class declares its data and sets up its starting state.
- **Collections** — `List<Item>` for items and inventory; `Dictionary<string, Exit>` for exits (Chapter 5).
- **Object references** — a `Player` holds a `Room`; a `Room` holds exits that point to other `Room`s.
- **Encapsulation** — the `Inventory` class hides its `List<Item>` and exposes `Add`/`Remove`/`Find`/`Display`.
- **Inheritance** — `KeyItem : Item` reuses `Item` and adds the ability to unlock a door.
- **Separation of concerns** — input parsing, game rules, and screen output each live in their own class.
- **Iterating over objects** — Chapter 5's goddess steps through a `List<Riddle>`.

---

## Suggested Exercises

Pick one and try it. Start in Chapter 1 if you're new — it's the smallest.

- **Easy:** Change a room's description or ASCII art, or add a new takeable item to a room.
- **Easy:** Add a new command alias (e.g. make `grab` work like `take`).
- **Medium:** Add a new room connected to a chapter's map.
- **Medium (Ch. 3+):** Add a fixture with `CanPickUp = false` and an `examine` clue.
- **Harder (Ch. 4+):** Add a second locked door with its own `KeyItem`, reusing the pattern.
- **Harder (Ch. 5):** Add a fourth riddle to the goddess, or a new accepted synonym to an existing one.

---

## Project Layout

```
CSharpTextAdventure/
  CSharpTextAdventure.csproj
  Program.cs              -- Entry point: shows the chapter menu

  Phase1/                 -- Ch. 1: The Caravanserai      (Room, Player, Game)
  Phase2/                 -- Ch. 2: The Mountain Pass      (+ Item, List inventory)
  Phase3/                 -- Ch. 3: The Oasis Bazaar       (+ Inventory class, examine)
  Phase4/                 -- Ch. 4: The Desert Fortress    (+ KeyItem, static Display)
  Phase5/                 -- Ch. 5: The Caravan & the Riddle (full architecture)
    Engine/     (Game, Parser, Actions, Riddle, RiddleEncounter)
    World/      (Room, Exit, WorldBuilder)
    Items/      (Item, KeyItem)
    Characters/ (Player, Inventory)
    UI/         (Display)
```

---

## For Instructors

- Each class is kept to roughly one screen so students can read it in full.
- The five chapters let you pace a course one idea at a time: classes & a loop → collections →
  encapsulation → inheritance & separation of concerns → full architecture.
- Each chapter is self-contained and playable in any order, so you can assign them out of sequence.
- Comments explain *why*, not *what* — encourage students to read the code, not just the comments.
- Code clarity is favored over architectural sophistication on purpose.

Happy exploring. The caravan leaves at dawn. 🐫
