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
using System.ComponentModel;

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
            //initializeAllMovieViewListTable();

            initializeMovieListView();

            initializeMovieRatingComboBox();

            ClearText();
        }

        private void initializeMovieListView()
        {
            MoviesSQLClass movieDAO = new MoviesSQLClass();
             
            Clear();
            foreach (var obj in movieDAO.Get())
                Add(obj);
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

        private void initializeMovieRatingComboBox()
        {
            
            List<string> data = new List<string>();

            foreach (var item in comboRate.Items)
                data.Add(((ComboBoxItem)item).Content.ToString());

            comboRate.Items.Clear();
            
            //comboRate.ItemsSource = null;

            comboRate.ItemsSource = data.OrderByDescending(c => c).ToArray();
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


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //todo: validate screen fields



            //todo: create a Movie dao class 
            MovieClass movie = new MovieClass();
            movie.ID = txtID.Text;
            movie.Title = txtTitle.Text;
            movie.Rating = comboRate.SelectedItem.ToString();
            movie.Year = comboYear.SelectedItem.ToString();
            movie.Plot = txtPlot.Text;
            movie.Copies = txtCopies.Text;
            movie.Genre = txtGenre.Text;

            //todo: check movie id, create a record if blank, else update the record
            //
            MoviesSQLClass sql = new MoviesSQLClass();
            if(movie.ID == string.Empty)
            {
                sql.Create(movie);
            }
            else
            {
                sql.Update(movie);
            }
            initializeMovieListView();
            SetMovieGrid(false);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            SetMovieGrid(false);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            ClearText();
            SetMovieGrid(true);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (txtID.Text == string.Empty)
            {
                MessageBox.Show("Error !!!\nPlease select a Customer record first.");
                return;
            }
            SetMovieGrid(true);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (txtID.Text == string.Empty)
            {
                MessageBox.Show("Error !!!\nPlease select a Customer record first.");
                return;
            }
            MovieClass movie = new MovieClass();
            movie.ID = txtID.Text;
            MoviesSQLClass sql = new MoviesSQLClass();
            sql.Delete(movie);
            initializeMovieListView();
            ClearText();
        }

        private void SetMovieGrid(bool flag)
        {
            SaveControl(flag);
            EditControl(!flag);
            if (flag)
                txtTitle.Focus();
            else
                ClearText();
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

        private void EditControl(bool flag)
        {
            buttonNew.IsEnabled = flag;
            buttonEdit.IsEnabled = flag;
            buttonDelete.IsEnabled = flag;
        }

        private void SaveControl(bool flag)
        {
            buttonSave.IsEnabled = flag;
            buttonCancel.IsEnabled = flag;
            EditMovieGrid.IsEnabled = flag;
        }

        #region Old Method Not Used in the Assigment 
        private void Clear()
        {
            ViewTable.Items.Clear();
        }

        private void initializeAllMovieViewListTable()
        {
            viewTableModel = new ViewModelClass();
            viewTableModel.AddMethod = Add;
            viewTableModel.ClearMethod = Clear;

            MoviesSQLClass service = new MoviesSQLClass();

            service.RetrieveAllMovies(viewTableModel);
            



        }
        #endregion

    }
}
