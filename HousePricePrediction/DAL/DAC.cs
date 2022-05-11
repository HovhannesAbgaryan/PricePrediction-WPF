using System.Data.SqlClient;

namespace HousePricePrediction.DAL
{
    /// <summary>
    /// Base Data Access Class
    /// </summary>
    public abstract class DAC
    {
        /// <summary>
        /// SQL Connection String Builder
        /// </summary>
        protected static readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = @".\sqlexpress",
            InitialCatalog = "HousePriceDB",
            IntegratedSecurity = true
        };
    }
}
