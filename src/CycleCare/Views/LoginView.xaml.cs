using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CycleCare.Utilities;
using CycleCare.Models;
using CycleCare.Service;
using System.Configuration;
using System.Reflection;

namespace CycleCare.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            HandleLoginAttempt();
        }

        private void DisplayMainMenuView()
        {
            DialogManager.ShowSuccessMessageBox("Bienvenido tonoto");
        }


        private void HandleLoginAttempt()
        {
            if (VerifyFields())
            {

                string username = txtUsername.Text;
                string password = GetPassword();
                string passwordHashed = EncriptionUtil.ToSHA2Hash(password);
                //LUEGO DEBO MANDAR PASSWORD HASHED

                var user = new User()
                {
                    Username = username,
                    Password = password
                };

                RequestLogin(user);
            }
            else
            {
                DialogManager.ShowWarningMessageBox("Los datos ingresados son erroneos");
            }
        }

        private async void RequestLogin(User user)
        {
            Response response = await UserService.Login(user);
            switch (response.Code)
            {
                case (int)HttpStatusCode.Created:
                    SaveToken(response.Token);
                    DisplayMainMenuView();
                    break;
                case (int)HttpStatusCode.BadRequest:
                    DialogManager.ShowWarningMessageBox("Usuario o contraseña incorrectos. Revisa tus credenciales.");
                    break;
            }
        }

        private void SaveToken(string tokenValue)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            KeyValueConfigurationElement token = configuration.AppSettings.Settings["TOKEN"];
            token.Value = tokenValue;
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private bool VerifyFields()
        {
            string username = txtUsername.Text;
            string password = GetPassword();
            bool passwordValidation = VerifyPassword(password);
            bool gamerTagValidation = VerifyUsername(username);

            return passwordValidation && gamerTagValidation;
        }

        private bool VerifyUsername(string username)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(username))
            {
                isValid = false;
            }

            return isValid;
        }

        private bool VerifyPassword(string password)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(password))
            {
                isValid = false;
            }

            return isValid;
        }

        private string GetPassword()
        {
            bool isChecked = tgbtnPasswordVisibility.IsChecked ?? false;
            string password = txtPasswordDisplay.Text;

            if (!isChecked)
            {
                SecureString passwordToAccess = pwbPassword.SecurePassword;
                password = new NetworkCredential(string.Empty, passwordToAccess).Password;
            }

            return password;
        }

        private void TgbtnPasswordVisibility_Checked(object sender, RoutedEventArgs e)
        {
            TogglePasswordVisibility(true);
        }

        private void TgbtnPasswordVisibility_Unchecked(object sender, RoutedEventArgs e)
        {
            TogglePasswordVisibility(false);
        }

        private void TogglePasswordVisibility(bool isVisible)
        {
            if (isVisible)
            {
                gridPassword.Visibility = Visibility.Collapsed;
                gridPasswordDisplay.Visibility = Visibility.Visible;
                gridPasswordDisplay.IsEnabled = true;
            }
            else
            {
                gridPassword.Visibility = Visibility.Visible;
                gridPasswordDisplay.Visibility = Visibility.Collapsed;
                gridPasswordDisplay.IsEnabled = false;
            }

            if (isVisible)
            {
                txtPasswordDisplay.Text = pwbPassword.Password;
                SetPasswordIcon("InvisibleIcon.png");
            }
            else
            {
                pwbPassword.Password = txtPasswordDisplay.Text;
                SetPasswordIcon("VisibleIcon.png");
            }
        }

        private void SetPasswordIcon(string iconPassword)
        {
            try
            {
                Image imgPasswordIcon = tgbtnPasswordVisibility.Template.FindName("imgPasswordIcon", tgbtnPasswordVisibility) as Image;

                if (imgPasswordIcon != null)
                {
                    imgPasswordIcon.Source = new BitmapImage(new Uri($"/Views/Resources/Icons/{iconPassword}", UriKind.Relative));
                }
            }
            catch (UriFormatException)
            {
                DialogManager.ShowErrorMessageBox("No se pudo encontrar el archivo de iconos para contraseña");
            }
        }

        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void SignUp_Click(object sender, MouseButtonEventArgs e)
        {
            SignUpView signUpView = new SignUpView();
            NavigationService.Navigate(signUpView);
        }
    }
}
