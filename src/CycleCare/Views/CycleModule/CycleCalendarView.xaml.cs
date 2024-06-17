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

namespace CycleCare.Views.CycleModule
{
    /// <summary>
    /// Interaction logic for CycleCalendarView.xaml
    /// </summary>
    public partial class CycleCalendarView : Page
    {
        public CycleCalendarView()
        {
            InitializeComponent();
            menuFrame.Content = new UserMenu(this);
        }

        private void BtnNewEntry_Click(object sender, RoutedEventArgs e)
        {
            NewCycleLogView newCycleLogView = new NewCycleLogView();
            NavigationService.Navigate(newCycleLogView);
        }
    }
}
