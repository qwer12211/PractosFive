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
    /// Логика взаимодействия для AdminPageManagers.xaml
    /// </summary>
    public partial class AdminPageManagers : Page
    {
        ManagersTableAdapter managers = new ManagersTableAdapter();
        public AdminPageManagers()
        {
            InitializeComponent();
            pole1.PreviewTextInput += Pole1_PreviewTextInput;
            pole2.PreviewTextInput += Pole1_PreviewTextInput;
            pole3.PreviewTextInput += Pole1_PreviewTextInput;
            dg_BD.ItemsSource = managers.GetData();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Text) || string.IsNullOrEmpty(pole3.Text))
            {
                MessageBox.Show("У вас есть пустые поля");
            }
            else
            {

                managers.InsertQuery(pole1.Text, pole2.Text, pole3.Text);
                dg_BD.ItemsSource = managers.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }

        }
        private void Pole1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Яa-zA-Z]+");

            e.Handled = regex.IsMatch(e.Text);
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
                    managers.UpdateQuery(pole1.Text, pole2.Text, pole3.Text, Convert.ToInt32(id));
                    dg_BD.ItemsSource = managers.GetData();
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
                    managers.DeleteQuery(Convert.ToInt32(id));
                    dg_BD.ItemsSource = managers.GetData();
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
                    pole1.Text = row.Row["NameManagers"].ToString();
                    pole2.Text = row.Row["SurnameManagers"].ToString();
                    pole3.Text = row.Row["MiddlenameManagers"].ToString();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
