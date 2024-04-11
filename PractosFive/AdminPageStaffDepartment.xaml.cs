using PractosFive.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для AdminPageStaffDepartment.xaml
    /// </summary>
    public partial class AdminPageStaffDepartment : Page
    {
        StaffDepartmentTableAdapter staffDepartment = new StaffDepartmentTableAdapter();
        StaffTableAdapter staff = new StaffTableAdapter();
        DepartmentTableAdapter department = new DepartmentTableAdapter();
        QueriesTableAdapter queries = new QueriesTableAdapter();
        public AdminPageStaffDepartment()
        {
            InitializeComponent();
            pole1.ItemsSource = staff.GetData();
            pole1.DisplayMemberPath = "NameStaff";
            pole2.ItemsSource = department.GetData();
            pole2.DisplayMemberPath = "NameDepartment";
            dg_BD.ItemsSource = staffDepartment.GetData();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            staffDepartment.InsertQuery(Convert.ToInt32(pole1.Text), Convert.ToInt32(pole2.Text));
            dg_BD.ItemsSource = staffDepartment.GetData();
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                staffDepartment.UpdateQuery(Convert.ToInt32(pole1.Text), Convert.ToInt32(pole2.Text), Convert.ToInt32(id));
                dg_BD.ItemsSource = staffDepartment.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                staffDepartment.DeleteQuery(Convert.ToInt32(id));
                dg_BD.ItemsSource = staffDepartment.GetData();
                dg_BD.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = dg_BD.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                int ordID = Convert.ToInt32(selectedRow["Staff_ID"]);
                int jobid = Convert.ToInt32(selectedRow["Department_ID"]);

                foreach (DataRowView item in pole1.Items)
                {
                    if (Convert.ToInt32(item["ID_Staff"]) == ordID)
                    {
                        pole1.SelectedItem = item;
                        break;
                    }
                }

                foreach (DataRowView item in pole2.Items)
                {
                    if (Convert.ToInt32(item["ID_Department"]) == jobid)
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

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            queries.Backup_Restaruant();
            MessageBox.Show("Бэкап создан");
        }
    }
}

