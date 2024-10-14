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
    /// Логика взаимодействия для Window7.xaml
    /// </summary>
    public partial class Window7 : Window
    {
        public Window7()
        {
            InitializeComponent();
            CarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList();
            txtSearch.TextChanged += TBoxSearch_TextChanged;

            // Заглушка для выбора марки
            var markaTypes = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Select(a => a.Марка).Distinct().ToList();
            markaTypes.Insert(0, "ВЫБЕРИТЕ МАРКУ");
            ComboMarka.ItemsSource = markaTypes;
            ComboMarka.SelectedIndex = 0; // Установка "Выберите марку" как первого элемента

            // Заглушка для выбора модели
            var modelTypes = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Select(b => b.Модель).Distinct().ToList();
            modelTypes.Insert(0, "ВЫБЕРИТЕ МОДЕЛЬ");
            ComboModel.ItemsSource = modelTypes;
            ComboModel.SelectedIndex = 0; // Установка "Выберите модель" как первого элемента
        }

        private void ComboMarka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedMarka = ComboMarka.SelectedItem as string;
            string selectedModel = ComboModel.SelectedItem as string;

            if (selectedMarka == "ВЫБЕРИТЕ МАРКУ")
            {
                CarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList();
            }
            else
            {
                var carsWithSelectedMarka = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Where(car => car.Марка == selectedMarka).ToList();
                CarDataGrid.ItemsSource = carsWithSelectedMarka;
            }

            // Обновляем список моделей в ComboModel
            if (selectedMarka != null && selectedMarka != "ВЫБЕРИТЕ МАРКУ")
            {
                var modelTypes = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ
                                    .Where(car => car.Марка == selectedMarka)
                                    .Select(b => b.Модель)
                                    .Distinct()
                                    .ToList();
                modelTypes.Insert(0, "ВЫБЕРИТЕ МОДЕЛЬ");
                ComboModel.ItemsSource = modelTypes;

                // Если выбранная модель после обновления списка моделей ещё доступна, оставляем её выбранной
                if (modelTypes.Contains(selectedModel))
                {
                    ComboModel.SelectedItem = selectedModel;
                }
                else
                {
                    ComboModel.SelectedIndex = 0; // Выбираем "Выберите модель", если предыдущая модель больше не доступна
                }
            }

            // Обновляем данные в DataGrid в зависимости от выбранных фильтров
            if (selectedMarka != null && selectedModel != null && selectedMarka != "ВЫБЕРИТЕ МАРКУ" && selectedModel != "ВЫБЕРИТЕ МОДЕЛЬ")
            {
                if (selectedMarka == "ВЫБЕРИТЕ МАРКУ" && selectedModel == "ВЫБЕРИТЕ МОДЕЛЬ")
                {
                    CarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList();
                }
                else if (selectedMarka == "ВЫБЕРИТЕ МАРКУ")
                {
                    CarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Where(car => car.Модель == selectedModel).ToList();
                }
                else if (selectedModel == "ВЫБЕРИТЕ МОДЕЛЬ")
                {
                    CarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Where(car => car.Марка == selectedMarka).ToList();
                }
                else
                {
                    CarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Where(car => car.Марка == selectedMarka && car.Модель == selectedModel).ToList();
                }
            }
        }
    






    private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text;

            // Выполнение запроса на поиск автомобилей по заданному значению (строковому или числовому)
            var filteredCars = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ
                .Where(car => car.Марка.Contains(searchText) ||
                              car.Модель.Contains(searchText) ||
                              car.Цвет.Contains(searchText) ||
                              car.Номерной_знак.Contains(searchText) ||
                              car.VIN_код.Contains(searchText) ||
                              car.Серия_номер_ПТС.Contains(searchText) ||
                              car.Серия_номер_СТС.Contains(searchText) ||
                              car.ID.ToString().Contains(searchText) ||
                              car.Год_выпуска.ToString().Contains(searchText))
                .ToList();

            // Отображение результатов поиска в DataGrid
            CarDataGrid.ItemsSource = filteredCars;
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
            Window14 window14 = (new Window14((sender as Button).DataContext as АВТОМОБИЛИ));
            window14.Show();
        }


        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var carRemoving = CarDataGrid.SelectedItems.Cast<АВТОМОБИЛИ>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить{carRemoving.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.RemoveRange(carRemoving);
                    АВТОПРОКАТEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    CarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void LookButton_Click(object sender, RoutedEventArgs e)
        {
            Window19 window19 = new Window19(((sender as Button).DataContext as АВТОМОБИЛИ));
            window19.Show();


        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Window14 window14 = new Window14(null);
            window14.Show();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            CarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList();
            //ClDataGrid.SelectedItems.Clear();
        }


        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                АВТОПРОКАТEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                CarDataGrid.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList();
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
