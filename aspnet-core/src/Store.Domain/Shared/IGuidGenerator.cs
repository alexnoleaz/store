namespace Store.Shared;

/// <summary>
/// Used to generate unique identifiers.
/// </summary>
public interface IGuidGenerator
{
    /// <summary>
    /// Generates a new Guid.
    /// </summary>
    Guid Create();
}
