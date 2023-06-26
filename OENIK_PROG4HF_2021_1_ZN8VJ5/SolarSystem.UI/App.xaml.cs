namespace SolarSystem.UI
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;
    using SolarSystem.Data.Models;
    using SolarSystem.Logic;
    using SolarSystem.Repository;
    using SolarSystem.UI.BL;
    using SolarSystem.UI.UI;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// Sets up ServiceLocator.
        /// </summary>
        public App()
        {
            ServiceLocator.SetLocatorProvider(() => MyIOC.Instance);
            SolarSystemDbContext ctx = new SolarSystemDbContext();
            PlanetRepository planetRepo = new PlanetRepository(ctx);
            StarRepository starRepo = new StarRepository(ctx);

            // TODO...
            MyIOC.Instance.Register<IEditorService, EditorServiceViaWindow>();
            MyIOC.Instance.Register<IMessenger>(() => Messenger.Default);
            MyIOC.Instance.Register<ICatalog>(() => new Catalog(null, starRepo, planetRepo));
            MyIOC.Instance.Register<IPlanetLogic, VMPlanetLogic>();
        }
    }
}
