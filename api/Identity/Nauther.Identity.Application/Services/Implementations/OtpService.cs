using Nauther.Framework.Infrastructure.Caching.RedisCache;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Infrastructure.DTOs;
using Nauther.Identity.Infrastructure.Services.OTP;

namespace Nauther.Identity.Application.Services.Implementations;

public class OtpService(
    IRedisCacheService redisCacheService,
    IOtpSender otpSender) : IOtpService
{
    private readonly IRedisCacheService _redisCacheService = redisCacheService;
    private readonly IOtpSender _otpSender = otpSender;

    public async Task SendOtpAsync(string phoneNumber)
    {
        var otp = GenerateOtp();
        var message = $"به سامانه یکپارچه نظام مبادلات پیمانکاری فرعی ایران خوش آمدید. کد یکبار مصرف: {otp}";

        await _otpSender.SendAsync(new SendSms()
        {
            Recipients = phoneNumber,
            Messages = message,
            UserId = 25
        });
        
        await _redisCacheService.AddAsync(phoneNumber, otp, TimeSpan.FromMinutes(5));
    }

    public async Task<bool> VerifyOtpAsync(string phoneNumber, string inputOtp)
    {
        var cachedOtp = await _redisCacheService.GetAsync<string>(phoneNumber);
        if (cachedOtp == null)
            return false;
        if (cachedOtp != inputOtp)
            return false;

        await _redisCacheService.RemoveAsync(phoneNumber);
        return true;
    }

    private string GenerateOtp()
    {
        var rnd = new Random();
        return rnd.Next(100000, 999999).ToString(); // 6-digit OTP
    }
}