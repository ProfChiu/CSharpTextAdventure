# A text adventure game made with C# dot net.

## Purpose

This is a **teaching project** for an introductory/intermediate C# course. The goal is to demonstrate core object-oriented programming concepts through a small, fully working game that students can play and read side by side. Code simplicity and clarity take priority over architectural sophistication.

---

## Setting & Premise

**The Silk Road, 1271 AD**

The player is a young Venetian merchant's apprentice traveling to rejoin Marco Polo's caravan. The adventure unfolds across five chapters that follow the journey geographically — from a roadside inn, over a mountain pass, through an oasis town and a desert fortress, to the caravan itself.

There is **no combat**. Tension comes from exploration, finding items, solving small puzzles, and unlocking areas. The final chapter ends with a surprise: a beautiful goddess who guards the road and poses a **riddle** — answered with words, never weapons.

Each chapter is **self-contained and playable in any order**. There is **no inventory carry-over** between chapters; every scenario starts fresh so students can jump into whichever one they're studying.

---

## Phased Learning Structure

The project has **five chapters** that all live in the same repo and compile together as one program. At startup the player picks a chapter. Students compare source code across chapters to see exactly what each new concept added.

```
dotnet run
→ Choose a chapter: 1, 2, 3, 4, or 5
```

Each chapter introduces **one headline concept** on top of the previous one, so new ideas arrive a single step at a time.

