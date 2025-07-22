namespace Backend.Common.Record;

public record UploadRule(string       Dir,
                         string       Description,
                         List<string> Formats,
                         double       LimitSizeMB);