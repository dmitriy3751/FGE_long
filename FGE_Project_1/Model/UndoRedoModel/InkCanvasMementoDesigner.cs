using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FGE_Project_1.UndoRedo.ViewModel;
using System.Windows.Controls;
using System.Windows.Ink;

namespace FGE_Project_1.UndoRedo
{
    public class InkCanvasMementoDesigner : IMementoDesigner
    {
        readonly InkCanvas _inkCanvas;

        public InkCanvasMementoDesigner(InkCanvas inkCanvas)
        {
            _inkCanvas = inkCanvas;
        }

        // Создание массива Strokes исходного inkCanvas'а
        public IMemento CreateMemento()
        {
            var copy = _inkCanvas.Strokes.ToArray();
            return new StrokesMemento() { State = copy };
        }

        public void SetMemento(IMemento memento)
        {
            _inkCanvas.Strokes = new StrokeCollection((Stroke[])memento.State);
        }
    }
}