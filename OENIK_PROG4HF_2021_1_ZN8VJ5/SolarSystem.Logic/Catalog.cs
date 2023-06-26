using System;

[assembly: CLSCompliant(true)]

namespace SolarSystem.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using SolarSystem.Data.Models;
    using SolarSystem.Repository;

    /// <summary>
    /// This class adds, gets, and removes entities from the repository.
    /// </summary>
    [CLSCompliant(false)]
    public class Catalog : ICatalog
    {
        private IStarSystemRepository systemRepo;
        private IStarRepository starRepo;
        private IPlanetRepository planetRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="Catalog"/> class.
        /// </summary>
        /// <param name="systemRepo">Connection to StarSystems table.</param>
        /// <param name="starRepo">Connection to Stars table.</param>
        /// <param name="planetRepo">Connection to Planets table.</param>
        public Catalog(IStarSystemRepository systemRepo, IStarRepository starRepo, IPlanetRepository planetRepo)
        {
            this.systemRepo = systemRepo;
            this.starRepo = starRepo;
            this.planetRepo = planetRepo;
        }

        /// <inheritdoc/>
        public int AddPlanet(string name, float mass, float distance, float diameter, string molecules, int hostId)
        {
            return this.planetRepo.Add(name, mass, distance, diameter, molecules, hostId);
        }

        /// <inheritdoc/>
        public int AddStar(string name, StellarType type, float temp, float mass, int systemId)
        {
            return this.starRepo.Add(name, type, temp, mass, systemId);
        }

        /// <inheritdoc/>
        public int AddStarSystem(string name, float distance, float age, string altName, string catalogType)
        {
            return this.systemRepo.Add(name, distance, age, altName, catalogType);
        }

        /// <inheritdoc/>
        public Planet GetOnePlanet(int id)
        {
            return this.planetRepo.GetOne(id);
        }

        /// <inheritdoc/>
        public Star GetOneStar(int id)
        {
            return this.starRepo.GetOne(id);
        }

        /// <inheritdoc/>
        public StarSystem GetOneStarSystem(int id)
        {
            return this.systemRepo.GetOne(id);
        }

        /// <inheritdoc/>
        public IList<StarSystem> GetAllStarSystems()
        {
            return this.systemRepo.GetAll().ToList();
        }

        /// <inheritdoc/>
        public IList<Star> GetAllStars()
        {
            return this.starRepo.GetAll().ToList();
        }

        /// <inheritdoc/>
        public IList<Planet> GetAllPlanets()
        {
            return this.planetRepo.GetAll().ToList();
        }

        /// <inheritdoc/>
        public bool RemovePlanet(int id)
        {
            return this.planetRepo.Remove(id);
        }

        /// <inheritdoc/>
        public bool RemoveStar(int id)
        {
            return this.starRepo.Remove(id);
        }

        /// <inheritdoc/>
        public bool RemoveStarSystem(int id)
        {
            return this.systemRepo.Remove(id);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string output = string.Empty;

            foreach (StarSystem system in this.systemRepo.GetAll().ToList())
            {
                output += $"{system}\n";
                var stars = this.starRepo.GetAll().Where(star => star.SystemId == system.Id);
                foreach (Star star in stars)
                {
                    output += $"\t{star}\n";
                    var planets = this.planetRepo.GetAll().Where(planet => planet.HostId == star.Id);
                    foreach (Planet planet in planets)
                    {
                        output += $"\t\t{planet}\n";
                    }

                    output += "\n";
                }
            }

            return output;
        }

        /// <inheritdoc/>
        public void UpdatePlanet(Planet updated)
        {
            this.planetRepo.Update(updated);
        }
    }
}
