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
    /// Логика взаимодействия для Equipment_Window.xaml
    /// </summary>
    public partial class Equipment_Window : Window
    {
        SqlDataAdapter adapter;
        DataTable Equipment;
        string stringConnection;



        public Equipment_Window()
        {
            stringConnection = @"Data Source=DBsrv\sql2021;Initial Catalog=!WineryShirshovDan007ca2;Integrated Security=True";
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Оборудование";
            Equipment = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(stringConnection);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

              



                adapter.InsertCommand = new SqlCommand("sp_InsertEqp", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_Оборудования", SqlDbType.Int, 0, "ID_Оборудования");
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название_Оборудования", SqlDbType.NVarChar, 50, "Название_Оборудования"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Обслуживание_Оборудования", SqlDbType.NVarChar, 50, "Обслуживание_Оборудования"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_Склада", SqlDbType.NVarChar, 50, "FK_Склада"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Колво_оборудования", SqlDbType.NVarChar, 50, "Колво_оборудования"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Тип_Оборудования", SqlDbType.NVarChar, 50, "Тип_Оборудования"));
                
                
                parameter.Direction = ParameterDirection.Output;


                connection.Open();
                adapter.Fill(Equipment);
                EquipmentGrid.ItemsSource = Equipment.DefaultView;

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
            adapter.Update(Equipment);
        }
        private void updateButton_Click(object sender, RoutedEventArgs ae)
        {
            UpdateDB();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EquipmentGrid.SelectedItems != null)
            {
                for (int i = 0; i < EquipmentGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = EquipmentGrid.SelectedItems[i] as DataRowView;
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

