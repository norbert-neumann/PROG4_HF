namespace SolarSystem.Logic
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SolarSystem.Data.Models;

    /// <summary>
    /// List of queries that is about a planet's habitibility.
    /// </summary>
    public interface IObservatory
    {
        /// <summary>
        /// Checks if a given planet is inside it's host star's habitable zone.
        /// </summary>
        /// <returns>List of all habitable planets.</returns>
        IList<Planet> GetHabitablePlanets();

        /// <summary>
        /// Checks if a given planet is inside it's host star's habitable zone (async version).
        /// </summary>
        /// <returns>Task that returns a list of all habitable planets.</returns>
        Task<List<Planet>> GetHabitablePlanetsAsync();

        /// <summary>
        /// Checks if a given planet is inside it's host star's habitable zone
        /// and water exsists on that planet..
        /// </summary>
        /// <returns>List of all superhabitable planets.</returns>
        IList<Planet> GetSuperhabitablePlanets();

        /// <summary>
        /// Checks if a star has more then one habitable planet.
        /// </summary>
        /// <returns>List of stars that have more then one habitable planet.</returns>
        IList<Star> GetStarsWithManyHabitablePlanets();

        /// <summary>
        /// Finds the nearest star that has atleast one habitable planet.
        /// If there are more then one habitable star with the same distance
        /// then the star with the smaller Id gets to be the nearest star.
        /// </summary>
        /// <returns>Nearest habitable planet's star.</returns>
        StarWithDistance NearestHabitableStar();
    }
}
