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
    /// Логика взаимодействия для Window22.xaml
    /// </summary>
    public partial class Window22 : Window
    {
        private readonly ТАРИФЫ _context = new ТАРИФЫ();
        private ТАРИФЫ _currentТАРИФЫ = new ТАРИФЫ();
        public Window22(ТАРИФЫ selectedТАРИФЫ)
        {
            InitializeComponent();

            if (selectedТАРИФЫ != null)
                _currentТАРИФЫ = selectedТАРИФЫ;

            DataContext = _currentТАРИФЫ;
        }

        private void LookButton_Click(object sender, RoutedEventArgs e)
        {
            var tarifs = АВТОПРОКАТEntities.GetContext().ТАРИФЫ.ToList(); // Получаем всех клиентов из базы данных

            if (tarifs.Any())
            {
                var tarif = tarifs.First(); // Берем первого клиента для отображения

                IDBox.Text = tarif.ID.ToString();
                IDAvtoBox.Text = tarif.ID_avto.ToString();
                POneBox.Text = tarif.Цена_1_2_суток.ToString();
                PTwoBox.Text = tarif.Цена_3_6_суток.ToString();
                PThreeBox.Text = tarif.Цена_7_15_суток.ToString();
                PFourBox.Text = tarif.Цена_16_30_суток.ToString();
                ZalogBox.Text = tarif.Залог.ToString();
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
