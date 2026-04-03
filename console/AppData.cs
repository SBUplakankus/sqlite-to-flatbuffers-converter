namespace sqlite2fbs.console;

public static class AppData
{
    public const string AppName = "sqlite2fbs";
    public const string AppVersion = "1.0.0";
    public const string AppAuthor = "Seán Burke";
    public const string DatabaseName = "CodexDatabase.db";
    public const string OutputFileName = "CodexData.bin";
    
    public static string BasePath => AppDomain.CurrentDomain.BaseDirectory;
    public static string DatabasePath => Path.Combine(BasePath, DatabaseName);
    public static string OutputDirectory => Path.Combine(BasePath, "output");
    public static string OutputFilePath => Path.Combine(OutputDirectory, OutputFileName);
    
    public const int DefaultBufferSize = 4096;
}