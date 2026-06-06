Console.WriteLine("=== The Silk Road — A C# Learning Adventure (1271 AD) ===");
Console.WriteLine();
Console.WriteLine("Each chapter is self-contained and playable on its own.");
Console.WriteLine("Choose a chapter:");
Console.WriteLine("  1 - The Caravanserai          classes & objects + the game loop");
Console.WriteLine("  2 - The Mountain Pass         collections: List<Item>, take/drop");
Console.WriteLine("  3 - The Oasis Bazaar          richer items + an Inventory class, examine");
Console.WriteLine("  4 - The Desert Fortress       inheritance (KeyItem) + static Display");
Console.WriteLine("  5 - The Caravan & the Riddle  full architecture + the guardian's riddle");
Console.WriteLine();
Console.Write("Enter 1, 2, 3, 4, or 5: ");

string? choice = Console.ReadLine();
Console.WriteLine();

switch (choice?.Trim())
{
    case "1": Phase1.GameRunner.Start(); break;
    case "2": Phase2.GameRunner.Start(); break;
    case "3": Phase3.GameRunner.Start(); break;
    case "4": Phase4.GameRunner.Start(); break;
    case "5": Phase5.GameRunner.Start(); break;
    default:
        Console.WriteLine("Unknown choice. Please run again and enter 1, 2, 3, 4, or 5.");
        break;
}
