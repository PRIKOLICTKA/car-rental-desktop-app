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
    /// Логика взаимодействия для Window10.xaml
    /// </summary>
    public partial class Window10 : Window
    {
        public Window10()
        {
            InitializeComponent();
            TarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().ТАРИФЫ.ToList();
            txtSearch.TextChanged += TBoxSearch_TextChanged;
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text;

            // Выполнение запроса на поиск автомобилей по заданному значению (строковому или числовому)
            var filteredAr = АВТОПРОКАТEntities.GetContext().ТАРИФЫ
                .Where(tar=>
                              tar.Марка_Модель.Contains(searchText) ||
                              tar.Цена_3_6_суток.ToString().Contains(searchText) ||
                              tar.ID.ToString().Contains(searchText) ||
                              tar.Цена_7_15_суток.ToString().Contains(searchText) ||
                              tar.Цена_1_2_суток.ToString().Contains(searchText) ||     
                              tar.Цена_16_30_суток.ToString().Contains(searchText) ||
                              tar.Залог.ToString().Contains(searchText) ||
                              tar.ID_avto.ToString().Contains(searchText))
                .ToList();

            // Отображение результатов поиска в DataGrid
            TarDataGrid.ItemsSource = filteredAr;
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

        private void LookButton_Click(object sender, RoutedEventArgs e)
        {
            Window22 window22 = new Window22(((sender as Button).DataContext as ТАРИФЫ));
            window22.Show();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Window17 window17 = (new Window17((sender as Button).DataContext as ТАРИФЫ));
            window17.Show();
        }


        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var tarRemoving = TarDataGrid.SelectedItems.Cast<ТАРИФЫ>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить{tarRemoving.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    АВТОПРОКАТEntities.GetContext().ТАРИФЫ.RemoveRange(tarRemoving);
                    АВТОПРОКАТEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    TarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().ТАРИФЫ.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Window17 window17 = new Window17(null);
            window17.Show();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            TarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().ТАРИФЫ.ToList();
            //ClDataGrid.SelectedItems.Clear();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                АВТОПРОКАТEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                TarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().ТАРИФЫ.ToList();
            }
        }


        private void GlButton_Click(object sender, RoutedEventArgs e)
        {
            Window6 window6 = new Window6();
            window6.Show();
            this.Close();
        }
        private void ClButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.Show();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
