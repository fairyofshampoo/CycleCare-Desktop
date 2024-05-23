
using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
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
        }

        private void BtnSaveReminder_Click(object sender, RoutedEventArgs e)
        {
            if (IsReminderValid())
            {
                var reminder = new Reminder()
                {
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    Date = dpReminderDate.SelectedDate.GetValueOrDefault(),
                    Time = new DateTime(dpReminderDate.SelectedDate.GetValueOrDefault().Year,
                                 dpReminderDate.SelectedDate.GetValueOrDefault().Month,
                                 dpReminderDate.SelectedDate.GetValueOrDefault().Day,
                                 (int)cbHours.SelectedItem,
                                 (int)cbMinutes.SelectedItem,0)
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
            switch (response.Code)
            {
                case (int)HttpStatusCode.Created:
                    DialogManager.ShowSuccessMessageBox("Recordatorio creado exitosamente");
                    break;
            }
        }

        private bool IsReminderValid()
        {
            return IsTitleValid(txtTitle.Text) &&
                   IsDescriptionValid(txtDescription.Text) &&
                   IsDateValid(dpReminderDate.SelectedDate) &&
                   IsTimeValid(cbHours.SelectedItem as ComboBoxItem, cbMinutes.SelectedItem as ComboBoxItem);
        }

        private bool IsTitleValid(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length > 70)
            {
                return false;
            }
            return true;
        }

        private bool IsDescriptionValid(string description)
        {
            if (string.IsNullOrWhiteSpace(description) || description.Length > 200)
            {
                return false;
            }
            return true;
        }

        private bool IsDateValid(DateTime? selectedDate)
        {
            if (!selectedDate.HasValue || selectedDate.Value.Date <= DateTime.Now.Date)
            {
                return false;
            }
            return true;
        }

        private bool IsTimeValid(ComboBoxItem cbHours, ComboBoxItem cbMinutes)
        {
            if (cbHours == null || cbMinutes == null)
            {
                return false;
            }
            int hours = int.Parse(cbHours.Content.ToString());
            int minutes = int.Parse(cbMinutes.Content.ToString());

            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59)
            {
                return false;
            }
            return true;
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ShowCalendar(object sender, RoutedEventArgs e)
        {
            if (sender is DatePicker datePicker)
            {
                datePicker.IsDropDownOpen = true;
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
