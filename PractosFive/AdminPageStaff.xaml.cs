using PractosFive.DataSet1TableAdapters;
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

namespace PractosFive
{
    /// <summary>
    /// Логика взаимодействия для AdminPageStaff.xaml
    /// </summary>
    public partial class AdminPageStaff : Page
    {
        StaffTableAdapter staff = new StaffTableAdapter();
        PostTableAdapter post = new PostTableAdapter();
        AuthorizationsTableAdapter authorizations = new AuthorizationsTableAdapter();

        public AdminPageStaff()
        {
            InitializeComponent();       
            pole4.ItemsSource = post.GetData();
            pole1.PreviewTextInput += Pole1_PreviewTextInput;
            pole2.PreviewTextInput += Pole1_PreviewTextInput;
            pole3.PreviewTextInput += Pole1_PreviewTextInput;
            pole4.DisplayMemberPath = "NamePost";
            pole5.ItemsSource = authorizations.GetData();
            pole5.DisplayMemberPath = "LoginAuthorization";
            dg_BD.ItemsSource = staff.GetData();
        }
        private void Pole1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Яa-zA-Z]+");

            e.Handled = regex.IsMatch(e.Text);
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Text) || string.IsNullOrEmpty(pole3.Text))
            {
                MessageBox.Show("У вас есть пустые поля");
            }
            else
            {
                
                    staff.InsertQuery(pole1.Text, pole2.Text, pole3.Text, Convert.ToInt32(pole4), Convert.ToInt32(pole5));
                    dg_BD.ItemsSource = staff.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                
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
                    staff.UpdateQuery(pole1.Text, pole2.Text, pole3.Text, Convert.ToInt32(pole4), Convert.ToInt32(pole5), Convert.ToInt32(id));
                    dg_BD.ItemsSource = staff.GetData();
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
                    staff.DeleteQuery(Convert.ToInt32(id));
                    dg_BD.ItemsSource = staff.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                }
                catch
                {
                    MessageBox.Show("Не удалось изменить из-за внешнего ключа");
                }
            }
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null && dg_BD.SelectedItem is DataRowView row)
            {
                pole1.Text = row.Row["NameStaff"].ToString();
                pole2.Text = row.Row["SurnameStaff"].ToString();
                pole3.Text = row.Row["MiddlenameStaff"].ToString();

                int ordID = 0;
                int jobid = 0;

                if (!row.Row.IsNull("Post_ID"))
                {
                    ordID = Convert.ToInt32(row.Row["Post_ID"]);
                }

                if (!row.Row.IsNull("Authorization_ID"))
                {
                    jobid = Convert.ToInt32(row.Row["Authorization_ID"]);
                }

                foreach (DataRowView item in pole4.Items)
                {
                    if (!item.Row.IsNull("ID_Post") && Convert.ToInt32(item.Row["ID_Post"]) == ordID)
                    {
                        pole4.SelectedItem = item;
                        break;
                    }
                }

                foreach (DataRowView item in pole5.Items)
                {
                    if (!item.Row.IsNull("ID_Authorization") && Convert.ToInt32(item.Row["ID_Authorization"]) == jobid)
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
