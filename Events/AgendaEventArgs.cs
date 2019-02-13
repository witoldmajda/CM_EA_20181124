using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class AgendaEventArgs : EventArgs
    {
        public Agenda Agenda { get; set; } // właściwość przesyłana która jest obiektem
    }
}
