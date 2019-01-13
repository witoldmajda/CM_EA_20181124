using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZadaniaWPF.ViewModel
{
    public class ZadanieViewModel :INotifyPropertyChanged
    {
        private Model.Zadanie model;

        public ZadanieViewModel(string opis, DateTime dataUtworzenia, DateTime planowanyTerminRealizacji, Model.PriorytetZadania priorytetZadania, bool czyZrealizowane)
        {
            model = new Model.Zadanie(opis, dataUtworzenia, planowanyTerminRealizacji, priorytetZadania, czyZrealizowane);
        }

        public ZadanieViewModel(Model.Zadanie zadanie)
        {
            this.model = zadanie;
        }

        public string Opis
        {
            get { return model.Opis; }
        }

        public Model.PriorytetZadania Priorytet
        {
            get
            {
                return model.Priorytet;
            }
        }

        public DateTime DataUtworzenia
        {
            get
            {
                return model.DataUtworzenia;
            }
        }

        public DateTime PlanowanyTerminRealizacji
        {
            get
            {
                return model.PlanowanyTerminRealizacji;
            }
        }

        public bool CzyZrealizowane
        {
            get
            {
                return model.CzyZrealizowane;
            }
        }

        public bool CzyZadaniePozostajeNieZrealizowanePoZadanymTerminie
        {
            get
            {
                return !CzyZrealizowane && DateTime.Now > PlanowanyTerminRealizacji;
            }
        }

        public Model.Zadanie GetModel()
        {
            return model;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(params string[] nazwyWlasnosci)
        {
            if (PropertyChanged != null)
            {
                foreach (string nazwaWlasnosci in nazwyWlasnosci)
                    PropertyChanged(this, new PropertyChangedEventArgs(nazwaWlasnosci));
            }
        }

        ICommand oznaczJakoZrealizowane;

        public ICommand OznaczJakoZrealizowane
        {
            get
            {
                if (oznaczJakoZrealizowane == null)
                    oznaczJakoZrealizowane = new RelayCommand(
                        o =>
                        {
                            model.CzyZrealizowane = true;
                            OnPropertyChanged("CzyZrealizowane", "CzyZadaniePozostajeNieZrealizowanePoZadanymTerminie");
                        },
                        o =>
                        {
                            return !model.CzyZrealizowane;
                        }
                        );
                return oznaczJakoZrealizowane;
            }
        }

        ICommand oznaczJakoNierealizowane = null;

        public ICommand OznaczJakoNiezrealizowane
        {
            get
            {
                if (oznaczJakoNierealizowane == null)
                    oznaczJakoNierealizowane = new RelayCommand(
                        o =>
                        {
                            model.CzyZrealizowane = false;
                            OnPropertyChanged("CzyZrealizowane", "CzyZadaniePozostajeNieZrealizowanePoZadanymTerminie");
                        },
                        o =>
                        {
                            return model.CzyZrealizowane;
                        }
                        );
                return oznaczJakoNierealizowane;
            }
        }
    }
}
