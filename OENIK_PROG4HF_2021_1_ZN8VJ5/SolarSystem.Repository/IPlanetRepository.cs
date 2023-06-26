namespace SolarSystem.Repository
{
    using SolarSystem.Data.Models;

    /// <summary>
    /// Adds update functionalities to Planet entity.
    /// </summary>
    public interface IPlanetRepository : IRepository<Planet>
    {
        /// <summary>
        /// Adds a new planet object to data layer.
        /// </summary>
        /// <param name="name">Planet's name.</param>
        /// <param name="mass">Planet's mass.</param>
        /// <param name="distance">Planet's distance..</param>
        /// <param name="diameter">Planet's diameter.</param>
        /// <param name="molecules">Molecules found on planet.</param>
        /// <param name="hostId">Star's id which this planet orbits.</param>
        /// <returns>Newly created planet's id.</returns>
        int Add(string name, float mass, float distance, float diameter, string molecules, int hostId);

        /// <summary>
        /// Adds newly found molecules to the given Planet.
        /// </summary>
        /// <param name="id">Entity's primary key.</param>
        /// <param name="molecules">New molecule(s).</param>
        void AddMolecules(int id, string molecules);

        /// <summary>
        /// Allows Planet's mass to be updated.
        /// </summary>
        /// <param name="id">Entity's primary key.</param>
        /// <param name="updatedMass">Planet's updated mass.</param>
        void ChangeMass(int id, float updatedMass);

        /// <summary>
        /// Syncs a planet's properties with a given instance.
        /// </summary>
        /// <param name="updated">The exisiting planet should have this properites.</param>
        void Update(Planet updated);

        /// <summary>
        /// Computes if the given planet is inside it's host star's habitable zone.
        /// </summary>
        /// <param name="id">Id of the planet.</param>
        /// <param name="zone">Planet's host star's habitable zone.</param>
        /// <returns>True if the planet's distance is not less then zone * 95%
        /// and not more then zone * 105%.</returns>
        bool InHabitableZone(int id, float zone);
    }
}
