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
    }
}
