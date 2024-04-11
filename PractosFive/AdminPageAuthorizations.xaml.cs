using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для AdminPageAuthorizations.xaml
    /// </summary>
    public partial class AdminPageAuthorizations : Page
    {
        AuthorizationsTableAdapter auth = new AuthorizationsTableAdapter();
        public AdminPageAuthorizations()
        {
            InitializeComponent();
            dg_BD.ItemsSource = auth.GetData();
        }
        private bool IsValidInput(string input)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            return regex.IsMatch(input);
        }



        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Text))
            {
                MessageBox.Show("У вас есть пустые поля");
            }
            else
            {
               
                    auth.InsertQuery(pole1.Text, pole2.Text);
                    dg_BD.ItemsSource = auth.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                
           
                
            }
                     
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dg_BD.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали данные для изменения");
            }
            else if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Text))
            {
                MessageBox.Show("У вас есть пустые поля");
            }
            else
            {
                try
                {
                    object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                    auth.UpdateQuery(pole1.Text, pole2.Text, Convert.ToInt32(id));
                    dg_BD.ItemsSource = auth.GetData();
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
                    auth.DeleteQuery(Convert.ToInt32(id));
                    dg_BD.ItemsSource = auth.GetData();
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
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["LoginAuthorization"].ToString();
                    pole2.Text = row.Row["PasswordAuthorization"].ToString();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
