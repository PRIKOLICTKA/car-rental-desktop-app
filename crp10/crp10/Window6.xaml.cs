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
using Microsoft.Office.Interop.Word;
using System.IO;
using System.Reflection;
using Path = System.IO.Path;



namespace crp10
{
    /// <summary>
    /// Логика взаимодействия для Window6.xaml
    /// </summary>
    public partial class Window6 : System.Windows.Window
    {
        private readonly КЛИЕНТЫ _context = new КЛИЕНТЫ();
        private КЛИЕНТЫ _currentКЛИЕНТЫ = new КЛИЕНТЫ();

        private Document wordDocument;
        private string document1Path = @"F:\ПРЕДЗАЩИТА2\Договор аренды.docx";
        private string document2Path = @"F:\ПРЕДЗАЩИТА2\Фин результаты.docx";
        private string document3Path = @"F:\ПРЕДЗАЩИТА2\Страховка.docx";
        //private Microsoft.Office.Interop.Word.Application wordApp;

        //string filePath = @"C:\Users\Home\Desktop\ДИПЛОМ\Договор аренды.docx";
        public Window6()
        {
            InitializeComponent();
            LViewCar.ItemsSource = АВТОПРОКАТEntities.GetContext().АВТОМОБИЛИ.ToList();
            UpdateTextBlock();

            document1Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Договор аренды.docx");
            document2Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Фин результаты.docx");
            document3Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Страховка.docx");

            using (var context = new АВТОПРОКАТEntities())
            {
                var last5Orders = context.АРЕНДА
                    .OrderByDescending(o => o.Дата_Начала_Аренды)
                    .Take(5)
                    .ToList();

                var cars = context.АВТОМОБИЛИ.ToList();

                var result = (from order in last5Orders
                              join car in cars on order.ID_avto equals car.ID
                              select new
                              {
                                  ID = order.ID,
                                  Фото = car.Фото,
                                  Марка = car.Марка,
                                  Модель = car.Модель
                              }).ToList();

                LViewCar.ItemsSource = result;
            }
        }
        private void UpdateTextBlock()
        {
      

            int maxId = АВТОПРОКАТEntities.GetContext().КЛИЕНТЫ.Max(r => r.ID); _currentКЛИЕНТЫ.ID = maxId;
            ClientCount.Text = $" {maxId}";
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

        private void AvButton_Click(object sender, RoutedEventArgs e)
        {
            Window7 window7 = new Window7();
            window7.Show();
            this.Close();
        }

        private void ZaButton_Click(object sender, RoutedEventArgs e)
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

        private void TrButton_Click(object sender, RoutedEventArgs e)
        {
            Window10 window10 = new Window10();
            window10.Show();
            this.Close();
        }

        private void ExButton_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OpenDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            string document1Path = @"C:\Users\Home\Desktop\ДИПЛОМ\Договор аренды.docx";
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordDocument = wordApp.Documents.Open(document1Path);
            wordApp.Visible = true;
        }

        private void addDoc2_Click(object sender, RoutedEventArgs e)
        {
            string document2Path = @"C:\Users\Home\Desktop\ДИПЛОМ\Фин результаты.docx";
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordDocument = wordApp.Documents.Open(document2Path);
            wordApp.Visible = true;
        }

        private void addDoc3_Click(object sender, RoutedEventArgs e)
        {
            string document3Path = @"C:\Users\Home\Desktop\ДИПЛОМ\Страховка.docx";
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordDocument = wordApp.Documents.Open(document3Path);
            wordApp.Visible = true;
        }
    }
}
