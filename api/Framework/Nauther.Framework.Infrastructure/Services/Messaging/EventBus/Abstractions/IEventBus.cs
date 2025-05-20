using Nauther.Framework.Infrastructure.Services.Messaging.EventBus.Events;

namespace Nauther.Framework.Infrastructure.Services.Messaging.EventBus.Abstractions;

public interface IEventBus //TODO : move into job microservice
{
    void Publish(IntegrationEvent @event);

    void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    void SubscribeDynamic<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;

    void UnsubscribeDynamic<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;
    
    void Unsubscribe<T, TH>()
        where TH : IIntegrationEventHandler<T>
        where T : IntegrationEvent;
}