# Development Progress — Paused

> Last updated: 2026-06-13
> Resume point documented below. Do not build until this file is reviewed.

---

## What Exists Right Now (Implemented & Compiling)

| Folder | Chapter | Status |
|---|---|---|
| `Phase1/` | Ch 1 — The Caravanserai | ✅ Complete |
| `Phase2/` | Ch 2 — The Mountain Pass | ✅ Complete (3-room version; rework planned) |
| `Phase3/` | Ch 3 — Full Adventure (full architecture) | ✅ Complete |

`Program.cs` currently presents a 3-chapter menu (`1`, `2`, `3`).
`dotnet build` is clean at 0 warnings / 0 errors.

---

## What Is Planned But Not Yet Built

### Phases 1–5 Curriculum (Silk Road story, text adventure)

Fully designed in `todo.md`. Build was approved (`todo.md` bottom: "Build approved — GO").
**No milestone has been started.**

| Milestone | Task | Status |
|---|---|---|
| 0 | `git mv Phase3/ Phase5/` + rename all `Phase3.*` namespaces → `Phase5.*` | ⬜ Not started |
| 1 | Rework `Phase2/` — expand from 3 rooms to 6 (Mountain Pass story) | ⬜ Not started |
| 2 | Create new `Phase3/` — The Oasis Bazaar (Inventory class, examine, CanPickUp) | ⬜ Not started |
| 3 | Create new `Phase4/` — The Desert Fortress (KeyItem inheritance, static Display) | ⬜ Not started |
| 4 | Expand `Phase5/` — more rooms + goddess riddle encounter + endgame scene | ⬜ Not started |
| 5 | Update `Program.cs` menu for 5 chapters + update `README.md` | ⬜ Not started |

### Phases 6–10 Curriculum (Roguelike, same Silk Road setting)

Designed in `docs/ROGUELIKE_DESIGN.md`. **Design only — not approved for build yet.**

| Phase | Chapter | Status |
|---|---|---|
| 6 | The Dungeon Map (2D arrays, scene system, renderer) | 📝 Designed |
| 7 | Monsters & Combat (Entity hierarchy, turn loop) | 📝 Designed |
| 8 | Items & Equipment (IUsable interface, equipment slots) | 📝 Designed |
| 9 | Procedural Dungeons (Random, BSP generator, depth scaling) | 📝 Designed |
| 10 | Full Roguelike (FOV, XP/levelling, win condition, death/respawn) | 📝 Designed |

---

## Key Decisions Already Locked

### Phases 1–5 (from `todo.md`)
- Current `Phase3/` becomes `Phase5/` (rename first, build second)
- Phase 1 is untouched
- Phases 2–5 are self-contained, playable in any order, no inventory carry-over
- Phase 5 ends with goddess + 3 riddles (answers: **map**, **dawn**, **footsteps**)
- No combat in any of Phases 1–5

### Phases 6–10 (from `docs/ROGUELIKE_DESIGN.md`)
- Setting: buried Silk Road ruins beneath the Phase 5 caravan camp
- Player is always human (Venetian apprentice, name: Tariq)
- Scene system: 3–5 rooms per scene, 5 floors × 3 scenes = 15 scenes total
- Screen: Angband-style left stat panel (20 cols) + right dungeon map (60 cols)
- Death: respawn at dungeon entrance, keep level/XP, lose gold
- Win: floor 5 scene 3 → Goddess's Vault → same 3 riddles → Relic Chamber → endgame
- 9 monsters designed (Sand Jackal, Silk Phantom, Tomb Guardian, Brass Serpent, Desert Wraith, River Shade, Djinn, Animated Armour, Caravan Revenant)
- No permadeath, no race selection, no carry-over between phases

---

## Where to Resume

### Option A — Resume Phases 1–5 first (recommended order)
1. Start with **Milestone 0**: `git mv Phase3/ Phase5/` and namespace rename
2. Verify `dotnet build` still clean
3. Proceed through Milestones 1–5 in order (each is self-contained)

### Option B — Skip to Phases 6–10 (roguelike)
1. Review `docs/ROGUELIKE_DESIGN.md` — get explicit approval to build
2. Start with **Phase 6** only (dungeon map, no monsters)
3. Phases 6–10 can be built independently of whether 1–5 are complete

---

## Open Questions (not yet decided)

- [ ] Should Phase 2 rework keep `Environment.Exit(0)` for win or match Phase 3's loop-break pattern?
- [ ] Phase 5 room count: the plan says "varies" — exact rooms not yet specified
- [ ] Phase 10 FOV: simple radius (easier to teach) or proper raycasting (more realistic)?
- [ ] Roguelike stat panel: fixed character name "Tariq" or let player type a name at start?
- [ ] Phase 10 win: after goddess riddles, should the Caravan Relic be a physical item to pick up, or just a cutscene?

---

## Reference Documents

| File | Contents |
|---|---|
| `todo.md` | Full 5-chapter curriculum plan with per-milestone task lists |
| `Claude.md` | Architecture reference + teaching guidelines for all 10 chapters |
| `docs/ROGUELIKE_DESIGN.md` | Full roguelike design: scenes, layout, monsters, items, combat, win path |
| `docs/PROGRESS.md` | This file |

---

## Active Branch

`claude/phase-count-WgQFU` — PR #7 open (draft) at:
https://github.com/ProfChiu/CSharpTextAdventure/pull/7

PR contains only the design documents (Claude.md update + ROGUELIKE_DESIGN.md).
No production code has been changed on this branch.
