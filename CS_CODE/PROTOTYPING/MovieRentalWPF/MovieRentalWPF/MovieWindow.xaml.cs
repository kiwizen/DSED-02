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
using System.Windows.Shapes;
using MovieRentalWPF.Model;
using MovieRentalWPF.DAO;
using MovieRentalWPF.SQL;

namespace MovieRentalWPF
{
    /// <summary>
    /// Interaction logic for MovieWindow.xaml
    /// </summary>
    public partial class MovieWindow : Window
    {
        private ViewModelClass viewTableModel;
        public MovieWindow()
        {
            InitializeComponent();
            initializeAllMovieViewListTable();
        }

        private void initializeAllMovieViewListTable()
        {
            viewTableModel = new ViewModelClass();
            viewTableModel.AddMethod = Add;
            viewTableModel.ClearMethod = Clear;

            MoviesSQLClass service = new MoviesSQLClass();

            service.RetrieveAllMovies(viewTableModel);
        }

        private void Clear()
        {
            ViewTable.Items.Clear();
        }

        private void Add(object p)
        {
            //ViewTable.Items.Add(p);
            MovieClass movie = p as MovieClass;

            ViewTable.Items.Add(new
            {
                ID_Title = movie.Title,
                ID_Year = movie.Year,
                ID_Genre = movie.Genre,
                ID_Rating = movie.Rating,
                ID_Copies = movie.Copies,
                ID_Plot = movie.Plot,
                ID_Cost = movie.Rental_Cost,
                ID = movie.ID
            });

        }

        private void ExitWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectedMovieClick(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
