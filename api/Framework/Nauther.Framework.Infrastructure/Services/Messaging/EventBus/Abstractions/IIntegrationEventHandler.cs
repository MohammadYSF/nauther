using Nauther.Framework.Infrastructure.Services.Messaging.EventBus.Events;

namespace Nauther.Framework.Infrastructure.Services.Messaging.EventBus.Abstractions;

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}

public interface IIntegrationEventHandler
{
}