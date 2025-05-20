namespace Nauther.Framework.Shared.ViewModels;

public abstract class AuditableViewModel : BaseViewModel
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}