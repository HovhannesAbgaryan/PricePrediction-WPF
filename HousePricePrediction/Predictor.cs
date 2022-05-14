using HousePricePrediction.DAL;
using HousePricePrediction.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System;

namespace HousePricePrediction
{
    public static class Predictor
    {
        #region Consts
        
        /// <summary>
        /// Training data Id
        /// </summary>
        private const int trainingDataId = 1;

        /// <summary>
        /// Testing data Id
        /// </summary>
        private const int testingDataId = 2;

        #endregion Consts

        #region Fields

        /// <summary>
        /// Create MLContext
        /// </summary>
        private static readonly MLContext mlContext = new MLContext();

        /// <summary>
        /// Linear Regression Model
        /// </summary>
        private static TransformerChain<RegressionPredictionTransformer<LinearRegressionModelParameters>>? model;

        #endregion Fields

        #region Functions

        #region Model Training

        /// <summary>
        /// Train the model
        /// </summary>
        public static void Train()
        {
            try
            {
                // Collect, import or create training data
                var trainingHouseData = HouseDAC.GetData(trainingDataId);

                // Load training data into an IDataView object
                IDataView trainingData = mlContext.Data.LoadFromEnumerable(trainingHouseData);

                // Specify data preparation and model training pipeline of operations to extract features and apply a machine learning algorithm
                var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Size" })
                    .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));

                // Train model by calling Fit() on the pipeline
                model = pipeline.Fit(trainingData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Model Training

        #region Model Prediction

        /// <summary>
        /// Predict
        /// </summary>
        /// <param name="size">House size</param>
        /// <returns>Predicted price</returns>
        public static float Predict(float size)
        {
            try
            {
                // Collect, import or create prediction data
                var house = new HouseData { Size = size };

                // Create prediction engine
                var predictionEngine = mlContext.Model.CreatePredictionEngine<HouseData, Prediction>(model);

                // Make a prediction
                var prediction = predictionEngine.Predict(house);

                return prediction.Price;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Model Prediction

        #region Model Evaluation

        /// <summary>
        /// Evaluate the model and iterate to improve
        /// </summary>
        /// <returns>Regression metrics</returns>
        public static RegressionMetrics Evaluate()
        {
            try
            {
                // Collect, import or create testing data
                var testingHouseData = HouseDAC.GetData(testingDataId);

                // Load testing data into an IDataView object
                IDataView testingData = mlContext.Data.LoadFromEnumerable(testingHouseData);

                // Predict price for testing data
                IDataView testingPriceData = model.Transform(testingData);

                // Calculate metrics for Regression
                var metrics = mlContext.Regression.Evaluate(testingPriceData, labelColumnName: "Price");

                return metrics;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Model Evaluation

        #endregion Functions
    }
}
