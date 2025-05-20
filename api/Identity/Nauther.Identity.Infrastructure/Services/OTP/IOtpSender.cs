using Nauther.Identity.Infrastructure.DTOs;

namespace Nauther.Identity.Infrastructure.Services.OTP;

public interface IOtpSender
{
    Task SendAsync(SendSms request);
}