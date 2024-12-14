using Store.Shared.Dependency;

namespace Store.Shared;

/// <summary>
/// Provides an implementation of <see cref="IPasswordHasher"/> using the BCrypt algorithm.
/// </summary>
public class BCryptPasswordHasher : IPasswordHasher, ISingletonDependency
{
    /// <inheritdoc />
    public string HashPassword(string password)
    {
        ArgumentNullException.ThrowIfNull(password, nameof(password));
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <inheritdoc />
    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        ArgumentNullException.ThrowIfNull(hashedPassword, nameof(hashedPassword));
        ArgumentNullException.ThrowIfNull(providedPassword, nameof(providedPassword));
        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}
