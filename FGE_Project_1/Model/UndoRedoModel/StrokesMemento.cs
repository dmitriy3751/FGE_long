using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FGE_Project_1.UndoRedo.ViewModel;

namespace FGE_Project_1.UndoRedo
{
    public class StrokesMemento : IMemento
    {
        public object State { get; set; }
    }
}
