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
    /// Логика взаимодействия для Stages_window.xaml
    /// </summary>
    public partial class Stages_window : Window
    {
        SqlDataAdapter adapter;
        DataTable StagesTable;
        string stringConnection;


        public Stages_window()
        {
            stringConnection = @"Data Source=DBsrv\sql2021;Initial Catalog=!WineryShirshovDan007ca2;Integrated Security=True";
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Этапы";
            StagesTable = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(stringConnection);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);





                adapter.InsertCommand = new SqlCommand("sp_InsertStages", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_Этапа", SqlDbType.Int, 0, "ID_Этапа");
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название", SqlDbType.NVarChar, 30, "Название"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Описание_процесса", SqlDbType.NVarChar, 150, "Описание_процесса"));


                parameter.Direction = ParameterDirection.Output;


                connection.Open();
                adapter.Fill(StagesTable);
                StagesGrid.ItemsSource = StagesTable.DefaultView;

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
            adapter.Update(StagesTable);
        }
        private void updateButton_Click(object sender, RoutedEventArgs ae)
        {
            UpdateDB();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StagesGrid.SelectedItems != null)
            {
                for (int i = 0; i < StagesGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = StagesGrid.SelectedItems[i] as DataRowView;
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
