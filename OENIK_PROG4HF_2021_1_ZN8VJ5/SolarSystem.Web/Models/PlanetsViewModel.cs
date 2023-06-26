namespace SolarSystem.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Planet view model containg the currently edited planet, list of all planets in the DB.
    /// </summary>
    public class PlanetsViewModel
    {
        /// <summary>
        /// Currently edited planet.
        /// </summary>
        public Planet EditedPlanet { get; set; }

        /// <summary>
        /// List of all planets in the DB.
        /// </summary>
        public List<Planet> ListOfPlanets { get; set; }
    }
}
