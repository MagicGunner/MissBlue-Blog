namespace Backend.Common.Extensions;

public class FileUploadException : Exception {
    public FileUploadException(string message) : base(message) {
    }

    public FileUploadException(string message, Exception innerException) : base(message, innerException) {
    }
}