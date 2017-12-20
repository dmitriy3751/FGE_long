using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FGE_Project_1.UndoRedo.ViewModel;

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
           
        // Реализация Undo. 
        public void Undo()
        {
            if (_undoStates.Count > 1)  // Если кол-во состояний не ноль 
            {
                _undoStates.Pop();  // Вытаскиваем это состояние со стэка
                var lastState = _undoStates.Peek(); // Получаем предпоследнее состояние
                _designer.SetMemento(lastState); // Переключаемся на предпоследнее состояние lastState
            }
        }
    }
}
