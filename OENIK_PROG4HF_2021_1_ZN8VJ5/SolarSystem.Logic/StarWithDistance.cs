namespace SolarSystem.Logic
{
    using System;
    using System.Globalization;
    using SolarSystem.Data.Models;

    /// <summary>
    /// Helper class that encapsulates a star with it's distance to the Sun.
    /// (By default the distance is stored in StarSystem).
    /// </summary>
    public class StarWithDistance : IComparable
    {
        private Star star;
        private float distance;

        /// <summary>
        /// Initializes a new instance of the <see cref="StarWithDistance"/> class.
        /// </summary>
        /// <param name="star">Star object.</param>
        /// <param name="distance">Star's distance to Sun.</param>
        public StarWithDistance(Star star, float distance)
        {
            this.star = star;
            this.distance = distance;
        }

        /// <summary>
        /// Gets this.star.
        /// After sorting the stars we will use this to get the closest habitable star.
        /// </summary>
        public Star Star
        {
            get { return this.star; }
        }

        /// <summary>
        /// Compares to stars by their distance.
        /// </summary>
        /// <param name="obj">Other star :).</param>
        /// <returns>1, if this star is closer then the other.</returns>
        public int CompareTo(object obj)
        {
            if (obj is StarWithDistance)
            {
                StarWithDistance other = obj as StarWithDistance;

                if (this.distance < other.distance)
                {
                    return 1;
                }
                else if (this.distance > other.distance)
                {
                    return -1;
                }
                else if (this.star.Id < other.star.Id)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }

            throw new ArgumentException("Not valid parameter.", paramName: nameof(obj));
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is StarWithDistance)
            {
                StarWithDistance other = obj as StarWithDistance;

                return this.star.Equals(other.star) &&
                    Math.Abs(this.distance - other.distance) < 0.01;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.star.GetHashCode() + (int)Math.Round(this.distance);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "Name: {0}, distance: {1} parsecs.", this.star.Name, this.distance);
        }
    }
}
