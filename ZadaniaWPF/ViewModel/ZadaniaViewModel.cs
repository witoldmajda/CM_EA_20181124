﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Windows.Input;
using System.Windows;

namespace ZadaniaWPF.ViewModel
{
    public class ZadaniaViewModel
    {
        private const string SciezkaPlikuXml = "zadania.xml";

        //przechowywanie dwóch kolekcji
        private Model.Zadania model;

        public ObservableCollection<ZadanieViewModel> ListaZadan { get; } = new ObservableCollection<ZadanieViewModel>();

        private void KopiujZadania()
        {
            ListaZadan.CollectionChanged -= SynchonizacjaModelu;
            ListaZadan.Clear();
            foreach (Model.Zadanie zadanie in model)
                ListaZadan.Add(new ZadanieViewModel(zadanie));
            ListaZadan.CollectionChanged += SynchonizacjaModelu;
        }


        public ZadaniaViewModel()
        {


            if (System.IO.File.Exists(SciezkaPlikuXml))
            {
                model = Model.PlikXML.Czytaj(SciezkaPlikuXml);
            }

            else
            {
                model = new Model.Zadania();

                //testy poczatek
                model.DodajZadanie(new Model.Zadanie("Pierwsze", DateTime.Now, DateTime.Now.AddDays(2), Model.PriorytetZadania.Wazne, false));
                model.DodajZadanie(new Model.Zadanie("Drugie", DateTime.Now, DateTime.Now.AddDays(2), Model.PriorytetZadania.Wazne, false));
                model.DodajZadanie(new Model.Zadanie("Trzecie", DateTime.Now, DateTime.Now.AddDays(1), Model.PriorytetZadania.MniejWazne, false));
                model.DodajZadanie(new Model.Zadanie("Czwarte", DateTime.Now, DateTime.Now.AddDays(3), Model.PriorytetZadania.Krytyczne, false));
                model.DodajZadanie(new Model.Zadanie("Piąte", DateTime.Now, new DateTime(2015, 03, 15, 1, 2, 3), Model.PriorytetZadania.Krytyczne, true));
                model.DodajZadanie(new Model.Zadanie("Szóste", DateTime.Now, new DateTime(2015, 03, 14, 1, 2, 3), Model.PriorytetZadania.Krytyczne, true));
                ////testy koniec
            }
            KopiujZadania();
        }



        private void SynchonizacjaModelu(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ZadanieViewModel noweZadanie = (ZadanieViewModel)e.NewItems[0];
                    if (noweZadanie != null)
                        model.DodajZadanie(noweZadanie.GetModel());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ZadanieViewModel usuwaneZadanie = (ZadanieViewModel)e.OldItems[0];
                    if (usuwaneZadanie != null)
                        model.UsunZadanie(usuwaneZadanie.GetModel());
                    break;
            }
        }

        private ICommand zapiszCommand;

        public ICommand Zapisz
        {
            get
            {
                if (zapiszCommand == null)
                    zapiszCommand = new RelayCommand(
                        argument =>
                        {
                            Model.PlikXML.Zapisz(SciezkaPlikuXml, model);
                        }
                        );
                return zapiszCommand;
            }

        }

        private ICommand usunZadanie;

        public ICommand UsunZadanie
        {
            get
            {
                if(usunZadanie == null)
                {
                    usunZadanie = new RelayCommand(
                        o =>
                        {
                            int indeksZadania = (int)o;
                            ZadanieViewModel zadanie = ListaZadan[indeksZadania];
                            if (!zadanie.CzyZrealizowane)
                            {
                                MessageBoxResult mbr = MessageBox.Show("Czy jestes pewien, że chcesz usunąć niezrealizowane zadanie?",
                                    "Zadania WPF", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                                if (mbr == MessageBoxResult.No) return;
                            }
                            ListaZadan.Remove(zadanie);
                        },
                        o =>
                        {
                            if (o == null) return false;
                            int indeksZadania = (int)o;
                            return indeksZadania >= 0;
                        }
                        );
                    
                }

                return usunZadanie;
            }            
        }

        private ICommand dodajZadanie;

        public ICommand DodajZadanie
        {
            get
            {
                if(dodajZadanie == null)
                {
                    dodajZadanie = new RelayCommand(
                        o => 
                        {
                            ZadanieViewModel zadanie = o as ZadanieViewModel;
                            if(zadanie != null)
                            {
                                ListaZadan.Add(zadanie);
                            }
                        },
                        o =>
                        {
                            return (o as ZadanieViewModel) != null;
                        }
                        );
                }
                return dodajZadanie;
            }
            
        }

        private ICommand sortujZadania;

        public ICommand SortujZadania
        {
            get
            {
                if(sortujZadania == null)
                
                    sortujZadania = new RelayCommand(
                        o =>
                        {
                            bool porownywaniePriorytetowCzyPlanowanychTerminowRealizacji = bool.Parse((string)o);
                            model.SortujZadania(porownywaniePriorytetowCzyPlanowanychTerminowRealizacji);
                            KopiujZadania();
                        }
                        );
                    return sortujZadania;
                                
            }            
        }


    }
}
