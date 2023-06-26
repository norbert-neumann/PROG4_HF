namespace SolarSystem.Repository
{
    using System.Linq;
    using SolarSystem.Data.Models;

    /// <summary>
    /// Adds update functionalities to Star class.
    /// </summary>
    public interface IStarRepository : IRepository<Star>
    {
        /// <summary>
        /// Adds a new Star instance to Data layer.
        /// </summary>
        /// <param name="name">Name of star.</param>
        /// <param name="type">Stellar type of star.</param>
        /// <param name="temp">Star's temperature.</param>
        /// <param name="mass">Star's mass.</param>
        /// <param name="systemId">Id of the StarSystem object, where this instance belongs.</param>
        /// <returns>Newly created instance's id.</returns>
        int Add(string name, StellarType type, float temp, float mass, int systemId);

        /// <summary>
        /// Changes Star's type.
        /// </summary>
        /// <param name="id">Entity's primary key.</param>
        /// <param name="newType">Star's updated type.</param>
        void ChangeType(int id, StellarType newType);

        /// <summary>
        /// Allows Star's mass to be updated.
        /// </summary>
        /// <param name="id">Entity's primary key.</param>
        /// <param name="updatedMass">Star's updated mass.</param>
        void ChangeMass(int id, float updatedMass);

        /// <summary>
        /// Allows Star's mass to be updated.
        /// </summary>
        /// <param name="id">Entity's primary key.</param>
        /// <param name="tempDelta">Star's current temperature will be multiplied with this number.</param>
        void ChangeTemperature(int id, float tempDelta);

        /// <summary>
        /// Recomputes the habitable zone of the given star by it's temperature.
        /// </summary>
        /// <param name="id">Entity's primary key.</param>
        void UpdateHabitableZone(int id);

        /// <summary>
        /// Returns all planets which orbit this star.
        /// </summary>
        /// <param name="id">Entity's primary key.</param>
        /// <returns>Collection of the star's planets.</returns>
        IQueryable<Planet> GetPlanets(int id);

        /// <summary>
        /// Decides if a star is a main sequence star.
        /// Main sequence stars: O, B, A, F, O, K, M.
        /// </summary>
        /// <param name="star">Star entity.</param>
        /// <returns>Returns true if a star is a main sequence star.</returns>
        bool IsMainSequence(Star star);

        /// <summary>
        /// Decides if a star is a stellar remnant.
        /// Stellar remnant if type = D, NeutronStar, BlackHole.
        /// </summary>
        /// <param name="star">Star entity.</param>
        /// <returns>Returns true if a star is a stellar remnant.</returns>
        bool IsStellarRemnant(Star star);

        /// <summary>
        /// Every star has a transition from main-sequence to stellar remnant.
        /// During this transition the star is in red giant phase.
        /// This methods decides if the star is in red giant phase.
        /// </summary>
        /// <param name="star">Star entity..</param>
        /// <returns>True if star is in red giant phase.</returns>
        bool InRedGiantPhase(Star star);
    }
}
