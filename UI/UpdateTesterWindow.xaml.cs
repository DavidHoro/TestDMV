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
    public partial class UpdateTesterWindow : Window
    {
        Ibl myBL = FactoryBL.Get_BL();
        Tester t;
        int MaxSeniority;
        bool[,] arr = new bool[6, 5];

        public UpdateTesterWindow()
        {
            InitializeComponent();
            Car_typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            Gear_typeComboBox.ItemsSource = Enum.GetValues(typeof(GearType));
        }

        private void IDTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IDTextBox.Text.Length == 0)
            {
                IDTextBox.Text = "Enter ID";
                IDTextBox.Foreground = Brushes.Gray;
            }
        }

        private void IDTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (IDTextBox.Text == "Enter ID")
                IDTextBox.Text = IDTextBox.Text.Remove(0);
            IDTextBox.Foreground = Brushes.Black;
        }

        private void IDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IDTextBox.Text.Length == 9 && !IDTextBox.Text.Contains(" "))
                FindButton.IsEnabled = true;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                func();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            try
            {
                t = myBL.Get_tester(IDTextBox.Text);
                UpdateTesterGrid.DataContext = t;
                AddressTextBox.Text = t.Address.ToString();
                t.Copy(arr, t.Schedule);
                IDTextBox.IsReadOnly = true;
                NextButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                UpButton1.IsEnabled = true;

                MaxSeniority = DateTime.Now.Year - t.Date_of_birth.Year - 40;
                if (MaxSeniority != 0)
                    UpButton2.IsEnabled = true;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
                IDTextBox.Clear();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this tester?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (myBL.Delete_tester(IDTextBox.Text))
                    MessageBox.Show("The tester was deleted", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow.myWindow.Close();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            bool ERROR = false;
            if (First_nameTextBox.Text.Length == 0)
            {
                ERROR = true;
                First_nameTextBox.BorderBrush = Brushes.Red;
                First_nameERROR.Visibility = Visibility.Visible;
            }
            if (Last_nameTextBox.Text.Length == 0)
            {
                ERROR = true;
                Last_nameTextBox.BorderBrush = Brushes.Red;
                Last_nameERROR.Visibility = Visibility.Visible;
            }
            if (!Tools.IsValidAddress(AddressTextBox.Text))
            {
                ERROR = true;
                AddressTextBox.BorderBrush = Brushes.Red;
                AddressERROR.Visibility = Visibility.Visible;
            }
            if (!Tools.IsValidPhoneNumber(PhoneNumberTextBox.Text))
            {
                ERROR = true;
                PhoneNumberTextBox.BorderBrush = Brushes.Red;
                PhoneERROR.Visibility = Visibility.Visible;
            }
            if (!Tools.IsValidEmail(EmailTextBox.Text))
            {
                ERROR = true;
                EmailTextBox.BorderBrush = Brushes.Red;
                EmailERROR.Visibility = Visibility.Visible;
            }


            if (!ERROR)
            {
                Application.Current.MainWindow = this;
                Application.Current.MainWindow.Width = 800;
                ScheduleGrid.Visibility = Visibility.Visible;
                UpdateTesterGrid.Visibility = Visibility.Hidden;
            }
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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            t.Address = AddressTextBox.Text.ToAddress();
            t.Max_tests = arr.Length;
            t.Copy(t.Schedule, arr);

            if(myBL.Updete_tester(t))
                MessageBox.Show("The tester info was updated", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

            MainWindow.myWindow.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Width = 600;
            ScheduleGrid.Visibility = Visibility.Hidden;
            UpdateTesterGrid.Visibility = Visibility.Visible;
        }

        #region Up and Down buttons
        private void UpButton1_Click(object sender, RoutedEventArgs e)
        {
            Max_distanceTextBox.Text = (int.Parse(Max_distanceTextBox.Text) + 1).ToString();
            DownButton1.IsEnabled = true;
        }

        private void DownButton1_Click(object sender, RoutedEventArgs e)
        {
            Max_distanceTextBox.Text = (int.Parse(Max_distanceTextBox.Text) - 1).ToString();

            if (int.Parse(SeniorityTextBox.Text) == 0)
                DownButton1.IsEnabled = false;
        }

        private void UpButton2_Click(object sender, RoutedEventArgs e)
        {
            SeniorityTextBox.Text = (int.Parse(SeniorityTextBox.Text) + 1).ToString();

            if (int.Parse(SeniorityTextBox.Text) == MaxSeniority)
                UpButton2.IsEnabled = false;

            DownButton2.IsEnabled = true;
        }

        private void DownButton2_Click(object sender, RoutedEventArgs e)
        {
            SeniorityTextBox.Text = (int.Parse(SeniorityTextBox.Text) - 1).ToString();

            if (int.Parse(SeniorityTextBox.Text) == 0)
                DownButton2.IsEnabled = false;

            UpButton2.IsEnabled = true;
        }

        #endregion

        #region Restrictions on input
        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void OnlyLetters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddressTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void EmailTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9.@]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion

        #region GotKeyboardFocus
        private void First_nameTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            First_nameERROR.Visibility = Visibility.Hidden;
            First_nameTextBox.BorderBrush = Brushes.Black;
        }

        private void Last_nameTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Last_nameERROR.Visibility = Visibility.Hidden;
            Last_nameTextBox.BorderBrush = Brushes.Black;
        }

        private void AddressTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            AddressERROR.Visibility = Visibility.Hidden;
            AddressTextBox.BorderBrush = Brushes.Black;
        }

        private void PhoneNumberTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            PhoneERROR.Visibility = Visibility.Hidden;
            PhoneNumberTextBox.BorderBrush = Brushes.Black;
        }

        private void EmailTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            EmailERROR.Visibility = Visibility.Hidden;
            EmailTextBox.BorderBrush = Brushes.Black;
        }
        #endregion

        void func()
        {
            List<Button> l = new List<Button>();
            Button b;
            foreach (var item in ScheduleGrid.Children)
            {
                if (item is Button)
                {
                    b = item as Button;
                        l.Add(b);
                }
            }

            for (int i = 0; i < l.Count-2; i++)
            {
                int x = (int)l[i].GetValue(Grid.RowProperty) - 1;
                int y = (int)l[i].GetValue(Grid.ColumnProperty) - 1;

                if(t.Schedule[x,y])
                    l[i].Background = Brushes.Yellow;
            }
        }

    }
}
