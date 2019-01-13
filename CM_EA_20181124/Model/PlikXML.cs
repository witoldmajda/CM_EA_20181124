using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CM_EA_20181124.Model
{
    public static class PlikXML
    {
        public static void Zapisz(string sciezkaPliku, Zadania zadania)
        {
            try
            {
                XDocument xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Data zapisania: " + DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                    new XElement("Zadania",
                    from Zadanie zadanie in zadania
                    select new XElement("Zadanie",
                    new XElement("Opis", zadanie.Opis),
                    new XElement("DataUtworzenia", zadanie.DataUtworzenia),
                    new XElement("PlanowanaDataRealizacji", zadanie.PlanowanyTerminRealizacji.ToString(CultureInfo.InvariantCulture)),
                    new XElement("Priorytet", (byte)zadanie.Priorytet),
                    new XElement("Czy zrealizowane", zadanie.CzyZrealizowane.ToString()))
                    )
                    );
                xml.Save(sciezkaPliku);
            }
            catch (Exception exc)
            {

                throw new Exception("Błąd przy zapisie danych do pliku XML", exc);
            }
        }

        public static Zadania Czytaj(string sciezkaPliku)
        {
            try
            {
                XDocument xml = XDocument.Load(sciezkaPliku);
                IEnumerable<Zadanie> dane =
                    from zadanie in xml.Root.Descendants("Zadanie")
                    select new Zadanie(
                        zadanie.Element("Opis").Value,
                        DateTime.Parse(zadanie.Element("DataUtworzenia").Value),
                        DateTime.Parse(zadanie.Element("PlanowanaDataRealizacji").Value),
                        (PriorytetZadania)byte.Parse(zadanie.Element("Priorytet").Value),
                        bool.Parse(zadanie.Element("CzyZrealizowane").Value));
                Zadania zadania = new Zadania();
                foreach (Zadanie zadanie in dane) zadania.DodajZadanie(zadanie);
                return zadania;
            }
            catch (Exception exc)
            {
                throw new Exception("Błąd przy odczycie danych z pliku XML", exc);
            }
        }
    }
}
