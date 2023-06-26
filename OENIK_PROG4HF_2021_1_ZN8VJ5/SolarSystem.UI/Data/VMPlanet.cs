namespace SolarSystem.UI.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using SolarSystem.Data.Models;

    /// <summary>
    /// Planet entity for the view model.
    /// </summary>
    [CLSCompliant(false)]
    public class VMPlanet : ObservableObject
    {
        private int id;
        private string name;
        private float distance;
        private float mass;
        private float diameter;
        private string molecules;
        private Star host;
        private bool isHabitable;

        /// <summary>
        /// Initializes a new instance of the <see cref="VMPlanet"/> class.
        /// </summary>
        public VMPlanet()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VMPlanet"/> class from a database object.
        /// </summary>
        /// <param name="dbPlanet">Source database object.</param>
        public VMPlanet(Planet dbPlanet)
        {
            if (dbPlanet != null)
            {
                this.id = dbPlanet.Id;
                this.name = dbPlanet.Name;
                this.distance = dbPlanet.Distance;
                this.diameter = dbPlanet.Diameter;
                this.mass = dbPlanet.Mass;
                this.molecules = dbPlanet.Molecules;
                this.host = dbPlanet.Host;
                this.isHabitable = dbPlanet.IsHabitable;
            }
        }

        /// <summary>
        /// Unique database generated key for each planet entity.
        /// </summary>
        public int Id
        {
            get { return this.id; } set { this.Set(ref this.id, value); }
        }

        /// <summary>
        /// Name of the planet.
        /// </summary>
        public string Name
        {
            get { return this.name; } set { this.Set(ref this.name, value); }
        }

        /// <summary>
        /// Distance from the host start in AU.
        /// </summary>
        public float Distance
        {
            get
            {
                return this.distance;
            }

            set
            {
                this.Set(ref this.distance, value);
                this.RaisePropertyChanged(nameof(this.IsHabitable));
            }
        }

        /// <summary>
        /// Mass of the planet relative to Earth's mass.
        /// </summary>
        public float Mass
        {
            get { return this.mass; } set { this.Set(ref this.mass, value); }
        }

        /// <summary>
        /// Diameter of the given planet realtive to Earth's diameter.
        /// </summary>
        public float Diameter
        {
            get { return this.diameter; } set { this.Set(ref this.diameter, value); }
        }

        /// <summary>
        /// String of found molecules separated by ",".
        /// </summary>
        public string Molecules
        {
            get { return this.molecules; } set { this.Set(ref this.molecules, value); }
        }

        /// <summary>
        /// Id of the planet's host star.
        /// </summary>
        public Star Host
        {
            get { return this.host; } set { this.Set(ref this.host, value); }
        }

        /// <summary>
        /// Checks if planet is inside it's host star's habitable zone.
        /// </summary>
        public bool IsHabitable
        {
            get
            {
                return this.Distance > this.Host.HabitableZone * 0.95 && this.Distance < this.Host.HabitableZone * 1.05;
            }

            set
            {
                this.isHabitable = value;
            }
        }

        /// <summary>
        /// Generates a copy from an other instance.
        /// </summary>
        /// <param name="other">Instance whose properties should be copied from.</param>
        public void CopyFrom(VMPlanet other)
        {
            this.GetType().GetProperties().ToList().
                ForEach(property => property.SetValue(this, property.GetValue(other)));
        }

        /// <summary>
        /// Copies this instance's properties to a database object.
        /// </summary>
        /// <returns>A database object with the same properties as this.</returns>
        public Planet AsDbPlanet()
        {
            Planet p = new Planet();
            p.Id = this.id;
            p.Name = this.name;
            p.Mass = this.mass;
            p.Diameter = this.diameter;
            p.Distance = this.distance;
            p.Molecules = this.molecules;
            p.Host = this.host;
            return p;
        }
    }
}
