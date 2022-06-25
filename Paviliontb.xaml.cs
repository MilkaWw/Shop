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
    /// Логика взаимодействия для Paviliontb.xaml
    /// </summary>
    public partial class Paviliontb : Window
    {
        string _name;
        string _name2;
        int idShop = 0;
        public Paviliontb(Shopping_center currentShop)
        {

            InitializeComponent();
            int idshop = currentShop.Shopping_center_ID;
            DGridPavilions.ItemsSource = ShopEntities.GetContext().Pavilion.Where(x => x.Added_value_factor > 0.1).ToList();
            DGridPavilions.ItemsSource = ShopEntities.GetContext().Pavilion.Where(x => x.Shopping_center_ID == idshop).ToList();
            ComboFloor.ItemsSource = ShopEntities.GetContext().Pavilion.Select(x => x.Floor).Distinct().ToList();
            ComboStatus.ItemsSource = ShopEntities.GetContext().Pavilion.Select(x => x.Status_pavilion).Distinct().ToList();

        }
        private void BtnBac_Click(object sender, RoutedEventArgs e)
        {
            Shop_centres win1 = new Shop_centres();
            win1.Show();
            this.Close();
        }
        private void BEdi_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridPavilions.SelectedItems.Cast<Pavilion>().FirstOrDefault();
            EditPav win = new EditPav(upd);
            win.Show();
            this.Close();
        }
        private void BtnClick_Add(object sender, RoutedEventArgs e)
        {
            EditPav win = new EditPav(null);
            win.Show();
            this.Close();
        }
        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboStatus.SelectedItem;
            List<Pavilion> SearchType = null;
            SearchType = ShopEntities.GetContext().Pavilion.Where(b => b.Status_pavilion.ToString() == c.ToString() && b.Added_value_factor > 0.1 && b.Shopping_center_ID == idShop).ToList();
            DGridPavilions.ItemsSource = SearchType;
        }
        private void SquareTextTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            _name = SquareTextFrom.Text;
            _name2 = SquareTextTo.Text;
            double num1 = 0;
            double.TryParse(_name, out num1);
            double num2 = 0;
            double.TryParse(_name2, out num2);

            DGridPavilions.ItemsSource = ShopEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.Shopping_center_ID == idShop && b.Added_value_factor > 0.1).ToList();

        }


        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            {
                _name = SquareTextFrom.Text;
                _name2 = SquareTextTo.Text;
                double num1 = 0;
                double.TryParse(_name, out num1);
                double num2 = 0;
                double.TryParse(_name2, out num2);
                var c = ComboStatus.SelectedItem;
                var a = ComboFloor.SelectedItem;

                try
                {
                    if (c == null)
                        DGridPavilions.ItemsSource = ShopEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.Shopping_center_ID == idShop && b.Added_value_factor > 0.1 && b.Floor.ToString() == a.ToString()).ToList();
                    if (a == null && num1.ToString() != null || num2.ToString() != null)
                        DGridPavilions.ItemsSource = ShopEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.Shopping_center_ID == idShop && b.Added_value_factor > 0.1 && b.Status_pavilion.ToString() == c.ToString()).ToList();
                    if (num1 == 0 && num2 == 0)
                        DGridPavilions.ItemsSource = ShopEntities.GetContext().Pavilion.Where(b => b.Shopping_center_ID == idShop && b.Floor.ToString() == a.ToString() && b.Added_value_factor > 0.1 && b.Status_pavilion.ToString() == c.ToString()).ToList();
                    if (num1.ToString() == null && num2.ToString() == null)
                        DGridPavilions.ItemsSource = ShopEntities.GetContext().Pavilion.Where(b => b.Shopping_center_ID == idShop && b.Floor.ToString() == a.ToString() && b.Added_value_factor > 0.1 && b.Status_pavilion.ToString() == c.ToString()).ToList();
                    if (num1 != 0 || num2 != 0 && c != null && a != null)
                        DGridPavilions.ItemsSource = ShopEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.Shopping_center_ID == idShop && b.Floor.ToString() == a.ToString() && b.Added_value_factor > 0.1 && b.Status_pavilion.ToString() == c.ToString()).ToList();
                    if (a == null && c != null && num1.ToString() != null && num2.ToString() != null)
                        DGridPavilions.ItemsSource = ShopEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.Shopping_center_ID == idShop && b.Added_value_factor > 0.1 && b.Status_pavilion.ToString() == c.ToString()).ToList();
                    if (a == null && num1 != 0 && num2 != 0)
                        DGridPavilions.ItemsSource = ShopEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.Shopping_center_ID == idShop && b.Added_value_factor > 0.1 && b.Status_pavilion.ToString() == c.ToString()).ToList();
                }
                catch
                {
                }
            }
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var PavilionsForRemoving = DGridPavilions.SelectedItems.Cast<Pavilion>().ToList();
            if (MessageBox.Show("Вы точно хотите удалить эти(-от) ТЦ", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PavilionsForRemoving.ForEach(x => x.Status_pavilion = "Удален");
                    ShopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены!");
                    DGridPavilions.ItemsSource = ShopEntities.GetContext().Pavilion.Where(b => b.Added_value_factor > 0.1).ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void ComboFloor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboFloor.SelectedItem;
            List<Pavilion> SearchType = null;
            SearchType = ShopEntities.GetContext().Pavilion.Where(b => b.Floor.ToString() == c.ToString() && b.Added_value_factor > 0.1 && b.Added_value_factor > 0.1 && b.Shopping_center_ID == idShop).ToList();
            DGridPavilions.ItemsSource = SearchType;
        }
    }
}