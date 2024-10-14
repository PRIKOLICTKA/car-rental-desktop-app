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
    /// Логика взаимодействия для Window20.xaml
    /// </summary>
    public partial class Window20 : Window
    {
        private readonly ZAYAVKI _context = new ZAYAVKI();
        private ZAYAVKI _currentZAYAVKI = new ZAYAVKI();
        public Window20(ZAYAVKI selectedZAYAVKI)
        {
            InitializeComponent();

            if (selectedZAYAVKI != null)
                _currentZAYAVKI = selectedZAYAVKI;

            DataContext = _currentZAYAVKI;
        }

        private void LookButton_Click(object sender, RoutedEventArgs e)
        {
            var zas = АВТОПРОКАТEntities.GetContext().ZAYAVKI.ToList(); // Получаем всех клиентов из базы данных

            if (zas.Any())
            {
                var za = zas.First(); // Берем первого клиента для отображения

                IDBox.Text = za.ID.ToString();
                MBox.Text = za.ID_avto.ToString();
                MoBox.Text = za.ID_user.ToString();
                    GBox.Text = za.Дата_Начала_Аренды.ToString();
                ClBox.Text = za.Фамилия;
                NBox.Text = za.Имя;
                VBox.Text = za.Отчество;
                    CTCBox.Text = za.Время_Вывоза.ToString();
                CtcBox.Text = za.Email;
                PTCBox.Text = za.Телефон;
                    PtcBox.Text = za.Время_Возврата.ToString();
                    PtBox.Text = za.Дата_Конца_Аренды.ToString() ;
                Stox.Text = za.Статус;
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
