using PractosFive.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для AdminPageOrders.xaml
    /// </summary>
    public partial class AdminPageOrders : Page
    {
        OrdersTableAdapter orders = new OrdersTableAdapter();
        DishTableAdapter dish = new DishTableAdapter();
        DepartmentTableAdapter department = new DepartmentTableAdapter();
        ChekTableAdapter chek = new ChekTableAdapter();
        ClientsTableAdapter clients = new ClientsTableAdapter();
        PaymentTableAdapter payment = new PaymentTableAdapter();
        public AdminPageOrders()
        {
            InitializeComponent();
            pole1.ItemsSource = dish.GetData();
            pole1.DisplayMemberPath = "DishName";
            pole2.ItemsSource = department.GetData();
            pole2.DisplayMemberPath = "NameDepartment";
            pole3.ItemsSource = chek.GetData();
            pole3.DisplayMemberPath = "SumCheck";
            pole4.ItemsSource = clients.GetData();
            pole4.DisplayMemberPath = "NameClient";
            pole5.ItemsSource = payment.GetData();
            pole5.DisplayMemberPath = "TypePayment";
            dg_BD.ItemsSource = orders.GetData();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            orders.InsertQuery(Convert.ToInt32(pole1.Text), Convert.ToInt32(pole2.Text), Convert.ToInt32(pole3.Text), Convert.ToInt32(pole4), Convert.ToInt32(pole5));
            dg_BD.ItemsSource = orders.GetData();
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                orders.UpdateQuery(Convert.ToInt32(pole1.Text), Convert.ToInt32(pole2.Text), Convert.ToInt32(pole3.Text), Convert.ToInt32(pole4), Convert.ToInt32(pole5), Convert.ToInt32(id));
                dg_BD.ItemsSource = orders.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch
            {
                MessageBox.Show("Не изменяй внешние ключи");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                orders.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = orders.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null && dg_BD.SelectedItem is DataRowView row)
            {
                

                int DishID = 0;
                int depid = 0;
                int chekpid = 0;
                int clientid = 0;
                int paymentid = 0;

                if (!row.Row.IsNull("Dish_ID"))
                {
                    DishID = Convert.ToInt32(row.Row["Dish_ID"]);
                }

                if (!row.Row.IsNull("Department_ID"))
                {
                    depid = Convert.ToInt32(row.Row["Department_ID"]);
                }

                if (!row.Row.IsNull("Chek_ID"))
                {
                    chekpid = Convert.ToInt32(row.Row["Chek_ID"]);
                }

                if (!row.Row.IsNull("Client_ID"))
                {
                    clientid = Convert.ToInt32(row.Row["Client_ID"]);
                }

                if (!row.Row.IsNull("Payment_ID"))
                {
                    paymentid = Convert.ToInt32(row.Row["Payment_ID"]);
                }

                foreach (DataRowView item in pole1.Items)
                {
                    if (!item.Row.IsNull("ID_Dish") && Convert.ToInt32(item.Row["ID_Dish"]) == DishID)
                    {
                        pole1.SelectedItem = item;
                        break;
                    }
                }

                foreach (DataRowView item in pole2.Items)
                {
                    if (!item.Row.IsNull("ID_Department") && Convert.ToInt32(item.Row["ID_Department"]) == depid)
                    {
                        pole2.SelectedItem = item;
                        break;
                    }
                }

                foreach (DataRowView item in pole3.Items)
                {
                    if (!item.Row.IsNull("ID_Chek") && Convert.ToInt32(item.Row["ID_Chek"]) == chekpid)
                    {
                        pole3.SelectedItem = item;
                        break;
                    }
                }

                foreach (DataRowView item in pole4.Items)
                {
                    if (!item.Row.IsNull("ID_Client") && Convert.ToInt32(item.Row["ID_Client"]) == clientid)
                    {
                        pole4.SelectedItem = item;
                        break;
                    }
                }
                foreach (DataRowView item in pole5.Items)
                {
                    if (!item.Row.IsNull("ID_Payment") && Convert.ToInt32(item.Row["ID_Payment"]) == paymentid)
                    {
                        pole5.SelectedItem = item;
                        break;
                    }
                }

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
