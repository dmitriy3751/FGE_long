using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FGE_Project_1.UndoRedo.Model
{
    public interface IMementoDesigner
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }
}
