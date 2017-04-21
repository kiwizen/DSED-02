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

namespace TestTableAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string ConnectionString = @"Server=tcp:kiwizen-dsed.database.windows.net,1433;"+
                                            "Initial Catalog=DSED;Persist Security Info=False;"+
                                            "User ID=kiwizen-dsed;Password=Monday99;"+
                                            "MultipleActiveResultSets=False;Encrypt=True;"+
                                            "TrustServerCertificate=False;Connection Timeout=30;";

        public MainWindow()
        {
            InitializeComponent();
            // start the app in the center of the screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            textBoxID.Text = "";

            RetreiveTestTableData();
        }

        private void RetreiveTestTableData()
        {
            string SQLString = "SELECT * FROM [dbo].[TestTable] ORDER BY ID;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SQLString, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        MyTestTable.Items.Clear();
                        while (reader.Read())
                        {
                            MyTestTable.Items.Add( new {
                                ID_Col = reader["ID"].ToString(),
                                Name_Col = reader["NAME"].ToString(),
                                Text_Col = reader["TEXT"].ToString()
                            });
                            //if (recordHandler != null)
                            //    recordHandler(reader);
                        }
                    }
                    connection.Close();
                }
                catch
                {
                    throw new Exception("SQL Excution Error");
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }

        }


        private void Ok_Click(object sender, RoutedEventArgs e)
        {

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Deleted_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            string SQLString = textBoxSQL.Text;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SQLString, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                catch
                {
                    throw new Exception("SQL Excution Error");
                }
                finally
                {
                    if(connection != null)
                        connection.Close();
                }

            }

            RetreiveTestTableData();
        }
    }
}
