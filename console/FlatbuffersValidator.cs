using CodexSystem;
using Google.FlatBuffers;

namespace sqlite2fbs.console;

public class FlatbuffersValidator
{
    private struct EntryCounters
    {
        public int Total;
        public int SubEntries;
        public int WithRequirement;
        public int WithAudio;
        public int WithImage;
    }

    private static EntryCounters CountEntryTree(Entry entry)
    {
        var counters = new EntryCounters
        {
            Total = 1,
            WithRequirement = entry.RequirementId > 0 ? 1 : 0,
            WithAudio = entry.AudioId > 0 ? 1 : 0,
            WithImage = entry.ImageId > 0 ? 1 : 0
        };

        for (var i = 0; i < entry.SubEntriesLength; i++)
        {
            var sub = entry.SubEntries(i).GetValueOrDefault();
            var subCounters = CountEntryTree(sub);
            counters.SubEntries += 1 + subCounters.SubEntries;
            counters.Total += subCounters.Total;
            counters.WithRequirement += subCounters.WithRequirement;
            counters.WithAudio += subCounters.WithAudio;
            counters.WithImage += subCounters.WithImage;
        }

        return counters;
    }

    public static void PrintStats(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: File '{filePath}' not found.");
            return;
        }

        Console.WriteLine($"\n--- FlatBuffers Validation Stats ({filePath}) ---");

        var bytes = File.ReadAllBytes(filePath);
        var buffer = new ByteBuffer(bytes);
        var root = CodexRoot.GetRootAsCodexRoot(buffer);

        Console.WriteLine($"File Size (bytes): {bytes.Length}");
        Console.WriteLine($"Total Categories:  {root.CategoriesLength}");
        Console.WriteLine($"Total Resources:   {root.ResourcesLength}\n");

        int images = 0, audio = 0, unknown = 0;
        for (var i = 0; i < root.ResourcesLength; i++)
        {
            var res = root.Resources(i).GetValueOrDefault();
            switch (res.ResourceType)
            {
                case ResourceType.Image: images++; break;
                case ResourceType.Audio: audio++; break;
                default: unknown++; break;
            }
        }

        Console.WriteLine($"Resource Breakdown: {images} Images | {audio} Audio | {unknown} Other\n");

        Console.WriteLine("Category Breakdown:");
        Console.WriteLine($"{"Name",-25} | {"Top-Level",-10} | {"Sub-Entries",-11} | {"Total",-5}");
        Console.WriteLine(new string('-', 60));

        var totalEntries = 0;
        var totalSubEntries = 0;
        var totalWithRequirement = 0;
        var totalWithAudio = 0;
        var totalWithImage = 0;

        for (var i = 0; i < root.CategoriesLength; i++)
        {
            var cat = root.Categories(i).GetValueOrDefault();

            var topLevel = cat.EntriesLength;
            var subEntries = 0;
            var categoryTotal = 0;

            for (var j = 0; j < cat.EntriesLength; j++)
            {
                var entry = cat.Entries(j).GetValueOrDefault();
                var counters = CountEntryTree(entry);
                subEntries += counters.SubEntries;
                categoryTotal += counters.Total;
                totalWithRequirement += counters.WithRequirement;
                totalWithAudio += counters.WithAudio;
                totalWithImage += counters.WithImage;
            }

            totalEntries += categoryTotal;
            totalSubEntries += subEntries;
            Console.WriteLine($"{cat.Name,-25} | {topLevel,-10} | {subEntries,-11} | {categoryTotal,-5}");
        }

        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"{"TOTAL",-25} | {totalEntries - totalSubEntries,-10} | {totalSubEntries,-11} | {totalEntries,-5}");
        Console.WriteLine($"Entries With Requirement: {totalWithRequirement}");
        Console.WriteLine($"Entries With Audio: {totalWithAudio}");
        Console.WriteLine($"Entries With Image: {totalWithImage}\n");
    }
}
