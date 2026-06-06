# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Purpose

This is a **teaching project** for an introductory/intermediate C# course. It demonstrates core OOP concepts through a small, fully working text adventure game. Code simplicity and clarity take priority over architectural sophistication. Each chapter introduces **exactly one headline concept** — do not smuggle in extra new syntax.

## Build & Run

```bash
dotnet build    # must produce 0 warnings / 0 errors
dotnet run      # shows chapter menu (currently chapters 1–3)
```

No test project exists. Validation is manual play-testing and a clean `dotnet build`.

## Project Structure & Architecture

Single `.csproj` targeting **.NET 8.0** with `Nullable`, `ImplicitUsings`, and `LangVersion latest` enabled.

The repo implements a **5-chapter curriculum** where students compare source code across phases to see each new concept. All chapters compile into one program; `Program.cs` shows the menu and dispatches to each phase's `GameRunner`.

### Current implementation state

| Ch. | Story | Folder | Status | Headline concept |
|-----|-------|--------|--------|-----------------|
| 1 | The Caravanserai | `Phase1/` | ✅ Done | Classes, objects, game loop |
| 2 | The Mountain Pass | `Phase2/` | ✅ Done (needs story rework per todo.md) | `List<Item>`, take/drop |
| 3 | The Oasis Bazaar | `Phase3/` | ⬜ Planned (new) | `Inventory` class, `CanPickUp`, `examine` |
| 4 | The Desert Fortress | `Phase4/` | ⬜ Planned (new) | `KeyItem : Item`, `static Display` |
| 5 | The Caravan & the Guardian's Riddle | `Phase5/` | ⬜ Planned (rename from current `Phase3/`) | Full architecture + goddess riddle |

**Important:** The current `Phase3/` folder contains the full-architecture code that will be renamed to `Phase5/` before new Phase 3 and Phase 4 are added. See `todo.md` for the detailed milestone plan.

### Phase architecture (concept ladder)

**Phase 1** (`Phase1/`) — flat folder, 3 files, no imports
- `Room` with `Name`, `Description`, `Art`, and `North?/South?/East?/West?` Room references
- `Player` with `CurrentRoom` only
- `Game` with `Run` / `HandleInput` / `TryMove` / `PrintRoom`

**Phase 2** (`Phase2/`) — flat folder, adds `Item` and `List<T>`
- Adds `List<Item>` to `Room` and `Player`
- Locked door check is intentionally hard-coded by room name (motivates later chapters)
- `Item` has `Name` only (simplest possible class)

**Phase 3** (planned new) — flat folder, single namespace
- Expands `Item` to `Name + Description + CanPickUp`
- New `Inventory` class wrapping `List<Item>` with `Add/Remove/Find/Display`
- `Player` holds an `Inventory` object, not a raw list
- New `examine` command

**Phase 4** (planned new) — flat folder, multiple files, no nested folders
- `KeyItem : Item` (inheritance, calls `: base(...)`, adds `UnlocksExitId`)
- `static Display` class — all `Console.Write` output centralised here
- Locked door checks for a `KeyItem` by type, not a hardcoded string
- **Deferred to Phase 5:** namespaces/folders, `Parser`/`Actions` split, `Dictionary<string,Exit>`, `out` params

**Phase 5** (current `Phase3/`, to be renamed) — full architecture with sub-folders
```
Phase5/
  GameRunner.cs
  Engine/   Game.cs  Parser.cs  Actions.cs
  World/    Room.cs  Exit.cs  WorldBuilder.cs
  Characters/  Player.cs  Inventory.cs
  Items/    Item.cs  KeyItem.cs
  UI/       Display.cs
```
- `Room.Exits` is `Dictionary<string, Exit>`; `Exit` carries `Destination`, `IsLocked`, `RequiredKey`
- `Parser.Parse` uses an `out quit` parameter; dispatches to static `Actions` methods
- `WorldBuilder` (static) constructs and wires the entire world
- `Display` (static) owns all console output
- Win condition: back in Courtyard carrying the Polo Satchel

> The `Phase5/Engine/` folder is named `Engine` (not `Game`) so the class `Game` never collides with its own namespace.

## Game Engine Flow (Phase 5 / current Phase 3)

1. `WorldBuilder.Build()` constructs rooms, wires exits, places items
2. `Game.Run()` loop: display room → read input → `Parser.Parse(input, player, out quit)` → `Actions.*` modifies state → check win/quit
3. Locked door: when a player moves into a locked exit, `Actions.Go` checks if inventory holds a `KeyItem` whose `UnlocksExitId` matches — auto-unlocks if so

## Teaching Conventions

- Each chapter folder is **self-contained** — students study one folder at a time, in any order. No inventory carries across chapters.
- `GameRunner.Start()` (or the equivalent wiring method) is the best place to show how objects connect.
- Phase 1 `Room.cs` is the first class students read — keep it short with zero imports.
- Comments explain *why*, not *what*.
- Compare adjacent chapters to motivate each new idea (e.g., Phase 2's hard-coded locked door → Phase 4's `KeyItem` check → Phase 5's data-driven `Exit`).
- Every `Room` has an `Art` property (short ASCII string) printed on entry. Students can replace art as a creative exercise.

## Locked Riddle Texts (Phase 5 goddess encounter)

Three riddles back-to-back; the game only advances on a correct answer (retry on wrong, no death):

1. *"I have cities, but no houses; mountains, but no trees; and water, but no fish. What am I?"* → **map** (accepted: `map`, `a map`)
2. *"I am always coming but never arrive; the caravan waits for me, then departs before me. What am I?"* → **dawn** (accepted: `dawn`, `sunrise`, `the morning`, `morning`)
3. *"The more of me you take, the more you leave behind. What am I?"* → **footsteps** (accepted: `footsteps`, `footstep`, `steps`, `footprints`)

A small `Riddle` class (question + `string[]` accepted answers) held in a list is the intended implementation — a natural example of iterating over objects.

## Setting & Story Arc

**Silk Road, 1271 AD** — a Venetian merchant's apprentice chasing Marco Polo's caravan. No combat; tension comes from exploration, items, and puzzles. Geographic ladder: **Inn → Mountain Pass → Oasis Town → Desert Fortress → Caravan Camp.** Phase 5 ends with a goddess who poses the riddles above, followed by a multi-line ASCII art closing scene reuniting the apprentice with Marco Polo.
