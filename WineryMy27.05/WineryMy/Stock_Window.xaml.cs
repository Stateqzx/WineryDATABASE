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
    /// Логика взаимодействия для Stock_Window.xaml
    /// </summary>
    public partial class Stock_Window : Window
    {
        SqlDataAdapter adapter;
        DataTable StockTable;
        string stringConnection;


        public Stock_Window()
        {
            stringConnection = @"Data Source=DBsrv\sql2021;Initial Catalog=!WineryShirshovDan007ca2;Integrated Security=True";
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Склад";
            StockTable = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(stringConnection);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);





                adapter.InsertCommand = new SqlCommand("sp_InsertStock", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_Склада", SqlDbType.Int, 0, "ID_Склада");
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Тип_Склада", SqlDbType.NVarChar, 40, "Тип_Склада"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название_cклада", SqlDbType.NVarChar, 40, "Название_склада"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Хранимые_объекты", SqlDbType.NVarChar, 50, "Хранимые_объекты"));
                
                


                parameter.Direction = ParameterDirection.Output;


                connection.Open();
                adapter.Fill(StockTable);
                StockGrid.ItemsSource = StockTable.DefaultView;

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
            adapter.Update(StockTable);
        }
        private void updateButton_Click(object sender, RoutedEventArgs ae)
        {
            UpdateDB();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StockGrid.SelectedItems != null)
            {
                for (int i = 0; i < StockGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = StockGrid.SelectedItems[i] as DataRowView;
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
