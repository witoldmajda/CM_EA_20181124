using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadaniaWPF.Model
{
    public class Zadania : IEnumerable<Zadanie> // implementacja interfejsu aby instancje klasy Zadania mogły działać jak kolekcje
    {
        private List<Zadanie> listaZadan = new List<Zadanie>();

        private Comparison<Zadanie> porownywaniePriorytetow = new Comparison<Zadanie>(
            (Zadanie zadanie1, Zadanie zadanie2) =>
            {
                int wynik = -zadanie1.Priorytet.CompareTo(zadanie2.Priorytet);
                if(wynik == 0) wynik = zadanie1.PlanowanyTerminRealizacji.CompareTo(zadanie2.PlanowanyTerminRealizacji);
                return wynik;
            }
        );

        private Comparison<Zadanie> porownywaniePlanowanychTerminowRealizacji = new Comparison<Zadanie>(
            (Zadanie zadanie1, Zadanie zadanie2) =>
            {
                int wynik = zadanie1.PlanowanyTerminRealizacji.CompareTo(zadanie2.PlanowanyTerminRealizacji);
                if (wynik == 0) wynik = -zadanie1.Priorytet.CompareTo(zadanie2.Priorytet);
                return wynik;
            }
            );

        public void SortujZadania(
            bool porownywaniePriorytetowCzyPlanowanychTerminowRealizacji)
        {
            if (porownywaniePriorytetowCzyPlanowanychTerminowRealizacji)
                listaZadan.Sort(porownywaniePriorytetow);
            else
                listaZadan.Sort(porownywaniePlanowanychTerminowRealizacji);
        }

        public void DodajZadanie(Zadanie zadanie)
        {
            listaZadan.Add(zadanie);
        }

        public bool UsunZadanie(Zadanie zadanie)
        {
            return listaZadan.Remove(zadanie);
        }

        public IEnumerator<Zadanie> GetEnumerator() // implementacja metody interfejsu aby miec dostep do kolekcji z pola listaZadan udostepniajac zdefiniowany w niej obiekt
        {
            return listaZadan.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() // implementacja metody interfejsu aby miec dostep do kolekcji z pola listaZadan udostepniajac zdefiniowany w niej obiekt
        {
            return (IEnumerator)this.GetEnumerator();
        }

        public int LiczbaZadan
        {
            get
            {
                return listaZadan.Count();
            }
        }

        public Zadanie this[int indeks]
        {
            get
            {
                return listaZadan[indeks];
            }

        }

       

    

    }
}
