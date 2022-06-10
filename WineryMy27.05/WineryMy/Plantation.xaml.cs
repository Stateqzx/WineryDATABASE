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
    /// Логика взаимодействия для Plantation.xaml
    /// </summary>
    public partial class Plantation : Window
    {
        SqlDataAdapter adapter;
        DataTable PlantationTable;
        string stringConnection;

        public Plantation()
        {
            stringConnection = @"Data Source=DBsrv\sql2021;Initial Catalog=!WineryShirshovDan007ca2;Integrated Security=True";
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Плантация";
            PlantationTable = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(stringConnection);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);





                adapter.InsertCommand = new SqlCommand("dbo.sp_InsertPlantation", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_Плантации", SqlDbType.Int, 0, "ID_Плантации");
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Сорт_винограда", SqlDbType.NVarChar, 50, "Сорт_винограда"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Посажено_винограда", SqlDbType.NVarChar, 50, "Посажено_винограда"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Собрано_винограда", SqlDbType.NVarChar, 50, "Собрано_винограда"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Описание_по_уходу", SqlDbType.NVarChar, 100, "Описание_по_уходу"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Описание_по_поливу", SqlDbType.NVarChar, 100, "Описание_по_поливу"));


                parameter.Direction = ParameterDirection.Output;


                connection.Open();
                adapter.Fill(PlantationTable);
                PlantationGrid.ItemsSource = PlantationTable.DefaultView;

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
            adapter.Update(PlantationTable);
        }
        private void updateButton_Click(object sender, RoutedEventArgs ae)
        {
            UpdateDB();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlantationGrid.SelectedItems != null)
            {
                for (int i = 0; i < PlantationGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = PlantationGrid.SelectedItems[i] as DataRowView;
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
