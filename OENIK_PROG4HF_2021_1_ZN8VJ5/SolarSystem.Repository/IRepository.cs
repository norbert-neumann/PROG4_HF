namespace SolarSystem.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic interface for all CRUD operations except update.
    /// </summary>
    /// <typeparam name="T">Generic entity type.</typeparam>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Gets the entity by it's id.
        /// </summary>
        /// <param name="id">Entity's primary key.</param>
        /// <returns>Entity object.</returns>
        T GetOne(int id);

        /// <summary>
        /// Returns all elements in the given DbSet.
        /// </summary>
        /// <returns>IQueryable expression tree.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Removes entity if it's in the database.
        /// </summary>
        /// <param name="id">Entity's id to remove.</param>
        /// <returns>True if the given entity was succesfully removed, false if it's not in database.</returns>
        bool Remove(int id);

        /// <summary>
        /// Returns all elements in the given DbSet asynchronously.
        /// </summary>
        /// <returns>A task that returns a list of entities.</returns>
        Task<IList<T>> GetAllAsync();
    }
}
