
using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Ink;

namespace FGE_Project_1.Model
{
    
    public class GlyphChanged
    {

        InkCanvas MyCanvas;
        
        public GlyphChanged(InkCanvas MyCanvas)
        {
            this.MyCanvas = MyCanvas;
        }

        //обработчик, который будет срабатывать при возникновении события ValueChanged - изменении значения слайдера и изменять толщину кисти в соответсвтии с ним

        public void GlyphSize(double value)
        {

            var drawingAttributes = MyCanvas.DefaultDrawingAttributes; //определяет внешний вид пера (выбираются стандартные настройки)
            drawingAttributes.Width = value; //ширина кисти
            drawingAttributes.Height = value; //высота кисти

            // изменение ширины выделенных глифов
            if (this.MyCanvas.GetSelectedStrokes().Count > 0)
            {
                foreach (Stroke stroke in MyCanvas.GetSelectedStrokes())
                {
                    stroke.DrawingAttributes.Width = value;
                    stroke.DrawingAttributes.Height = value;
                }
            }
        }

        public void BrushesSelect(int SelectedIndex)
        {

            //Выбор способа рисовки\удаления\изменения
            switch (SelectedIndex)
            {
                case 0:
                    MyCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    MyCanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
                    MyCanvas.DefaultDrawingAttributes.StylusTipTransform = new Matrix(1, 0, 0, 1, 0, 0);
                    break;
                case 1:
                    MyCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    MyCanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Rectangle;
                    MyCanvas.DefaultDrawingAttributes.StylusTipTransform = new Matrix(1, 0, 0, 1, 0, 0);
                    break;
                case 2:
                    MyCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    MyCanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Rectangle;
                    MyCanvas.DefaultDrawingAttributes.StylusTipTransform = new Matrix(1, 2, 3, 3, 0, 0);
                    break;
                case 3:
                    MyCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
                case 4:
                    MyCanvas.EditingMode = InkCanvasEditingMode.None;
                    break;
            }
        }

            public void Clear()
            {
                if (MyCanvas.Children.Count > 0) MyCanvas.Children.Clear();
                MyCanvas.Strokes.Clear();
            }



        

    }
}
