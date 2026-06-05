using Phase3.Characters;
using Phase3.UI;

namespace Phase3.Engine;

class Parser
{
    // Returns true if the player issued a quit command.
    public bool Parse(string input, Player player, out bool quitRequested)
    {
        quitRequested = false;

        string[] words = input.ToLower().Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (words.Length == 0) return false;

        string verb = words[0];
        string noun = words.Length > 1 ? string.Join(" ", words[1..]) : "";

        switch (verb)
        {
            case "go":
                Actions.Go(noun, player);
                break;

            // Cardinal shortcuts — treat the direction as the noun.
            case "north": case "n":
                Actions.Go("north", player);
                break;
            case "south": case "s":
                Actions.Go("south", player);
                break;
            case "east": case "e":
                Actions.Go("east", player);
                break;
            case "west": case "w":
                Actions.Go("west", player);
                break;

            case "look": case "l":
                Actions.Look(player);
                break;

            case "examine": case "x":
                Actions.Examine(noun, player);
                break;

            case "take": case "get":
                Actions.Take(noun, player);
                break;

            case "drop":
                Actions.Drop(noun, player);
                break;

            case "inventory": case "i": case "inv":
                Actions.ShowInventory(player);
                break;

            case "unlock":
                Actions.Unlock(noun, player);
                break;

            case "help": case "?":
                Display.Help();
                break;

            case "quit": case "exit": case "q":
                quitRequested = true;
                break;

            default:
                Display.Message($"I don't understand '{input}'. Type HELP for a list of commands.");
                break;
        }

        return false;
    }
}
