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
    /// Interaction logic for CustomCalendar.xaml
    /// </summary>
    public partial class CustomCalendar : UserControl
    {
        private DateTime _currentDate;

        public CustomCalendar()
        {
            InitializeComponent();
            _currentDate = DateTime.Now;
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
                Border dayBorder = new Border
                {
                    Child = dayText,
                    BorderBrush = new SolidColorBrush(Colors.LightGray),
                    BorderThickness = new Thickness(1)
                };

                int row = (day + startDayOfWeek - 1) / 7;
                int column = (day + startDayOfWeek - 1) % 7;

                Grid.SetRow(dayBorder, row);
                Grid.SetColumn(dayBorder, column);
                calendarGrid.Children.Add(dayBorder);
            }
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(-1);
            DisplayCalendar(_currentDate);
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(1);
            DisplayCalendar(_currentDate);
        }
    }
}