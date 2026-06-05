using Phase3.Characters;
using Phase3.UI;
using Phase3.World;

namespace Phase3.Engine;

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

    // Win: player is back in the Courtyard carrying the Polo Satchel.
    private bool IsWinCondition()
    {
        return _player.CurrentRoom.Name == "Courtyard"
            && _player.Inventory.Contains("Polo Satchel");
    }
}
