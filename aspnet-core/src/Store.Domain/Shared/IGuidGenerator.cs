namespace Store.Shared;

/// <summary>
/// Defines a contract for generating GUIDs.
/// </summary>
public interface IGuidGenerator
{
    /// <summary>
    /// Generates a new GUID.
    /// </summary>
    /// <returns>A new GUID.</returns>
    Guid Create();
}
