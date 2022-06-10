using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WineryMy
{
    /// <summary>
    /// Логика взаимодействия для RoomFactory_Window.xaml
    /// </summary>
    public partial class RoomFactory_Window : Window
    {
        SqlDataAdapter adapter;
        DataTable RoomFactory;
        string stringConnection;

        public RoomFactory_Window()
        {
            stringConnection = @"Data Source=DBsrv\sql2021;Initial Catalog=!WineryShirshovDan007ca2;Integrated Security=True";
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Помещение";
            RoomFactory = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(stringConnection);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);





                adapter.InsertCommand = new SqlCommand("sp_InsertRoomFactory", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_Помещения", SqlDbType.Int, 0, "ID_Помещения");
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название_Помещения", SqlDbType.NVarChar, 50, "Название_Помещения"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Предназначение_Помещения", SqlDbType.NVarChar, 50, "Предназначение_Помещения"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Режим_работы", SqlDbType.DateTime, 0,"Режим_работы"));


                parameter.Direction = ParameterDirection.Output;


                connection.Open();
                adapter.Fill(RoomFactory);
                RoomGrid.ItemsSource = RoomFactory.DefaultView;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

        }

        private void UpdateDB()
        {
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.Update(RoomFactory);
        }
        private void updateButton_Click(object sender, RoutedEventArgs ae)
        {
            UpdateDB();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomGrid.SelectedItems != null)
            {
                for (int i = 0; i < RoomGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = RoomGrid.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            UpdateDB();
        }
    }
}
