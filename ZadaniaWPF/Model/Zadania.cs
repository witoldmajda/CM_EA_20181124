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
