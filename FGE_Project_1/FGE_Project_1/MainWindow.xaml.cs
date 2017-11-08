using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

using System.Diagnostics;
using System.IO;
namespace FGE_Project_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        //обработчик SizeSlider_ValueChanged, который будет срабатывать при возникновении события ValueChanged - изменении значения слайдера и изменять толщину кисти в соответсвтии с ним
       private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           // проверка, было бы создано поле для рисования
           if (Canvas == null) 
               return;
           
            var drawingAttributes = Canvas.DefaultDrawingAttributes; //определяет внешний вид пера (выбираются стандартные настройки)
            drawingAttributes.Width = BrushSlider.Value; //ширина кисти
            drawingAttributes.Height = BrushSlider.Value; //высота кисти
        }


        //Добавлена возможность сохранения изображения в формате BMP 
        //Изображение сохраняется с именем "result.bmp" в исходной папке проекта
        //( нужно разрешить выбрать путь сохранения + исправить баг сохранения тулбара!!!)
       private void Button_Click(object sender, RoutedEventArgs e)
       {
           var bmp = new RenderTargetBitmap(
               (int)cs.ActualWidth, (int)cs.ActualHeight, 96, 96, PixelFormats.Default);
           bmp.Render(cs);
           var enc = new PngBitmapEncoder();
           enc.Frames.Add(BitmapFrame.Create(bmp));
           using (var s = File.Create("result.bmp"))
               enc.Save(s);
       }
    }

}
