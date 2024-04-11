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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        List<string> nameTable = new List<string> { "Авторизация", "Чек", "Клиенты", "Поставки", "Отдел", "Блюда", "Руководители", "Заказы", "Оплата", "Должность", "Поставщик", "Сотрудник", "Сотрудник и отдел" };
        public Admin()
        {
            InitializeComponent();
            cb_DB.ItemsSource = nameTable;
        }
        private void cb_DB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameTable = cb_DB.SelectedItem.ToString();

            if (nameTable == "Авторизация")
            {
                PageFrame.Content = new AdminPageAuthorizations();
            }
            else if (nameTable == "Чек")
            {
                PageFrame.Content = new AdminPageCheck();
            }
            else if (nameTable == "Клиенты")
            {
                PageFrame.Content = new AdminPageClients();
            }
            else if (nameTable == "Поставки")
            {
                PageFrame.Content = new AdminPageDelivery();
            }
            else if (nameTable == "Отдел")
            {
                PageFrame.Content = new AdminPageDepartment();
            }
            else if (nameTable == "Блюда")
            {
                PageFrame.Content = new AdminPageDish();
            }
            else if (nameTable == "Руководители")
            {
                PageFrame.Content = new AdminPageManagers();
            }
            else if (nameTable == "Заказы")
            {
                PageFrame.Content = new AdminPageOrders();
            }
            else if (nameTable == "Оплата")
            {
                PageFrame.Content = new AdminPagePayment();
            }
            else if (nameTable == "Должность")
            {
                PageFrame.Content = new AdminPagePost();
            }
            else if (nameTable == "Поставщик")
            {
                PageFrame.Content = new AdminPageProviders();
            }
            else if (nameTable == "Сотрудник")
            {
                PageFrame.Content = new AdminPageStaff();
            }
            else if (nameTable == "Сотрудник и отдел")
            {
                PageFrame.Content = new AdminPageStaffDepartment();
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
