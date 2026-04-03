namespace sqlite2fbs.console;

public static class DatabaseValidator
{
    public static void PrintStats(DatabaseParser parser)
    {
        Console.WriteLine($"\n--- SQLite Database Stats ({AppData.DatabaseName}) ---");
        Console.WriteLine($"Total Categories:   {parser.Categories.Count}");
        Console.WriteLine($"Total Resources:    {parser.Resources.Count}");
        Console.WriteLine($"Total Requirements: {parser.Requirements.Count}");
        Console.WriteLine($"Total Entries:      {parser.CodexEntries.Count}\n");

        var topLevelCount = parser.CodexEntries.Count(e => e.ParentEntryId == null);
        var subEntryCount = parser.CodexEntries.Count(e => e.ParentEntryId != null);
        
        var withAudio = parser.CodexEntries.Count(e => e.AudioId != null);
        var withImage = parser.CodexEntries.Count(e => e.ImageId != null);

        Console.WriteLine("Entry Breakdown:");
        Console.WriteLine($"- Top-Level Entries: {topLevelCount}");
        Console.WriteLine($"- Sub-Entries:       {subEntryCount}");
        Console.WriteLine($"- With Audio:        {withAudio}");
        Console.WriteLine($"- With Image:        {withImage}\n");
    }
}
