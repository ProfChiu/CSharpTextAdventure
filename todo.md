# TODO — 5-Chapter Curriculum & Progressive Story

> Plan only. **Do not build until approved.** Each chapter stays runnable from the
> `Program.cs` menu, builds into the one project, and must keep `dotnet build` at 0/0.

## Locked decisions
- [x] Current Phase 3 (full architecture) is renamed to **Phase 5**.
- [x] Two new bridge chapters, **Phase 3** and **Phase 4**, sit between Phase 2 and Phase 5.
- [x] `Display` in **Phase 4 is a `static` class** (matches Phase 5).
- [x] **Phase 1 is unchanged.** Phases **2–5 continue the Silk Road journey**, but each chapter is **fully self-contained and playable in any order** (no required sequence).
- [x] **No inventory carry-over between chapters.** Every chapter starts fresh so students can jump into any scenario.
- [x] **Room counts vary** — size each map to fit its story (no fixed number).
- [x] **Phase 5 ends with a surprise encounter and an end-game scene:** a **beautiful goddess** who poses a **riddle**. **No combat** — win by answering correctly.
- [x] All phases share the same world *style/premise*; the map **advances geographically** each chapter (no repeated rooms).

---

## Story arc (Silk Road, 1271 AD — a Venetian apprentice chasing Marco Polo's caravan)

| Ch. | Title | Where the story goes | Rooms | Headline tech concept |
|---|---|---|---|---|
| 1 | The Caravanserai *(unchanged)* | Stranded at the inn; recover the Polo Satchel | 3 | Classes & objects + game loop |
| 2 | The Mountain Pass | Ride out to catch the caravan; a storm blocks the pass | 6 | Collections — `List` items, take/drop |
| 3 | The Oasis Bazaar | Reach a Silk Road town; barter to hire a guide | 7 | Richer objects + `Inventory` class (encapsulation) |
| 4 | The Desert Fortress | A customs fortress bars the route; find passes & keys | 8 | Inheritance (`KeyItem : Item`) + `static Display` |
| 5 | The Caravan & the Guardian's Riddle | Catch the caravan; a goddess bars the way with a riddle; reunite | varies | Full architecture + **goddess riddle + end scene** |

Geographic ladder: **Inn → Mountain Pass → Oasis Town → Desert Fortress → Caravan Camp.**
(Each chapter stands alone — no carried items, playable in any order.)

---

## Milestone 0 — Rename current Phase 3 → Phase 5
- [x] `git mv Phase3/ Phase5/` (preserve history).
- [x] Rename namespaces `Phase3.*` → `Phase5.*` (`Engine`, `World`, `Items`, `Characters`, `UI`).
- [x] Update every `using Phase3.X` → `using Phase5.X`.
- [x] `GameRunner.cs`: `namespace Phase3` → `Phase5`.
- [x] `Program.cs`: menu `case "3" → Phase3.GameRunner` becomes `case "5" → Phase5.GameRunner`.
- [x] `dotnet build` clean before adding any new content.

---

## Milestone 1 — Phase 2 rework: "The Mountain Pass"
*Keep the existing tech level (single file, `List` inventory, `if/else` input, N/S/E/W fields). Only expand the world & story.*
- [x] Story: opens just after Phase 1 — apprentice has the satchel, rides for the caravan, a storm/rockslide seals the pass.
- [x] Build 6 rooms in `Phase2`: **Caravanserai Gate (start) → Foothill Trail → Old Shrine → Ice Cave → Wind Ridge → Hidden Pass (exit/win)**.
- [x] Items (simple `Item(Name)`): Rope, Oil Lantern, Flint, Dried Figs, Fur Cloak.
- [x] Puzzle/lock: Wind Ridge → Hidden Pass requires carrying Fur Cloak **and** Rope (hardcoded check, Phase-2 style).
- [x] Win: reach Hidden Pass → hook into Phase 3.
- [x] Concepts unchanged: `List<Item>`, take/drop/inventory/look/go. **No** new syntax.

---

## Milestone 2 — NEW Phase 3: "The Oasis Bazaar"
**Headline: multiple properties on a class + an `Inventory` class that wraps a collection.**
*Flat `Phase3/` folder, single `Phase3` namespace, `if/else` input, N/S/E/W fields.*
- [x] Story: apprentice descends to an oasis town and must barter to hire a desert guide.
- [x] 7 rooms: **Town Gate (start) → Market Square → Spice Stall → Carpet Weaver → Public Well → Stable → Guide's House (win)**.
- [x] `Item` expanded: `Name`, `Description`, `CanPickUp`.
- [x] `Inventory` class (NEW): wraps `List<Item>` with `Add` / `Remove` / `Find` / `Display`.
- [x] `Player` holds an `Inventory` object (not a raw list).
- [x] New command: `examine <item>` prints `Description`.
- [x] Use `CanPickUp = false` on fixtures (e.g., a Notice Board, the Public Well) so `take` refuses them.
- [x] Items: Silver Coin, Bolt of Silk, Water Skin, Brass Lamp (examine reveals a clue), Notice Board (fixed).
- [x] Win: trade Silk + Coin at the Guide's House to hire the guide.
- [x] **Deferred:** inheritance, `Dictionary`, namespaces/folders, separated Parser, `out` params.

---

