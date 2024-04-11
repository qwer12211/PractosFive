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
    /// Логика взаимодействия для AdminPagePost.xaml
    /// </summary>
    public partial class AdminPagePost : Page
    {
        PostTableAdapter post = new PostTableAdapter();
        public AdminPagePost()
        {
            InitializeComponent();
            dg_BD.ItemsSource = post.GetData();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            post.InsertQuery(pole1.Text);
            dg_BD.ItemsSource = post.GetData();
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                post.UpdateQuery(pole1.Text, Convert.ToInt32(id));
                dg_BD.ItemsSource = post.GetData();
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
                post.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = post.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                if (dg_BD.SelectedItem != null)
                {
                    DataRowView row = dg_BD.SelectedItem as DataRowView;
                    if (row != null)
                    {
                        pole1.Text = row.Row["NamePost"].ToString();

                    }

                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void ImportJson_Click(object sender, RoutedEventArgs e)
        {
            List<ImportNamePost> forimport = Des.DeserializeObject<List<ImportNamePost>>();
            if (forimport != null)
            {
                foreach (var item in forimport)
                {
                    post.InsertQuery(item.NamePost);
                }
                dg_BD.ItemsSource = null;
                dg_BD.ItemsSource = post.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            }
            else if (forimport != null)
            {
                MessageBox.Show("Вы не выбрали файл ");
            }
        }
    }
}
