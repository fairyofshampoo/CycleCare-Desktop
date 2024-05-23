using CycleCare.Models;
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
    /// Interaction logic for ReminderUC.xaml
    /// </summary>
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

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Supplier_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