## Milestone 3 — NEW Phase 4: "The Desert Fortress"
**Headline: inheritance (`KeyItem : Item`) + first separation of concerns (`static Display`).**
*Flat `Phase4/` folder, single `Phase4` namespace, multiple files but no nested folders, `if/else` input, N/S/E/W fields.*
- [x] Story: the guide leads them to a customs fortress controlling the route; gates need keys/passes.
- [x] 8 rooms: **Gatehouse (start) → Guard Barracks → Customs Hall → Archive → Cistern → Inner Courtyard → Watchtower → Sally Port (win)**.
- [x] `KeyItem : Item` (NEW): adds `UnlocksExitId`, calls `: base(name, description, canPickUp: true)`.
- [x] Locked doors check for a **`KeyItem`** (by type), not a hardcoded room-name string.
- [x] `Display` class (NEW, **static**): every `Console.Write` moves here (Inventory keeps its own, to avoid a name clash with the `Display` class).
- [x] Carry forward Phase 3's `Item`(3 props) + `Inventory` class.
- [x] Key items: Iron Gate Key, Customs Seal, Captain's Pass; plus Ledger (examine clue), Lantern.
- [x] Win: collect the right passes, exit the Sally Port toward the caravan.
- [x] **Deferred:** namespaces/folders, `Parser`/`Actions` split, `Dictionary<string,Exit>` + `Exit` object, `out` params.

---

## Milestone 4 — Phase 5 expansion: "The Caravan & the Guardian's Riddle" (goddess + endgame)
*Built on the renamed full-architecture code (folders/namespaces, `Parser` + `Actions`, `Dictionary<string,Exit>` + `Exit`, `out` param, `static Display`, `Inventory`). Expand the world from 3 rooms to a full map and add the climax.*
- [x] Story: they finally reach the caravan's camp at a moonlit oasis — but the last path is barred by a radiant **goddess**, guardian of the road.
- [x] Rooms (size to fit): e.g. **Camp Edge (start) → Overturned Wagons → Camel Lines → Supply Tent → Moonlit Oasis → Watch Lookout → Hidden Shrine (GUARDIAN) → Caravan Heart (endgame scene)**.
- [x] Expand `WorldBuilder` to wire all rooms + locked exits via `Exit` objects.
- [x] Items/keys: Marco's Seal, Brass Lantern, Tent Key (`KeyItem`), Shrine Token (`KeyItem` to reach the goddess).
- [x] **Goddess riddle design (surprise reveal, no combat) — THREE riddles back-to-back:**
  - [x] Entering the Hidden Shrine triggers a surprise scene — the player expects danger; instead a beautiful goddess appears (ASCII art reveal).
  - [x] She poses **three riddles in sequence**; the game reads the player's typed answer to each.
  - [x] **Solving all three** opens the way to the Caravan Heart (the win). Getting them all is the victory condition.
  - [x] Handle via a small `Riddle` class (Question + accepted answers) and a `Riddle[]`/`List<Riddle>` the encounter steps through — a natural place to teach a class + a loop over objects.
  - [x] Compare answers case-insensitively; accept a few synonyms per riddle.
  - [x] Wrong answer → a gentle hint and let the player retry that same riddle (no loss, no death); only advance to the next riddle on a correct answer.
  - [x] After the third correct answer → she steps aside and blesses the journey.
  - [x] Lock the **three riddle texts + accepted answers** in this plan before building (proposal pending your sign-off).
- [x] **Endgame scene:** passing the goddess and reaching Caravan Heart prints a multi-line closing scene (ASCII art) reuniting with Marco Polo → game ends.
- [x] Confirm the full win path is reachable end-to-end.

---

## Milestone 5 — Menu, docs, verification
- [ ] `Program.cs`: rewrite the menu to list all 5 chapters with one-line descriptions + correct `case "1"`…`"5"` wiring.
- [ ] `README.md`: update chapter table, project-layout tree, study path, and per-chapter "new concept" notes for all five.
- [ ] (Optional) `docs/CURRICULUM.md`: capture the story arc + concept ladder for instructors.
- [ ] Final `dotnet build` → 0 warnings / 0 errors.
- [ ] Scripted playthrough of each chapter's win path to confirm it completes.
- [ ] Commit per milestone on a feature branch; open PR.

---

## Concept ladder (what's genuinely NEW each chapter)
1. **P1** — classes, objects, a loop.
2. **P2** — `List` collections; take/drop.
3. **P3** — multi-property objects; an `Inventory` class with methods (encapsulation); `examine`/`CanPickUp`.
4. **P4** — inheritance (`KeyItem : Item`, `: base(...)`); centralized `static Display`.
5. **P5** — namespaces/folders; `Parser` + `Actions`; `Dictionary<string,Exit>` + `Exit` objects; `out` params; goddess riddle encounter; endgame scene.

---

## Resolved decisions
- [x] **Final encounter:** a surprise **goddess** who poses a **riddle** — **no combat**. Answer correctly to win.
- [x] **Room counts:** vary per chapter, no fixed number.
- [x] **No inventory carry-over:** each chapter is self-contained and playable in any order.
- [x] **`CLAUDE.md`:** updated to the 5-chapter structure (done as part of this plan).

## Locked riddles (goddess, Chapter 5 — must solve all 3)
1. *"I have cities, but no houses; mountains, but no trees; and water, but no fish. What am I?"* → **map** (map, a map)
2. *"I am always coming but never arrive; the caravan waits for me, then departs before me. What am I?"* → **dawn** (dawn, sunrise, the morning, morning)
3. *"The more of me you take, the more you leave behind. What am I?"* → **footsteps** (footsteps, footstep, steps, footprints)

## Confirmed
- [x] Three riddles locked (above). Phase 1 kept exactly as-is. **Build approved — GO.**
