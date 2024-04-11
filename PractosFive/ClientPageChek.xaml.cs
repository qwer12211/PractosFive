using PractosFive.DataSet1TableAdapters;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PractosFive
{
    /// <summary>
    /// Логика взаимодействия для ClientPageChek.xaml
    /// </summary>
    public partial class ClientPageChek : Page
    {
        ChekTableAdapter chek = new ChekTableAdapter();
        public ClientPageChek()
        {
            InitializeComponent();
            dg_BD.ItemsSource = chek.GetData();
        }
        

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
