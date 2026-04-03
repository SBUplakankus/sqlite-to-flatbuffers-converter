namespace sqlite2fbs.console;

public record CategoryRecord (
    long Id,
    string Name,
    long SortOrder);

public record CodexEntryRecord(
    long Id,
    long CategoryId,
    long? ParentEntryId,
    long? RequirementId,
    long EntryType,
    string Title,
    string Content,
    long? AudioId,
    long? ImageId,
    long SortOrder);

public record RequirementRecord(
    long Id,
    string Description);

public record ResourceRecord(
    long Id,
    string? FilePath,
    long ResourceType,
    string? Url,
    string? TtsMetadata);
