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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {    

        public Window2()
        {
            InitializeComponent();

            //Экземпляр таблицы КЛИЕНТЫ для вывода данных в DataGrid
            ClDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.ToList();

            //Посик информации 
            txtSearch.TextChanged += TBoxSearch_TextChanged;
            txtFilter.TextChanged += TBoxFilter_TextChanged;
        }

        //Посик информации 
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
                              client.ID.ToString().Contains(searchText) ||
                              client.ID_user.ToString().Contains(searchText))
                .ToList();

            // Отображение результатов поиска в DataGrid
            ClDataGrid.ItemsSource = filteredClients;
        }
        private void TBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = txtFilter.Text;

            // Выполнение запроса на поиск автомобилей по заданному значению (строковому или числовому)
            var filteredClients = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ
                .Where(client => client.ID_user.ToString().Contains(filterText))
                .ToList();

            // Отображение результатов поиска в DataGrid
            ClDataGrid.ItemsSource = filteredClients;
        }

        //Расширение Экрана
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

        //Автомотическое обновление данных
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                АВТОПРОКАТEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ClDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.ToList();
            }
        }

        //Кнопка добавить с аргументом null
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window11 window11 = new Window11(null);
            window11.Show();
        }

        private void LookButton_Click(object sender, RoutedEventArgs e)
        {
            Window18 window18 = new Window18(((sender as Button).DataContext as КЛИЕНТЫ));
            window18.Show();

           
        }


        //Кнопка редактирования с аргемнтом (дынных из выбранной строчки)
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Window11 window11 = (new Window11((sender as Button).DataContext as КЛИЕНТЫ));
            window11.Show();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window12 window12 = new Window12();
            window12.Show();
            this.Close();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
           var clientsRemoving = ClDataGrid.SelectedItems.Cast<КЛИЕНТЫ>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить{ clientsRemoving.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                  АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.RemoveRange(clientsRemoving);
                    АВТОПРОКАТEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                     ClDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.ToList();
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message.ToString() );
                }
            }
        }

        private void GlButton_Click(object sender, RoutedEventArgs e)
        {
            Window6 window6 = new Window6();
            window6.Show(); 
            this.Close();
        }

        private void CarButton_Click(object sender, RoutedEventArgs e)
        {
            Window7 window7 = new Window7();
            window7.Show();
            this.Close();
        }

        private void ZavButton_Click(object sender, RoutedEventArgs e)
        {
            Window8 window8 = new Window8();
            window8.Show();
            this.Close();
        }

        private void ArButton_Click(object sender, RoutedEventArgs e)
        {
            Window9 window9 = new Window9();
            window9.Show();
            this.Close();
        }

        private void TarButton_Click(object sender, RoutedEventArgs e)
        {
            Window10 window10 = new Window10();
            window10.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
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

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
                Refresh(); 
        }
        private void Refresh()
        {
          ClDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.ToList();
          //ClDataGrid.SelectedItems.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
