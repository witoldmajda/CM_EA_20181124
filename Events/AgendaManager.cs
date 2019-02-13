using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Events
{
    public class AgendaManager
    {

        public delegate void AddedAgendaEventHandler(object o, AgendaEventArgs e); //rejestracja delegata

        public event AddedAgendaEventHandler AddedAgenda; // twożenie eventu

        //skrcona wersja Eventu
        public event EventHandler<AgendaEventArgs> AddedAgendaShorter;



        //Twożenie Publishera Eventu

        protected virtual void OnAddedAgenda(Agenda newAgenda)
        {
            //if (AddedAgenda != null) //sprawdzamy czy ktokolwiek się zarejestrował
            //    AddedAgenda(this, EventArgs.Empty); // jeśłi ktoś się zarejestrował to podnosimy Event

            if (AddedAgenda != null && newAgenda != null)
                AddedAgenda(this, new AgendaEventArgs() { Agenda = newAgenda }); //argumentami jest obiekt który wysyłamy
        }

        //Publisher do skróconej wersji

        protected virtual void OnAddedAgendaShorter(Agenda newAgenda)
        {            

            if (AddedAgendaShorter != null && newAgenda != null)
                AddedAgendaShorter(this, new AgendaEventArgs() { Agenda = newAgenda }); //argumentami jest obiekt który wysyłamy
        }




        public void AddAgenda(Agenda newAgenda)
        {
            Console.WriteLine("AddAgenda Zaczynam Dodawać.....");
            Thread.Sleep(3000);
            // Miejsce na poinformowanie SMSSender
            OnAddedAgenda(newAgenda); //uruchomienie Publishera

            OnAddedAgendaShorter(newAgenda);

            Console.WriteLine("AddAgenda Skończyłem Dodawać.....");
        }
    }
}
