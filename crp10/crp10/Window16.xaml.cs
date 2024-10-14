using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Window16.xaml
    /// </summary>
    public partial class Window16 : Window
    {
        private АРЕНДА _currentАРЕНДА = new АРЕНДА();
        private bool _isTextChangedProgrammatically = false;


        public Window16(АРЕНДА selectedАРЕНДА)
        {
            InitializeComponent();

            if (selectedАРЕНДА != null)
                _currentАРЕНДА = selectedАРЕНДА;

            DataContext = _currentАРЕНДА;

            PsBox.TextChanged += PsBox_TextChanged;
            PrBox.TextChanged += PrBox_TextChanged;
            FioBox.TextChanged += PrBox_TextChanged;
            FBox.TextChanged += FBox_TextChanged;
            StBox.TextChanged += StBox_TextChanged;
            FIOBox.TextChanged += FIOBox_TextChanged;
        }

        private void ValidatePsBox()
        {
            if (!Regex.IsMatch(PsBox.Text, @"^\d*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры");
                PsBox.Text = Regex.Replace(PsBox.Text, @"[^\d]", "");
                PsBox.CaretIndex = PsBox.Text.Length;
            }
        }
        private void ValidatePrBox()
        {
            if (!Regex.IsMatch(PrBox.Text, @"^\d*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры");
                PrBox.Text = Regex.Replace(PrBox.Text, @"[^\d]", "");
                PrBox.CaretIndex = PrBox.Text.Length;
            }
        }
        private void ValidateFioBox()
        {
            if (!Regex.IsMatch(FioBox.Text, @"^\d*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры");
                FioBox.Text = Regex.Replace(FioBox.Text, @"[^\d]", "");
                FioBox.CaretIndex = FioBox.Text.Length;
            }
        }
        private void ValidateFBox()
        {
            if (!Regex.IsMatch(FBox.Text, @"^\d*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры");
                FBox.Text = Regex.Replace(FBox.Text, @"[^\d]", "");
                FBox.CaretIndex = FBox.Text.Length;
            }
        }
        private void ValidateStBox()
        {
            if (!Regex.IsMatch(StBox.Text, @"^[а-яА-ЯёЁ\s]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы");
                StBox.Text = Regex.Replace(StBox.Text, @"[^а-яА-ЯёЁ\s]", "");
                StBox.CaretIndex = StBox.Text.Length;
            }
        }
        private void ValidateFIOBox()
        {
            if (!Regex.IsMatch(FIOBox.Text, @"^[а-яА-ЯёЁ\s]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы");
                FIOBox.Text = Regex.Replace(FIOBox.Text, @"[^а-яА-ЯёЁ\s]", "");
                FIOBox.CaretIndex = FIOBox.Text.Length;
            }
        }


        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void FioBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateFioBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void PrBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidatePrBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void PsBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidatePsBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void FBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateFBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void StBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateStBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void FIOBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateFIOBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentАРЕНДА.ID == 0)
            {
                int maxId = АВТОПРОКАТEntities.GetContext().АРЕНДА.Max(r => r.ID); _currentАРЕНДА.ID = maxId + 1;
                //_currentКЛИЕНТЫ.recording_id = Guid.NewGuid();
                //АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.AddOrUpdate(_currentКЛИЕНТЫ);
            }

            if (_currentАРЕНДА.ID == 0)
                errors.AppendLine("Укажите ID Корректно");

            if (_currentАРЕНДА.ID_avto == 0)
                errors.AppendLine("Укажите ID Корректно");


            if (_currentАРЕНДА.ID_user == 0)
                errors.AppendLine("Укажите ID Корректно");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            АВТОПРОКАТEntities.GetContext().АРЕНДА.AddOrUpdate(_currentАРЕНДА);


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
