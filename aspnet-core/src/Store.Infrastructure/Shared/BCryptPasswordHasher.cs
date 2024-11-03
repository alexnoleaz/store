using Store.Shared.Dependency;

namespace Store.Shared;

/// <summary>
/// Implements <see cref="IPasswordHasher"/> by using <see cref="BCrypt.Net.BCrypt"/>.
/// </summary>
public class BCryptPasswordHasher : IPasswordHasher, ITransientDependency
{
    /// <inheritdoc />
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    /// <inheritdoc />
    public bool VerifyHashedPassword(string hashedPassword, string providedPassword) =>
        BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
}
