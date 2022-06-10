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
    /// Логика взаимодействия для Staff_Window.xaml
    /// </summary>
    public partial class Staff_Window : Window
    {
        SqlDataAdapter adapter;
        DataTable Staff;
        string stringConnection;

    



        public Staff_Window()
        {
            InitializeComponent();
            stringConnection = @"Data Source=DBsrv\sql2021;Initial Catalog=!WineryShirshovDan007ca2;Integrated Security=True";
        }


        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Сотрудник";
            Staff = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(stringConnection);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                DataGridTextColumn textColumn1 = new DataGridTextColumn();
                textColumn1.Width = 120;
                textColumn1.Header = "ID";
                textColumn1.Binding = new Binding("ID_Сотрудника");
                staffGrid.Columns.Add(textColumn1);

                DataGridTextColumn textColumn2 = new DataGridTextColumn();
                textColumn2.Width = 120;
                textColumn2.Header = "ФИО";
                textColumn2.Binding = new Binding("ФИО");
                staffGrid.Columns.Add(textColumn2);


                DataGridTextColumn textColumn3 = new DataGridTextColumn();
                textColumn3.Width = 120;
                textColumn3.Header = "Досье";
                textColumn3.Binding = new Binding("Досье_сотрудника");
                staffGrid.Columns.Add(textColumn3);

                adapter.InsertCommand = new SqlCommand("sp_InsertStaff",connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_Сотрудника", SqlDbType.Int, 0, "ID_Сотрудника");



                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Досье_сотрудника", SqlDbType.NVarChar, 100, "Досье_сотрудника")); 


                adapter.InsertCommand.Parameters.Add(new SqlParameter("@ФИО", SqlDbType.NVarChar, 100, "ФИО"));
                parameter.Direction = ParameterDirection.Output;


                connection.Open();
                adapter.Fill(Staff);
                staffGrid.ItemsSource = Staff.DefaultView;

            }

            catch(Exception ex)
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
            adapter.Update(Staff);
        }
        private void updateButton_Click(object sender, RoutedEventArgs ae)
        {
            UpdateDB();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (staffGrid.SelectedItems != null)
            {
                for (int i = 0; i < staffGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = staffGrid.SelectedItems[i] as DataRowView;
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
