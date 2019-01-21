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


        private void set_hours_(DateTime d)
        {
            List<string> hours = new List<string>();
            IEnumerable<Tester> TesterList = myBL.Available_testers_nearby(Test_datePicker.SelectedDate.Value, AddressTextBox.Text.ToAddress(), Tools.ToCarType(Car_typeTextBox),Tools.ToGearType(Gear_typeTextBox.Text));


            for (int i = 9; i <= 14; i++)
            {
                d = new DateTime(d.Year, d.Month, d.Day, i, 0, 0);
                if (TesterList.Any(item => myBL.Available_at(item, d)))
                    hours.Add(i + ":00");
            }

            Test_hourComboBox.ItemsSource = hours;
        }

        private void Test_datePicker_calendar_opened(object sender, RoutedEventArgs e)
        {
            Test_datePicker.DisplayDateStart = DateTime.Now.AddDays(2);
            Test_datePicker.DisplayDateEnd = DateTime.Now.AddDays(2).AddMonths(1);
 
            set_black_out_dates(DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddMonths(1));
        }

        private void Test_datePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (Test_datePicker.SelectedDate != null)
                set_hours();
        }

        private void Set_button_Click(object sender, RoutedEventArgs e)
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

            if (myBL.Add_test(test))
                MessageBox.Show("Test set", "yessss", MessageBoxButton.OK, MessageBoxImage.Information);

            MainWindow.myWindow.Close();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            if (IDtextBox.Text.Length == 9)
            {
                trainee = myBL.Get_trainee(IDtextBox.Text);
                Set_testGrid.DataContext = trainee;
                AddressTextBox.Text = trainee.Address.ToString();
            }
        }

        private void set_black_out_dates(DateTime start, DateTime end)
        {
            Test_datePicker.BlackoutDates.Clear();

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

        private void IDtextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                FindButton_Click(sender, e);
        }

    }
}
