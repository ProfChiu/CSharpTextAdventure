using CSharpTextAdventure.Items;

namespace CSharpTextAdventure.World;

static class WorldBuilder
{
    public static Room Build()
    {
        // --- Rooms ---
        var courtyard = new Room(
            "Courtyard",
            "A dusty open square under a darkening sky. The caravanserai walls loom around you. " +
            "A heavy wooden door leads south into the building. This is where your journey starts — " +
            "and where it must end before dawn."
        );

        var tradeHall = new Room(
            "Trade Hall",
            "A long vaulted hall lit by a single oil lamp. Overturned tables and scattered pottery " +
            "litter the floor. One section of the wall looks slightly different — " +
            "a loose brick that doesn't quite match the others."
        );

        var vault = new Room(
            "Merchant's Vault",
            "A small cedar-shelved chamber, surprisingly intact. On a low table sits a leather " +
            "satchel stamped with the Polo family crest — your missing documents."
        );

        // --- Items ---
        var looseBrick = new Item(
            "Loose Brick",
            "A brick that sticks out slightly from the wall. It looks like it could hide something.",
            canPickUp: false
        );

        var brassKey = new KeyItem(
            "Brass Key",
            "A heavy brass key, tarnished with age. It looks like it fits a large lock.",
            unlocksExitId: "vault-door"
        );

        var poloSatchel = new Item(
            "Polo Satchel",
            "A worn leather satchel stamped with the Polo family crest. Your travel documents are inside.",
            canPickUp: true
        );

        tradeHall.AddItem(looseBrick);
        vault.AddItem(poloSatchel);

        // --- Exits ---
        // Vault door is locked; carrying the Brass Key automatically unlocks it.
        var vaultDoor = new Exit(vault, isLocked: true, requiredKey: "vault-door");

        courtyard.AddExit("south", new Exit(tradeHall));
        tradeHall.AddExit("north", new Exit(courtyard));
        tradeHall.AddExit("south", vaultDoor);
        vault.AddExit("north", new Exit(tradeHall));

        // Store the brass key on the brick so the examine action can surface it.
        looseBrick.Description =
            "You pry the brick loose. Hidden in the gap behind it is a Brass Key!";
        tradeHall.AddItem(brassKey);     // placed but revealed only after examining brick
        brassKey.CanPickUp = false;      // hidden until brick is examined

        return courtyard;
    }
}
