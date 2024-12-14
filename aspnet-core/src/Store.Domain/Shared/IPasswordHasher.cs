namespace Store.Shared;

/// <summary>
/// Provides functionality for hashing passwords and verifying hashed passwords.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the provided password using a secure hashing algorithm.
    /// </summary>
    /// <param name="password">The plaintext password to hash.</param>
    /// <returns>A hashed representation of the password.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="password"/> is <c>null</c>.
    /// </exception>
    string HashPassword(string password);

    /// <summary>
    /// Verifies a hashed password against a provided plaintext password.
    /// </summary>
    /// <param name="hashedPassword">The hashed password to compare.</param>
    /// <param name="providedPassword">The plaintext password to verify.</param>
    /// <returns><c>true</c> if the provided password matches the hashed password; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="hashedPassword"/> or <paramref name="providedPassword"/> is <c>null</c>.
    /// </exception>
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}
