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

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для ListArr.xaml
    /// </summary>
    public partial class ListArr : Window
    {
        public ListArr()
        {
            InitializeComponent();
            ComboArr.ItemsSource = ShopEntities.GetContext().Sessia2().Select(x => x.Title).Distinct().ToList();
            //ShopEntities.GetContext().GetArendaList().Single().Title
        }

        private void ComboArr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboArr.SelectedItem;
            DGridRent.ItemsSource = ShopEntities.GetContext().Sessia2().Where(x => x.Title == c.ToString()).ToList();
        }
        private void BtnBac_Click(object sender, RoutedEventArgs e)
        {
            Table win1 = new Table();
            win1.Show();
            this.Close();
        }
    }

}
