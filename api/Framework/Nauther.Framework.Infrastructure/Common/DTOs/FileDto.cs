namespace Nauther.Framework.Infrastructure.Common.DTOs;

public class AddFileDto
{
    public string FileName { get; set; }
    public string FileData { get; set; }
    public int FileTypeId { get; set; }
    public DateTime? ExpiryDate { get; set; }
}

public class FileResponse<T> where T : class
{
    public T Result { get; set; }
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public List<string>? ValidationErrors { get; set; } = new();
}

public class AddFileResult
{
    public Guid Id { get; set; }
}


public class GetFileDto
{
    public Guid Id { get; set; }
}

public class GetFileResult
{
    public string Id { get; set; }
    public string FileId { get; set; }
    public string FileData { get; set; }
    public int FileTypeId { get; set; }
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public string MimeType { get; set; }
    public string FileLength { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public DateTime SendDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? Description { get; set; }
}