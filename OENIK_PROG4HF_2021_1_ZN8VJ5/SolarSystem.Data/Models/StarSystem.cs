namespace SolarSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a solar system entity in the database.
    /// </summary>
    [Table("system")]
    public class StarSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StarSystem"/> class.
        /// </summary>
        public StarSystem()
        {
            this.Stars = new HashSet<Star>();
        }

        /// <summary>
        /// Unique key for each system.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name of the system, all systems must have this.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Distance from the Sun in parsecs.
        /// </summary>
        public float Distance { get; set; }

        /// <summary>
        /// Age of the system in million years.
        /// </summary>
        // Todo: [Range()]
        public float? Age { get; set; }

        /// <summary>
        /// Alternative name of the system, usually this property in null.
        /// </summary>
        #nullable enable
        public string? AlternativeName { get; set; }
        #nullable disable

        /// <summary>
        /// The type of astronomical cataloue used for naming the system.
        /// </summary>
        public string CatalogType { get; set; }

        /// <summary>
        /// Naviagation property for the system's stars. One-to-many relation.
        /// </summary>
        [NotMapped]
        public virtual ICollection<Star> Stars { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Name}(Id:{this.Id}), distance: {this.Distance} parsecs, age: {this.Age} billion years";
        }
    }
}
