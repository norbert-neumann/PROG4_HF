namespace SolarSystem.Logic
{
    using System.Collections.Generic;
    using SolarSystem.Data.Models;

    /// <summary>
    /// Result class that encapsulates a System-Star-Planet triplet.
    /// </summary>
    public class ListResult
    {
        private StarSystem system;
        private IList<Star> stars;
        private IList<ICollection<Planet>> planets;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListResult"/> class.
        /// </summary>
        /// <param name="system">StarSystem object.</param>
        /// <param name="stars">Star object belonging to the given star systen.</param>
        /// <param name="planets">Planets orbiting these stars.</param>
        public ListResult(StarSystem system, IList<Star> stars, IList<ICollection<Planet>> planets)
        {
            this.system = system;
            this.stars = stars;
            this.planets = planets;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string output = this.system.ToString() + "\n";

            foreach (Star star in this.stars)
            {
                output += $"\t{star}\n";
                foreach (Planet planet in star.Planets)
                {
                    output += $"\t\t{planet}\n";
                }

                output += "\n";
            }

            return output;
        }
    }
}
