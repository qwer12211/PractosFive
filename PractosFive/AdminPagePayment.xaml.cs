using PractosFive.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

namespace PractosFive
{
    /// <summary>
    /// Логика взаимодействия для AdminPagePayment.xaml
    /// </summary>
    public partial class AdminPagePayment : Page
    {
        PaymentTableAdapter payment = new PaymentTableAdapter();
        public AdminPagePayment()
        {
            InitializeComponent();
            pole1.PreviewTextInput += Pole1_PreviewTextInput;
            dg_BD.ItemsSource = payment.GetData();
        }
        private void Pole1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Яa-zA-Z]+");

            e.Handled = regex.IsMatch(e.Text);
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(pole1.Text))
            {
                MessageBox.Show("У вас есть пустые поля");
            }
            else
            {
                payment.InsertQuery(pole1.Text);
                dg_BD.ItemsSource = payment.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            }         
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dg_BD.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали данные для изменения");
            }
            else if (string.IsNullOrEmpty(pole1.Text))
            {
                MessageBox.Show("У вас есть пустые поля");
            }
            else
            {
                try
                {
                    object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                    payment.UpdateQuery(pole1.Text, Convert.ToInt32(id));
                    dg_BD.ItemsSource = payment.GetData();
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
                    payment.DeleteQuery(Convert.ToInt32(id));
                    dg_BD.ItemsSource = payment.GetData();
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
                    pole1.Text = row.Row["TypePayment"].ToString();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void ImportJson_Click(object sender, RoutedEventArgs e)
        {
            
                List<ImportTypePayment> forimport = Des.DeserializeObject<List<ImportTypePayment>>();
                if (forimport != null)
                {
                foreach (var item in forimport)
                {
                    payment.InsertQuery(item.TypePayment);
                }
                dg_BD.ItemsSource = null;
                dg_BD.ItemsSource = payment.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                }
                else if (forimport != null)
                {
                MessageBox.Show(" Вы не выбрали файл ");
                }
        }
    }
}
