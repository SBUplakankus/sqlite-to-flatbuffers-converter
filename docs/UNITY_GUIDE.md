# Integrating FlatBuffers into Unity

This guide explains how to properly load your generated `CodexData.bin` flatbuffer file into a Unity project. Doing so allows you to access all your Codex records without instantiating any objects or deserializing JSON.

## 1. Prerequisites

1. **Google Flatbuffers DLL**
   Unity needs the FlatBuffers library to run the generated structs.
   - Go to the official [FlatBuffers Github Releases](https://github.com/google/flatbuffers/releases) or NuGet.
   - Download `Google.FlatBuffers.dll` (.NET Standard 2.0 or 2.1).
   - Place this `.dll` inside your Unity project at `Assets/Plugins/FlatBuffers/Google.FlatBuffers.dll`.

2. **Your Generated CodexSystem C# Scripts**
   - Copy the folder containing your generated `.cs` FlatBuffers struct files (e.g. `flatbuffers/CodexSystem/`) into your Unity project, such as `Assets/Scripts/CodexSystem/`.

3. **The Embedded Binary File**
   - Copy your created `CodexData.bin` file into your Unity project's `Assets/StreamingAssets/` folder. Unity copies files from this directory to the final build automatically.

## 2. Writing the Loader Script

Create a new C# script in Unity named `CodexDatabaseLoader.cs`. This script will open the `.bin` file, wrap it in a `ByteBuffer`, and map your `CodexRoot` to it.

```csharp
using System.IO;
using UnityEngine;
using Google.FlatBuffers;
using CodexSystem; // Your generated namespace

public class CodexDatabaseLoader : MonoBehaviour
{
    // Caches the root object for global fast access
    public CodexRoot RootData { get; private set; }

    void Start()
    {
        LoadDatabase();
    }

    private void LoadDatabase()
    {
        // 1. Get the path to your StreamingAssets folder
        string filePath = Path.Combine(Application.streamingAssetsPath, "CodexData.bin");

#if UNITY_ANDROID && !UNITY_EDITOR
        // (Note: On Android, StreamingAssets are inside the .apk, so you must use UnityWebRequest to read it)
        // This is a synchronous workaround for Android:
        byte[] bytes;
        using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath))
        {
            www.SendWebRequest();
            while (!www.isDone) { }
            bytes = www.downloadHandler.data;
        }
#else
        // 2. Read the raw bytes on Windows/Mac/iOS/Editor
        if (!File.Exists(filePath))
        {
            Debug.LogError($"CodexData.bin not found at: {filePath}");
            return;
        }
        byte[] bytes = File.ReadAllBytes(filePath);
#endif

        // 3. Mount the byte array into a FlatBuffers ByteBuffer
        ByteBuffer buffer = new ByteBuffer(bytes);

        // 4. Map the Root struct over the buffer (Instant, Zero Allocation!)
        RootData = CodexRoot.GetRootAsCodexRoot(buffer);

        Debug.Log($"[CodexDatabase] Loaded. Total Categories: {RootData.CategoriesLength}, Resources: {RootData.ResourcesLength}");
    }
    
    // Example access method
    public void PrintTopLevelEntries()
    {
        for (int i = 0; i < RootData.CategoriesLength; i++)
        {
            Category cat = RootData.Categories(i).GetValueOrDefault();
            Debug.Log($"Category: {cat.Name} has {cat.EntriesLength} Entries");
        }
    }
}
```

## 3. How to Query Data Efficiently

With FlatBuffers, memory is not "deserialized". The entire binary file acts as an array of structured bytes. Whenever you call a property like `entry.Title`, it resolves the string inline via an offset lookup.

**Good Practice Examples:**

```csharp
// Iterating through all resources
for (int i = 0; i < RootData.ResourcesLength; i++)
{
    Resource resource = RootData.Resources(i).GetValueOrDefault();
    if (resource.ResourceType == ResourceType.Image)
    {
        Debug.Log($"Found Image at ID: {resource.Id}, Path: {resource.FilePath}");
    }
}

// Fetching a specific category by Index
Category historyCategory = RootData.Categories(2).GetValueOrDefault();
for (int j = 0; j < historyCategory.EntriesLength; j++)
{
    Entry topLevelEntry = historyCategory.Entries(j).GetValueOrDefault();
    Debug.Log($"Entry Title: {topLevelEntry.Title}");

    // Fetching its sub-entries
    for (int k = 0; k < topLevelEntry.SubEntriesLength; k++)
    {
        Entry subEntry = topLevelEntry.SubEntries(k).GetValueOrDefault();
        Debug.Log($"  Sub-Entry: {subEntry.Title} - Sort: {subEntry.SortOrder}");
    }
}
```

## 4. Important Tips for Unity
- **Zero Allocations:** Looping through `Entities` generates zero garbage collection (GC) allocations since FlatBuffers use structs instead of classes.
- **Strings:** Fetching strings (like `entry.Title`) _will_ allocate a tiny amount of memory since Unity requires forming a new `System.String`. Cache strings locally if you plan to loop over and render them constantly on a UI update loop.
- **Enums:** Accessing enum types (`ResourceType`, `EntryType`) requires zero casting natively due to the FlatBuffers generator.
