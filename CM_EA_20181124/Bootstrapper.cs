using Caliburn.Micro;
using CM_EA_20181124.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CM_EA_20181124
{
    public class Bootstrapper: BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        protected override void Configure()
        {
            _container.Singleton<IEventAggregator, EventAggregator>();
        }

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ZadaniaViewModel>();
            //DisplayRootViewFor<ShellViewModel>();
        }
    }
}
