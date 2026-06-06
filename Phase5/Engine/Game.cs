using Phase5.Characters;
using Phase5.UI;
using Phase5.World;

namespace Phase5.Engine;

class Game
{
    private Player _player;
    private Parser _parser;

    public Game()
    {
        Room startingRoom = WorldBuilder.Build();
        _player = new Player(startingRoom);
        _parser = new Parser();
    }

    public void Run()
    {
        Display.Welcome();
        Display.Room(_player.CurrentRoom);

        while (true)
        {
            Display.Prompt();
            string? input = Console.ReadLine();

            if (input == null) break;   // EOF (piped input / Ctrl-D)

            _parser.Parse(input, _player, out bool quit);

            if (quit)
            {
                Display.Message("Farewell, young merchant. Perhaps next time.");
                break;
            }

            if (IsWinCondition())
            {
                Display.Win();
                break;
            }
        }
    }

    // Win: the player has passed the guardian and reached the Caravan Heart.
    private bool IsWinCondition()
    {
        return _player.CurrentRoom.Name == "Caravan Heart";
    }
}
