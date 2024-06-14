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
    /// Interaction logic for UpdateReminderView.xaml
    /// </summary>
    public partial class UpdateReminderView : Page
    {
        private Reminder ReminderData = new Reminder();
        public UpdateReminderView(Reminder reminder)
        {
            ReminderData = reminder;
            InitializeComponent();
            SetReminderData();
        }

        private void SetReminderData()
        {
            var reminderDate = ReminderData.CreationDate.ToLocalTime();
            txtDescription.Text = ReminderData.Description;
            txtTitle.Text = ReminderData.Title;
            dpReminderDate.SelectedDate = reminderDate.Date;
            PopulateHoursAndMinutes();

            cbHours.SelectedItem = reminderDate.Hour.ToString("D2");
            cbMinutes.SelectedItem = reminderDate.Minute.ToString("D2");
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


        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {
            RemindersView remindersView = new RemindersView();
            NavigationService.Navigate(remindersView);
        }

        private void BtnUpdateReminder_Click(object sender, RoutedEventArgs e)
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
                    CreationDate = date.AddHours(hours).AddMinutes(minutes).AddSeconds(0),
                    Id = ReminderData.Id,
                    ScheduleId = ReminderData.ScheduleId,
                    Username = ReminderData.Username
                };

                UpdateReminder(reminder);
            }
            else
            {
                DialogManager.ShowWarningMessageBox("Error en la validación del recordatorio. Por favor, revise los campos.");
            }
        }

        private async void UpdateReminder(Reminder reminder)
        {
            Response response = await ReminderService.UpdateReminder(reminder);
            switch (response.Code)
            {
                case (int)HttpStatusCode.Created:
                    DialogManager.ShowSuccessMessageBox("Recordatorio actualizado exitosamente.");
                    GoBack();
                    break;
                case (int)HttpStatusCode.Forbidden:
                    DialogManager.ShowWarningMessageBox("No tienes permisos para actualizar este recordatorio.");
                    break;
                case (int)HttpStatusCode.NotFound:
                    DialogManager.ShowWarningMessageBox("No se encontró el recordatorio.");
                    break;
                case (int)HttpStatusCode.InternalServerError:
                    DialogManager.ShowWarningMessageBox("Error al actualizar el recordatorio. Intente nuevamente.");
                    break;
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
            if (!selectedDate.HasValue)
            {
                return false;
            }
            return true;
        }

        private bool IsTimeValid()
        {
            if (cbHours == null || cbMinutes == null)
            {
                return false;
            }

            int hours = Convert.ToInt32(cbHours.SelectedItem.ToString());
            int minutes = Convert.ToInt32(cbMinutes.SelectedItem.ToString());
            DateTime date = dpReminderDate.SelectedDate.GetValueOrDefault().Date;
            date = date.AddHours(hours).AddMinutes(minutes).AddSeconds(0);
            DateTime now = DateTime.Now;

            if (date <= now)
            {
                return false;
            }

            return true;
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
