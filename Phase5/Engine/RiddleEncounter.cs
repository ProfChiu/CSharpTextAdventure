using Phase5.UI;

namespace Phase5.Engine;

// The guardian encounter. Triggered when the player first enters the Hidden Shrine.
// The goddess poses three riddles back-to-back. A wrong answer gives a hint and
// lets the player try the SAME riddle again — there is no death or loss. Solving
// all three returns true, which opens the way to the Caravan Heart.
static class RiddleEncounter
{
    public static bool Run()
    {
        Display.Goddess();

        // The three locked riddles, held in a list and stepped through in order.
        var riddles = new List<Riddle>
        {
            new Riddle(
                "I have cities, but no houses; mountains, but no trees; and water, but no fish. What am I?",
                "A traveler unrolls me to find the way across strange lands.",
                "map", "a map"),
            new Riddle(
                "I am always coming but never arrive; the caravan waits for me, then departs before me. What am I?",
                "It breaks pale over the dunes each morning.",
                "dawn", "sunrise", "the morning", "morning"),
            new Riddle(
                "The more of me you take, the more you leave behind. What am I?",
                "Look down at the sand behind you as you walk.",
                "footsteps", "footstep", "steps", "footprints"),
        };

        int number = 1;
        foreach (Riddle riddle in riddles)
        {
            if (!AskUntilSolved(riddle, number))
                return false;   // player abandoned the encounter
            number++;
        }

        Display.GoddessBlessing();
        return true;
    }

    // Ask one riddle until the player answers it correctly (or abandons).
    private static bool AskUntilSolved(Riddle riddle, int number)
    {
        Display.RiddleQuestion(number, riddle.Question);

        while (true)
        {
            Display.Prompt();
            string? answer = Console.ReadLine();

            if (answer == null) return false;   // EOF (piped input ran out)

            string trimmed = answer.Trim().ToLower();
            if (trimmed == "quit" || trimmed == "q")
                return false;   // step back from the goddess

            if (riddle.IsCorrect(answer))
            {
                Display.RiddleCorrect();
                return true;
            }

            Display.RiddleWrong(riddle.Hint);
        }
    }
}
