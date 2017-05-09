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
using MovieRentalWPF.DAO;
using MovieRentalWPF.SQL;

namespace MovieRentalWPF
{
    /// <summary>
    /// Interaction logic for IssueMovieWindow.xaml
    /// </summary>
    public partial class IssueMovieWindow : Window
    {
        public IssueMovieWindow()
        {
            InitializeComponent();

            initializeMovieListView();
        }
        private void ClearText()
        {
            txtTitle.Text = string.Empty;
            txtGenre.Text = string.Empty;
            txtCopies.Text = string.Empty;
            txtPlot.Text = string.Empty;
            txtID.Text = string.Empty;
            txtCost.Text = string.Empty;
        }
        private void initializeMovieListView()
        {
            MoviesSQLClass movieDAO = new MoviesSQLClass();

            ViewTable.Items.Clear();
            ClearText();

            foreach (var obj in movieDAO.GetMovieForRent())
            {
                MovieClass movie = obj as MovieClass;
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
        }
        private void comboYear_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();

            for (int i = 0, start_year = 1927; start_year + i <= DateTime.Now.Year; i++)
                data.Add((start_year + i).ToString());

            // Cast the sender object as ComboBox object
            var comboBox = sender as ComboBox;

            comboBox.ItemsSource = data.OrderByDescending(c => c).ToArray();

        }
        private void SelectedMovieClick(object sender, MouseButtonEventArgs e)
        {
            ListView view = sender as ListView;

            if (view.SelectedItem is null) return;

            dynamic item = view.SelectedItems[0];
            txtTitle.Text = item.ID_Title;
            txtGenre.Text = item.ID_Genre;
            txtCopies.Text = item.ID_Copies;
            txtPlot.Text = item.ID_Plot;

            comboRate.SelectedItem = item.ID_Rating;
            comboYear.SelectedItem = item.ID_Year;

            // The following textbox objects are created to store data
            //   but is not mean to be changed by user, hence the objects are hidden.
            txtCost.Text = item.ID_Cost;
            txtID.Text = item.ID;
        }
        private void ExitWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
