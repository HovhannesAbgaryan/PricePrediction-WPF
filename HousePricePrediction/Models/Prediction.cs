using Microsoft.ML.Data;

namespace HousePricePrediction.Models
{
    /// <summary>
    /// Prediction
    /// </summary>
    public class Prediction
    {
        /// <summary>
        /// Predicted price
        /// </summary>
        [ColumnName("Score")]
        public float Price { get; set; }
    }
}
