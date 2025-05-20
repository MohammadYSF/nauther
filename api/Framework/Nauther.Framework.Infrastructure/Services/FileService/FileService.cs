using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Nauther.Framework.Infrastructure.Common.Constants;
using Nauther.Framework.Infrastructure.Common.DTOs;
using Nauther.Framework.Infrastructure.Common.Models;

namespace Nauther.Framework.Infrastructure.Services.FileService;

public class FileService(IHttpClientFactory httpClientFactory, IOptionsMonitor<FileSettings> fileSettings) : IFileService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HttpClientNames.ThirdPartyService);
    private readonly FileSettings _fileSettings = fileSettings.CurrentValue;

    public async Task<FileResponse<GetFileResult>> GetFileContentAsync(GetFileDto request)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_fileSettings.Url}/api/Files/GetFile?id={request.Id}");
        requestMessage.Headers.Add("apikey", _fileSettings.ApiKey);
        
        var response = await _httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<FileResponse<GetFileResult>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return new FileResponse<GetFileResult>()
        {
            Result = result.Result,
            Success = result.Success,
            Message = result.Message,
            StatusCode = result.StatusCode,
            ValidationErrors = result.ValidationErrors
        };
    }

    public async Task<FileResponse<AddFileResult>> AddFileAsync(AddFileDto request)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_fileSettings.Url}/api/Files/AddFile");
        requestMessage.Headers.Add("apikey", _fileSettings.ApiKey);
        
        var jsonContent = JsonSerializer.Serialize(request);
        requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<FileResponse<AddFileResult>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return new FileResponse<AddFileResult>()
        {
            Result = result.Result,
            Success = result.Success,
            Message = result.Message,
            StatusCode = result.StatusCode,
            ValidationErrors = result.ValidationErrors
        };
    }
}