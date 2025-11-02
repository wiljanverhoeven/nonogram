using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using BCrypt.Net;

namespace nonogram
{
    public static class UserService
    {
        private static DbConnection _db = new DbConnection();
        private const int WorkFactor = 12;

        public static bool Register(string username, string password, string email)
        {
            string checkQuery = "SELECT COUNT(*) FROM user WHERE username = @username";
            var checkParam = new MySqlParameter("@username", username);

            int existingCount = Convert.ToInt32(_db.ExecuteScalar(checkQuery, checkParam));

            if (existingCount > 0)
                return false;

            string hashedPassword = HashPassword(password);

            string insertQuery = @"
                INSERT INTO user (username, email, password) 
                VALUES (@username, @email, @password)";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@username", username),
                new MySqlParameter("@email", email),
                new MySqlParameter("@password", hashedPassword)
            };

            int rowsAffected = _db.ExecuteNonQuery(insertQuery, parameters);
            return rowsAffected > 0;
        }

        public static bool Login(string username, string password)
        {
            string query = "SELECT password FROM user WHERE username = @username";
            var parameter = new MySqlParameter("@username", username);

            var result = _db.ExecuteScalar(query, parameter);

            if (result == null)
                return false;

            string storedHash = result.ToString();
            return VerifyPassword(password, storedHash);
        }

        public static User GetUser(string username)
        {
            string query = "SELECT userId, username, email FROM user WHERE username = @username";
            var parameter = new MySqlParameter("@username", username);

            var result = _db.ExecuteQuery(query, parameter);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new User
                {
                    UserId = Convert.ToInt32(row["userId"]),
                    Username = row["username"].ToString(),
                    Email = row["email"].ToString()
                };
            }

            return null;
        }

        private static string HashPassword(string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentException("Password cannot be empty");

            return BCrypt.Net.BCrypt.HashPassword(plainPassword, WorkFactor);
        }

        private static bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword) || string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }

        public static void MigrateExistingPasswords()
        {
            try
            {
                string selectQuery = "SELECT userId, password FROM user WHERE LENGTH(password) < 50 AND password NOT LIKE '$2%'";
                var result = _db.ExecuteQuery(selectQuery);

                foreach (System.Data.DataRow row in result.Rows)
                {
                    int userId = Convert.ToInt32(row["userId"]);
                    string plainPassword = row["password"].ToString();

                    string hashedPassword = HashPassword(plainPassword);

                    string updateQuery = "UPDATE user SET password = @password WHERE userId = @userId";
                    var parameters = new MySqlParameter[]
                    {
                        new MySqlParameter("@password", hashedPassword),
                        new MySqlParameter("@userId", userId)
                    };

                    _db.ExecuteNonQuery(updateQuery, parameters);
                    Console.WriteLine($"Migrated password for user ID: {userId}");
                }

                Console.WriteLine("Password migration completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Migration error: {ex.Message}");
            }
        }
    }
}