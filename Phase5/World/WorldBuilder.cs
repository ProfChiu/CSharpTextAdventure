using Phase5.Items;

namespace Phase5.World;

static class WorldBuilder
{
    public static Room Build()
    {
        // Geographic leg 5: the caravan's camp at a moonlit oasis. A single road
        // runs north through the camp to the Hidden Shrine, where the guardian
        // waits, and on to the Caravan Heart. Two locked tents/gates sit along the
        // way; the last gate is opened not by a key but by answering the goddess.

        // --- Rooms ---
        var campEdge = new Room(
            "Camp Edge",
            "The ragged edge of a caravan camp at night. Cook-fires gutter in the wind and " +
            "the road runs north between the tents. A brass lantern hangs from a post.",
            "   .  *  .  *\n   |campfire|\n  _|__~~~__|_"
        );

        var wagons = new Room(
            "Overturned Wagons",
            "Two trade wagons lie tipped on their sides, cargo spilled across the sand. " +
            "Something metal glints among the scattered crates. The road continues north.",
            "    ____\n   /o  o\\__\n   \\____/  \\"
        );

        var camelLines = new Room(
            "Camel Lines",
            "A picket line of dozing camels chewing in the dark. A drover's chest lies open " +
            "nearby. A heavy supply tent is lashed shut to the north.",
            "   /\\_/\\   /\\_/\\\n  (camel) (camel)\n   |   |   |   |"
        );

        var supplyTent = new Room(
            "Supply Tent",
            "Inside the supply tent: sacks of grain, water-skins, and trade goods. On a folding " +
            "table rests a small carved token. The oasis lies north.",
            "    /\\\n   /  \\\n  /____\\\n  |TENT|"
        );

        var oasis = new Room(
            "Moonlit Oasis",
            "A still pool ringed by palms, the full moon doubled on its black water. The path " +
            "climbs north to a lookout. It is very quiet here.",
            "   ( ( O ) )\n  ~~~~~~~~~~~\n   palms  pool"
        );

        var lookout = new Room(
            "Watch Lookout",
            "A low rise above the camp. From here you can see the whole oasis silvered by " +
            "moonlight — and, to the north, a small shrine you do not remember from the maps.",
            "      /\\\n     /  \\\n    / [] \\\n   /______\\"
        );

        var shrine = new Room(
            "Hidden Shrine",
            "A shrine of pale stone, older than the road itself. The air hums. You expected " +
            "danger here — bandits, perhaps — but what waits is something else entirely.",
            "    _______\n   |  ***  |\n   | ( ) ( )|\n   |_______|"
        );

        var caravanHeart = new Room(
            "Caravan Heart",
            "Beyond the shrine, the heart of the caravan: a circle of firelit tents, and a " +
            "figure rising to meet you with open arms.",
            "   /\\  /\\  /\\\n  /  \\/  \\/  \\\n  | CARAVAN  |"
        );

        // --- Items ---
        campEdge.AddItem(new Item(
            "Brass Lantern",
            "A brass lantern with fresh oil — enough light to cross the camp by night.",
            canPickUp: true));

        wagons.AddItem(new KeyItem(
            "Tent Key",
            "An iron key on a leather thong. It fits the lock lashing the supply tent shut.",
            unlocksExitId: "tent-lock"));

        camelLines.AddItem(new Item(
            "Marco's Seal",
            "A wax seal pressed with the Polo crest — proof to the caravan that you are one of " +
            "their own. Marco himself will know it.",
            canPickUp: true));

        supplyTent.AddItem(new KeyItem(
            "Shrine Token",
            "A carved stone token worn smooth by many hands. It seems meant for the shrine gate.",
            unlocksExitId: "shrine-lock"));

        // --- Exits ---
        // A mostly straight road north. Two locked gates use KeyItems; the final
        // gate (shrine -> caravan heart) is locked by "guardian" — no key opens it,
        // only answering the goddess's riddles does (see Actions.Go / RiddleEncounter).
        campEdge.AddExit("north", new Exit(wagons));
        wagons.AddExit("south", new Exit(campEdge));
        wagons.AddExit("north", new Exit(camelLines));
        camelLines.AddExit("south", new Exit(wagons));
        camelLines.AddExit("north", new Exit(supplyTent, isLocked: true, requiredKey: "tent-lock"));
        supplyTent.AddExit("south", new Exit(camelLines));
        supplyTent.AddExit("north", new Exit(oasis));
        oasis.AddExit("south", new Exit(supplyTent));
        oasis.AddExit("north", new Exit(lookout));
        lookout.AddExit("south", new Exit(oasis));
        lookout.AddExit("north", new Exit(shrine, isLocked: true, requiredKey: "shrine-lock"));
        shrine.AddExit("south", new Exit(lookout));
        shrine.AddExit("north", new Exit(caravanHeart, isLocked: true, requiredKey: "guardian"));
        caravanHeart.AddExit("south", new Exit(shrine));

        return campEdge;
    }
}
