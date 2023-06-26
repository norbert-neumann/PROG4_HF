namespace SolarSystem.Repository
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SolarSystem.Data.Models;

    /// <summary>
    /// Contains all functionalities for Star inherited from generic repository, and IStarRepo interface.
    /// </summary>
    [CLSCompliant(false)]
    public class StarRepository : BaseRepository<Star>, IStarRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StarRepository"/> class.
        /// </summary>
        /// <param name="ctx">Database object.</param>
        public StarRepository(DbContext ctx)
            : base(ctx)
        {
        }

        /// <inheritdoc/>
        public int Add(string name, StellarType type, float temp, float mass, int systemId)
        {
            Star newStar = new Star() { Name = name, Type = type, Temperature = temp, Mass = mass, SystemId = systemId };
            this.Ctx.Add<Star>(newStar);
            this.Ctx.SaveChanges();
            int id = newStar.Id;
            this.UpdateHabitableZone(id);
            this.Ctx.SaveChanges();
            return id;
        }

        /// <inheritdoc/>
        public void ChangeMass(int id, float updatedMass)
        {
            Star star;
            if ((star = this.GetOne(id)) != null)
            {
                star.Mass = updatedMass;
                this.Ctx.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public void ChangeTemperature(int id, float tempDelta)
        {
            Star star;
            if ((star = this.GetOne(id)) != null)
            {
                star.Temperature *= tempDelta;
                this.Ctx.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public void ChangeType(int id, StellarType newType)
        {
            Star star;
            if ((star = this.GetOne(id)) != null)
            {
                star.Type = newType;
                this.Ctx.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public override Star GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(star => star.Id == id);
        }

        /// <inheritdoc/>
        public IQueryable<Planet> GetPlanets(int id)
        {
            Star star;
            if ((star = this.GetOne(id)) != null)
            {
                return star.Planets.AsQueryable();
            }

            throw new ArgumentException("Star not found with given id.");
        }

        /// <inheritdoc/>
        public bool InRedGiantPhase(Star star)
        {
            return star?.Type == StellarType.RedGiant;
        }

        /// <inheritdoc/>
        public bool IsMainSequence(Star star)
        {
            return !(this.InRedGiantPhase(star) || this.IsStellarRemnant(star));
        }

        /// <inheritdoc/>
        public bool IsStellarRemnant(Star star)
        {
            bool typeCheck =
                star?.Type == StellarType.D ||
                star?.Type == StellarType.BlackHole ||
                star?.Type == StellarType.NeutronStar;
            return typeCheck;
        }

        /// <inheritdoc/>
        public override bool Remove(int id)
        {
            Star star;
            if ((star = this.GetOne(id)) != null)
            {
                this.Ctx.Remove(star);
                this.Ctx.SaveChanges();
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void UpdateHabitableZone(int id)
        {
            Star star;
            if ((star = this.GetOne(id)) != null)
            {
                star.HabitableZone = (float)Math.Round(star.Temperature / 1000 / 5.77f, 3);
                this.Ctx.SaveChanges();
            }
        }
    }
}
