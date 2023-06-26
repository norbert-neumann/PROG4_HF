namespace SolarSystem.UI.BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SolarSystem.Data.Models;
    using SolarSystem.UI.Data;

    /// <summary>
    /// Interface that implements all CRUD methods.
    /// </summary>
    [CLSCompliant(false)]
    public interface IPlanetLogic
    {
        /// <summary>
        /// Adds a planet to the current collection.
        /// </summary>
        /// <param name="list">Viewmodel collection.</param>
        void AddPlanet(IList<VMPlanet> list);

        /// <summary>
        /// Modifies a single planet's properties.
        /// </summary>
        /// <param name="planetToModify">Planet to be modified.</param>
        void ModPlanet(VMPlanet planetToModify);

        /// <summary>
        /// Deletes a planet from the VM collection.
        /// </summary>
        /// <param name="list">This is where te planet should be removed from.</param>
        /// <param name="planet">Planet to be deleted.</param>
        void DelPlanet(IList<VMPlanet> list, VMPlanet planet);

        /// <summary>
        /// Returns the planets from the database converted to VM planets.
        /// </summary>
        /// <returns>All planets from the Data layer.</returns>
        IList<VMPlanet> GetAllPlanets();

        /// <summary>
        /// Returns a unique list of host stars.
        /// </summary>
        /// <returns>List of host stars.</returns>
        IList<Star> GetAllHosts();
    }
}
