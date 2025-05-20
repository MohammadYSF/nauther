namespace Nauther.Framework.Infrastructure.Services.Messaging.EventBus.Abstractions;

public interface IDynamicIntegrationEventHandler
{
    Task Handle(dynamic eventData);
}