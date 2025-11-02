using MySql.Data.MySqlClient;
using System.Data;

namespace nonogram
{
    public class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection()
        {
            // Update with your actual database credentials
            _connectionString = "server=localhost;database=nonogram;uid=root;pwd=;";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // Method to execute queries that return data
        public DataTable ExecuteQuery(string query, params MySqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                connection.Open();

                using (var adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

        // Method to execute commands that don't return data
        public int ExecuteNonQuery(string commandText, params MySqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(commandText, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        // Method to get a single value
        public object ExecuteScalar(string commandText, params MySqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(commandText, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                connection.Open();
                return command.ExecuteScalar();
            }
        }
    }
}