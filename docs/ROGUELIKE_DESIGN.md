# Phase 6–10 Roguelike Design Document

> **Status:** Design only. No code yet. Review and approve before building.

---

## Story Context

Phases 6–10 continue directly from Phase 5. After reuniting with Marco Polo, the apprentice
learns the caravan camp sits above a **buried Silk Road ruin** — a vast underground complex of
ancient trading halls, tombs, and djinn vaults sealed for centuries. The caravan cannot depart
until a relic hidden in the deepest vault is recovered. The apprentice descends alone.

Setting, tone, and characters are the same as Phases 1–5.
All player characters are **human** (no race selection).
Monsters are invented Silk Road creatures (see roster below).
Death restarts the player at the **dungeon entrance door** — no permadeath.
Win condition: reach the deepest vault, solve the goddess's three riddles, retrieve the relic.

---

## Screen Layout

The display is split vertically. The stat panel occupies the left ~20 columns; the dungeon map
fills the remaining right portion. This matches the Angband/Moria style.

```
====================|============================================================
 Tariq (apprentice) |  ########################################
 Depth   : 1-2      |  #......................#...............#
 Level   : 1        |  #.............@........+...............#
 XP      : 40       |  #......................#...............#
 HP      : 18/18    |  ###########+############...............#
 AC      : 10       |             |          #################
 ATK     : 5        |  ###########            ################
 Gold    : 12       |  #.........#            #..............#
                    |  #....j....#            #..............>
 [ Equipped ]       |  #.........#            ################
 Weapon: Scimitar   |  ###########
 Armour: Silk Robe  |
====================|============================================================
 You see: a Sand Jackal.                         [ Press ? for help ]
```

- `@` — the player
- `#` — wall
- `.` — floor (walkable)
- `+` — door (walkable, blocks sight until opened)
- `>` — exit to next scene
- `<` — exit back to previous scene (or dungeon entrance)
- Monster glyphs: see roster below

The bottom status bar shows the most recent message (combat result, item found, etc.).

---

## Scene System

The dungeon is divided into **scenes**. A scene is one screenful of rooms. When the player
steps on an exit tile `>`, the current scene is discarded and a new scene loads.

### Why scenes (not one big open dungeon)

- Keeps the map visible without scrolling — fits the teaching context
- Makes procedural generation simpler: generate one scene at a time
- Natural "chapter" feel that mirrors Phases 1–5's room structure
- Each scene load is a clear save/checkpoint moment

### Scene dimensions

- **Map area:** 60 columns × 22 rows of tiles
- **Stat panel:** 20 columns × 24 rows (left side)
- Total console width needed: 80 columns (standard terminal default)

### Rooms per scene

Each scene contains **3–5 rooms**. Room sizes vary (minimum 3×3, maximum 10×8).
Rooms are connected by single-tile-wide corridors. Doors `+` appear at corridor/room junctions.

### Scene progression

```
DUNGEON ENTRANCE (fixed, always the same)
        |
      [ Scene 1-1 ]  3 rooms, 1-2 monsters, 1 item
        |  >
      [ Scene 1-2 ]  4 rooms, 2-3 monsters, 1-2 items
        |  >
      [ Scene 1-3 ]  5 rooms, 3-4 monsters, 2 items   ← depth 1 complete
        |  >
      [ Scene 2-1 ]  harder monsters, more items ...
        ...
      [ Scene 5-3 ]  FINAL VAULT → goddess encounter → win
```

Depth label format: `FLOOR - SCENE` (e.g. `2-1` = floor 2, scene 1).
Each floor has **3 scenes**. There are **5 floors**. Total: 15 scenes to reach the end.

The `<` tile on scene 1-1 leads back to the dungeon entrance (surface). All other `<` tiles
lead back one scene within the same floor.

### What changes between floors

| Floor | Atmosphere | New monster types | Hazard |
|---|---|---|---|
| 1 | Ruined trading halls, oil lamps | Sand Jackal, Silk Phantom | none |
| 2 | Collapsed treasury vaults | Tomb Guardian, Brass Serpent | crumbling floors (cosmetic) |
| 3 | Underground river passage | Desert Wraith, River Shade | darkness (narrow FOV) |
| 4 | Djinn workshop | Djinn, Animated Armour | doors lock randomly |
| 5 | The Goddess's Vault | Caravan Revenant, all previous | riddle gate |

---

## Room-to-Room Movement

Movement is **tile-by-tile** and **turn-based**.

- Player presses a direction key → player moves one tile → all monsters take one step
- Bump into a monster tile → attack that monster (no separate attack command)
- Bump into a closed door `+` → door opens (costs one turn)
- Step onto `>` → scene transition (new scene loads, player placed at matching `<`)
- Step onto `<` → previous scene loads (player placed at matching `>`)

### Direction keys

