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
    class CustomersSQLClass : SQLCommandClass, IDAOBaseClass
    {
        private string ViewAllCustomers { get; } = "SELECT * FROM VIEW_ALL_CUSTOMERS";

        public void Create(DAOBaseClass dao)
        {
            CustomerClass customer = dao as CustomerClass;
            //throw new NotImplementedException();
            string insertSQL = "INSERT INTO dbo.Customer " +
                                "([FirstName],[LastName],[Address],[Phone])" + " VALUES " +
                                "(@FirstName, @LastName, @Address, @Phone)";

            using (SqlConnection connection = new SqlConnection(DefaultConnectionName))
            using (SqlCommand command = new SqlCommand(insertSQL, connection))
            {
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@Phone", customer.Phone);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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

        public void Delete(DAOBaseClass dao)
        {
            CustomerClass customer = dao as CustomerClass;
            //throw new NotImplementedException();
            string updateSQL = "DELETE dbo.Customer WHERE CustID = @ID";

            using (SqlConnection connection = new SqlConnection(DefaultConnectionName))
            using (SqlCommand command = new SqlCommand(updateSQL, connection))
            {
                command.Parameters.AddWithValue("@ID", int.Parse(customer.ID));
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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

        public IEnumerable<DAOBaseClass> Get()
        {
            List<CustomerClass> data = new List<CustomerClass>();

            String sqlStatement = ViewAllCustomers + " ORDER BY LastName, FirstName ";

            using (SqlConnection connection = new SqlConnection(DefaultConnectionName))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStatement, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerClass customer = new CustomerClass();
                            customer.ID = reader["CustID"].ToString();
                            customer.FirstName = reader["FirstName"].ToString();
                            customer.LastName = reader["LastName"].ToString();
                            customer.Address = reader["Address"].ToString();
                            customer.Phone = reader["Phone"].ToString();
                            data.Add(customer);
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
            return data;
        }
        public void Update(DAOBaseClass dao)
        {
            CustomerClass customer = dao as CustomerClass;

            string updateSQL =  "UPDATE dbo.Customer " +
                                "SET [FirstName] = @FirstName, [LastName] = @LastName, " +
                                     "[Address] = @Address, [Phone] = @Phone " +
                                "WHERE CustID = @ID";

            using (SqlConnection connection = new SqlConnection(DefaultConnectionName))
            using (SqlCommand command = new SqlCommand(updateSQL, connection))
            {
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@Phone", customer.Phone);
                command.Parameters.AddWithValue("@ID", customer.ID);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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

        #region Obsolete Method for the assignment => clean up later
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
        #endregion

    }
}
