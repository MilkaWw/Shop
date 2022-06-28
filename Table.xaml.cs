﻿using System;
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
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table : Window
    {
        public Table()
        {
            InitializeComponent();
            
        }

        

        private void ButtonLI_Click(object sender, RoutedEventArgs e)
        {
            ListArr ListArr = new ListArr();
            this.Close();
            ListArr.Show();
        }

        private void ButtonSP_Click(object sender, RoutedEventArgs e)
        {
            Shop_centres Shop_centres = new Shop_centres();
            this.Close();
            Shop_centres.Show();
        }
    }
}
