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
    /// Логика взаимодействия для Window19.xaml
    /// </summary>
    public partial class Window19 : Window
    {
        private readonly АВТОМОБИЛИ _context = new АВТОМОБИЛИ();
        private АВТОМОБИЛИ _currentАВТОМОБИЛИ = new АВТОМОБИЛИ();
        public Window19(АВТОМОБИЛИ selectedАВТОМОБИЛИ)
        {
            InitializeComponent();

            if (selectedАВТОМОБИЛИ != null)
                _currentАВТОМОБИЛИ = selectedАВТОМОБИЛИ;

            DataContext = _currentАВТОМОБИЛИ;
        }

        private void LookButton_Click(object sender, RoutedEventArgs e)
        {
            var avtos = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList(); // Получаем всех клиентов из базы данных

            if (avtos.Any())
            {
                var avto = avtos.First(); // Берем первого клиента для отображения

                IDBox.Text = avto.ID.ToString();
                MBox.Text = avto.Марка;
                MoBox.Text = avto.Модель;
                GBox.Text = avto.Год_выпуска.ToString();
                ClBox.Text = avto.Цвет;
                NBox.Text = avto.Номерной_знак;
                VBox.Text = avto.VIN_код;
                CTCBox.Text = avto.Серия_номер_СТС;
                PTCBox.Text = avto.Серия_номер_ПТС;
                //PemontBox.Text = avto.Ремонт.ToString();
                CtraxBox.Text = avto.Страховка;
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
