namespace Phase5.Engine;

// A single riddle: the question, a gentle hint for wrong guesses, and the list of
// answers we'll accept. Holding several of these in a List and looping over them
// (see RiddleEncounter) is a natural way to practice iterating over objects.
class Riddle
{
    public string Question { get; }
    public string Hint     { get; }

    private readonly string[] _acceptedAnswers;

    // `params` lets the caller pass any number of accepted answers at the end.
    public Riddle(string question, string hint, params string[] acceptedAnswers)
    {
        Question         = question;
        Hint             = hint;
        _acceptedAnswers = acceptedAnswers;
    }

    // Case-insensitive match against any accepted answer.
    public bool IsCorrect(string answer)
    {
        string cleaned = answer.Trim().ToLower();
        foreach (string accepted in _acceptedAnswers)
        {
            if (cleaned == accepted.ToLower())
                return true;
        }
        return false;
    }
}
