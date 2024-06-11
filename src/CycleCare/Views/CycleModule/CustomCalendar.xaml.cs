using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private readonly DateTime _today = DateTime.Today;

        public CustomCalendar()
        {
            InitializeComponent();
            _currentDate = DateTime.Now;
            SetCalendarElements();
        }

        public async void SetCycleLogs()
        {
            CycleLogByMonthYearRequest request = new CycleLogByMonthYearRequest
            {
                Month = (int)_currentDate.Month,
                Year = (int)_currentDate.Year
            };
            var cycleLogs = await CycleService.GetCycleLogsByUser(request);
            if (cycleLogs.Code == 200)
            {
                foreach (var cycleLog in cycleLogs.CycleLogs)
                {
                    // Mostrar los cyclelogs en el calendario con un círculo color #FF0065 detrás del número cuyo foreground color debe ser blanco 
                    int day = cycleLog.CreationDate.Day;
                    TextBlock dayText = GetDayTextBlock(day);
                    Border dayBorder = CreateDayBorder(dayText, day, (int)cycleLog.CreationDate.DayOfWeek);
                    dayBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0065"));
                    dayText.Foreground = Brushes.White;
                }

                // Si la lista está vacía mostrar dialog de que no hay cyclelogs y mostrar calendario normal
                if (cycleLogs.CycleLogs.Count == 0)
                {
                    DialogManager.ShowWarningMessageBox("No hay registros disponibles.");
                    SetCalendarElements();
                }
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
                BorderThickness = new Thickness(1)
            };

            int row = (day + startDayOfWeek - 1) / 7;
            int column = (day + startDayOfWeek - 1) % 7;

            Grid.SetRow(dayBorder, row);
            Grid.SetColumn(dayBorder, column);

            if (IsToday(day))
            {
                HighlightDay(dayText, dayBorder);
            }

            return dayBorder;
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
            SetCycleLogs(); // Mostrar los cyclelogs del mes anterior
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            loadingStackPanel.Visibility = Visibility.Visible;
            calendarStackPanel.Visibility = Visibility.Collapsed;
            _currentDate = _currentDate.AddMonths(1);
            SetCycleLogs(); // Mostrar los cyclelogs del mes siguiente
        }
    }
}