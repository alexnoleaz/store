namespace Store.Shared;

/// <summary>
/// Defines a contract for hashing passwords and verifying hashed passwords.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the provided password using a secure hashing algorithm.
    /// </summary>
    /// <param name="password">The password to be hashed.</param>
    /// <returns>A hashed version of the password.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="password"/> is null.</exception>
    string HashPassword(string password);

    /// <summary>
    /// Verifies if the provided password matches the hashed password.
    /// </summary>
    /// <param name="hashedPassword">The hashed password to verify against.</param>
    /// <param name="providedPassword">The plain text password to check.</param>
    /// <returns><c>true</c> if the provided password matches the hashed password, otherwise <c>false</c>.</returns>
    /// <exception>Thrown when <paramref name="hashedPassword"/> or <paramref name="providedPassword"/> is null.</exception>
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}