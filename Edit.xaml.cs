using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private Shopping_center _currentShop = new Shopping_center();
        private int reg = 0;
        int maxid = ShopEntities.GetContext().Shopping_center.Max(g => g.Shopping_center_ID);
        public Edit(Shopping_center selectedShop)
        {
            InitializeComponent();
           
            if (selectedShop != null)
            {
                _currentShop = selectedShop;
                reg = 1;
            }
            else
            {
                _currentShop.Shopping_center_ID = maxid + 1;
            }

            DataContext = new { currentShop = _currentShop, listStatus = ShopEntities.GetContext().Shopping_center.Select(x => x.Status_center).Distinct().ToList() };

        }

        private void But_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files | *.BMP;*.JPG;*.PNG";
            fileDialog.InitialDirectory = @"C:\Users\damir\Desktop\уч.пр\Image ТЦ";

            fileDialog.Title = "Выбор фото ТЦ";

            if (fileDialog.ShowDialog() == true)
            {

                _currentShop.Photo = File.ReadAllBytes(fileDialog.FileName);
            }
            MessageBox.Show(" Файл выбран ");
        }

        private void ButSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentShop.Title.ToString()))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(_currentShop.City.ToString()))
                errors.AppendLine("Укажите город");
            if (string.IsNullOrWhiteSpace(_currentShop.Pavilion_Number.ToString()))
                errors.AppendLine("Укажите количество павильонов");
            if (string.IsNullOrWhiteSpace(_currentShop.Cost.ToString()))
                errors.AppendLine("Укажите стоимость тц");
            if (string.IsNullOrWhiteSpace(_currentShop.The_Coefficient_Of_Added_Value.ToString()))
                errors.AppendLine("Укажите коэф.добав.стоим.");
            if (string.IsNullOrWhiteSpace(_currentShop.Number_of_floors.ToString()))
                errors.AppendLine("Укажите этажность");
            if (reg == 0) ShopEntities.GetContext().Shopping_center.Add(_currentShop);

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            try
            {
                ShopEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена. Обновите таблицу");
                _currentShop = new Shopping_center();
                Shop_centres win = new Shop_centres();
                win.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
        }

        private void ButPav_Click(object sender, RoutedEventArgs e)
        {
            var currentShop = _currentShop;
            Paviliontb pavilion = new Paviliontb(currentShop);
            this.Close();
            pavilion.Show();
        }
    }
    }

