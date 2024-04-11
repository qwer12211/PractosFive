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

namespace PractosFive
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        List<string> nameTable = new List<string> { "Чек", "Блюдо", "Оплатат" };
        public Client()
        {
            InitializeComponent();
            cb_DB.ItemsSource = nameTable;
        }

        private void cb_DB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameTable = cb_DB.SelectedItem.ToString();

            if (nameTable == "Чек")
            {
                PageFrame.Content = new ClientPageChek();
            }
            else if (nameTable == "Блюдо")
            {
                PageFrame.Content = new ClientPageDish();
            }
            else if (nameTable == "Оплатат")
            {
                PageFrame.Content = new ClientPagePayment();
            }

        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void PageFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
