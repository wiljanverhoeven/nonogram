using MySql.Data.MySqlClient;
using System;
using System.Data;

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
