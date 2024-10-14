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
    /// Логика взаимодействия для Window21.xaml
    /// </summary>
    public partial class Window21 : Window
    {
        private readonly АРЕНДА _context = new АРЕНДА();
        private АРЕНДА _currentАРЕНДА = new АРЕНДА();
        public Window21(АРЕНДА selectedАРЕНДА)
        {
            InitializeComponent();

            if (selectedАРЕНДА != null)
                _currentАРЕНДА = selectedАРЕНДА;

            DataContext = _currentАРЕНДА;
        }

        private void LookButton_Click(object sender, RoutedEventArgs e)
        {
            var ars = АВТОПРОКАТEntities.GetContext().АРЕНДА.ToList(); // Получаем всех клиентов из базы данных

            if (ars.Any())
            {
                var ar = ars.First(); // Берем первого клиента для отображения

                IDBox.Text = ar.ID.ToString();
                IDavtoBox.Text = ar.ID_avto.ToString();
                IDuserBox.Text = ar.ID_user.ToString();
                IDclientBox.Text =ar.ID_client.ToString();
                StartArendBox.Text = ar.Дата_Начала_Аренды.ToString();
                EndArendBox.Text = ar.Дата_Конца_Аренды.ToString();
                Price.Text = ar.Цена.ToString();
                FIOBox.Text = ar.ФИО;
                Status.Text = ar.Статус;
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

