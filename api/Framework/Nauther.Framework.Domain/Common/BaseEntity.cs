namespace Nauther.Framework.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
}

public abstract class BaseEntity<T>
{
    public T Id { get; init; }
}