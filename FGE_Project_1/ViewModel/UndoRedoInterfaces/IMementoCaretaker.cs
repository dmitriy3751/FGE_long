using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FGE_Project_1.UndoRedo.ViewModel
{   
    // Интерфейс создания
    public interface IMementoCaretaker
    {
        void Initialize();
        void StoreState();
        void Undo();
    }
}
