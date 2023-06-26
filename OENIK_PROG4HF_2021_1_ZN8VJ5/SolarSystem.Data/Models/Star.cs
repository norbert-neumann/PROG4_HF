namespace SolarSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///  Represents a star entity in the database.
    /// </summary>
    [Table("star")]
    public class Star
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Star"/> class.
        /// </summary>
        public Star()
        {
            this.Planets = new HashSet<Planet>();
        }

        /// <summary>
        /// Unique database generated key for each star entity.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name of the star, all systems must have a name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Spectral type of the star (B, A, F, G, K, M).
        /// </summary>
        public StellarType Type { get; set; }

        /// <summary>
        /// Effective temperature of the star in Kelvin.
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// Mass of the star, enumerated in terms of the Sun's mass.
        /// </summary>
        public float Mass { get; set; }

        /// <summary>
        /// Distance from the star where liquid water can exist.
        /// </summary>
        public float HabitableZone { get; set; }

        /// <summary>
        /// Id of the system which this star is part of.
        /// </summary>
        [ForeignKey(nameof(System))]
        public int SystemId { get; set; }

        /// <summary>
        /// Navigation poperty for this star's system.
        /// </summary>
        [NotMapped]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual StarSystem System { get; set; }

        /// <summary>
        /// Navigational property for the star's planets.
        /// </summary>
        [NotMapped]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Planet> Planets { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Name}(Id:{this.Id}), type: {this.Type}, habitable zone: {this.HabitableZone}," +
                   $" mass: {this.Mass}, temperature: {this.Temperature}K";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Star)
            {
                Star other = (Star)obj;

                return (this.Id == other.Id) &&
                    (this.Name == other.Name) &&
                    (this.Mass == other.Mass) &&
                    (this.Temperature == other.Temperature) &&
                    (this.Type == other.Type) &&
                    (this.HabitableZone == other.HabitableZone);
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
