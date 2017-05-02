using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRentalWPF.Model;
using System.Data.SqlClient;
using MovieRentalWPF.DAO;

namespace MovieRentalWPF.SQL
{
    class CustomersSQLClass : SQLCommandClass
    {
        public void RetrieveAllCustomers(ViewModelClass model)
        {
            using (SqlConnection connection = new SqlConnection(DefaultConnectionName))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(ViewAllCustomers, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        model.Clear();
                        while (reader.Read())
                        {
                            CustomerClass customer = new CustomerClass();
                            customer.ID = reader["CustID"].ToString();
                            customer.FirstName = reader["LastName"].ToString();
                            customer.LastName = reader["LastName"].ToString();
                            customer.Address = reader["Address"].ToString();
                            customer.Phone = reader["Address"].ToString();

                            model.Add(customer);

                        }
                    }
                    connection.Close();
                }
                catch (Exception Error)
                {
                    throw new Exception("SQL Excution Error" + Error);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }
        }
    }
}
