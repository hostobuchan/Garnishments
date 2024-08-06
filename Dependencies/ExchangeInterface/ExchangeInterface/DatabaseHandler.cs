using System;
using System.Data.SqlClient;

namespace ExchangeInterface
{
    public static class DatabaseHandler
    {
        private static string ConnectionString = "Data Source=SQL1;Initial Catalog=IT;Integrated Security=True";

        public static string ReadProtectedString(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT PasswordHash FROM dbo.RefreshCredentials WHERE Email = @Email", connection);
                    command.Parameters.AddWithValue("@Email", email);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader["PasswordHash"].ToString();
                    }
                    else
                    {
                        throw new Exception($"No data found for email: {email}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }

        public static bool WriteProtectedString(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("IF EXISTS (SELECT 1 FROM dbo.RefreshCredentials WHERE Email = @Email) " +
                                                        "UPDATE dbo.RefreshCredentials SET PasswordHash = @PasswordHash WHERE Email = @Email " +
                                                        "ELSE " +
                                                        "INSERT INTO dbo.RefreshCredentials (Email, PasswordHash) VALUES (@Email, @PasswordHash)", connection);

                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHash", password);

                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"General error: {e.Message}");
                return false;
            }
        }
    }
}
