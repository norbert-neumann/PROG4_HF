namespace SolarSystem.UI.VM
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using SolarSystem.Data.Models;
    using SolarSystem.UI.BL;
    using SolarSystem.UI.Data;

    /// <summary>
    /// VM for the editor window.
    /// </summary>
    [CLSCompliant(false)]
    public class EditorViewModel : ViewModelBase
    {
        private IPlanetLogic logic;
        private VMPlanet planet;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorViewModel"/> class.
        /// </summary>
        /// <param name="logic">Connection to he buisness logic.</param>
        public EditorViewModel(IPlanetLogic logic)
        {
            this.logic = logic;
            this.planet = new VMPlanet();

            if (this.IsInDesignMode)
            {
                this.planet.Name = "TEST-1234";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorViewModel"/> class with zero parameters.
        /// </summary>
        public EditorViewModel()
            : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IPlanetLogic>())
        {
        }

        /// <summary>
        /// Planet which is currently edited.
        /// </summary>
        public VMPlanet Planet
        {
            get { return this.planet; }
            set { this.Set(ref this.planet, value); }
        }

        /// <summary>
        /// Collection of stars in the Data layer.
        /// Since this WPF app only adds new planets but no stars we don't need to sync this.
        /// </summary>
        public IList<Star> Hosts
        {
            get
            {
                if (IsInDesignModeStatic)
                {
                    return new List<Star>(new Star[] { new Star() { Name = "TEST_STAR-123" } });
                }
                else
                {
                    return this.logic.GetAllHosts();
                }
            }

            private set
            {
            }
        }
    }
}
