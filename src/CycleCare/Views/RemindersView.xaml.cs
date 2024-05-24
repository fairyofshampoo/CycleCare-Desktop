using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace CycleCare.Views
{
    /// <summary>
    /// Interaction logic for RemindersView.xaml
    /// </summary>
    public partial class RemindersView : Page
    {
        public RemindersView()
        {
            InitializeComponent();
            menuFrame.Content = new MenuBar(this);
            SetRemindersInPage();
        }

        private async void SetRemindersInPage()
        {
            Response response = await ReminderService.GetCurrentReminders();
            switch (response.Code)
            {
                case (int)HttpStatusCode.OK:
                    ShowReminders(response.Reminders);
                    break;
                case (int)HttpStatusCode.BadRequest:
                    DialogManager.ShowWarningMessageBox("Usuario incorrecto.");
                    break;
                case (int)HttpStatusCode.NotFound:
                    ShowNoItemsLabel();
                    break;
            }
        }

        private void ShowNoItemsLabel()
        {
            lstReminders.Items.Clear();
            Label lblNoItems = new Label();
            lblNoItems.Style = (Style)FindResource("NoItemsLabelStyle");
            lblNoItems.HorizontalAlignment = HorizontalAlignment.Center;
            lblNoItems.VerticalAlignment = VerticalAlignment.Center;
            lstReminders.Items.Add(lblNoItems);
        }

        private void ShowReminders(List<Reminder> reminders)
        {
            lstReminders.Items.Clear();
            foreach (Reminder reminder in reminders)
            {
                AddReminderToList(reminder);
            }
        }

        private void AddReminderToList(Reminder reminder)
        {
            ReminderUC reminderUC = new ReminderUC();
            reminderUC.RemindersView = this;
            reminderUC.SetReminderData(reminder);
            lstReminders.Items.Add(reminderUC);
        }

        private void BtnAddReminder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NewReminderView newReminderView = new NewReminderView();
            NavigationService.Navigate(newReminderView);
        }

        private void BtnGoBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public void RemoveReminder(ReminderUC reminderUC)
        {
            lstReminders.Items.Remove(reminderUC);
        }
    }
}
