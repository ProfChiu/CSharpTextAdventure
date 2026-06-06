# Curriculum Guide (for Instructors)

This project is a five-chapter teaching adventure for an introductory/intermediate C# course.
Each chapter is a complete, runnable game that introduces **exactly one** new headline concept on
top of the previous one. Chapters are self-contained and playable in any order (no inventory
carry-over), so you can assign them out of sequence.

## Story arc — the Silk Road, 1271 AD

A young Venetian merchant's apprentice chases Marco Polo's caravan. The map advances
geographically with each chapter:

> **Inn → Mountain Pass → Oasis Town → Desert Fortress → Caravan Camp.**

The final chapter ends with a surprise: a goddess who guards the road and poses three riddles —
answered with words, never weapons.

## Concept ladder — what is genuinely NEW each chapter

| Ch. | Story leg | New concept | Watch for in code |
|---|---|---|---|
| 1 | The Caravanserai | Classes & objects + the game loop | `Room`/`Player` classes; `while` loop in `Game.Run` |
| 2 | The Mountain Pass | Collections — `List<Item>` | `List<Item>` on `Room` and `Player`; `take`/`drop`; a hard-coded locked door |
| 3 | The Oasis Bazaar | Richer items + an `Inventory` class (encapsulation) | `Item` gains `Description`/`CanPickUp`; `Inventory` wraps a private `List<Item>`; `examine` |
| 4 | The Desert Fortress | Inheritance + a `static Display` class | `KeyItem : Item` with `: base(...)`; locks check a *key type*; all output in `Display` |
| 5 | The Caravan & the Riddle | Full architecture + the riddle encounter | folders/namespaces; `Parser`+`Actions`; `Dictionary<string,Exit>`; `out` param; `Riddle` list |

## How to teach with it

- **Play first, then read.** Have students finish a chapter before opening its files.
- **Compare adjacent chapters** to motivate each new idea. The most productive comparisons:
  - Phase 2's hard-coded `room.Name == "Wind Ridge"` lock **vs.** Phase 4's generic
    `KeyItem`-by-type lock **vs.** Phase 5's data-driven `Exit` objects.
  - Phase 2's raw `List<Item>` **vs.** Phase 3's encapsulating `Inventory` class.
  - Console calls scattered through `Game` (Ch. 1–3) **vs.** the centralized `static Display` (Ch. 4–5).
- **The wiring methods** (`GameRunner.Start()`, or `WorldBuilder.Build()` in Chapter 5) are the best
  place to show objects referencing each other.
- **Comments explain _why_, not _what_** — push students to read the code itself.

## The Chapter 5 riddles (answer key)

The goddess poses all three back-to-back; a wrong answer gives a hint and lets the player retry.

1. *"I have cities, but no houses; mountains, but no trees; and water, but no fish."* → **map**
   (accepts: map, a map)
2. *"I am always coming but never arrive; the caravan waits for me, then departs before me."* →
   **dawn** (accepts: dawn, sunrise, the morning, morning)
3. *"The more of me you take, the more you leave behind."* → **footsteps**
   (accepts: footsteps, footstep, steps, footprints)

## Suggested assessment exercises

- **Easy:** change a room's description or ASCII art; add a takeable item.
- **Medium:** add a room to a chapter's map; add a fixture (`CanPickUp = false`) with an `examine` clue.
- **Harder:** add a second locked door + `KeyItem` (Ch. 4); add a fourth riddle or a synonym (Ch. 5).
