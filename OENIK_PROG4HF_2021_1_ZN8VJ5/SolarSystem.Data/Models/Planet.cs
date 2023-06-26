namespace SolarSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a planet entity in the database.
    /// </summary>
    [Table("planet")]
    public class Planet
    {
        /// <summary>
        /// Unique database generated key for each planet entity.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name of the planet, required.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Mass of the planet relative to Earth's mass.
        /// </summary>
        public float Mass { get; set; }

        /// <summary>
        /// Distance from the host start in AU.
        /// </summary>
        public float Distance { get; set; }

        /// <summary>
        /// Diameter of the given planet realtive to Earth's diameter.
        /// </summary>
        public float Diameter { get; set; }

        /// <summary>
        /// String of found molecules separated by ",". nullable.
        /// </summary>
        #nullable enable
        public string? Molecules { get; set; }
        #nullable disable

        /// <summary>
        /// Id of the planet's host star.
        /// </summary>
        [ForeignKey(nameof(Host))]
        public int HostId { get; set; }

        /// <summary>
        /// Navigation property for the foreign key.
        /// </summary>
        [NotMapped]
        public virtual Star Host { get; set; }

        /// <summary>
        /// Checks if planet is inside it's host star's habitable zone.
        /// </summary>
        [NotMapped]
        public bool IsHabitable
        {
            get
            {
                return this.Distance > this.Host.HabitableZone * 0.95 && this.Distance < this.Host.HabitableZone * 1.05;
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Planet)
            {
                Planet other = (Planet)obj;

                return this.Id == other.Id &&
                    this.Name == other.Name &&
                    this.Diameter == other.Diameter &&
                    this.Distance == other.Distance &&
                    this.Mass == other.Mass &&
                    this.Molecules == other.Molecules &&
                    this.HostId == other.HostId;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string habitable = this.IsHabitable ? "Y" : "N";
            string molecules = string.IsNullOrEmpty(this.Molecules) ? "-" : this.Molecules;
            return $"{this.Name}(Id:{this.Id}), distance={this.Distance}, molecules: {molecules}, habitable: {habitable}";
        }

        /// <summary>
        /// Copies self's properties into another instance.
        /// (Reflection does not work here...)
        /// </summary>
        /// <param name="p">Instance where the poperties should be copied into.</param>
        public void CopyTo(Planet p)
        {
            if (p != null)
            {
                p.Id = this.Id;
                p.Name = this.Name;
                p.Mass = this.Mass;
                p.Diameter = this.Diameter;
                p.Distance = this.Distance;
                p.Molecules = this.Molecules;
                p.Host = this.Host;
            }
        }
    }
}
