using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using System.IO;
using System.Text.RegularExpressions;

namespace crp10
{
    /// <summary>
    /// Логика взаимодействия для Window14.xaml
    /// </summary>
    public partial class Window14 : Window
    {
        private АВТОМОБИЛИ _currentАВТОМОБИЛИ = new АВТОМОБИЛИ();
        private byte[] _photoBytes;
        private bool _isTextChangedProgrammatically = false;

        public Window14(АВТОМОБИЛИ selectedАВТОМОБИЛИ)
        {
            InitializeComponent();

            if (selectedАВТОМОБИЛИ != null)
                _currentАВТОМОБИЛИ = selectedАВТОМОБИЛИ;

            DataContext = _currentАВТОМОБИЛИ;
            //// Подключение обработчиков событий TextChanged для каждого TextBox
            MBox.TextChanged += MBox_TextChanged;
            //MoBox.TextChanged += MoBox_TextChanged;
            GBox.TextChanged += GBox_TextChanged;
            ClBox.TextChanged += ClBox_TextChanged;
            NBox.TextChanged += NBox_TextChanged;
            VBox.TextChanged += VBox_TextChanged;
            CTCBox.TextChanged += CTCBox_TextChanged;
     
            PTCBox.TextChanged += PTCBox_TextChanged;
          
            CtraxBox.TextChanged += CtraxBox_TextChanged;
            //PemontBox.TextChanged += PemontBox_TextChanged;
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ValidateMBox()
        {
            if (!Regex.IsMatch(MBox.Text, @"^[a-zA-Zа-яА-ЯёЁ\s]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы (латинские и русские)");
                MBox.Text = Regex.Replace(MBox.Text, @"[^a-zA-Zа-яА-ЯёЁ\s]", "");
                MBox.CaretIndex = MBox.Text.Length;
            }
        }

        //private void ValidateMoBox()
        //{
        //    if (!Regex.IsMatch(MoBox.Text, @"^[a-zA-Zа-яА-ЯёЁ\d\s]*$"))
        //    {
        //        ShowErrorMessage("В этом поле можно вводить только буквы (латинские и русские), цифры и пробелы");
        //        MoBox.Text = Regex.Replace(MoBox.Text, @"[^a-zA-Zа-яА-ЯёЁ\d\s]", "");
        //        MoBox.CaretIndex = MoBox.Text.Length;
        //    }
        //}

        private void ValidateGBox()
        {
            if (!Regex.IsMatch(GBox.Text, @"^\d*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры");
                GBox.Text = Regex.Replace(GBox.Text, @"[^\d]", "");
                GBox.CaretIndex = GBox.Text.Length;
            }
        }

        private void ValidateClBox()
        {
            if (!Regex.IsMatch(ClBox.Text, @"^[а-яА-ЯёЁ]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы (русские)");
                ClBox.Text = Regex.Replace(ClBox.Text, @"[^а-яА-ЯёЁ]", "");
                ClBox.CaretIndex = ClBox.Text.Length;
            }
        }

        private void ValidateNBox()
        {
            if (!Regex.IsMatch(NBox.Text, @"^[А-Я\d]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только заглавные русские буквы и цифры");
                NBox.Text = Regex.Replace(NBox.Text, @"[^А-Я\d]", "");
                NBox.CaretIndex = NBox.Text.Length;
            }
        }

        private void ValidateVBox()
        {
            if (!Regex.IsMatch(VBox.Text, @"^[A-Z\d]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только заглавные латинские буквы и цифры");
                VBox.Text = Regex.Replace(VBox.Text, @"[^A-Z\d]", "");
                VBox.CaretIndex = VBox.Text.Length;
            }
        }

        private void ValidateCTCBox()
        {
            if (!Regex.IsMatch(CTCBox.Text, @"^[\d ]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры и пробелы");
                CTCBox.Text = Regex.Replace(CTCBox.Text, @"[^\d ]", "");
                CTCBox.CaretIndex = CTCBox.Text.Length;
            }
        }

        //private void ValidateCtcBox()
        //{
        //    // Регулярное выражение для форматов YYYY-MM-DD и DD-MM-YYYY
        //    string pattern = @"^(\d{4}-\d{2}-\d{2}|\d{2}-\d{2}-\d{4})$";

        //    if (!Regex.IsMatch(CtcBox.Text, pattern))
        //    {
        //        ShowErrorMessage("В этом поле можно вводить только даты в формате YYYY-MM-DD или DD-MM-YYYY");
        //        // Исправление текста может быть специфичным для вашего случая, но здесь мы просто очищаем его
        //        CtcBox.Text = "";
        //        CtcBox.CaretIndex = CtcBox.Text.Length;
        //    }
        //}

      
       private void ValidatePTCBox()
        {
            if (!Regex.IsMatch(PTCBox.Text, @"^[А-Я\d ]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только заглавные русские буквы, цифры и пробелы");
                PTCBox.Text = Regex.Replace(PTCBox.Text, @"[^А-Я\d ]", "");
                PTCBox.CaretIndex = PTCBox.Text.Length;
            }
        }
        //private void ValidatePtcBox()
        //{
        //    // Регулярное выражение для форматов YYYY-MM-DD и DD-MM-YYYY
        //    string pattern = @"^(\d{4}-\d{2}-\d{2}|\d{2}-\d{2}-\d{4})$";

        //    if (!Regex.IsMatch(CtcBox.Text, pattern))
        //    {
        //        ShowErrorMessage("В этом поле можно вводить только даты в формате YYYY-MM-DD или DD-MM-YYYY");
        //        // Исправление текста может быть специфичным для вашего случая, но здесь мы просто очищаем его
        //        PtcBox.Text = "";
        //        PtcBox.CaretIndex = PtcBox.Text.Length;
        //    }
        //}

        private void ValidateCtraxBox()
        {
            if (!Regex.IsMatch(CtraxBox.Text, @"^\d{4}-\d{6}-\d{5}$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры и тире в формате 1234-567890-12345");
                CtraxBox.Text = "";
                CtraxBox.CaretIndex = CtraxBox.Text.Length;
            }
        }

        //private void ValidatePemontBox()
        //{
        //    if (!Regex.IsMatch(PemontBox.Text, @"^[\d\-]*$"))
        //    {
        //        ShowErrorMessage("В этом поле можно вводить только цифры и тире");
        //        PemontBox.Text = Regex.Replace(PemontBox.Text, @"[^\d\-]", "");
        //        PemontBox.CaretIndex = PemontBox.Text.Length;
        //    }
        //}

       

        private void MBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateMBox();
                _isTextChangedProgrammatically = false;
            }
        }

