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
            initializeCustomerListView();
            initializeMovieListView();
        }
        private void ClearMovieText()
        {
            txtTitle.Text = string.Empty;
            txtGenre.Text = string.Empty;
            txtCopies.Text = string.Empty;
            txtPlot.Text = string.Empty;
            txtMovieID.Text = string.Empty;
            txtCost.Text = string.Empty;
        }
        private void initializeCustomerListView()
        {
            CustomersSQLClass customerDAO = new CustomersSQLClass();

            CustomerViewTable.Items.Clear();
            ClearCustomerText();

            foreach (var obj in customerDAO.Get())
            {
                CustomerClass customer = obj as CustomerClass;
                CustomerViewTable.Items.Add(new
                {
                    ID_FirstName = customer.FirstName,
                    ID_SurName = customer.LastName,
                    ID_ContactNo = customer.Phone,
                    ID_Address = customer.Address,
                    ID = customer.ID
                });
            }
        }

        private void ClearCustomerText()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCustomerID.Text = string.Empty;
        }

        private void initializeMovieListView()
        {
            MoviesSQLClass movieDAO = new MoviesSQLClass();

            MovieViewTable.Items.Clear();
            ClearMovieText();

            foreach (var obj in movieDAO.GetMovieForRent())
            {
                MovieClass movie = obj as MovieClass;
                MovieViewTable.Items.Add(new
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
            txtMovieID.Text = item.ID;
        }
        private void ExitWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TransferMovieFromTable(ListView Source, ListView Destination)
        {
            if (Source.SelectedItem is null) return;

            int i = Source.SelectedIndex;

            dynamic item = Source.SelectedItems[0];
            Destination.Items.Add(new
            {
                ID_Title = item.ID_Title,
                ID_Year = item.ID_Year,
                ID_Genre = item.ID_Genre,
                ID_Rating = item.ID_Rating,
                ID_Copies = item.ID_Copies,
                ID_Plot = item.ID_Plot,
                ID_Cost = item.ID_Cost,
                ID = item.ID
            });

            Source.Items.RemoveAt(i);
            ClearMovieText();
        }

        private void AddToBasket(object sender, RoutedEventArgs e)
        {
            TransferMovieFromTable(MovieViewTable, IssueTable);
        }



        private void RemoveFromBasket(object sender, RoutedEventArgs e)
        {
            TransferMovieFromTable(IssueTable, MovieViewTable);        
        }


        private void SelectedCustomerClick(object sender, MouseButtonEventArgs e)
        {
            ListView view = sender as ListView;

            if (view.SelectedItem is null) return;

            dynamic item = view.SelectedItems[0];
            txtFirstName.Text = item.ID_FirstName;
            txtLastName.Text = item.ID_SurName;
            txtContactNo.Text = item.ID_ContactNo;
            txtAddress.Text = item.ID_Address;
            txtCustomerID.Text = item.ID;
        }
    }
}
