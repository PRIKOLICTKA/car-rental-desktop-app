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
    /// Логика взаимодействия для Window17.xaml
    /// </summary>
    public partial class Window17 : Window
    {
        private ТАРИФЫ _currentТАРИФЫ = new ТАРИФЫ();
        private bool _isTextChangedProgrammatically = false;


        public Window17(ТАРИФЫ selectedТАРИФЫ)
        {
            InitializeComponent();

            if (selectedТАРИФЫ != null)
                _currentТАРИФЫ = selectedТАРИФЫ;

            DataContext = _currentТАРИФЫ;

            UsBox.TextChanged += UsBox_TextChanged;
            PrBox.TextChanged += PrBox_TextChanged;
            PsBox.TextChanged += PsBox_TextChanged;
            FioBox.TextChanged += FioBox_TextChanged;
            TelBox.TextChanged += TelBox_TextChanged;
            TlBox.TextChanged += TlBox_TextChanged;
        }
        private void ValidateUsBox()
        {
            if (!Regex.IsMatch(UsBox.Text, @"^\d*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры");
                UsBox.Text = Regex.Replace(UsBox.Text, @"[^\d]", "");
                UsBox.CaretIndex = UsBox.Text.Length;
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
        private void ValidatePsBox()
        {
            if (!Regex.IsMatch(PsBox.Text, @"^\d*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры");
                PsBox.Text = Regex.Replace(PsBox.Text, @"[^\d]", "");
                PsBox.CaretIndex = PsBox.Text.Length;
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
        private void ValidateTelBox()
        {
            if (!Regex.IsMatch(TelBox.Text, @"^\d*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры");
                TelBox.Text = Regex.Replace(TelBox.Text, @"[^\d]", "");
                TelBox.CaretIndex = TelBox.Text.Length;
            }
        }
        private void ValidateTlBox()
        {
            if (!Regex.IsMatch(TlBox.Text, @"^\d*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры");
                TlBox.Text = Regex.Replace(TlBox.Text, @"[^\d]", "");
                TlBox.CaretIndex = TlBox.Text.Length;
            }
        }
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UsBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateUsBox();
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

        private void FioBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateFioBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void TelBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateTelBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void TlBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateTlBox();
                _isTextChangedProgrammatically = false;
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentТАРИФЫ.ID == 0)
            {
                int maxId = АВТОПРОКАТEntities.GetContext().ТАРИФЫ.Max(r => r.ID); _currentТАРИФЫ.ID = maxId + 1;
                //_currentКЛИЕНТЫ.recording_id = Guid.NewGuid();
                //АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.AddOrUpdate(_currentКЛИЕНТЫ);
            }

            if (_currentТАРИФЫ.ID == 0)
                errors.AppendLine("Укажите ID Корректно");

            if (_currentТАРИФЫ.ID_avto == 0)
                errors.AppendLine("Укажите ID Корректно");


            

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            АВТОПРОКАТEntities.GetContext().ТАРИФЫ.AddOrUpdate(_currentТАРИФЫ);


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
