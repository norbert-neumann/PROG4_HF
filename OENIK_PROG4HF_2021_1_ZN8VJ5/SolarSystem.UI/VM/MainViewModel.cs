namespace SolarSystem.UI.VM
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
    using SolarSystem.UI.BL;
    using SolarSystem.UI.Data;

    /// <summary>
    /// VM for the main window.
    /// </summary>
    [CLSCompliant(false)]
    public class MainViewModel : ViewModelBase
    {
        private IPlanetLogic logic;

        private VMPlanet selected;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="logic">Connection to he buisness logic.</param>
        public MainViewModel(IPlanetLogic logic)
        {
            this.logic = logic;
            this.Planets = new ObservableCollection<VMPlanet>();
            if (this.IsInDesignMode)
            {
                VMPlanet p1 = new VMPlanet()
                {
                    Name = "TEST-0000", Diameter = 5, Distance = 27, Mass = 2, Molecules = "H2O, O2",
                    Host = new SolarSystem.Data.Models.Star() { Name = "KOI-554" },
                };
                this.Planets.Add(p1);
            }
            else
            {
                this.logic.GetAllPlanets().ToList().ForEach(planet => this.Planets.Add(planet));
            }

            this.AddCmd = new RelayCommand(() => this.logic.AddPlanet(this.Planets));
            this.ModCmd = new RelayCommand(() => this.logic.ModPlanet(this.Selected));
            this.DelCmd = new RelayCommand(() => this.logic.DelPlanet(this.Planets, this.Selected));
            this.AllCmd = new RelayCommand(() => this.logic.GetAllPlanets());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class with zero parameters.
        /// </summary>
        public MainViewModel()
            : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IPlanetLogic>())
        {
        }

        /// <summary>
        /// Command pointing to Add operation in BL.
        /// </summary>
        public ICommand AddCmd { get; private set; }

        /// <summary>
        /// Command pointing to Modify operation in BL.
        /// </summary>
        public ICommand ModCmd { get; private set; }

        /// <summary>
        /// Command pointing to Delete operation in BL.
        /// </summary>
        public ICommand DelCmd { get; private set; }

        /// <summary>
        /// Command pointing to GetAll operation in BL.
        /// </summary>
        public ICommand AllCmd { get; private set; }

        /// <summary>
        /// Selected planet in the main window.
        /// </summary>
        public VMPlanet Selected
        {
            get { return this.selected; } set { this.Set(ref this.selected, value); }
        }

        /// <summary>
        /// All planets.
        /// </summary>
        public ObservableCollection<VMPlanet> Planets { get; private set; }
    }
}
