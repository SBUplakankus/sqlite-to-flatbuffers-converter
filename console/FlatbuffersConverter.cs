using Google.FlatBuffers;
using CodexSystem;

namespace sqlite2fbs.console;

public static class FlatbuffersConverter
{
    private static Offset<Entry> BuildEntry(CodexEntryRecord entry, Dictionary<long, List<CodexEntryRecord>> entriesByParent, FlatBufferBuilder builder)
    {
        var subEntryOffsets = new List<Offset<Entry>>();
        
        if (entriesByParent.TryGetValue(entry.Id, out var subEntries))
        {
            foreach (var sub in subEntries)
            {
                subEntryOffsets.Add(BuildEntry(sub, entriesByParent, builder));
            }
        }
        
        var subEntriesVector = Entry.CreateSubEntriesVector(builder, subEntryOffsets.ToArray());
        var titleOffset = builder.CreateString(entry.Title);
        var contentOffset = builder.CreateString(entry.Content);

        return Entry.CreateEntry(
            builder,
            (uint)entry.Id,
            (uint)(entry.RequirementId ?? 0L),
            (EntryType)entry.EntryType,
            titleOffset,
            contentOffset,
            (uint)(entry.AudioId ?? 0L),
            (uint)(entry.ImageId ?? 0L),
            (int)entry.SortOrder,
            subEntriesVector
        );
    }

    private static VectorOffset BuildResources(DatabaseParser parser, FlatBufferBuilder builder)
    {
        var resourceList = new List<Offset<Resource>>();

        foreach (var resource in parser.Resources)
        {
            var path = builder.CreateString(resource.FilePath ?? string.Empty);
            var offset = Resource.CreateResource(
                builder,
                (uint)resource.Id,
                path,
                (ResourceType)resource.ResourceType);
            
            resourceList.Add(offset);
        }
        return CodexRoot.CreateResourcesVector(builder, resourceList.ToArray());
    }

    private static VectorOffset BuildCategories(DatabaseParser parser, FlatBufferBuilder builder)
    {
        var entriesByParent = parser.CodexEntries
            .GroupBy(e => e.ParentEntryId ?? 0L)
            .ToDictionary(g => g.Key, g => g.ToList());
        
        var categoryList = new List<Offset<Category>>();

        foreach (var category in parser.Categories)
        {
            var entryOffsets = new List<Offset<Entry>>();

            if (entriesByParent.TryGetValue(0L, out var topLevelEntries))
            {
                var categoryEntries = topLevelEntries.Where(e => e.CategoryId == category.Id);
                foreach (var entry in categoryEntries)
                {
                    entryOffsets.Add(BuildEntry(entry, entriesByParent, builder));
                }
            }

            var entriesVector = Category.CreateEntriesVector(builder, entryOffsets.ToArray());
            var nameOffset = builder.CreateString(category.Name);

            var categoryOffset = Category.CreateCategory(
                builder,
                (uint)category.Id,
                nameOffset,
                (int)category.SortOrder,
                entriesVector
            );
            
            categoryList.Add(categoryOffset);
        }
        
        return CodexRoot.CreateCategoriesVector(builder, categoryList.ToArray());
    }
    
    /// <summary>
    /// Convert the SQLite Data into the Flatbuffers Binary File
    /// </summary>
    /// <param name="parser">Database Parser containing the converted SQLite</param>
    /// <returns></returns>
    public static string Convert(DatabaseParser parser)
    {
        var builder = new FlatBufferBuilder(AppData.DefaultBufferSize);
        
        var resourcesVector = BuildResources(parser, builder);
        var categoriesVector = BuildCategories(parser, builder);

        var rootOffset = CodexRoot.CreateCodexRoot(builder, categoriesVector, resourcesVector);
        builder.Finish(rootOffset.Value);

        if (Directory.Exists(AppData.OutputDirectory))
        {
            Directory.Delete(AppData.OutputDirectory, true);
        }
        Directory.CreateDirectory(AppData.OutputDirectory);
        
        File.WriteAllBytes(AppData.OutputFilePath, builder.SizedByteArray());
        return AppData.OutputFilePath;
    }
}