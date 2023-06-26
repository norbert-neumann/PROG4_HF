namespace SolarSystem.WpfClient
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    /// <summary>
    /// View model for the main window.
    /// </summary>
    public class MainVM : ViewModelBase
    {
        private IMainLogic logic;
        private ObservableCollection<VMPlanet> allPlanets;
        private VMPlanet selectedPlanet;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainVM"/> class.
        /// </summary>
        /// <param name="logic">Logic to operate on.</param>
        public MainVM(IMainLogic logic)
        {
            this.logic = logic; // TODO: IoC + Dependency Injection !!!

            this.AllPlanets = new ObservableCollection<VMPlanet>();

            this.LoadCmd = new RelayCommand(() =>
                   this.AllPlanets = new ObservableCollection<VMPlanet>(this.logic.ApiGetAllPlanets()));
            this.DelCmd = new RelayCommand(() => this.logic.ApiDeletePlanet(this.selectedPlanet));
            this.AddCmd = new RelayCommand(() => this.logic.EditPlanet(null, this.EditorFunc));
            this.ModCmd = new RelayCommand(() => this.logic.EditPlanet(this.selectedPlanet, this.EditorFunc));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainVM"/> class.
        /// </summary>
        public MainVM()
            : this(ServiceLocator.Current.GetInstance<IMainLogic>())
        {
        }

        /// <summary>
        /// Selected planet.
        /// </summary>
        public VMPlanet SelectedPlanet
        {
            get { return this.selectedPlanet; }
            set { this.Set(ref this.selectedPlanet, value); }
        }

        /// <summary>
        /// All planets in db.
        /// </summary>
        public ObservableCollection<VMPlanet> AllPlanets
        {
            get { return this.allPlanets; }
            set { this.Set(ref this.allPlanets, value); }
        }

        /// <summary>
        /// Editor function.
        /// </summary>
        public Func<VMPlanet, bool> EditorFunc { get; set; }

        /// <summary>
        /// Add relay command.
        /// </summary>
        public ICommand AddCmd { get; private set; }

        /// <summary>
        /// Delete relay command.
        /// </summary>
        public ICommand DelCmd { get; private set; }

        /// <summary>
        /// Update relay command.
        /// </summary>
        public ICommand ModCmd { get; private set; }

        /// <summary>
        /// Load relay command.
        /// </summary>
        public ICommand LoadCmd { get; private set; }
    }
}
