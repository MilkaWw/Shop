using System.Linq;
using System.Windows;


namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int employeID;
        static int counterButton; //считает, сколько раз была нажата кнопка входа
        public MainWindow()
        {
            InitializeComponent();

            Login.MaxLength = 60;
            Password.MaxLength = 6;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ShopEntities db = new ShopEntities())
            {
                var sqlUser = (from Staff in db.Staff
                               where Staff.Login.ToLower() == Login.Text.ToLower() && Staff.Password == Password.Password
                               select Staff).FirstOrDefault();

                if (sqlUser != null)
                {
                    if (sqlUser.Role == "Администратор")
                    {
                        AppInfo.SetEmployee(sqlUser.Employee_ID);
                        Management_sourcers Management_sourcers = new Management_sourcers(new Staff());
                        this.Close();
                        Management_sourcers.Show();

                    }
                    else //if (sqlUser.Role == "Менеджер А" || sqlUser.Role == "Менеджер С") 
                    {
                        Table Table = new Table();
                        this.Close();
                        Table.Show();
                     
                    }
                }
                else
                {

                    MessageBox.Show("Пользователь не найден", "", MessageBoxButton.OK);
                }
            }
            counterButton++;

            if (counterButton >= 3)
            {
                Captcha captcha = new Captcha();
                captcha.Show();

            }
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}

