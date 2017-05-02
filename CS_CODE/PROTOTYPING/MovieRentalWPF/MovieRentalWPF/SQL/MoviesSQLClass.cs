using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MovieRentalWPF.Model;
using MovieRentalWPF.DAO;

namespace MovieRentalWPF.SQL
{
    class MoviesSQLClass : SQLCommandClass
    {
        private string ViewAllMovies = "SELECT * FROM VIEW_ALL_MOVIES";
        public void RetrieveAllMovies(ViewModelClass model)
        {
            using (SqlConnection connection = new SqlConnection(DefaultConnectionName))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(ViewAllMovies, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        model.Clear();
                        while (reader.Read())
                        {
                            MovieClass movie = new MovieClass();
                            movie.ID = reader["MovieID"].ToString();
                            movie.Rating= reader["Rating"].ToString();
                            movie.Title = reader["Title"].ToString();
                            movie.Year = reader["Year"].ToString();
                            movie.Rental_Cost = reader["Rental_Cost"].ToString();
                            movie.Copies = reader["Copies"].ToString();
                            movie.Plot = reader["Plot"].ToString();
                            movie.Genre = reader["Genre"].ToString();
                            model.Add(movie);

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
