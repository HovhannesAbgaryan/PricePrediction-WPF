using System;
using System.Data;
using System.Data.SqlClient;

namespace HousePricePrediction.DAL
{
    /// <summary>
    /// Login Data Access Class
    /// </summary>
    public class LoginDAC : DAC
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>User's RoleId, if user with given Username/Password pair exists; otherwise - 0</returns>
        public static int Login(string username, string password)
        {
            try
            {
                using (var connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    // "Login" is a stored procedure in HousePriceDB.
                    using (var command = new SqlCommand("Login", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("Username", username);
                        command.Parameters.AddWithValue("Password", password);

                        int roleId = Convert.ToInt32(command.ExecuteScalar());

                        return roleId;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
