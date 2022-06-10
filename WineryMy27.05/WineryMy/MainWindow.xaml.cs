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
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;


namespace WineryMy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlDataAdapter adapter;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object winery, RoutedEventArgs e)
        {

            //string stringConnection = @"Data Source=DBsrv\sql2021;Initial Catalog=!WineryShirshovDan007ca2;Integrated Security=True";
            //SqlConnection cn = new SqlConnection(stringConnection);
            //cn.Open();

            //string sqlRequest = "SELECT * FROM Сотрудник";

            //SqlCommand sqlCommand = new SqlCommand(sqlRequest, cn);
            //SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            //Console.WriteLine(sqlDataReader.FieldCount);
            //sqlDataReader.Close();
            //cn.Close();
        }

        private void Window_staff(object sender, RoutedEventArgs e)
        {
            Staff_Window staffWindow = new Staff_Window();
            staffWindow.Show();
        }

        private void Window_equipment(object sender, RoutedEventArgs e)
        {
            Equipment_Window equipment_Window = new Equipment_Window();
            equipment_Window.Show();

        }

        private void Window_stock(object sender, RoutedEventArgs e)
        {
            Stock_Window stock_Window = new Stock_Window();
            stock_Window.Show();

        }

        private void Window_wine(object sender, RoutedEventArgs e)
        {
            Wine_Window wine_Window = new Wine_Window();
            wine_Window.Show();
        }

        private void Window_plant(object sender, RoutedEventArgs e)
        {
            Plantation plantation_window = new Plantation();
            plantation_window.Show();
        }

        private void Window_room(object sender, RoutedEventArgs e)
        {
            RoomFactory_Window roomFactory_Window = new RoomFactory_Window();
            roomFactory_Window.Show();
        }
        private void Window_RawMaterial(object sender, RoutedEventArgs e)
        {
            Raw_material raw_Material = new Raw_material();
            raw_Material.Show();
        }
        private void Window_stages(object sender, RoutedEventArgs e)
        {
            Stages_window stages_Window = new Stages_window();
            stages_Window.Show();
        }
        private void Window_process(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.Show();
        }

    }
}
