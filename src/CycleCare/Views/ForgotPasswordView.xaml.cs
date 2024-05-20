using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var user = new User()
                {
                    Email = txtMail.Text
                };

                SendMail(user);
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


        private async void SendMail(User user)
        {
            Response response = await UserService.Login(user);
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnConfirmCode_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnResendCode_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnSavePassword_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
