using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using RentVideoWPF.Model;

namespace RentVideoWPF.SERVICE
{
    public class RentedMoviesSQLClass : SQLCommandClass
    {

        public RentedMoviesSQLClass() : base()
        {
        }

        public void RetrieveAvailableMoviesToBeRented(ViewModelClass model)
        {
            using (SqlConnection connection = new SqlConnection(DefaultConnectionName))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(ViewAvaialbeMovies, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        model.Clear();
                        while (reader.Read())
                        {
                            model.Add(new
                            {
                                ID_Title = reader["Title"].ToString(),
                                ID_Year = reader["Year"].ToString(),
                                ID_Copies = reader["Copies"].ToString()
                            });

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
