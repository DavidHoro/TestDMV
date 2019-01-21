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
using BL;
using System.Text.RegularExpressions;

namespace UI
{
    /// <summary>
    /// Interaction logic for Set_test.xaml
    /// </summary>
    public partial class Set_test : Window
    {
        Ibl myBL = FactoryBL.Get_BL();
        Trainee trainee;
        public Set_test()
        {
            InitializeComponent();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            if (IDtextBox.Text.Length == 9)
            {
                try
                {
                    trainee = myBL.Get_trainee(IDtextBox.Text);
                    Set_testGrid.DataContext = trainee;
                    AddressTextBox.Text = trainee.Address.ToString();
                    IDtextBox.IsReadOnly = true;
                    AddressTextBox.IsEnabled = true;
                    Test_datePicker.IsEnabled = true;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
                    IDtextBox.Clear();
                }
            }
        }

        #region Set date and hour
        private void Test_datePicker_calendar_opened(object sender, RoutedEventArgs e)
        {
            Test_datePicker.DisplayDateStart = DateTime.Now.AddDays(2);
            Test_datePicker.DisplayDateEnd = DateTime.Now.AddDays(2).AddMonths(3);
 
            set_black_out_dates(Test_datePicker.DisplayDateStart.Value, Test_datePicker.DisplayDateEnd.Value);
        }

        private void Test_datePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (Test_datePicker.SelectedDate != null)
            {
                set_hours();
                Test_hourComboBox.IsEnabled = true;
            }
        }

        private void set_black_out_dates(DateTime start, DateTime end)
        {
            Test_datePicker.BlackoutDates.Clear();

            trainee.Address = AddressTextBox.Text.ToAddress();

            DateTime i = start.Date;

            while(i <= end)
            {
                    var v = myBL.Available_testers_by_day_nearby(i, trainee.Address, trainee.Car_type, trainee.Gear_type);

                    if (v.Count() == 0)
                        Test_datePicker.BlackoutDates.Add(new CalendarDateRange(i));

                i = i.AddDays(1);
            }
        }

        private void set_hours()
        {
            List<string> hours = new List<string>();
            DateTime DT = Test_datePicker.SelectedDate.Value;
            DT = DT.AddHours(9);

            for (int i = 9; i < 15; i++)
            {
                var v = myBL.Available_testers_nearby(DT, trainee.Address, trainee.Car_type, trainee.Gear_type);

                if (v.Count() != 0)
                    hours.Add(i + ":00");

                DT = DT.AddHours(1);
            }

            Test_hourComboBox.ItemsSource = hours;
        }
        #endregion

        private void IDtextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                FindButton_Click(sender, e);
        }

        private void Set_button_Click(object sender, RoutedEventArgs e)
        {
            if (AddressERROR.Visibility == Visibility.Hidden) 
            {
                DateTime testDT = Test_datePicker.SelectedDate.Value;
                testDT = testDT.AddHours(Tools.ToInt(Test_hourComboBox.SelectedItem));

                Tester tester = myBL.Available_testers_nearby(testDT, trainee.Address, trainee.Car_type, trainee.Gear_type).First();

                int Min = tester.Address.Distance(trainee.Address);

                foreach (Tester t in myBL.Available_testers_nearby(testDT, trainee.Address, trainee.Car_type, trainee.Gear_type))
                {
                    if (t.Address.Distance(trainee.Address) < Min)
                        tester = t;
                }

                Test test = new Test(trainee.ID, tester.ID, trainee.First_name + " " + trainee.Last_name, tester.First_name + " " + tester.Last_name, testDT, trainee.Address, trainee.Car_type, trainee.Gear_type);

                try
                {
                    myBL.Add_test(test);
                    MessageBox.Show("Test set", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Stop);
                }
                
                MainWindow.myWindow.Close();
            }
        }

        private void AddressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Tools.IsValidAddress(AddressTextBox.Text))
            {
                AddressERROR.Visibility = Visibility.Visible;
                AddressTextBox.BorderBrush = Brushes.Red;
                Test_datePicker.IsEnabled = false;
                Test_datePicker.SelectedDate = null;
                Test_hourComboBox.SelectedItem = null;
            }
            else
            {
                AddressERROR.Visibility = Visibility.Hidden;
                AddressTextBox.BorderBrush = Brushes.Black;
                Test_datePicker.IsEnabled = true;
            }
        }

        #region Restrictions on input
        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddressTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion
    }
}
