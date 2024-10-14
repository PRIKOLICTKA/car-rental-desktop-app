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
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
            var curerntClient = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.ToList();
            LViewClient.ItemsSource = curerntClient;

            txtSearch.TextChanged += TBoxSearch_TextChanged;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text;

            // Выполнение запроса на поиск автомобилей по заданному значению (строковому или числовому)
            var filteredClients = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ
                .Where(client => client.ФИО.Contains(searchText) ||
                              client.Права_номер.Contains(searchText) ||
                              client.Паспорт_номер.Contains(searchText) ||
                              client.Телефон.Contains(searchText) ||
                              client.Email.Contains(searchText) ||
                              //client.ID.ToString().Contains(searchText) ||
                              client.ID_user.ToString().Contains(searchText))
                .ToList();

            // Отображение результатов поиска в DataGrid
            LViewClient.ItemsSource = filteredClients;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }


        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text;

            // Выполнение запроса на поиск автомобилей по заданному значению
            var filteredClients = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.Where(client => client.ФИО.Contains(searchText) || client.Паспорт_номер.Contains(searchText) || client.Права_номер.Contains(searchText) || client.Телефон.Contains(searchText)
             || client.Email.Contains(searchText)).ToList();

            // Отображение результатов поиска в ListView
            LViewClient.ItemsSource = filteredClients;


        }


        private void ClButton_Click(object sender, RoutedEventArgs e)
        {
            Window3 window3 = new Window3();
            window3.Show();
            this.Close();
        }

        private void AvButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }

        private void TarButton_Click(object sender, RoutedEventArgs e)
        {
            Window4 window4 = new Window4();
            window4.Show();
            this.Close();
        }

        private void GlavButton_Click(object sender, RoutedEventArgs e)
        {
            Window5 window5 = new Window5();
            window5.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
