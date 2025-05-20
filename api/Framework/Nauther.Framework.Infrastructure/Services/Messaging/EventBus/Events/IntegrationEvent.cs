using Newtonsoft.Json;

namespace Nauther.Framework.Infrastructure.Services.Messaging.EventBus.Events;

public class IntegrationEvent
{
    [JsonProperty] public Guid Id { get; private set; }
    [JsonProperty] public DateTime CreationDate { get; private set; }

    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    [JsonConstructor]
    public IntegrationEvent(Guid id, DateTime creationDate)
    {
        Id = id;
        CreationDate = creationDate;
    }
}