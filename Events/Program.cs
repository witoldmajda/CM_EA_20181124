using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            AgendaManager amgr = new AgendaManager();
            SMSSender sms = new SMSSender();


            // rejestracja Eventu
            amgr.AddedAgenda += sms.OnAddedAgenda; // nasłuchiwanie
            amgr.AddedAgendaShorter += sms.OnAddedAgendaShorter;


            amgr.AddAgenda(new Agenda()
            {
                AgendaDate = DateTime.Now.AddDays(2),
                AgendaName = "Tytuł Filmu ...."
            }
                );
            

            Console.ReadKey();
        }
    }
}
