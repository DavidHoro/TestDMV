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
using System.Net.Mail;
using System.Net;
using BE;
using BL;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Reflection;
using DAL;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string admin = "Admin";
        string password = "1234";
        public static Window myWindow;
        Ibl myBL = FactoryBL.Get_BL();


        public MainWindow()
        {
            InitializeComponent();
            Initialize();
            // new Dal_XML_imp().Read();
            CalculetDestans calcObj = new CalculetDestans("pisga 45 st. jerusalem",
                "gilgal 78 st.ramat - gan");
          double km  = calcObj.Calc();

        }

        void Initialize()
        {
            bool[,] aa = new bool[6, 5];
            aa[0, 0] = true;
            aa[0, 1] = true;
            List<DateTime> d = new List<DateTime>();
            //  d.Add(new DateTime(2019, 01, 06, 9, 0, 0));


            Tester t = new Tester("123456789",
                "Menashe",
                "Moshe",
                "asdfg@gmail.com",
                new DateTime(1970, 10, 12),
                Gender.Male, "0541123215",
                new Address("dfg", "545", "dfg"),
                2,
                20,
                CarType.Motorcycle,
                GearType.Auto,
                aa,
                30,
                d);
            // t.Appointments.Add(new DateTime(2019, 1, 13, 9, 0, 0));
            myBL.Add_tester(t);

            Trainee tr = new Trainee("987654321", "Cohen", "Yoel", "BennyCohen@gmail.com", Gender.Male, "0548875540", new Address("King George", "21", "Jerusalem"),
                new DateTime(1990, 10, 12), CarType.Motorcycle, GearType.Auto, "SmartDrive", "Benny Goren", 20);
            myBL.Add_trainee(tr);
        }


        private void Log_in_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Tb_User_name.Text.ToLower() == admin.ToLower() && Pb_Password.Password == password)
            {
                Login_grid.Visibility = Visibility.Hidden;
                Menu_1.Visibility = Visibility.Visible;
                return;
            }
            if (IsTrainee(Tb_User_name.Text, Pb_Password.Password))
            {
                Login_grid.Visibility = Visibility.Hidden;
                TraineeGrid.Visibility = Visibility.Visible;
                My_Tests.ItemsSource = myBL.Get_all_tests(t => t.Trainee_ID.ToString() == Pb_Password.Password);
                return;
            }
            else
            {
                MessageBox.Show("The User Name or Password are incorrect", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                Tb_User_name.Clear();
                Pb_Password.Clear();
            }
        }

        private void Add_trainee_button_Click(object sender, RoutedEventArgs e)
        {
            myWindow = new Add_trainee();
            myWindow.ShowDialog();
        }

        private void Add_tester_button_Click(object sender, RoutedEventArgs e)
        {
            myWindow = new AddTesterWindow();
            myWindow.ShowDialog();
        }

        private void Set_test_button_Click(object sender, RoutedEventArgs e)
        {
            myWindow = new Set_test();
            myWindow.ShowDialog();
        }

        private void Bt_logOut_Click(object sender, RoutedEventArgs e)
        {
            Tb_User_name.Clear();
            Pb_Password.Clear();
            Menu_1.Visibility = Visibility.Hidden;
            Menu_2.Visibility = Visibility.Hidden;
            Login_grid.Visibility = Visibility.Visible;
        }

        private void More_button_Click(object sender, RoutedEventArgs e)
        {
            Menu_1.Visibility = Visibility.Hidden;
            Menu_2.Visibility = Visibility.Visible;
        }

        private void Update_TraineeButton_Click(object sender, RoutedEventArgs e)
        {
            myWindow = new UpdateTraineeWindow();
            myWindow.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Menu_2.Visibility = Visibility.Hidden;
            Menu_1.Visibility = Visibility.Visible;
        }

        private void BackButton1_Click(object sender, RoutedEventArgs e)
        {
            TableGrid.Visibility = Visibility.Hidden;
            Menu_2.Visibility = Visibility.Visible;
        }

        private void All_TestersButton_Click(object sender, RoutedEventArgs e)
        {
            TestersList.ItemsSource = myBL.Get_all_testers();
            Menu_2.Visibility = Visibility.Hidden;
            // All_testersGrid.Visibility = Visibility.Visible;
            TestersList.Visibility = Visibility.Visible;
        }

        private bool IsTrainee(string userName, string password)
        {
            try
            {
                Trainee t = myBL.Get_trainee(password);
                if ((t.First_name + " " + t.Last_name).ToLower() == userName.ToLower())
                    return true;

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Button_Click100(object sender, RoutedEventArgs e)
        {
            myWindow = new edit();
            myWindow.Show();
        }

        private void All_TraineesButton_Click(object sender, RoutedEventArgs e)
        {
            TraineesList.ItemsSource = myBL.Get_all_trainees();
            Menu_2.Visibility = Visibility.Hidden;
            //All_traineesGrid.Visibility = Visibility.Visible;
            TraineesList.Visibility = Visibility.Visible;
        }
    }
}
