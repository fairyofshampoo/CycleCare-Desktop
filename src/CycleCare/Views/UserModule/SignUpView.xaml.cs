using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CycleCare.Views
{
    public partial class SignUpView : Page
    {

        public SignUpView()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            clearErrorLabels();
            List<int> errors = isDataValid();
            if (errors.Any())
            {
                showErrors(errors);
            } else
            {
                Account newAccount = createAccount();
                RequestAccounCreation(newAccount);
            }
        }

        private async void RequestAccounCreation(Account newAccount)
        {
            Response response = await UserService.RegisterAccount(newAccount);

            switch (response.Code)
            {
                case (int)HttpStatusCode.Created:
                    DialogManager.ShowSuccessMessageBox("Se ha creado la cuenta correctamente");
                    break;
                case (int)HttpStatusCode.BadRequest:
                    DialogManager.ShowWarningMessageBox("El usuario ya existe o correo ya está siendo usado, pruebe con otro.");
                    break;
                case (int)HttpStatusCode.InternalServerError:
                    DialogManager.ShowErrorMessageBox("Error interno del servidor. Inténtalo más tarde.");
                    break;
            }
        }

        private Account createAccount()
        {
            Account account = new Account();
            account.name = txtName.Text;
            account.firstLastName = txtFirstLastName.Text;
            account.secondLastName = txtSecondLastName.Text;
            account.email = txtEmail.Text;
            string passwordHashed = EncriptionUtil.ToSHA2Hash(txtPassword.Text);
            account.password = passwordHashed;
            account.role = "USER";
            account.aproxCycleDuration = stringToNumber(txtCycleDuration.Text);
            account.aproxPeriodDuration = stringToNumber(txtPeriodDuration.Text);
            account.username = txtUsername.Text;
            account.isRegular = 1;
            return account;
        }

        private int stringToNumber(string number)
        {
            int newNumber = -1;
            try
            {
                 newNumber = int.Parse(number);
            }
            catch (FormatException)
            {
                Console.WriteLine("La cadena no es un formato válido de número.");
            }
            catch (OverflowException)
            {
                DialogManager.ShowWarningMessageBox("El número que ingresaste dentro de la duración de tu periodo o ciclo es muy grande.");
            }
            return newNumber;
        }

        private List<int> isDataValid()
        {
            List<int> errors = new List<int>();

            if (!ValidatePassword())
            {
                errors.Add(0);
            }

            if (!ValidatePersonalData())
            {
                errors.Add(1);
            }

            if(!ValidateEmail())
            {
                errors.Add(2);
            }

            if(!ValidateUser())
            {
                errors.Add(3);
            }

            if(!ValidateComboBox())
            {
                errors.Add(4);
            }

            if (!ValidDuration())
            {
                errors.Add(5);
            }

            if(!ValidaCycleDuration())
            {
                errors.Add(6);
            }

            return errors;
        }

        private void clearErrorLabels()
        {
            lblErrorPassword.Visibility = Visibility.Collapsed;
            lblNamesError.Visibility = Visibility.Collapsed;
            lblEmaiError.Visibility = Visibility.Collapsed;
            lblErrorUser.Visibility = Visibility.Collapsed;
            lblCmbError.Visibility = Visibility.Collapsed;
            lblNumberError.Visibility = Visibility.Collapsed;
        }

        private void showErrors(List<int> errors)
        {
            foreach (int error in errors)
            {
                switch (error) 
                { 
                    case 0:
                        lblErrorPassword.Visibility = Visibility.Visible;
                        break;

                    case 1:
                        lblNamesError.Visibility = Visibility.Visible;
                        break;
                    
                    case 2:
                        lblEmaiError.Visibility = Visibility.Visible;
                        break;
                    
                    case 3:
                        lblErrorUser.Visibility = Visibility.Visible;
                        break;
                    
                    case 4:
                        lblCmbError.Visibility = Visibility.Visible;    
                        break;
                    
                    case 5:
                        lblNumberError.Visibility = Visibility.Visible;
                        break;
                    
                    case 6:
                        lblNumberError.Visibility= Visibility.Visible;
                        break;


                }
            }
        }

        private bool ValidatePassword()
        {
            return ValidationManager.IsPasswordValid(txtPassword.Text);
        }

        private bool ValidatePersonalData()
        {
            string name = txtName.Text;
            string firstLastName = txtFirstLastName.Text;
            string secondLastName = txtSecondLastName.Text;

            bool isNameValid = ValidationManager.IsNameValid(name);
            bool isFirstNameValid = ValidationManager.IsNameValid (firstLastName);
            bool isLastNameValid = ValidationManager.IsNameValid (secondLastName);

            return isNameValid && isFirstNameValid && isLastNameValid;
        }

        private bool ValidateEmail()
        {
            string email = txtEmail.Text;
            return ValidationManager.IsValidEmail(email);
        }

        private bool ValidateUser()
        {
            string username = txtUsername.Text;
            return ValidationManager.IsNotEmpty(username);
        }

        private bool ValidateComboBox()
        {
            bool result = false;
            if(cmbCycle.SelectedIndex != -1)
            {
                result = true;
            }
            return result;
        }

        private bool ValidDuration()
        {
            string periodDuration = txtPeriodDuration.Text;
            return ValidationManager.IsValidPositiveInt(periodDuration);
        }

        private bool ValidaCycleDuration()
        {
            string cycleDuration = txtCycleDuration.Text;
            return ValidationManager.IsValidPositiveInt(cycleDuration);
        }

    }
}
