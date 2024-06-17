using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CycleCare.Views.CycleModule
{
    /// <summary>
    /// Interaction logic for CustomCalendar.xaml
    /// </summary>
    public partial class CustomCalendar : UserControl
    {
        private DateTime _currentDate;
        private readonly Color _highlightColor = (Color)ColorConverter.ConvertFromString("#9AD0D3");
        private readonly Color _predictionColor = (Color)ColorConverter.ConvertFromString("#FFE9BA");
        private readonly DateTime _today = DateTime.Today;

        public CustomCalendar()
        {
            InitializeComponent();
            _currentDate = DateTime.Now;
            SetCalendarElements();
            SetCycleLogs();
            SetPrediction();
        }

        private async void SetPrediction()
        {
            Response response = await CycleService.GetCyclePrediction();
            switch (response.Code)
            {
                case 200:
                    ShowPredictionInCalendar(response);
                    break;
                case 404:
                    Console.WriteLine("No hay predicción");
                    break;
                default:
                    DialogManager.ShowErrorMessageBox("Error al obtener la predicción. Por favor, intente nuevamente.");
                    break;
            }
        }
        private void ShowPredictionInCalendar(Response response)
        {
            DateTime nextPeriodStartDate = response.NextPeriodStartDate;
            DateTime nextPeriodEndDate = response.NextPeriodEndDate;

            for (DateTime date = nextPeriodStartDate; date <= nextPeriodEndDate; date = date.AddDays(1))
            {

                if (date.Month == _currentDate.Month && date.Year == _currentDate.Year)
                {

                    int day = date.Day;

                    TextBlock dayText = GetDayTextBlock(day);
                    if (dayText != null)
                    {

                        Border dayBorder = (Border)dayText.Parent;

                        dayBorder.Background = new SolidColorBrush(_predictionColor);
                        dayText.Foreground = Brushes.Black;
                    }
                }
            }
        }

        public async void SetCycleLogs()
        {
            Response cycleLogs = await CycleService.GetCycleLogsByUser((int)_currentDate.Year, (int)_currentDate.Month);
            switch (cycleLogs.Code)
            {
                case 200:
                    DisplayCycleLogs(cycleLogs);
                    break;
                case 404:
                    Console.WriteLine("No se encontraron registros de ciclos.");
                    break;
                case 500:
                    DialogManager.ShowErrorMessageBox("Error interno del servidor.");
                    break;
                default:
                    DialogManager.ShowErrorMessageBox("Error desconocido.");
                    break;
            }
        }

        private void DisplayCycleLogs(Response cycleLogs)
        {
            foreach (var cycleLog in cycleLogs.CycleLogs)
            {
                int day = cycleLog.CreationDate.Day;
                TextBlock dayText = GetDayTextBlock(day);
                Border dayBorder = (Border)dayText.Parent;
                dayBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0065"));
                dayText.Foreground = Brushes.White;
            }
        }

        private TextBlock GetDayTextBlock(int day)
        {
            foreach (UIElement child in calendarGrid.Children)
            {
                if (child is Border border && border.Child is TextBlock textBlock && textBlock.Text == day.ToString())
                {
                    return textBlock;
                }
            }
            return null;
        }

        private void SetCalendarElements()
        {
            loadingStackPanel.Visibility = Visibility.Collapsed;
            calendarStackPanel.Visibility = Visibility.Visible;
            DisplayCalendar(_currentDate);
        }

        private void DisplayCalendar(DateTime date)
        {
            tbMonthYear.Text = date.ToString("MMMM yyyy");
            calendarGrid.Children.Clear();

            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            for (int day = 1; day <= daysInMonth; day++)
            {
                TextBlock dayText = new TextBlock
                {
                    FontSize = 20,
                    Text = day.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Border dayBorder = CreateDayBorder(dayText, day, startDayOfWeek);
                calendarGrid.Children.Add(dayBorder);
            }
        }

        private Border CreateDayBorder(TextBlock dayText, int day, int startDayOfWeek)
        {
            Border dayBorder = new Border
            {
                Child = dayText,
                BorderBrush = new SolidColorBrush(GetBorderColor(day)),
                BorderThickness = new Thickness(1),
                Cursor = Cursors.Hand
            };

            int row = (day + startDayOfWeek - 1) / 7;
            int column = (day + startDayOfWeek - 1) % 7;

            Grid.SetRow(dayBorder, row);
            Grid.SetColumn(dayBorder, column);

            if (IsToday(day))
            {
                HighlightDay(dayText, dayBorder);
            }
            dayBorder.MouseLeftButtonUp += (sender, e) => OnDayClicked(day);

            return dayBorder;
        }

        private async void OnDayClicked(int day)
        {
            CycleLog cycleLogResponse = await CycleService.GetCycleLogByDay(_currentDate.Year, _currentDate.Month, day);

            switch (cycleLogResponse.Code)
            {
                case 200:
                    NavigateToUpdateCycleLog(cycleLogResponse);
                    break;
                case 404:
                    NavigateToNewCycleLog();
                    break;
                default:
                    DialogManager.ShowErrorMessageBox("Error al obtener el registro del ciclo. Por favor, intente nuevamente.");
                    break;
            }
        }

        private void NavigateToUpdateCycleLog(CycleLog cycleLogResponse)
        {
            try
            {
                MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
                if (parentWindow != null)
                {
                    parentWindow.NavigationFrame.Navigate(new UpdateCycleLogView(cycleLogResponse));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al navegar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NavigateToNewCycleLog()
        {
            try
            {
                MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
                if (parentWindow != null)
                {
                    parentWindow.NavigationFrame.Navigate(new NewCycleLogView());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al navegar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Color GetBorderColor(int day)
        {
            return IsToday(day) ? _highlightColor : Colors.LightGray;
        }

        private bool IsToday(int day)
        {
            return _today.Year == _currentDate.Year && _today.Month == _currentDate.Month && _today.Day == day;
        }

        private void HighlightDay(TextBlock dayText, Border dayBorder)
        {
            dayText.FontWeight = FontWeights.Bold;
            dayText.TextDecorations = TextDecorations.Underline;
            dayBorder.BorderBrush = new SolidColorBrush(_highlightColor);
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            loadingStackPanel.Visibility = Visibility.Visible;
            calendarStackPanel.Visibility = Visibility.Collapsed;
            _currentDate = _currentDate.AddMonths(-1);
            SetCalendarElements();
            SetCycleLogs();
            SetPrediction();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            loadingStackPanel.Visibility = Visibility.Visible;
            calendarStackPanel.Visibility = Visibility.Collapsed;
            _currentDate = _currentDate.AddMonths(1);
            SetCalendarElements();
            SetCycleLogs();
            SetPrediction();
        }
    }
}