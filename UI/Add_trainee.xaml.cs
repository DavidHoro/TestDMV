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
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace UI
{
    /// <summary>
    /// Interaction logic for Add_trainee.xaml
    /// </summary>
    public partial class Add_trainee : Window
    {
        Ibl myBL = FactoryBL.Get_BL();
        Trainee t = new Trainee();

        public Add_trainee()
        {
            InitializeComponent();

            //setting source to the comboBox controls
            GenderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            Car_typeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            Gear_typeComboBox.ItemsSource = Enum.GetValues(typeof(GearType));


            Add_traineeGrid.DataContext = t;

            Date_of_birthDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-Configuration.Min_trainee_age);

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
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
            if (IDTextBox.Text.Length < 9 || IDTextBox.Text.Contains(" "))
            {
                ERROR = true;
                IDTextBox.BorderBrush = Brushes.Red;
                IDERROR.Visibility = Visibility.Visible;
            }
            if(Date_of_birthDatePicker.SelectedDate == null)
            {
                ERROR = true;
                Date_of_birthDatePicker.BorderBrush = Brushes.DarkRed;
                DateERROR.Visibility = Visibility.Visible;
            }
            if(!IsValidAddress(AddressTextBox.Text))
            {
                ERROR = true;
                AddressTextBox.BorderBrush = Brushes.Red;
                AddressERROR.Visibility = Visibility.Visible;
            }
            if(!IsValidPhoneNumber(PhoneNumberTextBox.Text))
            {
                ERROR = true;
                PhoneNumberTextBox.BorderBrush = Brushes.Red;
                PhoneERROR.Visibility = Visibility.Visible;
            }
            if (!IsValidEmail(EmailTextBox.Text))
            {
                ERROR = true;
                EmailTextBox.BorderBrush = Brushes.Red;
                EmailERROR.Visibility = Visibility.Visible;
            }
            if(SchoolTextBox.Text.Length == 0)
            {
                ERROR = true;
                SchoolTextBox.BorderBrush = Brushes.Red;
                SchoolERROR.Visibility = Visibility.Visible;
            }
            if (TeacherTextBox.Text.Length == 0)
            {
                ERROR = true;
                TeacherTextBox.BorderBrush = Brushes.Red;
                TeacherERROR.Visibility = Visibility.Visible;
            }
       
            if (!ERROR)
                try
                {
                    t.Address = AddressTextBox.Text.ToAddress();
                    t.Date_of_birth = Date_of_birthDatePicker.SelectedDate.Value;
                    myBL.Add_trainee(t);
                    MessageBox.Show("Trainee was added to the system", "",MessageBoxButton.OK,MessageBoxImage.Information);
                    MainWindow.myWindow.Close();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
        }

        #region Validation test
        bool IsValidEmail(string Email)
        {
            try
            {
                var addr = new MailAddress(Email);
                return Email == addr.Address;
            }
            catch (Exception)
            {

                return false;
            }
        }

        bool IsValidAddress(string Address)
        {
            // פורמט כתובת תקין הוא כזה שבו מופיע שם הרחוב (אותיות ומספרים בלבד) ולאחריו פסיק
            //אחרי הפסיק יופיע מספר הבית (ספרות בלבד) ולאחריו פסיק נוסף
            //אחרי הפסיק השני יופיע שם העיר (אותיות בלבד
            int count = 0;

            foreach (var s in Address)
                if (s == ',')
                    count++;

            if (count != 2)                   //count < 2 ???
                return false;

            int i = 0;

            while (true)
            {
                if (Address[i] == ',')
                    break;
                if (!char.IsLetterOrDigit(Address[i]))
                    return false;
                i++;
            }if (i == 0) return false;        //מינימום רחוב באורך אות אחת

            while (true)
            {
                if (Address[i] == ',')
                    break;

                if (!char.IsNumber(Address[i]))
                    return false;
                i++;
            }
            if (i == Address.Length)       //מינימום עיר באורך אות אחת
                return false;
            return true;
        }

        bool IsValidPhoneNumber(string Number)
        {
            if (Number.Count() < 10)
                return false;

            if (Number[0] != '0' || Number[1] != '5')
                return false;

            return true;
        }
        #endregion
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            LessonsTextBox.Text = (int.Parse(LessonsTextBox.Text) + 1).ToString();

            if (int.Parse(LessonsTextBox.Text) == 40)
                UpButton.IsEnabled = false;

            DownButton.IsEnabled = true;
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            LessonsTextBox.Text = (int.Parse(LessonsTextBox.Text) - 1).ToString();

            if (int.Parse(LessonsTextBox.Text) == 0)
                DownButton.IsEnabled = false;

            UpButton.IsEnabled = true;
        }

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

        private void IDTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            IDERROR.Visibility = Visibility.Hidden;
            IDTextBox.BorderBrush = Brushes.Black;
        }

        private void Date_of_birthDatePicker_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            DateERROR.Visibility = Visibility.Hidden;
            Date_of_birthDatePicker.BorderBrush = Brushes.Black;
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

        private void SchoolTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SchoolERROR.Visibility = Visibility.Hidden;
            SchoolTextBox.BorderBrush = Brushes.Black;
        }

        private void TeacherTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TeacherERROR.Visibility = Visibility.Hidden;
            TeacherTextBox.BorderBrush = Brushes.Black;
        }
        #endregion

    }
}
