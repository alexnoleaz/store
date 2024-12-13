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
    string HashPassword(string password);

    /// <summary>
    /// Verifies a hashed password against a provided plaintext password.
    /// </summary>
    /// <param name="hashedPassword">The hashed password to compare.</param>
    /// <param name="providedPassword">The plaintext password to verify.</param>
    /// <returns><c>true</c> if the provided password matches the hashed password; otherwise, <c>false</c>.</returns>
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}