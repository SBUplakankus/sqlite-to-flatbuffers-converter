# FlatBuffers Schema — Veil Compact Codex

## The .fbs Schema

```fbs
// ============================================================
// veil_compact_codex.fbs
// FlatBuffers schema for the Veil Compact Codex
// Target: Unity C# (flatc --csharp --gen-onefile)
// Namespace: CodexSystem
// ============================================================

namespace CodexSystem;

enum ResourceType : byte { None = 0, Audio = 1, Image = 2 }
enum EntryType    : byte { Primary = 0, Secondary = 1 }

table Resource {
    id:            uint;
    file_path:     string;
    resource_type: ResourceType;
}

table Entry {
    id:             uint;
    requirement_id: uint;       // 0 = no requirement
    entry_type:     EntryType;
    title:          string;
    content:        string;
    audio_id:       uint;       // 0 = no audio
    image_id:       uint;       // 0 = no image
    sort_order:     int;
    sub_entries:    [Entry];    // populated for Primary entries only; empty for Secondary
}

table Category {
    id:         uint;
    name:       string;
    sort_order: int;
    entries:    [Entry];        // Primary entries only
}

table CodexRoot {
    categories: [Category];
    resources:  [Resource];
}

root_type CodexRoot;
```

---

## flatc Code Generation

```bash
flatc --csharp --gen-onefile -o ./Assets/Scripts/Generated/Codex/ veil_compact_codex.fbs
```

Produces a single `VeilCompactCodexGenerated.cs` in the target directory.

---

## Structure Notes

The FBS mirrors the ME2 two-tier structure exactly:

```
CodexRoot
└── categories: [Category]
    └── entries: [Entry]          ← Primary entries only
        └── sub_entries: [Entry]  ← Secondary entries
```

- `Category.entries` contains only Primary entries (`entry_type = Primary`)
- `Entry.sub_entries` contains only Secondary entries (`entry_type = Secondary`)
- Secondary entries have empty `sub_entries` (no third tier)
- `requirement_id = 0` means always unlocked — game save system handles evaluation
- `audio_id = 0` and `image_id = 0` mean no asset assigned

---

## Unity C# Usage Pattern

```csharp
using CodexSystem;
using FlatBuffers;
using UnityEngine;
using System.Collections.Generic;

public class CodexManager : MonoBehaviour
{
    private CodexRoot _root;

    // Fast lookup tables built at startup
    private Dictionary<uint, Entry>    _entryById    = new Dictionary<uint, Entry>();
    private Dictionary<uint, Resource> _resourceById = new Dictionary<uint, Resource>();

    void Awake()
    {
        TextAsset asset = Resources.Load<TextAsset>("codex_data");
        ByteBuffer buffer = new ByteBuffer(asset.bytes);
        _root = CodexRoot.GetRootAsCodexRoot(buffer);
        BuildLookups();
    }

    void BuildLookups()
    {
        for (int c = 0; c < _root.CategoriesLength; c++)
        {
            Category category = _root.Categories(c).Value;
            for (int e = 0; e < category.EntriesLength; e++)
            {
                Entry primary = category.Entries(e).Value;
                _entryById[primary.Id] = primary;

                for (int s = 0; s < primary.SubEntriesLength; s++)
                {
                    Entry secondary = primary.SubEntries(s).Value;
                    _entryById[secondary.Id] = secondary;
                }
            }
        }

        for (int r = 0; r < _root.ResourcesLength; r++)
        {
            Resource res = _root.Resources(r).Value;
            _resourceById[res.Id] = res;
        }
    }

    public Entry GetEntry(uint id)
    {
        _entryById.TryGetValue(id, out Entry entry);
        return entry;
    }

    public Resource GetResource(uint id)
    {
        _resourceById.TryGetValue(id, out Resource resource);
        return resource;
    }

    public Category GetCategory(int index)
    {
        return _root.Categories(index).Value;
    }

    public int CategoryCount => _root.CategoriesLength;
}
```

---

## Console App: SQLite → FlatBuffers Binary

The external tool reads the SQLite DB and writes a `.bytes` file for Unity:

```csharp
// Pseudocode outline — implement in your console app
var builder = new FlatBufferBuilder(1024 * 1024);

// 1. Build resources
var resourceOffsets = db.QueryResources().Select(r => {
    var path = builder.CreateString(r.FilePath);
    Resource.StartResource(builder);
    Resource.AddId(builder, (uint)r.Id);
    Resource.AddFilePath(builder, path);
    Resource.AddResourceType(builder, (ResourceType)r.ResourceType);
    return Resource.EndResource(builder);
}).ToArray();
var resourcesVec = CodexRoot.CreateResourcesVector(builder, resourceOffsets);

// 2. Build entries per category (primaries + their secondaries nested)
var categoryOffsets = db.QueryCategories().Select(cat => {
    var primaryEntries = db.QueryPrimaryEntries(cat.Id);
    var primaryOffsets = primaryEntries.Select(p => {
        var secondaries = db.QuerySecondaryEntries(p.Id);
        var secondaryOffsets = secondaries.Select(s => BuildEntry(builder, s, Array.Empty<Offset<Entry>>())).ToArray();
        var subVec = Entry.CreateSubEntriesVector(builder, secondaryOffsets);
        return BuildEntry(builder, p, subVec);
    }).ToArray();
    var entriesVec = Category.CreateEntriesVector(builder, primaryOffsets);
    var name = builder.CreateString(cat.Name);
    Category.StartCategory(builder);
    Category.AddId(builder, (uint)cat.Id);
    Category.AddName(builder, name);
    Category.AddSortOrder(builder, cat.SortOrder);
    Category.AddEntries(builder, entriesVec);
    return Category.EndCategory(builder);
}).ToArray();
var categoriesVec = CodexRoot.CreateCategoriesVector(builder, categoryOffsets);

// 3. Write root and finish
CodexRoot.StartCodexRoot(builder);
CodexRoot.AddCategories(builder, categoriesVec);
CodexRoot.AddResources(builder, resourcesVec);
var root = CodexRoot.EndCodexRoot(builder);
builder.Finish(root.Value);

File.WriteAllBytes("codex_data.bytes", builder.SizedByteArray());
```

---

## Size Estimate

At ~450 entries averaging 250 words each:
- Raw text content: ~900KB
- FlatBuffers overhead: ~10–15%
- Expected binary size: **~1.0–1.1MB**

Well within Unity asset budget. No need for content streaming at this scale.
