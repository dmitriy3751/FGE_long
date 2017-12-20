using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FGE_Project_1.UndoRedo.ViewModel
{
    // Интерфейс установки положений
    public interface IMementoDesigner
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }
}
