using PractosFive.DataSet1TableAdapters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для AdminPageCheck.xaml
    /// </summary>
    public partial class AdminPageCheck : Page
    {
        ChekTableAdapter chek = new ChekTableAdapter();

        public AdminPageCheck()
        {
            InitializeComponent();
            dg_BD.ItemsSource = chek.GetData();
            pole2.DisplayDateStart = DateTime.Today;
            pole2.DisplayDateEnd = DateTime.Today.AddDays(7);
        }
        private bool IsValidInput(string input)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            return regex.IsMatch(input);
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (pole1.Text.Length < 1 && pole1.Text.Length > 15 != (IsValidInput(pole1.Text)) || !IsValidInput(pole1.Text))
            {
                MessageBox.Show("Cумма от 1 до 15 букв и цифр!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Text))
                {
                    MessageBox.Show("Поля не имеют никакого внутри значения! Введите в поля данные");
                }
                else
                {
                    chek.InsertQuery(Convert.ToInt32(pole1.Text), pole2.Text);
                    dg_BD.ItemsSource = chek.GetData();
                    dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                }
            }          
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pole1.Text.Length < 1 && pole1.Text.Length > 15 != (IsValidInput(pole2.Text)) || !IsValidInput(pole2.Text))
                {
                    MessageBox.Show("Цена  должны содержать от 1 до 15 букв и цифр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Text))
                    {
                        MessageBox.Show("Поля не имеют никакого внутри значения! Введите в поля данные");
                    }
                    else
                    {
                        object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                        chek.UpdateQuery(Convert.ToInt32(pole1.Text), pole2.Text, Convert.ToInt32(id));
                        dg_BD.ItemsSource = chek.GetData();
                        dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не выбрано поле");
            }       
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pole1.Text.Length < 1 && pole1.Text.Length > 15 != (IsValidInput(pole1.Text)) || !IsValidInput(pole2.Text))
                {
                    MessageBox.Show("Сумма чека 1 до 15 букв и цифр!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(pole1.Text) || string.IsNullOrEmpty(pole2.Text))
                    {
                        MessageBox.Show("Поля не имеют никакого внутри значения! Введите в поля данные");
                    }
                    else
                    {
                        object id = (dg_BD.SelectedItem as DataRowView).Row[0];
                        chek.DeleteQuery(Convert.ToInt32(id));
                        dg_BD.ItemsSource = chek.GetData();
                        dg_BD.Columns[0].Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно удалить данные, так как они используются в другой таблице");
            }       
        }
        private void dg_BD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_BD.SelectedItem != null)
            {
                DataRowView row = dg_BD.SelectedItem as DataRowView;
                if (row != null)
                {
                    pole1.Text = row.Row["SumCheck"].ToString();
                    pole2.Text = row.Row["DataCheck"].ToString();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dg_BD.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void CreateCheck_Click(object sender, RoutedEventArgs e)
        {
            ChekTableAdapter chek = new ChekTableAdapter();
            var ChelInfo = chek.GetData();

          
            string fileName = "C:\\Users\\Misha\\Desktop\\Практика\\Chek.txt";          
            string orderDetails = "\t\t\tИтого: " +pole1.Text + "\n\n\n\t\t\tДата заказа: " +pole2.Text;
            MessageBox.Show("Чек создан ");

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(orderDetails);
            }
        }

        private void pole2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.MinValue;
            if (selectedDate < DateTime.Today)
            {
                datePicker.SelectedDate = DateTime.Today;
            }
            else if (selectedDate > DateTime.Today) 
            {
                datePicker.SelectedDate = DateTime.Today;
            }
        }
    }
}
