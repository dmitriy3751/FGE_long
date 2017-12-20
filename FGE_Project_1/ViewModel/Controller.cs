using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FGE_Project_1.Model;


namespace FGE_Project_1.ViewModel
{
    public class Controller
    {
        SaveAndOpen workImg;
        InkCanvas canvas;
        GlyphChanged glyph;
        Draw_Color draw;
        

        public Controller(InkCanvas canvas)
        {
            this.canvas = canvas;

            workImg = new SaveAndOpen(canvas);
            glyph = new GlyphChanged(canvas);
            draw = new Draw_Color(canvas);
        }

        public void OpenFile()
        {
            workImg.OpenImages();
        }

        public void SaveFile()
        {
            workImg.SaveImage();
        }

        public void ChangeWidth(double value)
        {
            glyph.GlyphSize(value);
        }

        public void ChangeBrush(int value)
        {
            glyph.BrushesSelect(value);
        }

        public void ClearAll()
        {
            glyph.Clear();
        }
        public void M_Move(Point point, double slider_value, int selected_brush, double point_x, double point_y)
        {
            draw.MouseMove(point, slider_value, selected_brush, point_x, point_y);
        }

        public void MLeftUp(double point_x, double point_y, int brush_index, double slider_value)
        {
            draw.MouseLeftButtonUp(point_x, point_y, brush_index, slider_value);
        }

        public void MLeftDown(Point startPoint, double thickness)
        {
            draw.MouseLeftButtonDown(startPoint, thickness);
        }

        public void border_color(Color color)
        {
            draw.Border_Color(color);
        }

        public void fill_color(Color color)
        {
            draw.FillColor(color);
        }

        public void brush_select(int value)
        {
            draw.BrushShapesCombo(value);
        }
    }
}
