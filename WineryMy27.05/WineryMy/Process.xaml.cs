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
    /// Логика взаимодействия для Process.xaml
    /// </summary>
    public partial class Process : Window
    {
        SqlDataAdapter adapter;
        DataTable ProcessTable;
        string stringConnection;
        public Process()
        {
            stringConnection = @"Data Source=DBsrv\sql2021;Initial Catalog=!WineryShirshovDan007ca2;Integrated Security=True";
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Сырье";
            ProcessTable = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(stringConnection);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);





                adapter.InsertCommand = new SqlCommand("sp_InsertProcess", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_Процесса", SqlDbType.Int, 0, "ID_Процесса");
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_Этапа", SqlDbType.Int, 0, "FK_Этапа"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название_процесса", SqlDbType.NVarChar, 150, "Название_процесса"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Описание", SqlDbType.NVarChar, 150, "Описание"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Особенность_процесса", SqlDbType.NVarChar, 150, "Особенность_процесса"));


                parameter.Direction = ParameterDirection.Output;


                connection.Open();
                adapter.Fill(ProcessTable);
                ProcessTableGrid.ItemsSource = ProcessTable.DefaultView;

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
            adapter.Update(ProcessTable);
        }
        private void updateButton_Click(object sender, RoutedEventArgs ae)
        {
            UpdateDB();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessTableGrid.SelectedItems != null)
            {
                for (int i = 0; i < ProcessTableGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = ProcessTableGrid.SelectedItems[i] as DataRowView;
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
