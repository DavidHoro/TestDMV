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
    /// Interaction logic for edit.xaml
    /// </summary>
    public partial class edit : Window
    {
        Criteria c;
        public edit()
        {
            InitializeComponent();
            c = new Criteria();
            Grid1.DataContext = c;
        }

    #region Buttons events
        private void UpButton1_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Text = (int.Parse(TextBox1.Text) + 1).ToString();

            if (int.Parse(TextBox1.Text) == 10)
                UpButton1.IsEnabled = false;

            DownButton1.IsEnabled = true;
        }

        private void DownButton1_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Text = (int.Parse(TextBox1.Text) - 1).ToString();

            if (int.Parse(TextBox1.Text) == 0)
                DownButton1.IsEnabled = false;

            UpButton1.IsEnabled = true;
        }

        private void UpButton2_Click(object sender, RoutedEventArgs e)
        {
            TextBox2.Text = (int.Parse(TextBox2.Text) + 1).ToString();

            if (int.Parse(TextBox2.Text) == 10)
                UpButton2.IsEnabled = false;

            DownButton2.IsEnabled = true;
        }

        private void DownButton2_Click(object sender, RoutedEventArgs e)
        {
            TextBox2.Text = (int.Parse(TextBox2.Text) - 1).ToString();

            if (int.Parse(TextBox2.Text) == 0)
                DownButton2.IsEnabled = false;

            UpButton2.IsEnabled = true;
        }

        private void UpButton3_Click(object sender, RoutedEventArgs e)
        {
            TextBox3.Text = (int.Parse(TextBox3.Text) + 1).ToString();

            if (int.Parse(TextBox3.Text) == 10)
                UpButton3.IsEnabled = false;

            DownButton3.IsEnabled = true;
        }

        private void DownButton3_Click(object sender, RoutedEventArgs e)
        {
            TextBox3.Text = (int.Parse(TextBox3.Text) - 1).ToString();

            if (int.Parse(TextBox3.Text) == 0)
                DownButton3.IsEnabled = false;

            UpButton3.IsEnabled = true;
        }

        private void UpButton4_Click(object sender, RoutedEventArgs e)
        {
            TextBox4.Text = (int.Parse(TextBox4.Text) + 1).ToString();

            if (int.Parse(TextBox4.Text) == 10)
                UpButton4.IsEnabled = false;

            DownButton4.IsEnabled = true;
        }

        private void DownButton4_Click(object sender, RoutedEventArgs e)
        {
            TextBox4.Text = (int.Parse(TextBox4.Text) - 1).ToString();

            if (int.Parse(TextBox4.Text) == 0)
                DownButton4.IsEnabled = false;

            UpButton4.IsEnabled = true;
        }

        private void UpButton5_Click(object sender, RoutedEventArgs e)
        {
            TextBox5.Text = (int.Parse(TextBox5.Text) + 1).ToString();

            if (int.Parse(TextBox5.Text) == 10)
                UpButton5.IsEnabled = false;

            DownButton5.IsEnabled = true;
        }

        private void DownButton5_Click(object sender, RoutedEventArgs e)
        {
            TextBox5.Text = (int.Parse(TextBox5.Text) - 1).ToString();

            if (int.Parse(TextBox5.Text) == 0)
                DownButton5.IsEnabled = false;

            UpButton5.IsEnabled = true;
        }
        #endregion
        
    }
}
