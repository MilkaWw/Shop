using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для Arenda.xaml
    /// </summary>
    public partial class Arenda : Window
    {
        
private Pavilion pavilion;
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public List<Tenants> tenantsCollection { get; set; }
        public Tenants currentTenants { get; set; }
        public Arenda(Pavilion pavilion)
        {
            InitializeComponent();
            this.pavilion = pavilion;
            Start = DateTime.Today;
            Stop = DateTime.Today;
            tenantsCollection = ShopEntities.GetContext().Tenants.ToList(); //c адерндаторов чтобы знать кто арендует
            var tEN = new Tenants();
           
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Start <= Stop && Start >= DateTime.Today) 
            {
                bool stat = Start == DateTime.Today;
                ShopEntities.GetContext().RentOrBookPavilionInMall(!stat, pavilion.Pavilion_Number, pavilion.Shopping_center_ID, Start, Stop, currentTenants.Tenant_ID, MainWindow.employeID);
                MessageBox.Show(stat ? "Арендовано" : "Забронировано");
            }

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            EditPav win = new EditPav(pavilion);
            win.Show();
            this.Close();
        }

       
    }
}

