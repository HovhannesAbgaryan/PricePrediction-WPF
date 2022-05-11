using System;
using System.Windows;

namespace HousePricePrediction.UI
{
    /// <summary>
    /// Interaction logic for PredictionWindow.xaml
    /// </summary>
    public partial class PredictionWindow : Window
    {
        #region Consts

        /// <summary>
        /// Default text of house size TextBox
        /// </summary>
        private const string houseSizeTextBoxDefaultText = "House size";
        
        #endregion Consts

        #region Fields

        /// <summary>
        /// User's RoleId
        /// </summary>
        private readonly int roleId;

        #endregion Fields

        #region Constructors

        public PredictionWindow()
        {
            InitializeComponent();
        }

        public PredictionWindow(int roleId) : this()
        {
            this.roleId = roleId;
            SetPanelVisibilities();
        }

        #endregion Constructors

        #region Functions

        /// <summary>
        /// Set visibilities of StackPanels on window
        /// </summary>
        private void SetPanelVisibilities()
        {
            switch (roleId)
            {
                case 1: // For admin: all StackPanels are visible 
                    trainingPanel.Visibility = Visibility.Visible;
                    predictionPanel.Visibility = Visibility.Visible;
                    evaluationPanel.Visibility = Visibility.Visible;
                    break;

                case 2: // For user: training and evaluation StackPanels are hidden, prediction StackPanel is visible
                    trainingPanel.Visibility = Visibility.Hidden;
                    predictionPanel.Visibility = Visibility.Visible;
                    evaluationPanel.Visibility = Visibility.Hidden;
                    break;

                default: // By default: all StackPanels are hidden
                    trainingPanel.Visibility = Visibility.Hidden;
                    predictionPanel.Visibility = Visibility.Hidden;
                    evaluationPanel.Visibility = Visibility.Hidden;
                    break;
            }
        }

        #endregion Functions

        #region Events

        #region Model Training

        private void train_Click(object sender, RoutedEventArgs e)
        {
            string caption = "Model Training";

            try
            {
                Predictor.Train();
                MessageBox.Show("The model has been trained successfully.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred when training the model.", caption, MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        #endregion Model Training

        #region Model Prediction

        #region House size TextBox

        private void houseSizeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            houseSizeTextBox.Text = houseSizeTextBox.Text == houseSizeTextBoxDefaultText ? string.Empty : houseSizeTextBox.Text;
        }

        private void houseSizeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            houseSizeTextBox.Text = houseSizeTextBox.Text == string.Empty ? houseSizeTextBoxDefaultText : houseSizeTextBox.Text;
        }

        #endregion House size TextBox

        private void predict_Click(object sender, RoutedEventArgs e)
        {
            string caption = "Model Prediction";

            try
            {
                float houseSize = Convert.ToSingle(houseSizeTextBox.Text);
                float predictedPrice = Predictor.Predict(houseSize);
                MessageBox.Show($"Predicted price for size: {houseSize * 1000} sq ft = {predictedPrice * 100:C}k.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred when predicting.", caption, MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        #endregion Model Prediction

        #region Model Evaluation

        private void evaluate_Click(object sender, RoutedEventArgs e)
        {
            string caption = "Model Evaluation";

            try
            {
                var metrics = Predictor.Evaluate();
                MessageBox.Show($"RSquared: {metrics.RSquared:0.##}. Root Mean Squared Error: {metrics.RootMeanSquaredError:0.##}", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred when evaluating the model.", caption, MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        #endregion Model Evaluation

        #region Log out

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion Log out

        #endregion Events
    }
}
