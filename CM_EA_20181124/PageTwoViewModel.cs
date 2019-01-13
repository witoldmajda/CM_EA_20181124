using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM_EA_20181124
{
    public class PageTwoViewModel : PropertyChangedBase, IHandle<EventContent>
    {
        private string _message;
        private int _ilosc;
        private readonly IEventAggregator _eventAggregator;
        public PageTwoViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            Message = "Przyslana wiadomosc";
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public int Ilosc
        {
            get
            {
                return _ilosc;
            }
            set
            {
                _ilosc = value;
                NotifyOfPropertyChange(() => Ilosc);
            }
        }

        public void Handle(EventContent message)
        {
            Message = message.Message;
            Ilosc = message.Ilosc;
        }
    }
}
