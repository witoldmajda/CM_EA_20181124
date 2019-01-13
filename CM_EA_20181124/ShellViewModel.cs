using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CM_EA_20181124
{
    public class ShellViewModel : Conductor<object>
    {
        public PageOneViewModel PageOne { get; private set; }
        public PageTwoViewModel PageTwo { get; private set; }

        public ShellViewModel()
        {
            IEventAggregator eventAggregator = new EventAggregator();
            PageOne = new PageOneViewModel(eventAggregator);
            PageTwo = new PageTwoViewModel(eventAggregator);
        }
       
    }
}
