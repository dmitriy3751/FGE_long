using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using FGE_Project_1.UndoRedo.ViewModel;
using FGE_Project_1.UndoRedo;
using FGE_Project_1.Model;
using FGE_Project_1.ViewModel;

namespace FGE_Project_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMementoCaretaker _undoRedoCaretaker;
        Controller control;

        public MainWindow()
        {
            InitializeComponent();


            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo, OnExecutedCommands));
            MyCanvas.MouseLeftButtonUp += new MouseButtonEventHandler(MyCanvas_MouseLeftButtonUp);
            MyCanvas.MouseMove += new MouseEventHandler(MyCanvas_MouseMove);
            MyCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(MyCanvas_MouseLeftButtonDown);

            var mementoDesigner = new InkCanvasMementoDesigner(MyCanvas);
            _undoRedoCaretaker = new UndoRedoCaretaker(mementoDesigner);
            _undoRedoCaretaker.Initialize();

            control = new Controller(MyCanvas);
        }

        //обработчик движения мыши
        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //if (MyCanvas == null) return;
            control.M_Move(e.GetPosition(MyCanvas), BrushSlider.Value, Brushes.SelectedIndex, e.GetPosition(MyCanvas).X, e.GetPosition(MyCanvas).Y);
        }



        //обработчик отжатия левой кнопки мыши
        private void MyCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _undoRedoCaretaker.StoreState();
            }

            //if (MyCanvas == null) return;
            control.MLeftUp(e.GetPosition(MyCanvas).X, e.GetPosition(MyCanvas).Y, Brushes.SelectedIndex, BrushSlider.Value);
        }

        //обработчик нажатия левой кнопки мыши
        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (MyCanvas == null) return;
            control.MLeftDown(e.GetPosition(MyCanvas), BrushSlider.Value);
        }


        //выбор цвета границ фигур
        private void BorderColor_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (MyCanvas == null) return;
            //Color borderColor = (Color)(BorderColorCombo.SelectedItem as PropertyInfo).GetValue(null, null);
            //// ReSharper disable once PossibleNullReferenceException
            control.border_color(BorderColorCombo.SelectedColor.Value);
        }

        //выбор цвета заливки фигур (прямоугольник, эллипс)
        private void FillColor_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (MyCanvas == null) return;
            control.fill_color(FillColorCombo.SelectedColor.Value);
        }


        //обработчик MenuItem_Click, который будет срабатывать при возникновении события ValueChanged - изменении значения слайдера и изменять толщину кисти в соответсвтии с ним

        private void SizeSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (MyCanvas == null) return;
            control.ChangeWidth(BrushSlider.Value); 
        }
        
        // изменение кисти
        private void Brushes_Select(object sender, SelectionChangedEventArgs e)
        {
            if (MyCanvas == null) return;
            control.ChangeBrush(Brushes.SelectedIndex);
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            control.ClearAll();
        }



        private void BrushShapesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MyCanvas == null) return;
            control.brush_select(BrushShapesCombo.SelectedIndex);
        }

       //Сохранение изображения в формате BMP
       private void Button_Click_Save(object sender, RoutedEventArgs e)
       {
           control.SaveFile();
       }

       //Загрузка изображения в формате BMP.
       private void Button_Click_Open(object sender, RoutedEventArgs e)
       {
           control.OpenFile();
       }

       //Обработчик Undo
       private void OnExecutedCommands(object sender, ExecutedRoutedEventArgs e)
       {
           if (e.Command == ApplicationCommands.Undo)
           {
               _undoRedoCaretaker.Undo();
           }
       }


    }

}
