using MySql.Data.MySqlClient;

namespace nonogram
{
    public static class DatabaseHelper
    {
        private static string connectionString = "Server=localhost;Database=nonogram;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
