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
using PractosFive.DataSet1TableAdapters;

namespace PractosFive
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthorizationsTableAdapter auth = new AuthorizationsTableAdapter();
        ClientsTableAdapter clients = new ClientsTableAdapter();
        StaffTableAdapter staff = new StaffTableAdapter();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var allLogins = auth.GetData().Rows;

                foreach (DataRow row in allLogins)
                {
                    if (row["LoginAuthorization"].ToString() == LoginBox.Text && row["PasswordAuthorization"].ToString() == PasswordBox.Password)
                    {
                        int Authorization_ID = (int)row["ID_Authorization"];

                        DataRow[] clientRow = clients.GetData().Select("Authorization_ID = " + Authorization_ID);
                        if (clientRow.Length > 0)
                        {
                            Client client = new Client();
                            client.Show();
                            Close();
                            return;


                        }

                        DataRow[] staffRow = staff.GetData().Select("Authorization_ID = " + Authorization_ID);
                        if (staffRow.Length > 0)
                        {
                            
                           
                            Admin admin = new Admin();
                            admin.Show();
                            Close();
                            return;
                        }

                        MessageBox.Show("Неверные логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                MessageBox.Show("Неверные логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
    
}
