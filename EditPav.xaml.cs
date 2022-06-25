using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для EditPav.xaml
    /// </summary>
    public partial class EditPav : Window
    { 
        private Pavilion _currentPav = new Pavilion();
        private int reg = 0;
        int maxid = ShopEntities.GetContext().Pavilion.Max(g => g.Shopping_center_ID);
    
        public EditPav(Pavilion selectedPav)
        {
            InitializeComponent();
            if (selectedPav != null)
            {
                _currentPav = selectedPav;
                reg = 1;
            }
            else
            {
                _currentPav.Shopping_center_ID = maxid + 1;
            }

            DataContext = new { currentShop = _currentPav, listStatus = ShopEntities.GetContext().Pavilion.Select(x => x.Status_pavilion).Distinct().ToList() };

        }
        private void ButSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentPav.Floor.ToString()))
                errors.AppendLine("Укажите номер этажа");
            if (string.IsNullOrWhiteSpace(_currentPav.Pavilion_Number.ToString()))
                errors.AppendLine("Укажите номер пав.");
            if (string.IsNullOrWhiteSpace(_currentPav.Status_pavilion.ToString()))
                errors.AppendLine("Укажите статус");
            if (string.IsNullOrWhiteSpace(_currentPav.Square.ToString()))
                errors.AppendLine("Укажите площадь");
            if (string.IsNullOrWhiteSpace(_currentPav.Added_value_factor.ToString()))
                errors.AppendLine("Укажите коэф.добав.стоим.");
            if (string.IsNullOrWhiteSpace(_currentPav.Cost_per_square_meter.ToString()))
                errors.AppendLine("Укажите стоимость за кв.м");
            if (reg == 0) ShopEntities.GetContext().Pavilion.Add(_currentPav);

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            try
            {
                ShopEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена. Обновите таблицу");
                _currentPav = new Pavilion();
                Shop_centres win = new Shop_centres();
                win.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }

        }

        private void ButAr_Click(object sender, RoutedEventArgs e)
        {
            Arenda win = new Arenda(_currentPav);
            win.Show();
            this.Close();
        }
    }
}
