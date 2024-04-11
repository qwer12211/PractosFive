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
    /// Логика взаимодействия для AdminPageDepartment.xaml
    /// </summary>
    public partial class AdminPageDepartment : Page
    {
        DepartmentTableAdapter department = new DepartmentTableAdapter();
        ManagersTableAdapter managers = new ManagersTableAdapter();
        public AdminPageDepartment()
        {
            InitializeComponent();
            pole1.PreviewTextInput += Pole1_PreviewTextInput;
            pole2.ItemsSource = managers.GetData();
            pole2.DisplayMemberPath = "NameManagers";
            dg_BD.ItemsSource = department.GetData();
        }
        private void Pole1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Яa-zA-Z]+");

            e.Handled = regex.IsMatch(e.Text);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Text))
            {
                MessageBox.Show("У вас есть пустые поля");
            }
            else
            {
                if (pole2.SelectedItem is DataRowView selectedauth)
                {
                    department.InsertQuery(pole1.Text, Convert.ToInt32(selectedauth["ColumnName"]));
                    dg_BD.ItemsSource = department.GetData();
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
            else if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Text))
            {
                MessageBox.Show("У вас есть пустые поля");
            }
            else
            {
                try
                {
                    object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                    department.UpdateQuery(pole1.Text, Convert.ToInt32(pole2.Text), Convert.ToInt32(id));
                    dg_BD.ItemsSource = department.GetData();
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
                    department.DeleteQuery(Convert.ToInt32(id));
                    dg_BD.ItemsSource = department.GetData();
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
                pole1.Text = row.Row["NameDepartment"].ToString();

                int ordID = 0;


                if (!row.Row.IsNull("Managers_ID"))
                {
                    ordID = Convert.ToInt32(row.Row["Managers_ID"]);
                }
           

                foreach (DataRowView item in pole2.Items)
                {
                    if (!item.Row.IsNull("ID_Managers") && Convert.ToInt32(item.Row["ID_Managers"]) == ordID)
                    {
                        pole2.SelectedItem = item;
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
