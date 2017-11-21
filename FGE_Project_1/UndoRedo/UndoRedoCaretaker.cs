using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FGE_Project_1.UndoRedo.Model;

namespace FGE_Project_1.UndoRedo
{
    public class UndoRedoCaretaker : IMementoCaretaker
    {
        private readonly Stack<IMemento> _undoStates = new Stack<IMemento>();
        private readonly IMementoDesigner _designer;

        public UndoRedoCaretaker(IMementoDesigner designer)
        {
            _designer = designer;
        }

        public void Initialize()
        {
            _undoStates.Clear();
            StoreState();
        }

        public void StoreState()
        {
            var memento = _designer.CreateMemento();
            _undoStates.Push(memento);
        }

        public void Undo()
        {
            if (_undoStates.Count > 1)
            {
                _undoStates.Pop();
                var lastState = _undoStates.Peek();
                _designer.SetMemento(lastState);
            }
        }
    }
}
