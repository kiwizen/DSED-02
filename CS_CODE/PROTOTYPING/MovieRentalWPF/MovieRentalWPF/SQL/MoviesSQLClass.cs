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
    class MoviesSQLClass : SQLCommandClass, IDAOBaseClass
    {
        private string ViewAllMovies = "SELECT * FROM VIEW_ALL_MOVIES";

        public void Create(DAOBaseClass dao)
        {
            MovieClass movie = dao as MovieClass;
            //throw new NotImplementedException();
            string insertSQL = "INSERT INTO dbo.Movies " +
                "([Rating],[Title],[Year],[Copies],[Plot],[Genre])" + " VALUES "  +
                "(@Rating, @Title, @Year, @Copies, @Plot, @Genre)";

            using (SqlConnection connection = new SqlConnection(DefaultConnectionName))
            using (SqlCommand command = new SqlCommand(insertSQL, connection))
            {
                command.Parameters.AddWithValue("@Rating", movie.Rating);
                command.Parameters.AddWithValue("@Title", movie.Title);
                command.Parameters.AddWithValue("@Year", movie.Year);
                command.Parameters.AddWithValue("@Copies", movie.Copies);
                command.Parameters.AddWithValue("@Plot", movie.Plot);
                command.Parameters.AddWithValue("@Genre", movie.Genre);
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
            MovieClass movie = dao as MovieClass;
            //throw new NotImplementedException();
            string updateSQL = "DELETE dbo.Movies WHERE MOVIEID = @ID";

            using (SqlConnection connection = new SqlConnection(DefaultConnectionName))
            using (SqlCommand command = new SqlCommand(updateSQL, connection))
            {
                command.Parameters.AddWithValue("@ID", int.Parse(movie.ID));
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
            List<MovieClass> data = new List<MovieClass>();

            using (SqlConnection connection = new SqlConnection(DefaultConnectionName))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(ViewAllMovies, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //model.Clear();
                        while (reader.Read())
                        {
                            MovieClass movie = new MovieClass();
                            movie.ID = reader["MovieID"].ToString();
                            movie.Rating = reader["Rating"].ToString();
                            movie.Title = reader["Title"].ToString();
                            movie.Year = reader["Year"].ToString();
                            movie.Rental_Cost = reader["Rental_Cost"].ToString();
                            movie.Copies = reader["Copies"].ToString();
                            movie.Plot = reader["Plot"].ToString();
                            movie.Genre = reader["Genre"].ToString();
                            //model.Add(movie);
                            data.Add(movie);
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
            MovieClass movie = dao as MovieClass;
            //throw new NotImplementedException();
            string updateSQL = "UPDATE dbo.Movies " +
                "SET [Rating] = @Rating, [Title] = @Title, [Year] = @Year, " +
                     "[Copies] = @Copies, [Plot] = @Plot, [Genre] = @Genre " +
                "WHERE MOVIEID = @ID";

            using (SqlConnection connection = new SqlConnection(DefaultConnectionName))
            using (SqlCommand command = new SqlCommand(updateSQL, connection))
            {
                command.Parameters.AddWithValue("@Rating", movie.Rating);
                command.Parameters.AddWithValue("@Title", movie.Title);
                command.Parameters.AddWithValue("@Year", movie.Year);
                command.Parameters.AddWithValue("@Copies", movie.Copies);
                command.Parameters.AddWithValue("@Plot", movie.Plot);
                command.Parameters.AddWithValue("@Genre", movie.Genre);
                command.Parameters.AddWithValue("@ID", movie.ID);
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
        #region to be implemented in the future
        public DAOBaseClass Get(int ID)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Old Method not used in the assignment
        public void UpdateData(MovieClass movie)
        {
            //private string updateMovie = 
        }

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
                            movie.Rating = reader["Rating"].ToString();
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
        #endregion

    }
}
