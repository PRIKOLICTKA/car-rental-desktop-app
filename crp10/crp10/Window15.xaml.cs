using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace crp10
{
    /// <summary>
    /// Логика взаимодействия для Window15.xaml
    /// </summary>
    public partial class Window15 : Window
    {
        private ZAYAVKI _currentZAYAVKI = new ZAYAVKI();
        private bool _isTextChangedProgrammatically = false;


        public Window15(ZAYAVKI selectedZAYAVKI)
        {
            InitializeComponent();

            if (selectedZAYAVKI != null)
                _currentZAYAVKI = selectedZAYAVKI;

            DataContext = _currentZAYAVKI;

            MBox.TextChanged += MBox_TextChanged;
            MoBox.TextChanged += MoBox_TextChanged;
          
            ClBox.TextChanged += ClBox_TextChanged;
            NBox.TextChanged += NBox_TextChanged;
            VBox.TextChanged += VBox_TextChanged;
            CTCBox.TextChanged += CTCBox_TextChanged;
            StatusBox.TextChanged += StatusBox_TextChanged;
            TtBox.TextChanged += TtBox_TextChanged;
            StatusBox.TextChanged += StatusBox_TextChanged;
            EndBox.TextChanged += EndBox_TextChanged;
           
        }
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ValidateMBox()
        {
            if (!Regex.IsMatch(MBox.Text, @"^\d*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры");
                MBox.Text = Regex.Replace(MBox.Text, @"[^\d]", "");
                MBox.CaretIndex = MBox.Text.Length;
            }
        }

        private void ValidateMoBox()
        {
            if (!Regex.IsMatch(MoBox.Text, @"^\d*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры");
                MoBox.Text = Regex.Replace(MoBox.Text, @"[^\d]", "");
                MoBox.CaretIndex = MoBox.Text.Length;
            }
        }

    

        private void ValidateClBox()
        {
            if (!Regex.IsMatch(ClBox.Text, @"^[а-яА-ЯёЁ\s]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы");
                ClBox.Text = Regex.Replace(ClBox.Text, @"[^а-яА-ЯёЁ\s]", "");
                ClBox.CaretIndex = ClBox.Text.Length;
            }
        }

        private void ValidateNBox()
        {
            if (!Regex.IsMatch(NBox.Text, @"^[а-яА-ЯёЁ\s]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы");
                NBox.Text = Regex.Replace(NBox.Text, @"[^а-яА-ЯёЁ\s]", "");
                NBox.CaretIndex = NBox.Text.Length;
            }
        }

        private void ValidateVBox()
        {
            if (!Regex.IsMatch(VBox.Text, @"^[а-яА-ЯёЁ\s]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы");
                VBox.Text = Regex.Replace(VBox.Text, @"[^а-яА-ЯёЁ\s]", "");
                VBox.CaretIndex = VBox.Text.Length;
            }
        }

        private void ValidateCTCBox()
        {
            if (!Regex.IsMatch(CTCBox.Text, @"^([01]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$"))
            {
                ShowErrorMessage("В этом поле можно вводить только время в формате HH:MM:SS");
                CTCBox.Text = ""; // Очистка текстбокса в случае ошибки
                CTCBox.CaretIndex = CTCBox.Text.Length;
            }
        }

        private void ValidateCtcBox()
        {
            if (!Regex.IsMatch(CtcBox.Text, @"^[\w\.\-@]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы, цифры и символы (., -, @)");
                CtcBox.Text = Regex.Replace(CtcBox.Text, @"[^\w\.\-@]", "");
                CtcBox.CaretIndex = CtcBox.Text.Length;
            }
        }

        private void ValidateTtBox()
        {
            // Регулярное выражение для проверки, что строка содержит буквы, пробелы, скобки, точки и слеши
            if (!Regex.IsMatch(TtBox.Text, @"^[a-zA-Zа-яА-ЯёЁ\s\(\)\./]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы, пробелы, скобки, точки и символы /");
                TtBox.Text = Regex.Replace(TtBox.Text, @"[^a-zA-Zа-яА-ЯёЁ\s\(\)\./]", "");
                TtBox.CaretIndex = TtBox.Text.Length;
            }
        }

        private void ValidateEndBox()
        {
            // Регулярное выражение для проверки, что строка содержит буквы, пробелы, скобки, точки и слеши
            if (!Regex.IsMatch(EndBox.Text, @"^[a-zA-Zа-яА-ЯёЁ\s\(\)\./]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы, пробелы, скобки, точки и символы /");
                EndBox.Text = Regex.Replace(EndBox.Text, @"[^a-zA-Zа-яА-ЯёЁ\s\(\)\./]", "");
                EndBox.CaretIndex = EndBox.Text.Length;
            }
        }

        private void ValidatePTCBox()
        {
            if (!Regex.IsMatch(PTCBox.Text, @"^[\d\+\(\)\-\s]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только цифры и символы (+, (, ), -)");
                PTCBox.Text = Regex.Replace(PTCBox.Text, @"[^\d\+\(\)\-\s]", "");
                PTCBox.CaretIndex = PTCBox.Text.Length;
            }
        }
        private void ValidateStatusBox()
        {
            if (!Regex.IsMatch(StatusBox.Text, @"^[а-яА-ЯёЁ]*$"))
            {
                ShowErrorMessage("В этом поле можно вводить только буквы (русские)");
                StatusBox.Text = Regex.Replace(StatusBox.Text, @"[^а-яА-ЯёЁ]", "");
                StatusBox.CaretIndex = StatusBox.Text.Length;
            }
        }

        private void ValidatePtcBox()
        {
            if (!Regex.IsMatch(PtcBox.Text, @"^([01]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$"))
            {
                ShowErrorMessage("В этом поле можно вводить только время в формате HH:MM:SS");
                PtcBox.Text = ""; // Очистка текстбокса в случае ошибки
                PtcBox.CaretIndex = PtcBox.Text.Length;
            }
        }




        private void MBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateMBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void MoBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateMoBox();
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

        private void CtcBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateCtcBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void PTCBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidatePTCBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void PtcBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidatePtcBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void StatusBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateStatusBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void TtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateTtBox();
                _isTextChangedProgrammatically = false;
            }
        }
        private void EndBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isTextChangedProgrammatically)
            {
                _isTextChangedProgrammatically = true;
                ValidateEndBox();
                _isTextChangedProgrammatically = false;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentZAYAVKI.ID == 0)
            {
                int maxId = АВТОПРОКАТEntities.GetContext().ZAYAVKI.Max(r => r.ID); _currentZAYAVKI.ID = maxId + 1;
                //_currentКЛИЕНТЫ.recording_id = Guid.NewGuid();
                //АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.AddOrUpdate(_currentКЛИЕНТЫ);
            }
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentZAYAVKI.Фамилия))
                errors.AppendLine("Укажите Фамилию");

            if (string.IsNullOrWhiteSpace(_currentZAYAVKI.Имя))
                errors.AppendLine("Укажите Имя");

            if (string.IsNullOrWhiteSpace(_currentZAYAVKI.Отчество))
                errors.AppendLine("Укажите Отчество");

            if (string.IsNullOrWhiteSpace(_currentZAYAVKI.Телефон))
                errors.AppendLine("Укажите Телефон");

            if (string.IsNullOrWhiteSpace(_currentZAYAVKI.Email))
                errors.AppendLine("Укажите Emai");

            if (_currentZAYAVKI.ID == 0)
                errors.AppendLine("Укажите ID Корректно");

            if (_currentZAYAVKI.ID_avto == 0)
                errors.AppendLine("Укажите ID Корректно");


            if (_currentZAYAVKI.ID_user == 0)
                errors.AppendLine("Укажите ID Корректно");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            АВТОПРОКАТEntities.GetContext().ZAYAVKI.AddOrUpdate(_currentZAYAVKI);


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
