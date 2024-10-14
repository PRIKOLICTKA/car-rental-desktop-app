using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для Window11.xaml
    /// </summary>
    public partial class Window11 : Window
    {
        private byte[] _photoBytes;
        private КЛИЕНТЫ _currentКЛИЕНТЫ = new КЛИЕНТЫ();
        private bool _isTextChangedProgrammatically = false;


        public Window11(КЛИЕНТЫ selectedКЛИЕНТЫ)
        {
            InitializeComponent();

            if (selectedКЛИЕНТЫ != null)
                _currentКЛИЕНТЫ = selectedКЛИЕНТЫ;

            DataContext = _currentКЛИЕНТЫ;
            // Подключение обработчиков событий
            UsBox.TextChanged += UsBox_TextChanged;
            PrBox.TextChanged += PrBox_TextChanged;
            PsBox.TextChanged += PsBox_TextChanged;
            FioBox.TextChanged += FioBox_TextChanged;
            TelBox.TextChanged += TelBox_TextChanged;
            EmBox.TextChanged += EmBox_TextChanged;
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

        private void ValidatePrPsBox(TextBox textBox)
        {
            if (!Regex.IsMatch(textBox.Text, @"^[\d ]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры и пробел");
                textBox.Text = Regex.Replace(textBox.Text, @"[^\d ]", "");
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void ValidateFioBox()
        {
            if (!Regex.IsMatch(FioBox.Text, @"^[а-яА-ЯёЁ\s]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы");
                FioBox.Text = Regex.Replace(FioBox.Text, @"[^а-яА-ЯёЁ\s]", "");
                FioBox.CaretIndex = FioBox.Text.Length;
            }
        }

        private void ValidateTelBox()
        {
            if (!Regex.IsMatch(TelBox.Text, @"^[\d\+\(\)\-\s]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры и символы (+, (, ), -)");
                TelBox.Text = Regex.Replace(TelBox.Text, @"[^\d\+\(\)\-\s]", "");
                TelBox.CaretIndex = TelBox.Text.Length;
            }
        }

        private void ValidateEmBox()
        {
            if (!Regex.IsMatch(EmBox.Text, @"^[\w\.\-@]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы, цифры и символы (., -, @)");
                EmBox.Text = Regex.Replace(EmBox.Text, @"[^\w\.\-@]", "");
                EmBox.CaretIndex = EmBox.Text.Length;
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
                ValidatePrPsBox(PrBox);
                _isTextChangedProgrammatically = false;
            }
        }

        private void PsBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidatePrPsBox(PsBox);
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

        private void EmBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateEmBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                _photoBytes = File.ReadAllBytes(filePath);

                ImageControl.Source = ConvertByteArrayToImage(_photoBytes);
            }
        }
        private ImageSource ConvertByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            var imageSourceConverter = new ImageSourceConverter();
            return (ImageSource)imageSourceConverter.ConvertFrom(byteArray);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
             {
           
            StringBuilder errors = new StringBuilder();

           
            if (_currentКЛИЕНТЫ.ID == 0)
            {
                int maxId = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.Max(r => r.ID); _currentКЛИЕНТЫ.ID = maxId + 1;
                //_currentКЛИЕНТЫ.recording_id = Guid.NewGuid();
                //АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.AddOrUpdate(_currentКЛИЕНТЫ);
            }
            if (string.IsNullOrWhiteSpace(_currentКЛИЕНТЫ.ФИО))
                errors.AppendLine("Укажите ФИО");

            if (string.IsNullOrWhiteSpace(_currentКЛИЕНТЫ.Паспорт_номер))
                errors.AppendLine("Укажите № Паспорта");

            if (string.IsNullOrWhiteSpace(_currentКЛИЕНТЫ.Права_номер))
                errors.AppendLine("Укажите № Прав");

            if (string.IsNullOrWhiteSpace(_currentКЛИЕНТЫ.Телефон))
                errors.AppendLine("Укажите Телефон");

            if (string.IsNullOrWhiteSpace(_currentКЛИЕНТЫ.Email))
                errors.AppendLine("Укажите Email");

            if (_currentКЛИЕНТЫ.ID_user == null)
                errors.AppendLine("Укажите ID Корректно");

            if (_photoBytes == null || _photoBytes.Length == 0)
                errors.AppendLine("Добавьте фото");



            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _currentКЛИЕНТЫ.Фото = _photoBytes;

            АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.AddOrUpdate(_currentКЛИЕНТЫ);
            

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
        //private void PsBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!Regex.IsMatch(PrBox.Text, @"^\d{4} \d{6}$"))
        //    {
        //        MessageBox.Show("В этом поле можно вводить только 10 цифр (формат: 1111 111111)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        PrBox.Text = Regex.Replace(PrBox.Text, @"[^\d ]", "").Trim();
        //        if (PrBox.Text.Length > 4)
        //        {
        //            PrBox.Text = PrBox.Text.Insert(4, " ");
        //        }
        //        PrBox.CaretIndex = PrBox.Text.Length;
        //    }
        //}

        //private void UsBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!Regex.IsMatch(UsBox.Text, @"^\d*$"))
        //    {
        //        MessageBox.Show("В этом поле можно вводить только цифры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        UsBox.Text = Regex.Replace(UsBox.Text, @"[^\d]", "");
        //        UsBox.CaretIndex = UsBox.Text.Length;
        //    }
        //}

        //private void PrBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!Regex.IsMatch(PrBox.Text, @"^\d{4} \d{6}$"))
        //    {
        //        MessageBox.Show("В этом поле можно вводить только 10 цифр (формат: 1111 111111)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        PrBox.Text = Regex.Replace(PrBox.Text, @"[^\d ]", "").Trim();
        //        if (PrBox.Text.Length > 4)
        //        {
        //            PrBox.Text = PrBox.Text.Insert(4, " ");
        //        }
        //        PrBox.CaretIndex = PrBox.Text.Length;
        //    }
        //}

        //private void FioBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!Regex.IsMatch(FioBox.Text, @"^[а-яА-ЯёЁ\s]*$"))
        //    {
        //        MessageBox.Show("В этом поле можно вводить только буквы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        FioBox.Text = Regex.Replace(FioBox.Text, @"[^а-яА-ЯёЁ\s]", "");
        //        FioBox.CaretIndex = FioBox.Text.Length;
        //    }
        //}
        //private void TelBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!Regex.IsMatch(TelBox.Text, @"^[\d\+\(\)\-\s]*$"))
        //    {
        //        MessageBox.Show("В этом поле можно вводить только цифры, прмиер: +7 (993) 666-63-77 или 89754507687", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        TelBox.Text = Regex.Replace(TelBox.Text, @"[^\d\+\(\)\-\s]", "");
        //        TelBox.CaretIndex = TelBox.Text.Length;
        //    }
        //}

        //private void EmBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!Regex.IsMatch(EmBox.Text, @"^[\w\.\-@]*$"))
        //    {
        //        MessageBox.Show("В этом поле можно вводить только буквы, цифры и символы (., -, @)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        EmBox.Text = Regex.Replace(EmBox.Text, @"[^\w\.\-@]", "");
        //        EmBox.CaretIndex = EmBox.Text.Length;
        //    }
        //}
    

    //public class AutoRentDbContext : DbContext
    //{
    //    public AutoRentDbContext() : base("name=AutoRentDbContext")
    //    {
    //    }
    //}

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
