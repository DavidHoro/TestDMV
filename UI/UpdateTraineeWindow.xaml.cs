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
    /// Interaction logic for UpdateTraineeWindow.xaml
    /// </summary>
    public partial class UpdateTraineeWindow : Window
    {
        Ibl myBL = FactoryBL.Get_BL();

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

            Trainee t;
            if (IDTextBox.Text.Length == 9)
            {
                t = myBL.Get_trainee(IDTextBox.Text);
                UpdateTraineeGrid.DataContext = t;
                AddressTextBox.Text = t.Address.ToString();
            }
        }

        private void IDTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(IDTextBox.Text == "Enter ID")
                IDTextBox.Text = IDTextBox.Text.Remove(0);
            IDTextBox.Foreground = Brushes.Black;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this trainee?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                myBL.Delete_trainee(IDTextBox.Text);
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

    }
}
