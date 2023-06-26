namespace SolarSystem.Logic
{
    using System.Collections.Generic;
    using SolarSystem.Data.Models;

    /// <summary>
    /// Every class that implements this interface should be responsible for
    /// adding, getting, and removing entities from the Data layer.
    /// </summary>
    public interface ICatalog
    {
        /// <summary>
        /// Adds a new star system object to Data layer.
        /// </summary>
        /// <param name="name">Solar system's name.</param>
        /// <param name="distance">Solar system's distance from Sun.</param>
        /// <param name="age">Solar system's age in million years.</param>
        /// <param name="altName">Solar system's alternative name.</param>
        /// <param name="catalogType">Solar system's catalog type.</param>
        /// <returns>Newly created solar system's id.</returns>
        int AddStarSystem(string name, float distance, float age, string altName, string catalogType);

        /// <summary>
        /// Adds new star to Data layer.
        /// </summary>
        /// <param name="name">Star's name.</param>
        /// <param name="type">Star's stellar type.</param>
        /// <param name="temp">Star's temperatury in 1000 Kelvin.</param>
        /// <param name="mass">Star's mass relative to Sun's mass.</param>
        /// <param name="systemId">Solar system's id where this star belongs.</param>
        /// <returns>Newly added star's id.</returns>
        int AddStar(string name, StellarType type, float temp, float mass, int systemId);

        /// <summary>
        /// Adds new planet to Data layer.
        /// </summary>
        /// <param name="name">Planet's name.</param>
        /// <param name="mass">Planet's mass relative to Earth's mass.</param>
        /// <param name="distance">Planet's distance from it's host star.</param>
        /// <param name="diameter">Planet's diameter realtive to Earth's.</param>
        /// <param name="molecules">Molecules found on this planet.</param>
        /// <param name="hostId">Planet's host star's id.</param>
        /// <returns>Newly added planet's id.</returns>
        int AddPlanet(string name, float mass, float distance, float diameter, string molecules, int hostId);

        /// <summary>
        /// Syncs a planet's properties with a given instance.
        /// </summary>
        /// <param name="updated">The exisiting planet should have this properites.</param>
        void UpdatePlanet(Planet updated);

        /// <summary>
        /// Gets a StarSystem entity.
        /// </summary>
        /// <param name="id">Entity's id.</param>
        /// <returns>A StarSystem object, if the given id exists in db, null otherwise.</returns>
        StarSystem GetOneStarSystem(int id);

        /// <summary>
        /// Gets a Star entity.
        /// </summary>
        /// <param name="id">Entity's id.</param>
        /// <returns>A Star object, if the given id exists in db, null otherwise.</returns>
        Star GetOneStar(int id);

        /// <summary>
        /// Gets a Planet entity.
        /// </summary>
        /// <param name="id">Entity's id.</param>
        /// <returns>A Planet object, if the given id exists in db, null otherwise.</returns>
        Planet GetOnePlanet(int id);

        /// <summary>
        /// Gets every Star System entity from the Data layer.
        /// </summary>
        /// <returns>Every StarSystem entity.</returns>
        IList<StarSystem> GetAllStarSystems();

        /// <summary>
        /// Gets every Star entity from the Data layer.
        /// </summary>
        /// <returns>Every Star entity.</returns>
        IList<Star> GetAllStars();

        /// <summary>
        /// Gets every Planet entity from the Data layer.
        /// </summary>
        /// <returns>Every Planet entity.</returns>
        IList<Planet> GetAllPlanets();

        /// <summary>
        /// Removes the given star system if it's in the database.
        /// </summary>
        /// <param name="id">Star system's id to remove.</param>
        /// <returns>True if the given entity was succesfully removed, false if it's not in database.</returns>
        bool RemoveStarSystem(int id);

        /// <summary>
        /// Removes the given star if it's in the database.
        /// </summary>
        /// <param name="id">Star's id to remove.</param>
        /// <returns>True if the given entity was succesfully removed, false if it's not in database.</returns>
        bool RemoveStar(int id);

        /// <summary>
        /// Removes the given planet if it's in the database.
        /// </summary>
        /// <param name="id">Planet's id to remove.</param>
        /// <returns>True if the given entity was succesfully removed, false if it's not in database.</returns>
        bool RemovePlanet(int id);
    }
}
