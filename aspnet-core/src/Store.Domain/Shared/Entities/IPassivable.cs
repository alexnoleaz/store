namespace Store.Shared.Entities;

public interface IPassivable
{
    bool IsActive { get; set; }
}
