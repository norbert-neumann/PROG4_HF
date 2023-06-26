using System;

[assembly: CLSCompliant(true)]

namespace SolarSystem.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Abstract class that implements all generic CRUD operations.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    [CLSCompliant(false)]
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        private DbContext ctx;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="ctx">Data layer's context.</param>
        protected BaseRepository(DbContext ctx)
        {
            this.Ctx = ctx;
        }

        /// <summary>
        /// Refernce to data layer's context.
        /// </summary>
        protected DbContext Ctx
        {
            get { return this.ctx; }
            set { this.ctx = value; }
        }

        /// <summary>
        /// Returns all entities from the context.
        /// </summary>
        /// <returns>Collection of entities as IQueryable that can be built into an expression tree.</returns>
        public IQueryable<T> GetAll()
        {
            return this.Ctx.Set<T>();
        }

        /// <inheritdoc/>
        public async Task<IList<T>> GetAllAsync()
        {
           return await this.Ctx.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public abstract T GetOne(int id);

        /// <inheritdoc/>
        public abstract bool Remove(int id);
    }
}
