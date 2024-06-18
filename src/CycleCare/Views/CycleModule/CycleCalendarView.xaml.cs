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

        private async void SetPrediction()
        {
            Response response = await CycleService.GetCyclePrediction();
            switch (response.Code)
            {
                case 200:
                    ShowDaysToNextPeriod(response);
                    break;
                case 404:
                    tvDaysToPeriod.Text = "?";
                    txtPhaseTitle.Text = "No hay predicción";
                    tvPhaseDescription.Text = "No se ha podido predecir la fecha de tu próximo periodo. Por favor, registra tus ciclos para obtener una predicción más precisa.";
                    break;
                default:
                    DialogManager.ShowErrorMessageBox("Error al obtener la predicción. Por favor, intente nuevamente.");
                    break;
            }
        }

        private void ShowDaysToNextPeriod(Response response)
        {
            int daysToPeriod = 0;

            if (response.NextPeriodStartDate > DateTime.Now)
            {
                daysToPeriod = (response.NextPeriodStartDate - DateTime.Now).Days;
                tvDaysToPeriod.Text = (daysToPeriod).ToString();
            }

            ShowCurrentPhase(daysToPeriod);
        }

        private void ShowCurrentPhase(int daysToPeriod)
        {
            if (daysToPeriod <= 0)
            {
                txtPhaseTitle.Text = "Menstruación";
                tvPhaseDescription.Text = "Durante la menstruación, el cuerpo de la mujer se deshace del revestimiento del útero. Esto dura de 3 a 7 días. Puedes experimentar calambres, fatiga y cambios de humor.";
            }
            else if (daysToPeriod <= 4)
            {
                txtPhaseTitle.Text = "Folicular";
                tvPhaseDescription.Text = "Durante la fase folicular, los ovarios producen estrógeno, lo que ayuda a engrosar el revestimiento del útero. Te puedes sentir más energética y con mejor estado de ánimo.";
            }
            else if (daysToPeriod <= 14)
            {
                txtPhaseTitle.Text = "Ovulación";
                tvPhaseDescription.Text = "Durante la ovulación, un óvulo es liberado de uno de los ovarios y se desplaza por las trompas de Falopio. Puedes sentir un aumento de energía, un ligero dolor abdominal y mayor libido.";
            }
            else
            {
                txtPhaseTitle.Text = "Lútea";
                tvPhaseDescription.Text = "Durante la fase lútea, el folículo vacío que liberó el óvulo comienza a producir progesterona para preparar el útero para un posible embarazo. Puedes experimentar hinchazón, sensibilidad en los senos y cambios de humor.";
            }
        }

        private void BtnNewEntry_Click(object sender, RoutedEventArgs e)
        {
            AddNewCycleLog();
        }

        private async void AddNewCycleLog()
        {
            DateTime currentDate = DateTime.Now;
            CycleLog response = await CycleService.GetCycleLogByDay((int)currentDate.Year, (int)currentDate.Month, (int)currentDate.Day);
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
