using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Nauther.Framework.Infrastructure.Common.Constants;
using Nauther.Framework.Infrastructure.Common.Models;
using Nauther.Identity.Infrastructure.DTOs;

namespace Nauther.Identity.Infrastructure.Services.OTP;

public class OtpSender(
    IHttpClientFactory httpClientFactory) : IOtpSender
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HttpClientNames.ThirdPartyService);

    public async Task SendAsync(SendSms request)
    {
        throw new NotImplementedException();
    }
}