using System;

[assembly: CLSCompliant(true)]

namespace SolarSystem.Data.Models
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Database object containing all three tables.
    /// </summary>
    [CLSCompliant(false)]
    public class SolarSystemDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolarSystemDbContext"/> class.
        /// </summary>
        public SolarSystemDbContext()
        {
            this.Database.EnsureCreated();
        }

        /// <summary>
        /// DbSet for Systems.
        /// </summary>
        public virtual DbSet<StarSystem> Systems { get; set; }

        /// <summary>
        /// DbSet for Stars.
        /// </summary>
        public virtual DbSet<Star> Stars { get; set; }

        /// <summary>
        /// DbSet for Planets.
        /// </summary>
        public virtual DbSet<Planet> Planets { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder != null && !optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\SolarSystemDB.mdf; Integrated Security=True; MultipleActiveResultSets=true");
            }
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Entity<Planet>(entity =>
                {
                    entity.HasOne(planet => planet.Host).WithMany(star => star.Planets).HasForeignKey(planet => planet.HostId);
                });

                modelBuilder.Entity<Star>(entity =>
                {
                    entity.HasOne(star => star.System).WithMany(system => system.Stars).HasForeignKey(star => star.SystemId);
                });

                // Seed data
                StarSystem[] systems = new StarSystem[]
                {
                    new StarSystem() { Id = 1, Name = "TOI-1699", Age = 3848, Distance = 6.132f },
                    new StarSystem() { Id = 2, Name = "Kepler-326", Age = 6058, Distance = 5.212f },
                    new StarSystem() { Id = 3, Name = "KOI-2020", Age = 9509, Distance = 9.4f },
                    new StarSystem() { Id = 4, Name = "EPIC-285503580", Age = 5194, Distance = 7.971f },
                    new StarSystem() { Id = 5, Name = "Gl 726", Age = 6337, Distance = 7.358f },
                };

                Star[] stars = new Star[]
                {
                    new Star() { Id = 1, Name = "Gl 726 B", Type = StellarType.M, Mass = 0.2f, Temperature = 3000, HabitableZone = 0.1f, SystemId = 5 },
                    new Star() { Id = 2, Name = "TOI-1699 A", Type = StellarType.F, Mass = 7.8f, Temperature = 7000, HabitableZone = 1.213f, SystemId = 1 },
                    new Star() { Id = 3, Name = "Kepler-326 A", Type = StellarType.F, Mass = 5.7f, Temperature = 6000, HabitableZone = 1.04f, SystemId = 2 },
                    new Star() { Id = 4, Name = "Kepler-326 B", Type = StellarType.B, Mass = 5.7f, Temperature = 11000, HabitableZone = 3.906f, SystemId = 2 },
                    new Star() { Id = 5, Name = "KOI-2020 A", Type = StellarType.G, Mass = 1.2f, Temperature = 5000, HabitableZone = 0.967f, SystemId = 3 },
                    new Star() { Id = 6, Name = "KOI-2020 B", Type = StellarType.M, Mass = 0.1f, Temperature = 1000, HabitableZone = 0.26f, SystemId = 3 },
                    new Star() { Id = 7, Name = "EPIC-285503580 A", Type = StellarType.A, Mass = 4.3f, Temperature = 9000, HabitableZone = 2.843f, SystemId = 4 },
                    new Star() { Id = 8, Name = "Gl 726 A", Type = StellarType.A, Mass = 40, Temperature = 9000, HabitableZone = 1.56f, SystemId = 5 },
                };

                Planet[] planets = new Planet[]
                {
                    new Planet() { Id = 1, Name = "Gl 726 Ba", Distance = 0.8f, Diameter = 1.2f, Mass = 1.6f, Molecules = "Na, Ch4", HostId = 8 },
                    new Planet() { Id = 2, Name = "Gl 726 Bb", Distance = 7.1f, Diameter = 4.37f, Mass = 5.3f, Molecules = "C, C02, Ch4, H", HostId = 1 },
                    new Planet() { Id = 3, Name = "Kepler-326 Aa", Distance = 5.9f, Diameter = 6.2f, Mass = 4.3f, Molecules = "Na, Nh3, H", HostId = 3 },
                    new Planet() { Id = 4, Name = "Kepler-326 Ba", Distance = 3.8f, Diameter = 1.5f, Mass = 1.8f, Molecules = string.Empty, HostId = 4 },
                    new Planet() { Id = 5, Name = "Kepler-326 Bb", Distance = 10.4f, Diameter = 50.7f, Mass = 40.9f, Molecules = "Na, Nh3, H, C, C02, Ch4", HostId = 4 },
                    new Planet() { Id = 6, Name = "KOI-2020 Aa", Distance = 3.04f, Diameter = 5.1f, Mass = 5.9f, Molecules = "Na, C02, Ch4, H2O", HostId = 5 },
                    new Planet() { Id = 7, Name = "KOI-2020 Ba", Distance = 0.26f, Diameter = 0.7f, Mass = 0.6f, Molecules = "H2O, Nh3, H, C, C02", HostId = 6 },
                    new Planet() { Id = 8, Name = "KOI-2020 Bb", Distance = 0.271f, Diameter = 1.2f, Mass = 1.07f, Molecules = string.Empty, HostId = 6 },
                    new Planet() { Id = 9, Name = "EPIC-285503580 A", Distance = 7.77f, Diameter = 8.35f, Mass = 9.7f, Molecules = "H2O, Nh3, H, C02", HostId = 7 },
                    new Planet() { Id = 10, Name = "Gl 726 Aa", Distance = 17.8f, Diameter = 136.7f, Mass = 98.9f, Molecules = "Na, Nh3, H, C, C02, Ch4", HostId = 8 },
                };

                modelBuilder.Entity<StarSystem>().HasData(systems);
                modelBuilder.Entity<Star>().HasData(stars);
                modelBuilder.Entity<Planet>().HasData(planets);
            }
        }
    }
}
