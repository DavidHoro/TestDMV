﻿using System;
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
    /// Interaction logic for UpdateTraineeWindow.xaml
    /// </summary>
    public partial class UpdateTraineeWindow : Window
    {
        Ibl myBL = FactoryBL.Get_BL();
        Trainee t;

        public UpdateTraineeWindow()
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
            if(IDTextBox.Text == "Enter ID")
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
                t = myBL.Get_trainee(IDTextBox.Text);
                UpdateTraineeGrid.DataContext = t;
                AddressTextBox.Text = t.Address.ToString();
                IDTextBox.IsReadOnly = true;
                UpdateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                UpButton.IsEnabled = true;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
                IDTextBox.Clear();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
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
            if (SchoolTextBox.Text.Length == 0)
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
            {
                if (myBL.Updete_trainee(t))
                    MessageBox.Show("The trainee info was updated", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.myWindow.Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this trainee?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(myBL.Delete_trainee(IDTextBox.Text))
                    MessageBox.Show("The trainee was deleted", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow.myWindow.Close();
            }
        }

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
