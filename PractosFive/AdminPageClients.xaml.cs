using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
using PractosFive.DataSet1TableAdapters;

namespace PractosFive
{
    /// <summary>
    /// Логика взаимодействия для AdminPageClients.xaml
    /// </summary>
    public partial class AdminPageClients : Page
    {
        ClientsTableAdapter clients = new ClientsTableAdapter();
        AuthorizationsTableAdapter auth = new AuthorizationsTableAdapter();
        public AdminPageClients()
        {
            InitializeComponent();
            pole1.PreviewTextInput += Pole1_PreviewTextInput;
            pole2.PreviewTextInput += Pole4_PreviewTextInput;
            pole3.ItemsSource = auth.GetData();
            pole3.DisplayMemberPath = "LoginAuthorization";
            dg_BD.ItemsSource = clients.GetData();
        }

        private void Pole4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Pole1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Яa-zA-Z]+");

            e.Handled = regex.IsMatch(e.Text);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Text) || pole3.SelectedItem == null)
            {
                MessageBox.Show("У вас есть пустые поля");
            }
            else
            {
                if (pole3.SelectedItem is DataRowView selectedauth)
                {
                    int selectedauthID = Convert.ToInt32(selectedauth["ID_Authorization"]);
                    clients.InsertQuery(pole1.Text, pole2.Text, selectedauthID);
                    dg_BD.ItemsSource = clients.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                }
            }
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dg_BD.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали данные для изменения");
            }
            else if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Text) || string.IsNullOrEmpty(pole3.Text))
            {
                MessageBox.Show("У вас есть пустые поля");
            }
            else
            {
                try
                {
                    object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                    clients.UpdateQuery(pole1.Text, pole2.Text, Convert.ToInt32(pole3.Text), Convert.ToInt32(id));
                    dg_BD.ItemsSource = clients.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                }
                catch
                {
                    MessageBox.Show("Не удалось изменить из-за внешнего ключа");
                }
            }
        }



        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dg_BD.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали данные для удаления");
            }
            else
            {
                try
                {
                    object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                    clients.DeleteQuery(Convert.ToInt32(id));
                    dg_BD.ItemsSource = clients.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null && dg_BD.SelectedItem is DataRowView row)
            {
                pole1.Text = row.Row["NameClient"].ToString();
                pole2.Text = row.Row["TelephoneClient"].ToString();

                int ordID = 0;

                if (!row.Row.IsNull("Authorization_ID"))
                {
                    ordID = Convert.ToInt32(row.Row["Authorization_ID"]);
                }
             

                foreach (DataRowView item in pole3.Items)
                {
                    if (!item.Row.IsNull("ID_Authorization") && Convert.ToInt32(item.Row["ID_Authorization"]) == ordID)
                    {
                        pole3.SelectedItem = item;
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

