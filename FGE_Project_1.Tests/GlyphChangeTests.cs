using System;
using System.Drawing;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Ink;

using FGE_Project_1;
using FGE_Project_1.Model;
using FGE_Project_1.ViewModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FGE_Project_1.Tests
{
    using NUnit.Framework;
    //Результат Сообщение:	System.InvalidOperationException : Вызывающим потоком должен быть STA, поскольку этого требуют большинство компонентов UI.

    //[TestFixture]
    [TestClass]
    public class GlyphChangeTests1
    {

        // Тест ползунка ширины кисти
        //[Test]
        [TestMethod]
        public void SlideTest1()
        {

            InkCanvas MyCanvas = new InkCanvas();
            GlyphChanged glyph = new GlyphChanged(MyCanvas);

            var drawingAttributes = MyCanvas.DefaultDrawingAttributes;
            drawingAttributes.Width = 3; //ширина кисти
            drawingAttributes.Height = 4; //высота кисти

            Assert.That(drawingAttributes.Width, Is.EqualTo(3));
            Assert.That(drawingAttributes.Height, Is.EqualTo(4));
            for (int i = 1; i < 100; ++i)
            {
                glyph.GlyphSize(i);
                Assert.That(drawingAttributes.Width, Is.EqualTo(i));
                Assert.That(drawingAttributes.Height, Is.EqualTo(i));
            }
        }

        // Тест ползунка ширины кисти для ViewModel
        //[Test]
        [TestMethod]
        public void SlideTest2()
        {
            InkCanvas MyCanvas = new InkCanvas();
            Controller control = new Controller(MyCanvas);

            var drawingAttributes = MyCanvas.DefaultDrawingAttributes;
            drawingAttributes.Width = 3; //ширина кисти
            drawingAttributes.Height = 4; //высота кисти

            Assert.That(drawingAttributes.Width, Is.EqualTo(3));
            Assert.That(drawingAttributes.Height, Is.EqualTo(4));

            for (int i = 1; i < 12; ++i)
            {
                control.ChangeWidth(i);
                Assert.That(drawingAttributes.Width, Is.EqualTo(i));
                Assert.That(drawingAttributes.Height, Is.EqualTo(i));

            }
        }

        //Тест увеличения\уменьшения ширины глифов
        //[Test]
        [TestMethod]
        public void StrokeTest()
        {

            InkCanvas MyCanvas = new InkCanvas();
            GlyphChanged glyph = new GlyphChanged(MyCanvas);

            Assert.AreEqual(MyCanvas.GetSelectedStrokes().Count, 0);

        }


        //Тест выбора кисти
        //[Test]
        [TestMethod]
        public void BrushTest1()
        {

            InkCanvas MyCanvas = new InkCanvas();
            GlyphChanged glyph = new GlyphChanged(MyCanvas);

            //Изначальная кисть должна быть Ink и Ellipse
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.Ink);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTip, StylusTip.Ellipse);

            // При выборе различных кисте (от 0й до 4й) меняются значения СтилусТип и СтилусТипТрансформейшн
            glyph.BrushesSelect(0);
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.Ink);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTip, StylusTip.Ellipse);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTipTransform, new Matrix(1, 0, 0, 1, 0, 0));

            glyph.BrushesSelect(1);
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.Ink);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTip, StylusTip.Rectangle);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTipTransform, new Matrix(1, 0, 0, 1, 0, 0));

            glyph.BrushesSelect(2);
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.Ink);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTip, StylusTip.Rectangle);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTipTransform, new Matrix(1, 2, 3, 3, 0, 0));

            glyph.BrushesSelect(3);
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.Select);

            glyph.BrushesSelect(4);
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.None);
        }

        //Тест выбора кисти для ViewModel
        //[Test]
        [TestMethod]
        public void BrushTest2()
        {

            InkCanvas MyCanvas = new InkCanvas();
            Controller control = new Controller(MyCanvas);

            //Изначальная кисть должна быть Ink и Ellipse
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.Ink);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTip, StylusTip.Ellipse);

            // При выборе различных кисте (от 0й до 4й) меняются значения СтилусТип и СтилусТипТрансформейшн
            control.ChangeBrush(0);
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.Ink);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTip, StylusTip.Ellipse);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTipTransform, new Matrix(1, 0, 0, 1, 0, 0));

            control.ChangeBrush(1);
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.Ink);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTip, StylusTip.Rectangle);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTipTransform, new Matrix(1, 0, 0, 1, 0, 0));

            control.ChangeBrush(2);
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.Ink);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTip, StylusTip.Rectangle);
            Assert.AreEqual(MyCanvas.DefaultDrawingAttributes.StylusTipTransform, new Matrix(1, 2, 3, 3, 0, 0));

            control.ChangeBrush(3);
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.Select);

            control.ChangeBrush(4);
            Assert.AreEqual(MyCanvas.EditingMode, InkCanvasEditingMode.None);

        }
    }
}
