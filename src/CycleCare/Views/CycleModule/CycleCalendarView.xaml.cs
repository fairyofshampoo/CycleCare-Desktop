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
            SetPrediction();
        }

        private async SetPrediction()
        {
        }

        private void BtnNewEntry_Click(object sender, RoutedEventArgs e)
        {
            AddNewCycleLog();
        }

        private async void AddNewCycleLog()
        {
            DateTime currentDate = DateTime.Now;
            Response response = await CycleService.GetCycleLogByDay((int)currentDate.Year, (int)currentDate.Month, (int)currentDate.Day);
            switch (response.Code)
            {
                case 200:
                    DialogManager.ShowWarningMessageBox("Ya has realizado el registro de hoy");
                    break;
                case 404:
                    NewCycleLogView newCycleLogView = new NewCycleLogView();
                    NavigationService.Navigate(newCycleLogView);
                    break;
                default:
                    DialogManager.ShowErrorMessageBox("Error al crear el recordatorio. Por favor, intente nuevamente.");
                    break;
            }
        }
    }
}
