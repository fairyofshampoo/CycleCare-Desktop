using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace CycleCare.Views
{
    /// <summary>
    /// Interaction logic for ForgotPasswordView.xaml
    /// </summary>
    public partial class ForgotPasswordView : Page
    {
        public ForgotPasswordView()
        {
            InitializeComponent();
        }

        private void BtnSendMail_Click(object sender, RoutedEventArgs e)
        {
            if (IsEmailValid(txtMail.Text))
            {

                SendMail(txtMail.Text);
            }
            else
            {
                DialogManager.ShowErrorMessageBox("El correo ingresado no es válido.");
            }
        }

        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var emailRegex = new System.Text.RegularExpressions.Regex(
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                return emailRegex.IsMatch(email);
            }
            catch (Exception)
            {
                return false;
            }
        }


        private async void SendMail(string email)
        {
            Response response = await UserService.RequestResetPassword(email);
            switch (response.Code)
            {
                case (int)HttpStatusCode.Created:
                    codeStackPanel.Visibility = Visibility.Visible;
                    mailStackPanel.Visibility = Visibility.Collapsed;
                    break;
                case(int)HttpStatusCode.NotFound:
                    DialogManager.ShowWarningMessageBox("No se encontró el email ingresado. Intenta nuevamente");
                    break;
            }
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnConfirmCode_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateCode(txtCode.Text))
            {
                codeStackPanel.Visibility = Visibility.Collapsed;
                passwordStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                DialogManager.ShowWarningMessageBox("El código ingresado no es válido.");
            }
        }

        private bool ValidateCode(string text)
        {
            return !string.IsNullOrEmpty(text) && text.Length <= 8;
        }

        private void BtnResendCode_Click(object sender, MouseButtonEventArgs e)
        {
            SendMail(txtMail.Text);
        }

        private void BtnSavePassword_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePasswords(txtPassword.Text, txtConfirmPassword.Text))
            {
                UpdatePassword();
            }
        }

        private async void UpdatePassword()
        {
            string passwordHashed = EncriptionUtil.ToSHA2Hash(txtPassword.Text);
            string confirmPasswordHashed = EncriptionUtil.ToSHA2Hash(txtConfirmPassword.Text);
            var request = new ChangePasswordRequest()
            {
                NewPassword = passwordHashed,
                ConfirmPassword = confirmPasswordHashed,
                Token = txtCode.Text
            };

            Response response = await UserService.UpdatePassword(txtMail.Text, request);
            switch (response.Code)
            {
                case (int)HttpStatusCode.OK:
                    DialogManager.ShowSuccessMessageBox("Contraseña actualizada correctamente");
                    NavigationService.GoBack();
                    break;
                case (int)HttpStatusCode.BadRequest:
                    DialogManager.ShowWarningMessageBox("Las contraseñas no coinciden");
                    break;
                case(int)HttpStatusCode.NotFound:
                    DialogManager.ShowWarningMessageBox("No se encontró el email ingresado. Intenta nuevamente");
                    break;
                case(int)HttpStatusCode.Unauthorized:
                    DialogManager.ShowWarningMessageBox("El código ingresado es inválido o ha experidado. Intenta nuevamente");
                    break;
            }
        }

        private bool ValidatePasswords(string txtPassword, string txtPasswordConfirm)
        {
            if (string.IsNullOrEmpty(txtPassword) || txtPassword.Length < 8)
            {
                return false;
            }

            if (txtPassword != txtPasswordConfirm)
            {
                return false;
            }

            var passwordRegex = new System.Text.RegularExpressions.Regex(
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$");

            return passwordRegex.IsMatch(txtPassword);
        }
    }
}