        //private void MoBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!_isTextChangedProgrammatically)
        //    {
        //        _isTextChangedProgrammatically = true;
        //        ValidateMoBox();
        //        _isTextChangedProgrammatically = false;
        //    }
        //}

        private void GBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateGBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void ClBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateClBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void NBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateNBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void VBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateVBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void CTCBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateCTCBox();
                _isTextChangedProgrammatically = false;
            }
        }

        //private void CtcBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!_isTextChangedProgrammatically)
        //    {
        //        _isTextChangedProgrammatically = true;
        //        ValidateCtcBox();
        //        _isTextChangedProgrammatically = false;
        //    }
        //}

        private void PTCBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidatePTCBox();
                _isTextChangedProgrammatically = false;
            }
        }

        //private void PtcBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!_isTextChangedProgrammatically)
        //    {
        //        _isTextChangedProgrammatically = true;
        //        ValidatePtcBox();
        //        _isTextChangedProgrammatically = false;
        //    }
        //}

        private void CtraxBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateCtraxBox();
                _isTextChangedProgrammatically = false;
            }
        }
        //private void PemontBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!_isTextChangedProgrammatically)
        //    {
        //        _isTextChangedProgrammatically = true;
        //        ValidatePemontBox();
        //        _isTextChangedProgrammatically = false;
        //    }
        //}
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
            if (_currentАВТОМОБИЛИ.ID == 0)
            {
                int maxId = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.Max(r => r.ID); _currentАВТОМОБИЛИ.ID = maxId + 1;
                //_currentКЛИЕНТЫ.recording_id = Guid.NewGuid();
                //АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.AddOrUpdate(_currentКЛИЕНТЫ);
            }

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentАВТОМОБИЛИ.Марка))
                errors.AppendLine("Укажите Марку");

            if (string.IsNullOrWhiteSpace(_currentАВТОМОБИЛИ.Модель))
                errors.AppendLine("Укажите Модель");

            if (string.IsNullOrWhiteSpace(_currentАВТОМОБИЛИ.Цвет))
                errors.AppendLine("Укажите Цвет");

            if (string.IsNullOrWhiteSpace(_currentАВТОМОБИЛИ.Номерной_знак))
                errors.AppendLine("Укажите Номер");

            if (string.IsNullOrWhiteSpace(_currentАВТОМОБИЛИ.VIN_код))
                errors.AppendLine("Укажите VIN код");

            if (string.IsNullOrWhiteSpace(_currentАВТОМОБИЛИ.Серия_номер_СТС))
                errors.AppendLine("Укажите № СТС");

            if (string.IsNullOrWhiteSpace(_currentАВТОМОБИЛИ.Серия_номер_ПТС))
                errors.AppendLine("Укажите № ПТС");

            if (_photoBytes == null || _photoBytes.Length == 0)
                errors.AppendLine("Добавьте фото");

            //if (_currentКЛИЕНТЫ.ID == null)
            //    errors.AppendLine("Укажите ID Корректно");

            //if (_currentАВТОМОБИЛИ.ID == null)
            //    errors.AppendLine("Укажите ID Корректно");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _currentАВТОМОБИЛИ.Фото = _photoBytes;

            АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.AddOrUpdate(_currentАВТОМОБИЛИ);


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
