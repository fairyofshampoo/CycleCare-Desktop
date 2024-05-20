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
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : Page
    {
        private Page page;
        public MenuBar(Page page)
        {
            InitializeComponent();
            this.page = page;
        }

        private void BtnReminders_Click(object sender, RoutedEventArgs e)
        {
            RemindersView remindersView = new RemindersView();
            page.NavigationService.Navigate(remindersView);
        }

        private void BtnCalendar_Click(object sender, RoutedEventArgs e)
        {
            //Falta el view de Calendar
        }

        private void BtnContent_Click(object sender, RoutedEventArgs e)
        {
            ContentsView contentsView = new ContentsView();
            page.NavigationService.Navigate(contentsView);
        }

        private void BtnStats_Click(object sender, RoutedEventArgs e)
        {
            SleepStatsView sleepStatsView = new SleepStatsView();
            page.NavigationService.Navigate(sleepStatsView);
        }
    }
}
