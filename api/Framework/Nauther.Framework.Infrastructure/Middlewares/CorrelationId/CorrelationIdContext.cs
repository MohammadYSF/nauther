namespace Nauther.Framework.Infrastructure.Middlewares.CorrelationId;

public static class CorrelationIdContext
{
    private static readonly AsyncLocal<string> _correlationId = new AsyncLocal<string>();

    public static string CorrelationId
    {
        get => _correlationId.Value;
        set => _correlationId.Value = value;
    }
}