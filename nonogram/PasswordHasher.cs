using BCrypt.Net;

public static class PasswordHasher
{
    private const int WorkFactor = 12;

    public static string HashPassword(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
            throw new ArgumentException("Password cannot be empty");

        // Fix: Use BCrypt.Net.BCrypt.HashPassword(plainPassword, WorkFactor)
        return BCrypt.Net.BCrypt.HashPassword(plainPassword, WorkFactor);
    }

    public static bool VerifyPassword(string plainPassword, string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword) || string.IsNullOrWhiteSpace(hashedPassword))
            return false;

        return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }
}