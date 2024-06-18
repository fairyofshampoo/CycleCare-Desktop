
using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CycleCare.Views
{
    /// <summary>
    /// Interaction logic for NewReminderView.xaml
    /// </summary>
    public partial class NewReminderView : Page
    {
        public NewReminderView()
        {
            InitializeComponent();
            PopulateHoursAndMinutes();
        }

        private void PopulateHoursAndMinutes()
        {
            cbHours.Items.Clear();
            cbMinutes.Items.Clear();

            for (int i = 0; i < 24; i++)
            {
                cbHours.Items.Add(i.ToString("D2"));
            }

            for (int i = 0; i < 60; i++)
            {
                cbMinutes.Items.Add(i.ToString("D2"));
            }
        }
        private void BtnSaveReminder_Click(object sender, RoutedEventArgs e)
        {
            if (IsReminderValid())
            {
                int hours = Convert.ToInt32(cbHours.SelectedItem.ToString());
                int minutes = Convert.ToInt32(cbMinutes.SelectedItem.ToString());
                DateTime date = dpReminderDate.SelectedDate.GetValueOrDefault().Date;

                var reminder = new Reminder()
                {
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    CreationDate = date.AddHours(hours).AddMinutes(minutes).AddSeconds(0)
                };
                CreateReminder(reminder);
            }
            else
            {
                DialogManager.ShowWarningMessageBox("Error en la validación del recordatorio. Por favor, revise los campos.");
            }
        }

        private async void CreateReminder(Reminder reminder)
        {
            Response response = await ReminderService.CreateReminder(reminder);
            if(response.Code == (int)HttpStatusCode.Created)
            {
                DialogManager.ShowSuccessMessageBox("Recordatorio creado exitosamente");
            } else
            {
                DialogManager.ShowErrorMessageBox("Error al crear el recordatorio. Por favor, intente nuevamente.");
            }
        }

        private bool IsReminderValid()
        {
            return IsTitleValid(txtTitle.Text) &&
                   IsDescriptionValid(txtDescription.Text) &&
                   IsDateValid(dpReminderDate.SelectedDate) &&
                   IsTimeValid();
        }

        private bool IsTitleValid(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length > 70)
            {
                Console.WriteLine("Title is not valid");
                return false;
            }
            return true;
        }

        private bool IsDescriptionValid(string description)
        {
            if (string.IsNullOrWhiteSpace(description) || description.Length > 200)
            {
                Console.WriteLine("Description is not valid");
                return false;
            }
            return true;
        }

        private bool IsDateValid(DateTime? selectedDate)
        {
            Console.WriteLine(selectedDate);
            if (!selectedDate.HasValue)
            {
                Console.WriteLine("Date is not valid");
                return false;
            }
            return true;
        }

        private bool IsTimeValid()
        {
            if (cbHours == null || cbMinutes == null)
            {
                Console.WriteLine("Time is not valid");
                return false;
            }

            int hours = Convert.ToInt32(cbHours.SelectedItem.ToString());
            int minutes = Convert.ToInt32(cbMinutes.SelectedItem.ToString());
            DateTime date = dpReminderDate.SelectedDate.GetValueOrDefault().Date;
            date.AddHours(hours).AddMinutes(minutes).AddSeconds(0);
            DateTime now = DateTime.Now;

            if (date <= now)
            {
                return false;
            }

            return true;
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            RemindersView remindersView = new RemindersView();
            NavigationService.Navigate(remindersView);
        }

        private void ShowCalendar(object sender, MouseButtonEventArgs e)
        {
            if (sender is DatePicker datePicker)
            {
                datePicker.IsDropDownOpen = true;
            }
        }
    }
}
