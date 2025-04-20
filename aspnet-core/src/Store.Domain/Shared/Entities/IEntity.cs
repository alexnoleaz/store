namespace Store.Shared.Entities;

public interface IEntity<TPrimaryKey>
{
    TPrimaryKey Id { get; set; }

    bool IsTransient();
}

public interface IEntity : IEntity<int> { }