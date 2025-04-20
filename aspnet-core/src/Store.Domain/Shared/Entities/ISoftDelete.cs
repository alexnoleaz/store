namespace Store.Shared.Entities;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
