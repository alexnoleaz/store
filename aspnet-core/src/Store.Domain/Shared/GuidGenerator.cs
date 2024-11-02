using Store.Shared.Dependency;

namespace Store.Shared;

public class GuidGenerator : IGuidGenerator, ITransientDependency
{
    public virtual Guid Create() => Guid.NewGuid();
}
