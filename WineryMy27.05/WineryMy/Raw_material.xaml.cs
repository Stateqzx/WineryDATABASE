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
    /// Логика взаимодействия для Raw_material.xaml
    /// </summary>
    public partial class Raw_material : Window
    {
        SqlDataAdapter adapter;
        DataTable RawMaterial;
        string stringConnection;
        public Raw_material()
        {
            stringConnection = @"Data Source=DBsrv\sql2021;Initial Catalog=!WineryShirshovDan007ca2;Integrated Security=True";
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Сырье";
            RawMaterial = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(stringConnection);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);





                adapter.InsertCommand = new SqlCommand("sp_InsertRawMaterial", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_Сырья", SqlDbType.Int, 0, "ID_Сырья");
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название_Сырья", SqlDbType.NVarChar, 50, "Название_Сырья"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_Склада", SqlDbType.Int, 0 , "FK_Склада"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Колво", SqlDbType.NVarChar, 50, "Колво"));


                parameter.Direction = ParameterDirection.Output;


                connection.Open();
                adapter.Fill(RawMaterial);
                RawMaterialGrid.ItemsSource = RawMaterial.DefaultView;

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
            adapter.Update(RawMaterial);
        }
        private void updateButton_Click(object sender, RoutedEventArgs ae)
        {
            UpdateDB();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RawMaterialGrid.SelectedItems != null)
            {
                for (int i = 0; i < RawMaterialGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = RawMaterialGrid.SelectedItems[i] as DataRowView;
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

