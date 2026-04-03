using Microsoft.Data.Sqlite;
using Dapper;

namespace sqlite2fbs.console;

public class DatabaseParser
{
    public List<CategoryRecord> Categories { get; private set; } = [];
    public List<ResourceRecord> Resources { get; private set; }  = [];
    public List<CodexEntryRecord>  CodexEntries { get; private set; }  = [];
    public List<RequirementRecord> Requirements { get; private set; }  = [];

    /// <summary>
    /// Parse the Data in the SQLite Database. Converts each table into a list of strongly-typed records for easy access during FlatBuffer conversion.
    /// </summary>
    public void Parse()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        
        using var connection = new SqliteConnection($"Data Source={AppData.DatabasePath}");
        connection.Open();
        
        Categories = connection.Query<CategoryRecord>("SELECT * FROM categories").ToList();
        Resources = connection.Query<ResourceRecord>("SELECT * FROM resources").ToList();
        CodexEntries = connection.Query<CodexEntryRecord>("SELECT * FROM codex_entries").ToList();
        Requirements = connection.Query<RequirementRecord>("SELECT * FROM requirements").ToList();
        
        connection.Close();
    }
}