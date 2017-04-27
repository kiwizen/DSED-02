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

namespace MovieRentalWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void OpenCustomerScreen(object sender, RoutedEventArgs e)
        {
            CustomerWindow window = new CustomerWindow();
            window.Show();
        }

        private void IssueMovie(object sender, RoutedEventArgs e)
        {
            IssueMovieWindow window = new IssueMovieWindow();
            window.Show();
        }
    }
}
