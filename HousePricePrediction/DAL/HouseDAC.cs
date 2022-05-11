using HousePricePrediction.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HousePricePrediction.DAL
{
    /// <summary>
    /// House Data Access Class
    /// </summary>
    internal class HouseDAC : DAC
    {
        /// <summary>
        /// Get house data
        /// </summary>
        /// <param name="typeId">House data type Id</param>
        /// <returns>House data</returns>
        internal static List<HouseData> GetData(int typeId)
        {
            var houseData = new List<HouseData>();

            try
            {
                using (var connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT [Size], [Price] " +
                                   "FROM [HousePriceDB].[dbo].[HouseData] " +
                                   "WHERE [TypeId] = @TypeId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("TypeId", typeId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                houseData.Add(new HouseData
                                {
                                    Size = Convert.ToSingle(reader["Size"]),
                                    Price = Convert.ToSingle(reader["Price"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return houseData;
        }
    }
}
