﻿using Microsoft.Win32;
using RuleAPP.Model;
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

namespace RuleAPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        Product product = new Product();
        public AddEditProductPage(Product currentProduct)
        {
            InitializeComponent();

            if (currentProduct != null)
            {
                product = currentProduct;

                btnDeleteProduct.Visibility = Visibility.Visible;
                txtArticle.IsEnabled = false;
            }
            DataContext = product;
            cmbCategory.ItemsSource = CategoryList;
            cmbCategory2.ItemsSource = CategoryList2;
        }

        public string[] CategoryList2 =
        {
            "шт.",
            "уп."
        };
        public string[] CategoryList =
        {
            "Аксессуары",
            "Автозапчасти",
            "Автосервис",
            "Съёмники подшибников",
            "Ручные инструменты",
            "Зарядные устройства"
        };


        private void btnEnterImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog GetImageDialog = new OpenFileDialog();

            GetImageDialog.Filter = "Фацлы изображений: (*.png)| *.png"; //Ставим фильтр видимости файлов
            GetImageDialog.InitialDirectory = "C:\\Users\\Home\\Desktop\\Тренировка ДЕМ ЭКЗАМЕН";
            if (GetImageDialog.ShowDialog() == true)
            {
                product.ProductPhoto = GetImageDialog.SafeFileName;
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы действительно хотите удалить {product.ProductName}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    Controller.DbConnection.GetContext().Product.Remove(product);
                    Controller.DbConnection.GetContext().SaveChanges();
                    MessageBox.Show("Запись удалена!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (product.ProductCost < 0)
                errors.AppendLine("Стоимость не может быть отрицательной!");
            //if (product.ProductDiscountAmount > product.MaxDiscountAmount)
            //    errors.AppendLine("Действующая скидка на товар не может быть больше максимальной скидки!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());   //Выводим ошибки
                return;
            }

            //if (product.ProductArticleNumber == null)
            //    Controller.DbConnection.GetContext().Product.Add(product);
            //Controller.DbConnection.GetContext().SaveChanges();
            try
            {
                Controller.DbConnection.GetContext().Product.Add(product);
                Controller.DbConnection.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