```
  y  k  u        7  8  9
  h     l   or   4     6    (numpad)
  b  j  n        1  2  3
```

Cardinal + diagonal movement (8 directions). For simplicity, Phase 6 starts with
4-direction only (WASD / arrow keys); diagonals are added in Phase 10.

---

## Stat Panel Details

```
[ Character ]
 Name    : Tariq
 Depth   : 2-1
 Level   : 3
 XP      : 210 / 400

[ Vitals ]
 HP      : 24 / 30
 AC      : 14

[ Combat ]
 ATK     : 8
 DEF     : 4

[ Wealth ]
 Gold    : 47

[ Equipped ]
 Weapon  : Iron Scimitar (+3)
 Armour  : Silk Robe (+2)
```

Stats are recalculated each turn from base values + equipment bonuses.

---

## Combat

Bump-attack (step onto monster tile).

```
Roll to hit:  1d20 + ATK  vs  monster DEF
  hit  → damage = ATK - monster.DEF/2 (min 1)
  miss → "Your blow glances off."
```

Monsters attack back at end of their turn using same formula with their stats.

Death: HP reaches 0 → message → player teleports back to dungeon entrance with full HP.
      Level and XP are kept. Gold is lost (dropped on the floor of the scene where they died,
      scene is reset so it cannot be recovered).

---

## Monster Roster

| Name | Glyph | Floor | HP | ATK | DEF | Behaviour | Flavour |
|---|---|---|---|---|---|---|---|
| Sand Jackal | `j` | 1 | 8 | 4 | 1 | Chase when adjacent | Skeletal dogs that hunt in pairs |
| Silk Phantom | `p` | 1–2 | 6 | 6 | 0 | Phases through doors | Translucent figure in ancient cloth |
| Tomb Guardian | `G` | 2 | 20 | 8 | 6 | Patrols a fixed path | Stone-armed sentinel |
| Brass Serpent | `s` | 2–3 | 10 | 7 | 2 | Chase + poison (skip turn) | Animated metal snake |
| Desert Wraith | `W` | 3 | 12 | 10 | 3 | Drains ATK on hit | Smoke and claw |
| River Shade | `r` | 3 | 8 | 5 | 1 | Invisible until adjacent | Reflection of a drowned merchant |
| Djinn | `D` | 4 | 18 | 12 | 4 | Teleports when struck | Capricious fire spirit |
| Animated Armour | `A` | 4 | 25 | 9 | 8 | Slow, heavy | Empty suit that still fights |
| Caravan Revenant | `R` | 5 | 22 | 14 | 5 | Chase + drops loot on death | Merchant who never left |

---

## Item System

Items are found in rooms (on floor tiles) or dropped by monsters.

| Category | Examples | Effect |
|---|---|---|
| Weapon | Iron Scimitar, Bone Dagger, Merchant's Staff | + ATK |
| Armour | Silk Robe, Leather Vest, Bronze Cuirass | + AC / DEF |
| Consumable | Healing Draught, Oil Flask, Antidote | restore HP / throw fire / cure poison |
| Valuable | Gold Coin, Silver Ring, Jade Bead | adds Gold |
| Key item | Djinn Seal, Vault Token | unlocks a specific locked exit |

Equipment slots: **Weapon** (one) and **Armour** (one). Equipping a new item replaces the old one
(old item drops to floor). Consumables are used from inventory with `u` and are then removed.

---

## Win Condition

Floor 5, Scene 3 contains the **Goddess's Vault** — a large chamber with no monsters.
Entering it triggers the same goddess encounter scripted in Phase 5:

1. ASCII art reveal of the goddess
2. Three riddles in sequence (same text as Phase 5; answers: **map**, **dawn**, **footsteps**)
3. All three answered correctly → she steps aside
4. Player enters the **Relic Chamber** (one more room) and picks up the **Caravan Relic**
5. Endgame scene: surface, caravan departs, closing text + ASCII art

Wrong riddle answer → gentle hint + retry same riddle (no penalty, no death).

---

## Phase-by-Phase Feature Additions

| Phase | Feature added | C# concept introduced |
|---|---|---|
| 6 | Dungeon map renders; player moves through hardcoded scenes | `Tile[,]` 2D array, `Position` record struct |
| 7 | Monsters appear; bump-to-attack; turn loop | Abstract `Entity` base class, virtual methods |
| 8 | Items spawn; pick up, equip, use | `IUsable` interface, `is` / `as` type checks |
| 9 | Scenes generated procedurally; multiple floors; depth scaling | `Random`, BSP room-placement algorithm |
| 10 | FOV lighting; XP/levelling; full stat panel; win condition | FOV radius, LINQ over game state, polish |

Each phase's `GameRunner.Start()` is the entry point. All phases compile in the same project.
The main menu (`Program.cs`) will list chapters 1–10 when all phases are complete.
