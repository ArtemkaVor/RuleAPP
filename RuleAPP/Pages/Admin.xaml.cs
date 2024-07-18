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
using RuleAPP.Windows;
using RuleAPP.Model;
using RuleAPP.Pages;

namespace RuleAPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        User user = new User();
        public Admin(User currentUser)
        {
            InitializeComponent();
            var product = Controller.DbConnection.GetContext().Product.ToList();
            LViewProduct.ItemsSource = product;
            DataContext = this;
            UpdateData();
            txtAllAmount.Text = product.Count().ToString();
            user = currentUser;
            User();
        }
        private void User()
        {
            if (user != null)
                txtFullname.Text = user.UserSurname.ToString() + user.UserName.ToString() + " " + user.UserPatronymic.ToString();
            else
                txtFullname.Text = "Гость";
        }

        public string[] SortingList { get; set; } =
        {
            "Без сортировки",
            "Возраст",
            "Убыв",

        };

        public string[] FilterList { get; set; } =
        {
            "все",
            "0%-9,99%",
            "10%-14,99",
            "15% и more"
        };

        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            var result = Controller.DbConnection.GetContext().Product.ToList();

            if (cmbSorting.SelectedIndex == 1)
                result = result.OrderBy(p => p.ProductCost).ToList();
            if (cmbSorting.SelectedIndex == 2)
                result = result.OrderByDescending(p => p.ProductCost).ToList();


            if (cmbFilter.SelectedIndex == 1)
                result = result.Where(p => p.MaxDiscountAmount >= 0 && p.MaxDiscountAmount < 10).ToList();
            if (cmbFilter.SelectedIndex == 2)
                result = result.Where(p => p.MaxDiscountAmount >= 10 && p.MaxDiscountAmount < 15).ToList();
            if (cmbFilter.SelectedIndex == 3)
                result = result.Where(p => p.MaxDiscountAmount >= 15).ToList();
            result = result.Where(p => p.ProductName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            LViewProduct.ItemsSource = result;
            txtResultAmount.Text = result.Count().ToString();

        }

        private void txtSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        List<Product> orderProducts = new List<Product>();
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            orderProducts.Add(LViewProduct.SelectedItem as Product);
            if (orderProducts.Count > 0)
            {
                btnOrder.Visibility = Visibility.Visible;
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow order = new OrderWindow(orderProducts, user);
            order.ShowDialog();
        }

        private void LViewProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditProductPage(LViewProduct.SelectedItem as Product));
        }

        private void btnAddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditProductPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Controller.DbConnection.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                LViewProduct.ItemsSource = Controller.DbConnection.GetContext().Product.ToList();
            }
        }
    }
}
