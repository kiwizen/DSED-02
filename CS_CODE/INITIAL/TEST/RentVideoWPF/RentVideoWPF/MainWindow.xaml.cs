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
using RentVideoWPF.Model;
using RentVideoWPF.SERVICE;

namespace RentVideoWPF
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModelClass viewTableModel;
        public MainWindow()
        {
            InitializeComponent();
            initializeMovieViewListTable();
        }

        private void initializeMovieViewListTable()
        {
            viewTableModel = new ViewModelClass();
            viewTableModel.AddMethod = Add;
            viewTableModel.ClearMethod = Clear;

            RentedMoviesSQLClass service = new RentedMoviesSQLClass();

            service.RetrieveAvailableMoviesToBeRented(viewTableModel);
        }

        private void Clear()
        {
            ViewTable.Items.Clear();
        }

        private void Add(object p)
        {
            ViewTable.Items.Add(p);
        }
    }
}
