using Store.Shared.Dependencies;

namespace Store.Shared;

/// <summary>
/// An implementation of <see cref="IGuidGenerator"/> that generates GUIDs using <see cref="Guid.NewGuid"/>.
/// </summary>
public class RegularGuidGenerator : IGuidGenerator, ISingletonDependency
{
    /// <inheritdoc />
    public Guid Create() => Guid.NewGuid();
}
