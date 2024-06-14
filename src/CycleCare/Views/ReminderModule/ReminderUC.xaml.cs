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
    public partial class ReminderUC : UserControl
    {
        public RemindersView RemindersView { get; set; }
        private Reminder ReminderData = new Reminder();
        public ReminderUC()
        {
            InitializeComponent();
        }

        public void SetReminderData(Reminder reminder)
        {
            ReminderData = reminder;
            var reminderDate = reminder.CreationDate.ToLocalTime();
            txtDate.Text = reminderDate.ToString();
            txtTitle.Text = reminder.Title;
        }

        private void Reminder_Clicked(object sender, MouseButtonEventArgs e)
        {
            UpdateReminderView updateReminderView = new UpdateReminderView(ReminderData);
            RemindersView.NavigationService.Navigate(updateReminderView);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteReminder();
        }

        private async void DeleteReminder()
        {
            string reminderId = ReminderData.Id.ToString();
            Response response = await ReminderService.DeleteReminder(reminderId);

            switch (response.Code)
            {
                case (int)HttpStatusCode.OK:
                    DialogManager.ShowSuccessMessageBox("Recordatorio eliminado exitosamente.");
                    RemindersView.RemoveReminder(this);
                    break;
                case (int)HttpStatusCode.Forbidden:
                    DialogManager.ShowWarningMessageBox("No tienes permisos para eliminar este recordatorio.");
                    break;
                case (int)HttpStatusCode.NotFound:
                    DialogManager.ShowWarningMessageBox("No se encontró el recordatorio.");
                    break;
                case (int)HttpStatusCode.InternalServerError:
                    DialogManager.ShowWarningMessageBox("Error al borrar el recordatorio. Intente nuevamente.");
                    break;
            }
        }
    }
}
