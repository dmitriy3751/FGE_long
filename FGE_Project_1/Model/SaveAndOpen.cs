using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FGE_Project_1.Model
{
    public class SaveAndOpen
    {
        InkCanvas MyCanvas;
        public SaveAndOpen(InkCanvas MyCanvas)
        {
            this.MyCanvas = MyCanvas;
        }

        // Функция сохранения изображения c использованием класса SaveFileDialog
        public void SaveImage() 
        {
            Microsoft.Win32.SaveFileDialog saveimg = new Microsoft.Win32.SaveFileDialog();
            saveimg.DefaultExt = ".BMP";  // Задание определенных свойств класса (Расширений)
            saveimg.Filter = "Image (.BMP)|*.BMP";      
            if (saveimg.ShowDialog() == true)   // Если окно открылось
            {
                Thickness margin = MyCanvas.Margin;
                MyCanvas.Margin = new Thickness(0);
                RenderTargetBitmap rtb = CanvasToBitmap();     // Получение изображения в растровом формате
                BitmapEncoder bmpEncoder = new BmpBitmapEncoder();  // Определяем кодировщик, для кодирования изображения
                bmpEncoder.Frames.Add(BitmapFrame.Create(rtb));     // Задаем фрейм для изображения
                try
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(); // Создаем поток в память.
                    bmpEncoder.Save(ms);         // Кодируем изображение в наш поток
                    ms.Close();
                    System.IO.File.WriteAllBytes(saveimg.FileName, ms.ToArray()); // Создаем файл, записываем в него масив байтов и закрываем
                }
                catch (Exception ex)    // Обработка исключений
                {
                    System.Windows.MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                MyCanvas.Margin = margin;
            }
        }

        RenderTargetBitmap CanvasToBitmap()
        {
            Size size = new Size(MyCanvas.ActualWidth, MyCanvas.ActualHeight);
            MyCanvas.Measure(size);
            Rect rect = new Rect(size);   // Получаем ширину и высоту нашего будущего изображения(квадрата)
            MyCanvas.Arrange(rect);
            int dpi = 96;
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)size.Width, (int)size.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default); // через  обьект класса RenderTargetBitmap будем преобразовывать canvas в растровое изображение
            rtb.Render(MyCanvas);
            return rtb;
        }

        // Функция открытия и добавления изображения с использованием класса OpenFileDialog
        public void OpenImages() 
        {
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            f.Filter = "BMP(*.BMP)|*.BMP";  // Задание определенных свойств класса
            f.RestoreDirectory = true;
            if (f.ShowDialog() == true) // Если окно открылось
            {
                ImageBrush img = new ImageBrush();
                img.ImageSource = new BitmapImage(new Uri(f.FileName, UriKind.Relative)); 
                if (MyCanvas.Children.Count > 0) MyCanvas.Children.Clear(); // Удаляем все с рабочего поля
                this.MyCanvas.Strokes.Clear();
                MyCanvas.Background = img;  // Помещаем изображение
            }
        }

    }
}
