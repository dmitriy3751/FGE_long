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
using FGE_Project_1.UndoRedo.Model;
using FGE_Project_1.UndoRedo;

namespace FGE_Project_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMementoCaretaker _undoRedoCaretaker;
        //Функционал Undo ( реализован ) и Redo ( в доработке ) реализован на основе 
        //шаблона IMemento.

        public MainWindow()
        {
            InitializeComponent();
            BrushColorCombo.ItemsSource = typeof(Colors).GetProperties();
            PropertyInfo[] colors = BrushColorCombo.ItemsSource.Cast<PropertyInfo>().ToArray();
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i].Name == "Black")
                {
                    BrushColorCombo.SelectedIndex = i;
                    break;
                }
            }

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo, OnExecutedCommands));
            Canvas.MouseUp += new MouseButtonEventHandler(Canvas_MouseUp);

            var mementoDesigner = new InkCanvasMementoDesigner(Canvas);
            _undoRedoCaretaker = new UndoRedoCaretaker(mementoDesigner);
            _undoRedoCaretaker.Initialize();
        }

        //Заполнение
        void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _undoRedoCaretaker.StoreState();
            }
        }

        //Обработчик Undo
        private void OnExecutedCommands(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Undo)
            {
                _undoRedoCaretaker.Undo();
            }
        }

        private void BrushColorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color selectedColor = (Color)(BrushColorCombo.SelectedItem as PropertyInfo).GetValue(null, null);
            Canvas.DefaultDrawingAttributes.Color = selectedColor;
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
           Microsoft.Win32.SaveFileDialog saveimg = new Microsoft.Win32.SaveFileDialog();
           Canvas can = new Canvas();
           saveimg.DefaultExt = ".BMP";
           saveimg.Filter = "Image (.BMP)|*.BMP";
           saveimg.ShowDialog();

           var bmp = new RenderTargetBitmap(
              (int)cs.ActualWidth, (int)cs.ActualHeight, 96, 96, PixelFormats.Default);
           bmp.Render(cs);
           var enc = new PngBitmapEncoder();
           enc.Frames.Add(BitmapFrame.Create(bmp));
           using (FileStream file = File.Create(saveimg.FileName))
               enc.Save(file);
       }

       
       private void Button_Click_Open(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            f.Filter = "BMP(*.BMP)|*.BMP";
            if (f.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(f.FileName));
            }
        }

       private void BrushShapesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {
           if (Canvas == null) return;

           switch (BrushShapesCombo.SelectedIndex)
           {
               case 0:
                   //Canvas.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
                   break;
               case 1:
                   //Canvas.DefaultDrawingAttributes.StylusTip = StylusTip.Rectangle;
                   break;
           }
       }

    }

}
