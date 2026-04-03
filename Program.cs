using sqlite2fbs.console;

Console.WriteLine($"=======================================================");
Console.WriteLine($" Welcome to {AppData.AppName} v{AppData.AppVersion}");
Console.WriteLine($" By {AppData.AppAuthor}");
Console.WriteLine($"=======================================================");
Console.WriteLine("This utility parses the structured Codex SQLite database");
Console.WriteLine("and converts it into a high-performance binary FlatBuffer.\n");

var running = true;

while (running)
{
    Console.WriteLine("\nPlease select an option:");
    Console.WriteLine("1. Build FlatBuffers (.bin) from SQLite Database");
    Console.WriteLine("2. Display SQL Debug Data");
    Console.WriteLine("3. Display FlatBuffers Debug Data");
    Console.WriteLine("4. Exit");
    Console.Write("\n> ");

    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            Console.WriteLine("\n[1/2] Parsing SQLite Database...");
            var parser = new DatabaseParser();
            parser.Parse();

            Console.WriteLine("[2/2] Converting to FlatBuffers...");
            var outputFile = FlatbuffersConverter.Convert(parser);

            Console.WriteLine($"Conversion complete: Saved to {outputFile}");
            break;

        case "2":
            Console.WriteLine("\nParsing SQLite Database for stats...");
            var dbParser = new DatabaseParser();
            dbParser.Parse();

            DatabaseValidator.PrintStats(dbParser);
            break;

        case "3":
            if (File.Exists(AppData.OutputFilePath))
            {
                Console.WriteLine("\nValidating Generated FlatBuffer...");
                var fbValidator = new FlatbuffersValidator();
                FlatbuffersValidator.PrintStats(AppData.OutputFilePath);
            }
            else
            {
                Console.WriteLine($"\n[!] Error: FlatBuffer file not found at '{AppData.OutputFilePath}'.");
                Console.WriteLine("    Please build it first by selecting Option 1.");
            }
            break;

        case "4":
            running = false;
            Console.WriteLine("\nExiting. Goodbye!");
            break;

        default:
            Console.WriteLine("\n[!] Invalid option. Please enter 1, 2, 3, or 4.");
            break;
    }
}
