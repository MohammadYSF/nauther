namespace Nauther.Framework.Shared.ViewModels;

public abstract class BaseViewModel
{
    public Guid Id { get; set; }
}

public abstract class BaseViewModel<T>
{
    public T Id { get; set; }
}