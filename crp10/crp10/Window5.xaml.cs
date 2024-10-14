using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
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
using Path = System.IO.Path;

namespace crp10
{
    /// <summary>
    /// Логика взаимодействия для Window5.xaml
    /// </summary>
    public partial class Window5 : System.Windows.Window
    {
        private readonly КЛИЕНТЫ _context = new КЛИЕНТЫ();
        private КЛИЕНТЫ _currentКЛИЕНТЫ = new КЛИЕНТЫ();
        private Document wordDocument;
        private string document1Path = @"C:\Users\Home\Desktop\ДИПЛОМ\Бухгалтерский баланс.docx";
        private string document2Path = @"C:\Users\Home\Desktop\ДИПЛОМ\Отчет о движении денежных средств.docx";
        private string document3Path = @"C:\Users\Home\Desktop\ДИПЛОМ\Отчет о целевом использовании средств.docx";
        public Window5()
        {
            InitializeComponent();
            UpdateTextBlock();
            document1Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Бухгалтерский баланс.docx");
            document2Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Отчет о движении денежных средств.docx");
            document3Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Отчет о целевом использовании средств.docx");
        }
        private void UpdateTextBlock()
        {
            //int maxClientId =  _context.ID.ToString().Max();
            //ClientCount.Text = $" {maxClientId}";

            int maxId = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.Max(r => r.ID); _currentКЛИЕНТЫ.ID = maxId;
            ClientCount.Text = $" {maxId}";
        }

        private void OpenDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            string document1Path = @"C:\Users\Home\Desktop\ДИПЛОМ\Бухгалтерский баланс.docx";
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordDocument = wordApp.Documents.Open(document1Path);
            wordApp.Visible = true;
        }

        private void addDoc2_Click(object sender, RoutedEventArgs e)
        {
            string document2Path = @"C:\Users\Home\Desktop\ДИПЛОМ\Отчет о движении денежных средств.docx";
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordDocument = wordApp.Documents.Open(document2Path);
            wordApp.Visible = true;
        }

        private void addDoc3_Click(object sender, RoutedEventArgs e)
        {
            string document3Path = @"C:\Users\Home\Desktop\ДИПЛОМ\Отчет о целевом использовании средств.docx";
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordDocument = wordApp.Documents.Open(document3Path);
            wordApp.Visible = true;
        }
        private void CarButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();    
            window1.Show();
            this.Close();
        }

        private void TarifButton_Click(object sender, RoutedEventArgs e)
        {
            Window4 window4 = new Window4();    
            window4.Show();
            this.Close();
        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            Window3 window3 = new Window3();
            window3.Show();
            this.Close();
        }

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
                    System.Windows.Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
