using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CM_EA_20181124
{
    public class PageOneViewModel : PropertyChangedBase
    {
        private string _name;
        private readonly IEventAggregator _eventAggregator;


        public PageOneViewModel(IEventAggregator eventAggregator)
        {
            Name = "Witek";
            _eventAggregator = eventAggregator;

        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }       

        public void Send()
        {
            //Name = "Łukasz";
            MessageBox.Show(string.Format("Hello {0}!", Name));
            _eventAggregator.PublishOnUIThread(new EventContent { Message = Name, Ilosc = 5 });
        }

        public bool CanSend()
        {
            return true;
        }

    }
}
