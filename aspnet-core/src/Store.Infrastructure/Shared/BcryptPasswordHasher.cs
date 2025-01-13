using Store.Shared.Dependencies;
using Store.Shared.Validations;

namespace Store.Shared;

/// <summary>
/// An implementation of <see cref="IPasswordHasher"/> using <see cref="BCrypt"/> for secure password hashing.
/// </summary>
public class BcryptPasswordHasher : IPasswordHasher, ISingletonDependency
{
    /// <inheritdoc />
    public string HashPassword(string password)
    {
        ArgumentValidator.NotNull(password);
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <inheritdoc />
    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        ArgumentValidator.NotNull(hashedPassword);
        ArgumentValidator.NotNull(providedPassword);

        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}
