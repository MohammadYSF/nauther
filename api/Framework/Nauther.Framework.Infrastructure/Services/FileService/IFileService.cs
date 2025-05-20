using Nauther.Framework.Infrastructure.Common.DTOs;

namespace Nauther.Framework.Infrastructure.Services.FileService;

public interface IFileService
{
    Task<FileResponse<GetFileResult>> GetFileContentAsync(GetFileDto request);
    Task<FileResponse<AddFileResult>> AddFileAsync(AddFileDto request);
}