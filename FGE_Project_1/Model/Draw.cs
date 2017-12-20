using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Ink;
using System.Windows.Input;

namespace FGE_Project_1.Model
{

    class Draw_Color
    {
        InkCanvas MyCanvas;
        double m_StartX = 0;
        double m_StartY = 0;
        bool m_DrawingStarted = false;
        Point m_startingPoint;
        Polyline m_line;
        bool m_moving = false;
        Shape m_curMoving;
        Color borderColor = Colors.Black;
        Color fillColor = Colors.White;
        Shape m_shape;
        int shapeIndex = 0;

        public Draw_Color(InkCanvas MyCanvas)
        {
            this.MyCanvas = MyCanvas;
        }

        public void MouseMove(Point point, double slider_value, int selected_brush, double point_x, double point_y)
        {
            if (shapeIndex == 0)
            {
                m_startingPoint = point;
                m_line = new Polyline();
                m_line.Stroke = new SolidColorBrush(borderColor);
                m_line.StrokeThickness = slider_value;

                MyCanvas.Children.Add(m_line);
            }

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (!m_DrawingStarted && (shapeIndex == 1 || shapeIndex == 2 || shapeIndex == 3) && selected_brush == 4)
                {
                    m_StartX = point_x;
                    m_StartY = point_y;
                    m_DrawingStarted = true;
                }
                else if (shapeIndex == 0)
                {
                    Point cur = point;
                    if (m_startingPoint != cur)
                    {
                        m_line.Points.Add(cur);
                    }
                }
            }
        }

        public void MouseLeftButtonUp(double point_x, double point_y, int brush_index, double slider_value)
        {
            if (m_DrawingStarted == false)
                return;

            m_DrawingStarted = false;
            double endX = point_x;
            double endY = point_y;
            if (m_moving)
            {
                m_moving = false;
            }
            else if ((shapeIndex == 2 || shapeIndex == 3) && brush_index == 4)
            {
                if (shapeIndex == 3)
                {
                    m_shape = new Rectangle();
                }
                else if (shapeIndex == 2)
                {
                    m_shape = new Ellipse();
                }
                m_shape.Stroke = new SolidColorBrush(borderColor);
                m_shape.StrokeThickness = slider_value;
                m_shape.Fill = new SolidColorBrush(fillColor);

                if (endX > m_StartX)
                {
                    m_shape.Width = endX - m_StartX;
                    InkCanvas.SetLeft(m_shape, m_StartX);
                }
                else
                {
                    m_shape.Width = m_StartX - endX;
                    InkCanvas.SetLeft(m_shape, endX);
                }
                if (endY > m_StartY)
                {
                    m_shape.Height = endY - m_StartY;
                    InkCanvas.SetTop(m_shape, m_StartY);
                }
                else
                {
                    m_shape.Height = m_StartY - endY;
                    InkCanvas.SetTop(m_shape, endY);
                }

                MyCanvas.Children.Add(m_shape);
            }
            else if (shapeIndex == 1 && brush_index == 4)
            {
                Line line = new Line();
                line.X1 = m_StartX;
                line.Y1 = m_StartY;
                line.X2 = endX;
                line.Y2 = endY;
                line.StrokeThickness = slider_value;
                MyCanvas.Children.Add(line);
                line.Stroke = new SolidColorBrush(borderColor);
                line.Fill = new SolidColorBrush(borderColor);
            }
        }

        public void MouseLeftButtonDown(Point startPoint, double thickness)
        {
            if (shapeIndex == 0)
            {
                m_startingPoint = startPoint;
                m_line = new Polyline();
                m_line.Stroke = new SolidColorBrush(borderColor);
                m_line.StrokeThickness = thickness;

                MyCanvas.Children.Add(m_line);
            }
        }

        public void Border_Color(Color color)
        {
            borderColor = color;
            MyCanvas.DefaultDrawingAttributes.Color = color;
        }

        public void FillColor(Color color)
        {
            fillColor = color;
        }

        public void BrushShapesCombo(int selected)
        {
            if (MyCanvas == null) return;

            switch (selected)
            {
                case 0:
                    shapeIndex = 0;//свободная кисть
                    break;
                case 1:
                    shapeIndex = 1;//прямая линия
                    break;
                case 2:
                    shapeIndex = 2;//эллипс
                    break;
                case 3:
                    shapeIndex = 3;//прямоугольник
                    break;

            }
        }
    }
}
