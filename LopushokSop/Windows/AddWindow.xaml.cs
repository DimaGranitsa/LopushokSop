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
using LopushokSop.Classes;
using LopushokSop.DataBaseModel;


namespace LopushokSop.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
            CmbType.ItemsSource = ConnectingClass.connect.ProductType.ToList();
        }

        private void TxtTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
           // LanguagesErrorClass.NumberError(Convert.ToChar(TxtTitle.Text));

        }

        private void TxtArtile_TextChanged(object sender, TextChangedEventArgs e)
        {
           // LanguagesErrorClass.RussianError(Convert.ToChar(TxtArtile.Text));
        }

        private void TxtCountPerson_TextChanged(object sender, TextChangedEventArgs e)
        {
           // LanguagesErrorClass.RussianError(Convert.ToChar(TxtArtile.Text));
        }

        private void TxtDecription_TextChanged(object sender, TextChangedEventArgs e)
        {
           // LanguagesErrorClass.NumberError(Convert.ToChar(TxtDecription.Text));
        }

        private void TxtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
           // LanguagesErrorClass.RussianError(Convert.ToChar(TxtArtile.Text));
        }

        private void TxtMinCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            //LanguagesErrorClass.RussianError(Convert.ToChar(TxtArtile.Text));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtArtile.Text)|| string.IsNullOrEmpty(TxtCountPerson.Text) || string.IsNullOrEmpty(TxtDecription.Text) || string.IsNullOrEmpty(TxtMinCost.Text)|| string.IsNullOrEmpty(TxtNumber.Text) || string.IsNullOrEmpty(TxtTitle.Text) || CmbType.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста заполните все поля");
            }
            else
            {
            Product product = new Product()
            {
                Title= TxtTitle.Text,
                ProductTypeID = ((ProductType)CmbType.SelectedItem).ID,
                ArticleNumber = TxtArtile.Text,
                Description = TxtDecription.Text,
                ProductionPersonCount = Convert.ToInt32(TxtCountPerson.Text),
                ProductionWorkshopNumber = Convert.ToInt32(TxtCountPerson.Text),
                MinCostForAgent = Convert.ToDecimal(TxtMinCost.Text)
            };
            ConnectingClass.connect.Product.Add(product);
            ConnectingClass.connect.SaveChanges();
            MessageBox.Show("Запись добалена","Добавление");

            }
        }
    }
}
