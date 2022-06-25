using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace Shop


{
    /// <summary>
    /// Логика взаимодействия для AddEmp.xaml
    /// </summary>
    public partial class AddAdm : Window
    {
        private Staff _currentAdm;
        private int reg = 0;
        public AddAdm(Staff upd)
        {
            InitializeComponent();
            if (upd != null)
            {
                reg = 5;
                _currentAdm = upd;
            }
            else
                _currentAdm = new Staff();
            DataContext = _currentAdm;
        }







        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            Management_sourcers win = new Management_sourcers(null);
            win.Show();
            this.Close();
        }

        private void ButSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentAdm.Surname.ToString()))
                errors.AppendLine("Укажите Фамилия");
            if (string.IsNullOrWhiteSpace(_currentAdm.Name.ToString()))
                errors.AppendLine("Укажите Имя");
            if (string.IsNullOrWhiteSpace(_currentAdm.Middle_name.ToString()))
                errors.AppendLine("Укажите Отчество");
            if (string.IsNullOrWhiteSpace(_currentAdm.Login.ToString()))
                errors.AppendLine("Укажите Логин");
            if (string.IsNullOrWhiteSpace(_currentAdm.Password.ToString()))
                errors.AppendLine("Укажите Пароль");
            if (string.IsNullOrWhiteSpace(_currentAdm.Role.ToString()))
                errors.AppendLine("Укажите Роль");
            if (reg == 0) ShopEntities.GetContext().Staff.Add(_currentAdm);

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            try
            {
                ShopEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена. Обновите таблицу");
               // _currentAdm = new Staff();
                Management_sourcers win1 = new Management_sourcers(null);
                win1.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.InnerException.Message.ToString());
            }
        }

        private void But_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files | *.BMP;*.JPG;*.PNG";
            fileDialog.InitialDirectory = @"C:\Users\damir\Desktop\уч.пр\Image Сотрудники";

            fileDialog.Title = "Выбор фото Сотрудника";

            if (fileDialog.ShowDialog() == true)
            {

                _currentAdm.Photo = File.ReadAllBytes(fileDialog.FileName);
            }
            MessageBox.Show(" Файл выбран ");
        }
    }
}
