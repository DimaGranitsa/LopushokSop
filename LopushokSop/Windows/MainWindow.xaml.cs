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
using LopushokSop.DataBaseModel;
using LopushokSop.Classes;

using LopushokSop.Windows;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;

namespace LopushokSop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProductMaterial product;
        public MainWindow()
        {
            InitializeComponent();
            RefreshPAgination();
            RefreshComboBox();  
            Sortinfo();
            RefreshButtons();
            CmbSort.Items.Add("Сортировать");
            CmbFilter.ItemsSource = ConnectingClass.connect.ProductType.ToList();
            LvProduct.ItemsSource = ConnectingClass.connect.Product.ToList(); 
        }
        int pageNumber;
        private void RefreshPAgination()
        {
            LvProduct.ItemsSource = null;
            if (CmbSort.Text != null)
            {
                Sortinfo();
            }
            else
            {
                LvProduct.ItemsSource = ConnectingClass.connect.Product.Skip(pageNumber * 20).Take(20).ToList();
            }
        }
        private void RefreshComboBox()
        {
            CmbSort.Items.Add("От А-Я");
            CmbSort.Items.Add("От Я-А");
            CmbSort.Items.Add("Номер цеха по возростанию");
            CmbSort.Items.Add("Номер цеха по убыванию");
       
        }
        private void Sortinfo()
        {
            switch (CmbSort.SelectedItem)
            {
                case "От А-Я":
                    LvProduct.ItemsSource = null;
                    LvProduct.ItemsSource = ConnectingClass.connect.Product.OrderBy(x => x.Title).Skip(pageNumber * 20).Take(20).ToList();
                    break;
                case "От Я-А":
                    LvProduct.ItemsSource = null;
                    LvProduct.ItemsSource = ConnectingClass.connect.Product.OrderByDescending(x => x.Title).Skip(pageNumber * 20).Take(20).ToList();
                    break;
                case "Номер цеха по возростанию":
                    LvProduct.ItemsSource = null;
                    LvProduct.ItemsSource = ConnectingClass.connect.Product.OrderBy(x => x.ProductionWorkshopNumber).Skip(pageNumber * 20).Take(20).ToList();
                    break;
                case "Номер цеха по убыванию":
                    LvProduct.ItemsSource = null;
                    LvProduct.ItemsSource = ConnectingClass.connect.Product.OrderByDescending(x => x.ProductionWorkshopNumber).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                default:
                    LvProduct.ItemsSource = null;
                    LvProduct.ItemsSource = ConnectingClass.connect.Product.ToList();
                    break;
            }
        }
      
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LvProduct.ItemsSource = ConnectingClass.connect.Product.Where(z => z.Title.Contains(TxtSearch.Text)).ToList();
        }

      
        public void refresh()
        {
            LvProduct.ItemsSource = ConnectingClass.connect.Product.ToList();
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectingClass.connect.Product.ToList().Count % 10 == 0)
            {
                if (pageNumber == (ConnectingClass.connect.Product.ToList().Count / 10) - 1)
                    return;
            }
            else
            {
                if (pageNumber == (ConnectingClass.connect.Product.ToList().Count / 10))
                    return;
            }
            pageNumber++;
            RefreshPAgination();
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 0)
                return;
            pageNumber--;
            RefreshPAgination();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pageNumber = Convert.ToInt32(button.Content) - 1;
            RefreshPAgination();
        }
        private void RefreshButtons()
        {
            WPButtons.Children.Clear();
            if (ConnectingClass.connect.Product.ToList().Count % 10 == 0)
            {
                for (int i = 0; i < ConnectingClass.connect.Product.ToList().Count / 10; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 14;
                    WPButtons.Children.Add(button);
                }
            }
            else
            {
                for (int i = 0; i < ConnectingClass.connect.Product.ToList().Count / 10 + 1; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 14;
                    WPButtons.Children.Add(button);
                }
            }
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sortinfo();
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = CmbFilter.SelectedItem as ProductType;
            LvProduct.ItemsSource = ConnectingClass.connect.Product.Where(z => z.ProductTypeID == c.ID).ToList();
        }

        private void qw_Click(object sender, RoutedEventArgs e)
        {
            Windows.AddWindow q = new Windows.AddWindow();
            q.Show();

        }

        private void wq_Click(object sender, RoutedEventArgs e)
        {
            var a = LvProduct.SelectedItem as Product;
            if (a != null)
            {
                ConnectingClass.connect.Product.Remove(a);
                ConnectingClass.connect.SaveChanges();
                MessageBox.Show($"Запись {a.ArticleNumber} удалена", "Удаление записи", MessageBoxButton.OK, MessageBoxImage.Information);
                LvProduct.ItemsSource = ConnectingClass.connect.Product.ToList();
            }
            else
            {
                MessageBox.Show("такой записи нее");
            }
        }
    }
}
