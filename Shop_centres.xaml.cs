using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для Shop_centres.xaml
    /// </summary>
    public partial class Shop_centres : Window
    {
        public Shop_centres()
        {
            InitializeComponent();
            DGridShopping.ItemsSource = ShopEntities.GetContext().Shopping_center.OrderBy(x => x.City).ThenBy(x => x.Status_center).Where(x => x.Status_center != "Удален").Where(x => x.The_Coefficient_Of_Added_Value > 0.1).ToList();
            ComboCity.ItemsSource = ShopEntities.GetContext().Shopping_center.Select(x => x.City).Distinct().ToList();
            ComboStatus.ItemsSource = ShopEntities.GetContext().Shopping_center.Where(x => x.Status_center != "Удален").Select(x => x.Status_center).Distinct().ToList();
            

        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (ComboStatus.SelectedIndex == 0) //первый элемент списка
            {
                DGridShopping.ItemsSource = ShopEntities.GetContext().Shopping_center.Where(x => x.Status_center == "План").ToList();
            }
            if (ComboStatus.SelectedIndex == 1)//второй элемент списка
            {
                DGridShopping.ItemsSource = ShopEntities.GetContext().Shopping_center.Where(x => x.Status_center == "Строительсто").ToList();
            }
            if (ComboStatus.SelectedIndex == 2)//третий элемент списка
            {
                DGridShopping.ItemsSource = ShopEntities.GetContext().Shopping_center.Where(x => x.Status_center == "Реализация").ToList();
            }
        }

        private void ComboCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboCity.SelectedItem;
            List<Shopping_center> SearchType = null;
            SearchType = ShopEntities.GetContext().Shopping_center.Where(b => b.City == c.ToString() && b.Status_center != "Удален").ToList();
            DGridShopping.ItemsSource = SearchType;
        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridShopping.SelectedItems.Cast<Shopping_center>().FirstOrDefault();
            Edit Edit = new Edit(upd);
            this.Close();
            Edit.Show();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win1 = new MainWindow();
            win1.Show();
            this.Close();
        }

        private void BtnClick_Delete(object sender, RoutedEventArgs e)
        {
            var ShoppingsForRemoving = DGridShopping.SelectedItems.Cast<Shopping_center>().ToList();
            if (MessageBox.Show("Вы уверены, что хотите удалить этот ТЦ", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ShoppingsForRemoving.ForEach(x => x.Status_center = "Удален");
                    ShopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Запись удалена!");
                    DGridShopping.ItemsSource = ShopEntities.GetContext().Shopping_center.ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnClick_Add(object sender, RoutedEventArgs e)
        {
            Edit win = new Edit(null);
            win.Show();
            this.Close();
        }
    }
}

      