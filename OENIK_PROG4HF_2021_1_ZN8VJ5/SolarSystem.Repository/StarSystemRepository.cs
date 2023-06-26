namespace SolarSystem.Repository
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SolarSystem.Data.Models;

    /// <summary>
    /// Contains all functionalities for StarSystem inherited from generic repository, and IStarSystemRepo interface.
    /// </summary>
    [CLSCompliant(false)]
    public class StarSystemRepository : BaseRepository<StarSystem>, IStarSystemRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StarSystemRepository"/> class.
        /// </summary>
        /// <param name="ctx">Database object.</param>
        public StarSystemRepository(DbContext ctx)
            : base(ctx)
        {
        }

        /// <inheritdoc/>
        public int Add(string name, float distance, float age, string altName, string catalogType)
        {
            StarSystem newSystem = new StarSystem() { Name = name, Distance = distance, Age = age, AlternativeName = altName, CatalogType = catalogType };
            this.Ctx.Add<StarSystem>(newSystem);
            this.Ctx.SaveChanges();
            return newSystem.Id;
        }

        /// <inheritdoc/>
        public void AddAlternativeName(int id, string altName)
        {
            StarSystem system;
            if ((system = this.GetOne(id)) != null)
            {
                if (string.IsNullOrEmpty(system.AlternativeName))
                {
                    system.AlternativeName += altName;
                }
                else
                {
                    system.AlternativeName += ", " + altName;
                }

                this.Ctx.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public override StarSystem GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(system => system.Id == id);
        }

        /// <inheritdoc/>
        public IQueryable<Star> GetStars(int id)
        {
            StarSystem system;
            if ((system = this.GetOne(id)) != null)
            {
                return system.Stars.AsQueryable();
            }

            throw new ArgumentException("Star system not found with given id.");
        }

        /// <inheritdoc/>
        public override bool Remove(int id)
        {
            StarSystem system;
            if ((system = this.GetOne(id)) != null)
            {
                this.Ctx.Remove(system);
                this.Ctx.SaveChanges();
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void UpdateAge(int id, float age)
        {
            StarSystem system;
            if ((system = this.GetOne(id)) != null)
            {
                system.Age = age;
                this.Ctx.SaveChanges();
            }
        }
    }
}
