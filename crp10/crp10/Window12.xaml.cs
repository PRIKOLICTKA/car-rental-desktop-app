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
    /// Логика взаимодействия для Window12.xaml
    /// </summary>
    public partial class Window12 : Window
    {
        public Window12()
        {
            InitializeComponent();
            UsDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().ПОЛЬЗОВАТЕЛИ.ToList();
            txtSearch.TextChanged += TBoxSearch_TextChanged;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text;

            // Выполнение запроса на поиск автомобилей по заданному значению (строковому или числовому)
            var filteredAr = АВТОПРОКАТEntities.GetContext().ПОЛЬЗОВАТЕЛИ
                .Where(user =>
                              user.Login.Contains(searchText) ||
                              user.Password.Contains(searchText) ||
                              user.ID.ToString().Contains(searchText) ||
                              user.ID_sotrudnik.ToString().Contains(searchText) ||
                              user.ID_role.ToString().Contains(searchText))
                .ToList();

            // Отображение результатов поиска в DataGrid
            UsDataGrid.ItemsSource = filteredAr;
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Window13 window13 = (new Window13((sender as Button).DataContext as ПОЛЬЗОВАТЕЛИ));
            window13.Show();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var usersRemoving = UsDataGrid.SelectedItems.Cast<ПОЛЬЗОВАТЕЛИ>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить{usersRemoving.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    АВТОПРОКАТEntities.GetContext().ПОЛЬЗОВАТЕЛИ.RemoveRange(usersRemoving);
                    АВТОПРОКАТEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    UsDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().ПОЛЬЗОВАТЕЛИ.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window13 window13 = new Window13();
            window13.Show();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                АВТОПРОКАТEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                UsDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().ПОЛЬЗОВАТЕЛИ.ToList();
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
            Application.Current.Shutdown();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            UsDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().ПОЛЬЗОВАТЕЛИ.ToList();
            //ClDataGrid.SelectedItems.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.Show();
            this.Close();
        }
    }
}
