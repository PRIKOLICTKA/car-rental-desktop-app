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
using System.Windows.Shapes;

namespace crp10
{
    /// <summary>
    /// Логика взаимодействия для Window18.xaml
    /// </summary>
    public partial class Window18 : Window
    {
        private readonly  КЛИЕНТЫ _context = new КЛИЕНТЫ();
        private КЛИЕНТЫ _currentКЛИЕНТЫ = new КЛИЕНТЫ();
        public Window18(КЛИЕНТЫ selectedКЛИЕНТЫ)
        {
            InitializeComponent();

            if (selectedКЛИЕНТЫ != null)
                _currentКЛИЕНТЫ = selectedКЛИЕНТЫ;

            DataContext = _currentКЛИЕНТЫ;
        }
        private void LookButton_Click(object sender, RoutedEventArgs e)
        {
            var clients = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.ToList(); // Получаем всех клиентов из базы данных

            if (clients.Any())
            {
                var client = clients.First(); // Берем первого клиента для отображения

                ClBox.Text = client.ID.ToString();
                PsBox.Text = client.Паспорт_номер;
                PrBox.Text = client.Права_номер;
                UsBox.Text = client.ID_user.ToString();
                FioBox.Text = client.ФИО;
                TelBox.Text = client.Телефон;
                EmBox.Text = client.Email;
                // Другие поля также могут быть заполнены аналогичным образом
            }
            else
            {
                MessageBox.Show("Данные о клиентах не найдены.");
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
