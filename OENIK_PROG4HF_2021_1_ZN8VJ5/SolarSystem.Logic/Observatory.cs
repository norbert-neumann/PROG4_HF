namespace SolarSystem.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SolarSystem.Data.Models;
    using SolarSystem.Repository;

    /// <summary>
    /// Implements IObservatory interface with eager loading.
    /// </summary>
    public class Observatory : IObservatory
    {
        private IStarSystemRepository systemRepo;
        private IStarRepository starRepo;
        private IPlanetRepository planetRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="Observatory"/> class.
        /// </summary>
        /// <param name="systemRepo">Connection to StarSystems table.</param>
        /// <param name="starRepo">Connection to Stars table.</param>
        /// <param name="planetRepo">Connection to Planets table.</param>
        public Observatory(IStarSystemRepository systemRepo, IStarRepository starRepo, IPlanetRepository planetRepo)
        {
            this.systemRepo = systemRepo;
            this.starRepo = starRepo;
            this.planetRepo = planetRepo;
        }

        /// <inheritdoc/>
        public IList<Planet> GetHabitablePlanets()
        {
            var q = from planet in this.planetRepo.GetAll().ToList()
                    join star in this.starRepo.GetAll().ToList()
                    on planet.HostId equals star.Id
                    where planet.Distance > star.HabitableZone * 0.95 &&
                    planet.Distance < star.HabitableZone * 1.05
                    select planet;

            return q.ToList();
        }

        /// <inheritdoc/>
        public IList<Star> GetStarsWithManyHabitablePlanets()
        {
            var q = from star in this.starRepo.GetAll().ToList()
                    join planet in this.GetHabitablePlanets()
                    on star.Id equals planet.HostId
                    group star by star.Id into g
                    where g.Count() > 1
                    select g.Key;

            var q1 = this.starRepo.GetAll().Where(star => q.Contains(star.Id));

            return q1.ToList();
        }

        /// <inheritdoc/>
        public IList<Planet> GetSuperhabitablePlanets()
        {
            return this.GetHabitablePlanets().Where(p => p.Molecules.Contains("H2O", StringComparison.Ordinal)).ToList();
        }

        /// <inheritdoc/>
        public async Task<List<Planet>> GetHabitablePlanetsAsync()
        {
            return (await this.planetRepo.GetAllAsync().ConfigureAwait(false)).Where(p => p.IsHabitable).ToList();
        }

        /// <inheritdoc/>
        public StarWithDistance NearestHabitableStar()
        {
            var habitableStars = from planet in this.GetHabitablePlanets()
                    join star in this.starRepo.GetAll()
                    on planet.HostId equals star.Id
                    select star;

            var starWithDistance = from star in habitableStars
                                   join system in this.systemRepo.GetAll()
                                   on star.SystemId equals system.Id
                                   select new StarWithDistance(star, system.Distance);

            return starWithDistance.OrderBy(star => star).FirstOrDefault();
        }
    }
}
