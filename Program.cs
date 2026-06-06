Console.WriteLine("=== The Caravanserai — Learning Edition ===");
Console.WriteLine();
Console.WriteLine("Choose a chapter:");
Console.WriteLine("  1 - Chapter 1: Rooms & Movement      (4 files, ~110 lines)");
Console.WriteLine("  2 - Chapter 2: Items & Inventory     (5 files, ~270 lines)");
Console.WriteLine("  3 - Chapter 3: The Oasis Bazaar      (5 files, Inventory class + examine)");
Console.WriteLine("  4 - Chapter 4: The Desert Fortress   (7 files, inheritance + static Display)");
Console.WriteLine("  5 - Chapter 5: Full Adventure        (11 files, full architecture)");
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
