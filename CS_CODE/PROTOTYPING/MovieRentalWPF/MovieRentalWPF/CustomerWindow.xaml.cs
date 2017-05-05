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
using MovieRentalWPF.SQL;
using MovieRentalWPF.DAO;

namespace MovieRentalWPF
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private ViewModelClass viewTableModel;
        public CustomerWindow()
        {
            InitializeComponent();

            InitializeDefaultSetting();

            initializeCustomerListView();
        }

        private void InitializeDefaultSetting()
        {
            ClearText();
            SaveControl(false);
            EditControl(true);
        }

        private void ClearText()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtID.Text = string.Empty;
        }

        private void initializeCustomerListView()
        {
            CustomersSQLClass customerDAO = new CustomersSQLClass();

            ViewTable.Items.Clear();
            ClearText();

            foreach (var obj in customerDAO.Get())
            {
                CustomerClass customer = obj as CustomerClass;
                ViewTable.Items.Add(new
                {
                    ID_FirstName = customer.FirstName,
                    ID_SurName = customer.LastName,
                    ID_ContactNo = customer.Phone,
                    ID_Address = customer.Address,
                    ID = customer.ID
                });
            }
        }
        private void ExitWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            txtID.Text = item.ID;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            ClearText();
            SetCustomerGrid(true);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (txtID.Text == string.Empty)
            {
                MessageBox.Show("Error !!!\nPlease select a Customer record first.");
                return;
            }
            SetCustomerGrid(true);
        }

        private void SetCustomerGrid(bool flag)
        {
            SaveControl(flag);
            EditControl(!flag);
            if (flag)
                txtFirstName.Focus();
            else
                ClearText();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (txtID.Text == string.Empty)
            {
                MessageBox.Show("Error !!!\nPlease select a Customer record first.");
                return;
            }
            CustomerClass customer = new CustomerClass();
            customer.ID = txtID.Text;

            CustomersSQLClass sql = new CustomersSQLClass();
            sql.Delete(customer);

            initializeCustomerListView();
            ClearText();
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
            EditCustomerGrid.IsEnabled = flag;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //todo: validate screen fields



            //
            CustomerClass customer = new CustomerClass();
            customer.ID = txtID.Text;
            customer.FirstName = txtFirstName.Text;
            customer.LastName = txtLastName.Text;
            customer.Phone = txtContactNo.Text;
            customer.Address = txtAddress.Text;

            //
            CustomersSQLClass sql = new CustomersSQLClass();
            if (customer.ID == string.Empty)
            {
                sql.Create(customer);
            }
            else
            {
                sql.Update(customer);
            }
            initializeCustomerListView();
            SetCustomerGrid(false);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            SetCustomerGrid(false);
        }
        #region Obsolete Method for the assignment => clean up later
        private void Clear()
        {
            ViewTable.Items.Clear();
        }
        private void initializeAllCustomerViewListTable()
        {
            viewTableModel = new ViewModelClass();
            viewTableModel.AddMethod = Add;
            viewTableModel.ClearMethod = Clear;

            CustomersSQLClass service = new CustomersSQLClass();

            service.RetrieveAllCustomers(viewTableModel);
        }
        private void Add(object p)
        {
            //ViewTable.Items.Add(p);
            CustomerClass customer = p as CustomerClass;

            ViewTable.Items.Add(new
            {
                ID_FirstName = customer.FirstName,
                ID_SurName = customer.LastName,
                ID_ContactNo = customer.Phone,
                ID_Address = customer.Address,
                ID = customer.ID
            });

        }
        #endregion
    }
}
