using HousePricePrediction.DAL;
using System;
using System.Windows;

namespace HousePricePrediction.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region Consts

        /// <summary>
        /// Default text of Username TextBox 
        /// </summary>
        private const string usernameTextBoxDefaultText = "Username";

        /// <summary>
        /// Default text of PasswordBox
        /// </summary>
        private const string passwordBoxDefaultText = "Password";

        #endregion Consts

        #region Constructors

        public LoginWindow()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Events

        #region Username TextBox

        private void usernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            usernameTextBox.Text = usernameTextBox.Text == usernameTextBoxDefaultText ? string.Empty : usernameTextBox.Text;
        }

        private void usernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            usernameTextBox.Text = usernameTextBox.Text == string.Empty ? usernameTextBoxDefaultText : usernameTextBox.Text;
        }

        #endregion Username TextBox

        #region PasswordBox

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = passwordBox.Password == passwordBoxDefaultText ? string.Empty : passwordBox.Password;
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = passwordBox.Password == string.Empty ? passwordBoxDefaultText : passwordBox.Password;
        }

        #endregion PasswordBox

        #region Login

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int roleId = LoginDAC.Login(usernameTextBox.Text, passwordBox.Password);

                if (roleId == 0)
                    MessageBox.Show("User is not found.", "Login", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else
                {
                    var predictionWindow = new PredictionWindow(roleId);
                    predictionWindow.Show();
                    Close();
                }
            }
            catch (Exception)
            {
                throw;
            }  
        }

        #endregion Login

        #endregion Events
    }
}
