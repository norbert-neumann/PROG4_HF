namespace SolarSystem.Repository
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SolarSystem.Data.Models;

    /// <summary>
    /// Contains all functionalities for Planet inherited from generic repository, and IPlanetRepo interface.
    /// </summary>
    [CLSCompliant(false)]
    public class PlanetRepository : BaseRepository<Planet>, IPlanetRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetRepository"/> class.
        /// </summary>
        /// <param name="ctx">Database object.</param>
        public PlanetRepository(DbContext ctx)
            : base(ctx)
        {
        }

        /// <inheritdoc/>
        public int Add(string name, float mass, float distance, float diameter, string molecules, int hostId)
        {
            Planet newPlanet = new Planet() { Name = name, Mass = mass, Distance = distance, Diameter = diameter, Molecules = molecules, HostId = hostId };
            this.Ctx.Add(newPlanet);
            this.Ctx.SaveChanges();
            return newPlanet.Id;
        }

        /// <inheritdoc/>
        public void AddMolecules(int id, string molecules)
        {
            Planet planet;
            if ((planet = this.GetOne(id)) != null)
            {
                if (string.IsNullOrEmpty(planet.Molecules))
                {
                    planet.Molecules += molecules;
                }
                else
                {
                    planet.Molecules += " " + molecules;
                }

                this.Ctx.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public void ChangeMass(int id, float updatedMass)
        {
            Planet planet;
            if ((planet = this.GetOne(id)) != null)
            {
                planet.Mass = updatedMass;
                this.Ctx.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public override Planet GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(planet => planet.Id == id);
        }

        /// <inheritdoc/>
        public bool InHabitableZone(int id, float zone)
        {
            Planet planet;
            if ((planet = this.GetOne(id)) != null)
            {
                float minDist = zone * 0.95f;
                float maxDist = zone * 1.05f;
                return planet.Distance > minDist && planet.Distance < maxDist;
            }

            return false;
        }

        /// <inheritdoc/>
        public override bool Remove(int id)
        {
            Planet planet;
            if ((planet = this.GetOne(id)) != null)
            {
                this.Ctx.Remove(planet);
                this.Ctx.SaveChanges();
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void Update(Planet updated)
        {
            Planet planet;
            if (updated != null && (planet = this.GetOne(updated.Id)) != null)
            {
                updated.CopyTo(planet);
                this.Ctx.SaveChanges();
            }
        }
    }
}
