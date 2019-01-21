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
using System.Windows.Shapes;
using BE;

namespace UI
{
    /// <summary>
    /// Interaction logic for Add_tester.xaml
    /// </summary>
    public partial class Add_tester : Window
    {
        bool[,] arr = new bool[6, 5];

        int MaxSeniority = 0;

        Tester t = new Tester();

        public Add_tester()
        {
            InitializeComponent();

            //setting source to the comboBox controls
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            car_typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            gear_typeComboBox.ItemsSource = Enum.GetValues(typeof(GearType));

            Add_testerGrid.DataContext = t;
        }

        private void date_of_birthDatePicker_opend(object sender, RoutedEventArgs e)
        {
            date_of_birthDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-Configuration.Min_tester_age);
            date_of_birthDatePicker.DisplayDateStart = DateTime.Now.AddYears(-Configuration.Max_tester_age);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            int x = (int)bt.GetValue(Grid.RowProperty) - 1;
            int y = (int)bt.GetValue(Grid.ColumnProperty) - 1;

            arr[x, y] = !arr[x, y];

            if (bt.Background == Brushes.Gray)
                bt.Background = Brushes.Yellow;
            else
                bt.Background = Brushes.Gray;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Width = 600;
            ScheduleGrid.Visibility = Visibility.Hidden;
            Add_testerGrid.Visibility = Visibility.Visible;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Width = 800;
            ScheduleGrid.Visibility = Visibility.Visible;
            Add_testerGrid.Visibility = Visibility.Hidden;
        }

        private void date_of_birthDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (date_of_birthDatePicker.SelectedDate != null)
            {
                MaxSeniority = DateTime.Now.Year - date_of_birthDatePicker.SelectedDate.Value.Year - 40;
                UpButton.IsEnabled = true;
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            SeniorityTextBox.Text = (int.Parse(SeniorityTextBox.Text) + 1).ToString();

            if (int.Parse(SeniorityTextBox.Text) == MaxSeniority)
                UpButton.IsEnabled = false;

            DownButton.IsEnabled = true;
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            SeniorityTextBox.Text = (int.Parse(SeniorityTextBox.Text) - 1).ToString();

            if (int.Parse(SeniorityTextBox.Text) == 0)
                DownButton.IsEnabled = false;

            UpButton.IsEnabled = true;
        }
    }
}
