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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            var curerntAvto = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList();
            LViewCar.ItemsSource = curerntAvto;

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

            txtSearch.TextChanged += TBoxSearch_TextChanged;
        }

        private void ComboMarka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedMarka = ComboMarka.SelectedItem as string;
            string selectedModel = ComboModel.SelectedItem as string;

            if (selectedMarka == "ВЫБЕРИТЕ МАРКУ")
            {
                LViewCar.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList();
            }
            else
            {
                var carsWithSelectedMarka = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Where(car => car.Марка == selectedMarka).ToList();
                LViewCar.ItemsSource = carsWithSelectedMarka;
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

            // Обновляем данные в ListView в зависимости от выбранных фильтров
            if (selectedMarka != null && selectedModel != null && selectedMarka != "ВЫБЕРИТЕ МАРКУ" && selectedModel != "ВЫБЕРИТЕ МОДЕЛЬ")
            {
                if (selectedMarka == "ВЫБЕРИТЕ МАРКУ" && selectedModel == "ВЫБЕРИТЕ МОДЕЛЬ")
                {
                    LViewCar.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList();
                }
                else if (selectedMarka == "ВЫБЕРИТЕ МАРКУ")
                {
                    LViewCar.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Where(car => car.Модель == selectedModel).ToList();
                }
                else if (selectedModel == "ВЫБЕРИТЕ МОДЕЛЬ")
                {
                    LViewCar.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Where(car => car.Марка == selectedMarka).ToList();
                }
                else
                {
                    LViewCar.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Where(car => car.Марка == selectedMarka && car.Модель == selectedModel).ToList();
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
            LViewCar.ItemsSource = filteredCars;
        }


        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text;

            // Выполнение запроса на поиск автомобилей по заданному значению
            var filteredCars = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Where(car => car.Марка.Contains(searchText) || car.Модель.Contains(searchText) 
            || car.VIN_код.Contains(searchText) || car.Номерной_знак.Contains(searchText)
             || car.Серия_номер_ПТС.Contains(searchText) || car.Серия_номер_СТС.Contains(searchText) || car.Цвет.Contains(searchText)).ToList();

            // Отображение результатов поиска в ListView
            LViewCar.ItemsSource = filteredCars;

           
        }




        //private void ComboMarka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    string selectedMarka = ComboMarka.SelectedItem as string;

        //    if (selectedMarka == "ВСЕ МАРКИ")
        //    {
        //        LViewCar.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList();
        //    }
        //    else
        //    {
        //        var carsWithSelectedMarka = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Where(car => car.Марка == selectedMarka).ToList();
        //        LViewCar.ItemsSource = carsWithSelectedMarka;
        //    }
        //}



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

        private void ComboModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
