using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для Window13.xaml
    /// </summary>
    public partial class Window13 : Window
    {

        private ПОЛЬЗОВАТЕЛИ _currentПОЛЬЗОВАТЕЛИ = new ПОЛЬЗОВАТЕЛИ();

        public Window13(ПОЛЬЗОВАТЕЛИ selectedПОЛЬЗОВАТЕЛИ)
        {
            InitializeComponent();

            if (selectedПОЛЬЗОВАТЕЛИ != null)
                _currentПОЛЬЗОВАТЕЛИ = selectedПОЛЬЗОВАТЕЛИ;

            DataContext = _currentПОЛЬЗОВАТЕЛИ;
        }

        public Window13()
        {
            InitializeComponent();
            DataContext = _currentПОЛЬЗОВАТЕЛИ;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentПОЛЬЗОВАТЕЛИ.ID == 0)
            {
                int maxId = АВТОПРОКАТEntities.GetContext().ПОЛЬЗОВАТЕЛИ.Max(r => r.ID); _currentПОЛЬЗОВАТЕЛИ.ID = maxId + 1;
                //_currentКЛИЕНТЫ.recording_id = Guid.NewGuid();
                //АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.AddOrUpdate(_currentКЛИЕНТЫ);
            }

            if (string.IsNullOrWhiteSpace(_currentПОЛЬЗОВАТЕЛИ.Login))
                errors.AppendLine("Укажите Логин");

            if (string.IsNullOrWhiteSpace(_currentПОЛЬЗОВАТЕЛИ.Password))
                errors.AppendLine("Укажите Пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            АВТОПРОКАТEntities.GetContext().ПОЛЬЗОВАТЕЛИ.AddOrUpdate(_currentПОЛЬЗОВАТЕЛИ);


            try
            {
                АВТОПРОКАТEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
               
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
    
}
