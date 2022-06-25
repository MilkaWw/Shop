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
    /// Логика взаимодействия для Management_sourcers.xaml
    /// </summary>
    public partial class Management_sourcers : Window
    {
       int idEmp = 0;
        public Management_sourcers(Staff _currentEmp)
        {
            InitializeComponent();
            idEmp = _currentEmp?.Employee_ID ?? 0;
            DGridEmp.ItemsSource = ShopEntities.GetContext().Staff.Where(x => x.Role != "Удален").ToList();
        }

        private void BtnBac_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win1 = new MainWindow();
            win1.Show();
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAdm win = new AddAdm(null);
            win.Show();
            this.Close();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridEmp.SelectedItems.Cast<Staff>().FirstOrDefault();
            AddAdm win = new AddAdm(upd);//, PavilionsEntities.GetContext().Employees.Find(idEmp));
            win.Show();
            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var EmpForRemoving = DGridEmp.SelectedItems.Cast<Staff>().ToList();
            if (MessageBox.Show("Вы точно хотите Удалить этих(-ого) Сотрудников", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    EmpForRemoving.ForEach(x => x.Role = "Удален");
                    ShopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены!");
                    DGridEmp.ItemsSource = ShopEntities.GetContext().Staff.Where(x => x.Role != "Удален").ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text != "")
            {
                var filteredList = ShopEntities.GetContext().Staff.Where(t => t.Surname.ToLower().Contains(tb.Text.ToLower())).ToList(); //Получаем список по введенному тексту в TextBox(Поиск)
                DGridEmp.ItemsSource = null; //Обнуляем список
                DGridEmp.ItemsSource = filteredList; //Обновляем список
            }
            else
            {
                DGridEmp.ItemsSource = ShopEntities.GetContext().Staff.Where(x => x.Role != "Удален").ToList(); //Первоначальный список
            }
        }
    }
}