| Ch. | Story leg | Headline concept (what's NEW) |
|---|---|---|
| 1 | The Caravanserai | Classes & objects + the game loop |
| 2 | The Mountain Pass | Collections — `List<Item>`, take/drop |
| 3 | The Oasis Bazaar | Richer objects + an `Inventory` class (encapsulation) |
| 4 | The Desert Fortress | Inheritance (`KeyItem : Item`) + a `static Display` class |
| 5 | The Caravan & the Guardian's Riddle | Full architecture + goddess riddle + end-game scene |

### Chapter 1 — The Caravanserai (`Phase1/`)

Unchanged from the original project. Stranded at the inn, the apprentice recovers the Polo Satchel.

**Concepts:** classes, properties, constructors, object references (`Room?` holding a `Room`), `while` loop, `if/else`, null checks, method decomposition.

```
Phase1/
  Room.cs      — Name, Description, Art, North/South/East/West? properties
  Player.cs    — CurrentRoom property only
  Game.cs      — GameRunner.Start() wires the world; Game has Run/HandleInput/TryMove/PrintRoom
```

### Chapter 2 — The Mountain Pass (`Phase2/`)

The journey continues: satchel in hand, the apprentice rides for the caravan, but a storm seals the pass. Explore the trail and gather supplies to cross.

**Concepts:** `List<T>`, `foreach`, a `bool` flag inside a loop, conditionals guarding state transitions, a win condition.

```
Phase2/   (flat folder, single namespace, if/else input, North/South/East/West fields)
  Room.cs      — Phase 1 Room + List<Item> Items
  Player.cs    — Phase 1 Player + List<Item> Inventory
  Item.cs      — Name only (simplest class)
  Game.cs      — Phase 1 Game + TakeItem/DropItem/ShowInventory + a hard-coded locked door
```

The locked-door check is intentionally hard-coded by room name. *"What if there were 20 locked doors?"* motivates later chapters.

### Chapter 3 — The Oasis Bazaar (`Phase3/`)

The apprentice reaches a Silk Road town and must barter to hire a desert guide.

**Concepts:** a class with several properties; an `Inventory` class that wraps a collection and exposes methods (encapsulation); the `examine` command; `CanPickUp` gating what `take` accepts.

```
Phase3/   (flat folder, single namespace, if/else input, North/South/East/West fields)
  Room.cs      — same shape as Phase 2
  Item.cs      — EXPANDED: Name, Description, CanPickUp
  Player.cs    — holds an Inventory object (not a raw List)
  Inventory.cs — NEW: wraps List<Item> with Add / Remove / Find / Display
  Game.cs      — adds the examine command
```

**Deferred to later chapters:** inheritance, `Dictionary`, namespaces/folders, a separate parser, `out` params.

### Chapter 4 — The Desert Fortress (`Phase4/`)

A customs fortress bars the route; the apprentice must find passes and keys to get through.

**Concepts:** **inheritance** (`KeyItem : Item` with `: base(...)`); **separation of concerns** via a centralized **`static Display`** class.

```
Phase4/   (flat folder, single namespace, MULTIPLE files but no nested folders)
  Room.cs, Player.cs, Inventory.cs   — carried over from Phase 3
  Item.cs                            — Name, Description, CanPickUp
  KeyItem.cs   — NEW: KeyItem : Item, adds UnlocksExitId, calls : base(...)
  Display.cs   — NEW: static class; all Console output moves here
  Game.cs      — locked doors check for a KeyItem by type, not a hard-coded name
```

**Deferred to Chapter 5:** namespaces/folders, `Parser`/`Actions` split, `Dictionary<string,Exit>` + `Exit` objects, `out` params.

### Chapter 5 — The Caravan & the Guardian's Riddle (`Phase5/`)

The apprentice finally reaches the caravan's camp at a moonlit oasis — but the last path is barred by a surprise: a radiant **goddess**, guardian of the road, who poses **three riddles in a row**. Answer all three correctly to be allowed through and reunite with Marco Polo in a closing scene.

**Concepts:** the full professional layout — namespaces & folders, a `Parser` + `Actions` split, `Dictionary<string,Exit>` with `Exit` objects that carry their own lock state, an `out` parameter, plus a scripted **riddle encounter** and **end-game scene**.

```
Phase5/   (full architecture — folders & namespaces)
  GameRunner.cs
  Engine/
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

**Final encounter (no combat):** entering the Hidden Shrine reveals the goddess (ASCII art). She poses **three riddles back-to-back**; the game reads the player's typed answer to each (case-insensitive, a few accepted synonyms). A wrong answer gives a gentle hint and lets the player retry that riddle — there is no death or loss; the game only advances on a correct answer. **Solving all three** opens the way to the **Caravan Heart**, which prints the multi-line end-game scene. A small `Riddle` class (question + accepted answers) held in a list is a natural way to teach iterating over objects.

> Note: the `Phase5` namespace folder is named `Engine/` (not `Game/`) so a class named `Game` never collides with its own namespace.

---

## ASCII Art

Every `Room` has an `Art` property (string) printed before the room description on entry. Art is a short ASCII scene representing a prop or landmark. The goddess reveal and the end-game scene use larger ASCII art. Students can replace any art as a creative exercise.

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

### Chapter 6 — The Dungeon Map (`Phase6/`)

The apprentice descends into buried Silk Road ruins beneath the caravan camp.

**Concepts:** `Tile[,]` 2D arrays, `Position` record struct, console render loop, scene-based map loading.

```
Phase6/   (flat folder, single namespace)
  GameRunner.cs
  Tile.cs       — enum: Floor, Wall, Door, ExitDown, ExitUp
  Position.cs   — record struct (X, Y)
  Scene.cs      — holds Tile[,] grid + player start position
  Player.cs     — Position, HP, basic stats
  Renderer.cs   — draws stat panel + dungeon map to console
  Game.cs       — input loop, move player, scene transition
```

One hardcoded dungeon scene (3–5 rooms). Player `@` moves with arrow/WASD keys. Stat panel on
the left (Angband-style). No monsters or items yet. Stepping on `>` loads the next hardcoded
scene. See `docs/ROGUELIKE_DESIGN.md` for the full map layout and scene system.

### Chapter 7 — Monsters & Combat (`Phase7/`)

**Concepts:** Abstract `Entity` base class, `virtual` methods, turn-based game loop.

- Monsters with AI (wander → chase when player is in sight)
- Bump into monster tile to attack (dice roll: ATK vs DEF)
- HP system; death restarts player at dungeon entrance with full HP
- Monster roster: Sand Jackal, Silk Phantom (floors 1–2)

### Chapter 8 — Items & Equipment (`Phase8/`)

**Concepts:** `IUsable` interface, `is` / `as` type checks, equipment slots.

- Weapons (+ATK), Armour (+AC/DEF), Consumables (Healing Draught, Oil Flask)
- Pick up `g`, equip `e`, use `u` — one weapon slot, one armour slot
- Items spawn in rooms and drop from monsters

### Chapter 9 — Procedural Dungeons (`Phase9/`)

**Concepts:** `Random` class, BSP room-placement algorithm, depth scaling.

- Scenes generated at runtime (random room count, size, and placement)
- 5 floors × 3 scenes each = 15 scenes to reach the final vault
- Monsters and items scale with depth (see `docs/ROGUELIKE_DESIGN.md` floor table)

### Chapter 10 — The Full Roguelike (`Phase10/`)

**Concepts:** FOV radius algorithm, XP/levelling, LINQ over game state, full win condition.

- Field of view: only tiles within lantern radius are visible
- XP and level-up system (stat boosts on level)
- Full Angband-style stat panel (HP, AC, ATK, DEF, XP, Gold, Depth)
- Win: floor 5 scene 3 → Goddess's Vault → three riddles → Relic Chamber → endgame scene
- Death: respawn at entrance, keep level/XP, lose gold

> Design details for Chapters 6–10 (scene layout, screen format, monster roster, item list,
> win path) are in `docs/ROGUELIKE_DESIGN.md`.

---

## Teaching Guidelines

- Each chapter's folder is self-contained — students study one folder at a time, in any order.
- `GameRunner.Start()` in each chapter is the "wiring" method — the best place to show how objects connect.
- Phase 1 `Room` is the first class students read — keep it short with no imports.
- Each chapter adds exactly **one** headline concept; don't smuggle in extra new syntax.
- Comments explain *why*, not *what*.
- Compare adjacent chapters to motivate each new idea (e.g. Phase 2's hard-coded locked door vs. Phase 4's `KeyItem`, vs. Phase 5's data-driven `Exit`).
- The goddess riddle in Chapter 5 is resolved with words, never combat — keep it gentle and retry-friendly.
