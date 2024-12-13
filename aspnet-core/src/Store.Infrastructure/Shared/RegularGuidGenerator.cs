using Store.Shared.Dependency;

namespace Store.Shared;

/// <summary>
/// Generates Guids using the default <see cref="Guid.NewGuid"/> method.
/// </summary>
public class RegularGuidGenerator : IGuidGenerator, ISingletonDependency
{
    /// <inheritdoc />
    public virtual Guid Create() => Guid.NewGuid();
}
