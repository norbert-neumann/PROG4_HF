[assembly: System.CLSCompliant(false)]

namespace SolarSystem.WpfClient
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Messaging;
    using SolarSystem.Data.Models;
    using SolarSystem.Logic;
    using SolarSystem.Repository;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            ServiceLocator.SetLocatorProvider(() => MyIOC.Instance);
            MyIOC.Instance.Register<IMessenger>(() => Messenger.Default);
            MyIOC.Instance.Register<IMainLogic, MainLogic>();
        }
    }
}
