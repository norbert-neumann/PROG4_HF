namespace SolarSystem.Repository
{
    using System.Linq;
    using SolarSystem.Data.Models;

    /// <summary>
    /// Adds update functionalities to StarSystem class.
    /// </summary>
    public interface IStarSystemRepository : IRepository<StarSystem>
    {
        /// <summary>
        /// Adds a new StarSystem instance to Data layer.
        /// </summary>
        /// <param name="name">Star System's name.</param>
        /// <param name="distance">Star System's from Sun.</param>
        /// <param name="age">Star System's age.</param>
        /// <param name="altName">Star System's alternative name.</param>
        /// <param name="catalogType">Star System's catalog type.</param>
        /// <returns>Newly created star system's id.</returns>
        int Add(string name, float distance, float age, string altName, string catalogType);

        /// <summary>
        /// Adds a non catalouge based name to the given star system.
        /// </summary>
        /// <param name="id">Entity's primary key.</param>
        /// <param name="altName">Name to be added to alternative names.</param>
        void AddAlternativeName(int id, string altName);

        /// <summary>
        /// Updates the star system's age.
        /// </summary>
        /// <param name="id">Entity's primary key.</param>
        /// <param name="age">Star systems's updated age.</param>
        void UpdateAge(int id, float age);

        /// <summary>
        /// Returns all stars which are part of this system.
        /// </summary>
        /// <param name="id">Entity's primary key.</param>
        /// <returns>Collection of the system's stars.</returns>
        IQueryable<Star> GetStars(int id);
    }
}
