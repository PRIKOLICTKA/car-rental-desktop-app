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
    /// Логика взаимодействия для Window9.xaml
    /// </summary>
    public partial class Window9 : Window
    {
        public Window9()
        {
            InitializeComponent();
            ArDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АРЕНДА.OrderBy(z => z.ID_user).ToList();
            txtSearch.TextChanged += TBoxSearch_TextChanged;

            // Инициализация ComboBox данными из таблицы АРЕНДА
            var renters = АВТОПРОКАТEntities.GetContext().АРЕНДА.Select(r => r.ФИО).Distinct().ToList();
            renters.Insert(0, "Выберите ФИО");
            txtFilter.ItemsSource = renters;
            txtFilter.SelectedIndex = 0; // Установка "Выберите ФИО" как первого элемента

            // Обработчик события для ComboBox
            txtFilter.SelectionChanged += TxtFilter_SelectionChanged;


            var users = АВТОПРОКАТEntities.GetContext().АРЕНДА
                        .Select(z => z.ID_user)
                        .Distinct()
                        .OrderBy(id => id)
                        .ToList();
            // Добавление "Выберите ID_user" как первого элемента
            var userStrings = users.Select(id => id.ToString()).ToList();
            userStrings.Insert(0, "Выберите ID_user");
            txtFilter2.ItemsSource = userStrings;
            txtFilter2.SelectedIndex = 0; // Установка "Выберите ID_user" как первого элемента

            // Обработчик события для ComboBox
            txtFilter2.SelectionChanged += TxtFilter2_SelectionChanged;
        }
        private void TxtFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedFio = txtFilter.SelectedItem as string;

            if (selectedFio != null && selectedFio != "Выберите ФИО")
            {
                // Фильтрация данных в DataGrid по выбранному ФИО
                var filteredRentals = АВТОПРОКАТEntities.GetContext().АРЕНДА.Where(r => r.ФИО == selectedFio).ToList();
                ArDataGrid.ItemsSource = filteredRentals;
            }
            else
            {
                // Если выбрано "Выберите ФИО", показываем все записи
                ArDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АРЕНДА.ToList();
            }
        }

        private void TxtFilter2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedIdUser = txtFilter2.SelectedItem as string;

            if (selectedIdUser != null && selectedIdUser != "Выберите ID_user")
            {
                // Фильтрация данных в DataGrid по выбранному ID_user
                var filteredArenda = АВТОПРОКАТEntities.GetContext().АРЕНДА
                                        .Where(z => z.ID_user.ToString() == selectedIdUser)
                                        .OrderBy(z => z.ID_user)
                                        .ToList();
                ArDataGrid.ItemsSource = filteredArenda;
            }
            else
            {
                // Если выбрано "Выберите ID_user", показываем все записи
                ArDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АРЕНДА.OrderBy(z => z.ID_user).ToList();
            }
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text;

            // Выполнение запроса на поиск автомобилей по заданному значению (строковому или числовому)
            var filteredAr = АВТОПРОКАТEntities.GetContext().АРЕНДА
                .Where(are => 
                              are.Статус.Contains(searchText)||
                              are.Дата_Начала_Аренды.ToString().Contains(searchText) ||
                              are.Дата_Конца_Аренды.ToString().Contains(searchText) ||
                              are.ID.ToString().Contains(searchText) ||
                              are.ID_user.ToString().Contains(searchText) ||
                              are.ID_client.ToString().Contains(searchText) ||
                              are.ID_avto.ToString().Contains(searchText)) 
                            
                .ToList();

            // Отображение результатов поиска в DataGrid
            ArDataGrid.ItemsSource = filteredAr;
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
            Window21 window21 = new Window21(((sender as Button).DataContext as АРЕНДА));
            window21.Show();


        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Window16 window16 = (new Window16((sender as Button).DataContext as АРЕНДА));
            window16.Show();
        }


        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var arRemoving = ArDataGrid.SelectedItems.Cast<АРЕНДА>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить{arRemoving.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    АВТОПРОКАТEntities.GetContext().АРЕНДА.RemoveRange(arRemoving);
                    АВТОПРОКАТEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    ArDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АРЕНДА.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Window16 window16 = new Window16(null);
            window16.Show();

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            ArDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АРЕНДА.ToList();
            //ClDataGrid.SelectedItems.Clear();
        }


        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                АВТОПРОКАТEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ArDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АРЕНДА.ToList();
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
