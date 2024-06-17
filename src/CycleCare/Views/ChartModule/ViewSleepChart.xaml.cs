using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using LiveCharts;
using LiveCharts.Wpf;

namespace CycleCare.Views.ChartModule
{
    public partial class ViewSleepChart : Page
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }

        public List<int> totalHours { get; set; }

        public Func<int, string> Values { get; set; }

        public ViewSleepChart()
        {
            InitializeComponent();
            GetSleepHours();
        }

        private async void GetSleepHours()
        {
            Response response = await CycleService.GetSleepHours();
            switch (response.Code)
            {
                case (int)HttpStatusCode.OK:
                    List<SleepHours> sleepHours = response.sleepHours;
                    CreateChart(sleepHours);
                    CalculateAVG(sleepHours);
                    break;
                case (int)HttpStatusCode.NotFound:
                    DialogManager.ShowWarningMessageBox("No has registrado horas de sueño.");
                    SleepHoursAVG.Text = "0 Horas";
                    break;
                case (int)HttpStatusCode.InternalServerError:
                    DialogManager.ShowErrorMessageBox("Error interno del servidor. Inténtalo más tarde.");
                    break;
            }
        }

        private void CreateChart(List<SleepHours> sleepHours)
        {
            ChartValues<int> chartValues = GetChartValues(sleepHours);
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Horas de sueño",
                    Values = chartValues
                }
            };
            Values = value => value.ToString("N");
            int count = sleepHours.Count;
            Labels = new string[count];
            for (int i = 0; i < count; i++)
            {
                Labels[i] = sleepHours[i].dayOfWeek;
                int hour = StringHoursToInt(sleepHours[i].totalSleepHours);
            }
            DataContext = this;
        }

        private ChartValues<int> GetChartValues(List<SleepHours> sleepHours)
        {
            ChartValues<int> chartValues = new ChartValues<int>();
            for(int index=0; index < sleepHours.Count; index++)
            {
                int hour = StringHoursToInt(sleepHours[index].totalSleepHours);
                chartValues.Add(hour);
            }
            return chartValues;
        }

        private int StringHoursToInt(string sleepHours)
        {
            int hour = int.Parse(sleepHours);
            return hour;
        }

        private void CalculateAVG(List<SleepHours> sleepHours)
        {
            List<int> hours = new List<int>();
            for (int index = 0; index < sleepHours.Count; index++)
            {
                int hour = StringHoursToInt(sleepHours[index].totalSleepHours);
                hours.Add(hour);
            }
            double average = hours.Average();
            SleepHoursAVG.Text = average.ToString() + " Horas";
        }


        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
