namespace SolarSystem.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using SolarSystem.Data.Models;

    /// <summary>
    /// Planet form model.
    /// </summary>
    public class Planet
    {
        /// <summary>
        /// Planet's unique id in the DB.
        /// </summary>
        [Display(Name = "Planet Id")]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Planet's name.
        /// </summary>
        [Display(Name = "Planet Name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Planet's mass.
        /// </summary>
        [Display(Name = "Planet Mass")]
        public float Mass { get; set; }

        /// <summary>
        /// Planet's distance to it's host..
        /// </summary>
        [Display(Name = "Planet's distance to host")]
        [Required]
        public float Distance { get; set; }

        /// <summary>
        /// Planet's diameter.
        /// </summary>
        [Display(Name = "Planet Diameter")]
        public float Diameter { get; set; }

        /// <summary>
        /// Planet's molecules.
        /// </summary>
        [Display(Name = "Planet's Molecules")]
        public string Molecules { get; set; }

        /// <summary>
        /// Planet's host's id..
        /// </summary>
        [Display(Name = "Host")]
        [Required]
        public int HostId { get; set; }

        /// <summary>
        /// Planet's host.
        /// </summary>
        public Star Host { get; set; }

        /// <summary>
        /// Boolean indicating whether the planet is habitable or not.
        /// </summary>
        [Display(Name = "Habitable?")]
        public bool IsHabitable { get; set; }

        /// <summary>
        /// Collection of all available stars. These are the host candidates.
        /// </summary>
        [Display(Name = "Host Id")]
        public IEnumerable<Star> Stars { get; set; }
    }
}
