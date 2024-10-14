using System;
using System.Collections.Generic;
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

namespace crp10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseConnection dbConnection;

        public MainWindow()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
            Console.WriteLine(dbConnection.ConnectionString); // Выводим строку подключения к базе данных
        }

    

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            using (var context = new АВТОПРОКАТEntities())
            {
                var user = context.ПОЛЬЗОВАТЕЛИ.FirstOrDefault(u => u.Login == login && u.Password == password);
                if (user != null)
                {
                    if (user.ID_role == 2)
                    {
                        Window5 window5 = new Window5();
                        window5.Show();
                        this.Close();
                    }
                    else if (user.ID_role == 1)
                    {
                        Window6 window6 = new Window6();
                        window6.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Недопустимая роль пользователя.");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.");
                }
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Отображение сообщения с подтверждением
                if (MessageBox.Show("Вы точно хотите выйти?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown(); // Закрытие приложения
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
