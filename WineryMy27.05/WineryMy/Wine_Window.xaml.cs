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
    /// Логика взаимодействия для Wine_Window.xaml
    /// </summary>
    public partial class Wine_Window : Window
    {
        SqlDataAdapter adapter;
        DataTable WineTable;
        string stringConnection;

        public Wine_Window()
        {
            stringConnection = @"Data Source=DBsrv\sql2021;Initial Catalog=!WineryShirshovDan007ca2;Integrated Security=True";
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Вино";
            WineTable = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(stringConnection);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);





                adapter.InsertCommand = new SqlCommand("dbo.sp_InsertWine", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_Продукта", SqlDbType.Int, 0, "ID_Продукта");
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название_продукта", SqlDbType.NVarChar, 30, "Название_продукта"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_Склада", SqlDbType.Int, 0, "FK_Склада"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Колво", SqlDbType.NVarChar, 20, "Колво"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Сорт_вина", SqlDbType.NVarChar, 30, "Сорт_вина"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Выдержка", SqlDbType.NVarChar, 30, "Выдержка"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Цена", SqlDbType.Money, 20, "Цена"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Описание", SqlDbType.NVarChar, 150, "Описание"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Производство", SqlDbType.NVarChar, 100, "Производство"));




                parameter.Direction = ParameterDirection.Output;


                connection.Open();
                adapter.Fill(WineTable);
                WineGrid.ItemsSource = WineTable.DefaultView;

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
            adapter.Update(WineTable);
        }
        private void updateButton_Click(object sender, RoutedEventArgs ae)
        {
            UpdateDB();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (WineGrid.SelectedItems != null)
            {
                for (int i = 0; i < WineGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = WineGrid.SelectedItems[i] as DataRowView;
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

