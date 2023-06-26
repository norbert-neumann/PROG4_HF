using System;

[assembly: CLSCompliant(true)]

namespace SolarSystem.Program
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using ConsoleTools;
    using Microsoft.EntityFrameworkCore;
    using SolarSystem.Data.Models;
    using SolarSystem.Logic;
    using SolarSystem.Repository;

    /// <summary>
    /// This class is responsible for initializing each layer and
    /// providing an interactive interface for the user.
    /// Each logic function has a separate method here that formats and prints
    /// the functions' results.
    /// </summary>
    public static class App
    {
        private static bool startedRunning;
        private static bool finishedRunning;
        private static List<Planet> result;

        /// <summary>
        /// This is where the layers and the menus get initialized.
        /// </summary>
        public static void Main()
        {
            SolarSystemDbContext ctx = new SolarSystemDbContext();
            IStarSystemRepository systemRepo = new StarSystemRepository(ctx);
            IStarRepository starRepo = new StarRepository(ctx);
            IPlanetRepository planetRepo = new PlanetRepository(ctx);
            Simulation simulation = new Simulation(starRepo);
            IObservatory observatory = new Observatory(systemRepo, starRepo, planetRepo);
            ICatalog catalog = new Catalog(systemRepo, starRepo, planetRepo);

            var getMenu = new ConsoleMenu();
            getMenu.Add(">> GET ALL SOLAR SYSTEMS", () => GetAllSystems(catalog));
            getMenu.Add(">> GET ALL STARS", () => GetAllStars(catalog));
            getMenu.Add(">> GET ALL PLANETS", () => GetAllPlanets(catalog));
            getMenu.Add(">> GET ONE SOLAR SYSTEM", () => GetOneStarSystem(catalog));
            getMenu.Add(">> GET ONE STAR", () => GetOneStar(catalog));
            getMenu.Add(">> GET ONE PLANET", () => GetOnePlanet(catalog));
            getMenu.Add(">> BACK", () => CloseMenu(getMenu));

            var subMenu = new ConsoleMenu();
            subMenu.Add(">> ADD NEW SOLAR SYSTEM", () => AddSolarSystem(catalog));
            subMenu.Add(">> ADD NEW STAR", () => AddStar(catalog));
            subMenu.Add(">> ADD NEW PLANET", () => AddPlanet(catalog));
            subMenu.Add(">> REMOVE SOLAR SYSTEM", () => RemoveSolarSystem(catalog));
            subMenu.Add(">> REMOVE STAR", () => RemoveStar(catalog));
            subMenu.Add(">> REMOVE PLANET", () => RemovePlanet(catalog));
            subMenu.Add(">> GET...", () => ShowMenu(getMenu));
            subMenu.Add(">> BACK", () => CloseMenu(subMenu));

            var mainMenu = new ConsoleMenu();
            mainMenu.Add(">> LIST ALL", () => GetAll(catalog));
            mainMenu.Add(">> GET HABITABLE PLANETS", () => HabitablePlanets(observatory));
            mainMenu.Add(">> GET HABITABLE PLANETS ASYNC", () => HabitablePlanetsAsync(observatory));
            mainMenu.Add(">> STARS WITH MANY HABITABLE PLANETS", () => StarsWithManyHabitablePlanets(observatory));
            mainMenu.Add(">> GET SUPERHABITABLE PLANETS", () => SuperhabitablePlanets(observatory));
            mainMenu.Add(">> NEAREST HABITABLE STAR", () => NearestHabitableStar(observatory));
            mainMenu.Add(">> RUN SIMULATION", () => RunSimulation(simulation));
            mainMenu.Add(">> MORE...", () => ShowMenu(subMenu));
            mainMenu.Add(">> CLOSE", () => CloseMenu(mainMenu));

            mainMenu.Show();
            ctx.Dispose();
        }

        private static void CloseMenu(ConsoleMenu menu)
        {
            menu.CloseMenu();
        }

        private static void ShowMenu(ConsoleMenu menu)
        {
            menu.Show();
        }

        private static void GetAll(ICatalog catalog)
        {
            Console.WriteLine(":: LIST ALL ::");

            Console.WriteLine(catalog);
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void HabitablePlanets(IObservatory observatory)
        {
            Console.WriteLine(":: HABITABLE PLANETS ::");

            observatory.GetHabitablePlanets().ToList().
                ForEach(x => Console.WriteLine(x));
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static async void HabitablePlanetsAsync(IObservatory observatory)
        {
            if (!startedRunning)
            {
                // Console.WriteLine("Begining...");
                startedRunning = true;
                result = await observatory.GetHabitablePlanetsAsync().ConfigureAwait(false);
                finishedRunning = true;
            }
            else if (startedRunning && finishedRunning)
            {
                Console.WriteLine(":: HABITABLE PLANETS ::");
                result.ForEach(p => Console.WriteLine(p));
                startedRunning = false;
                finishedRunning = false;
                Console.Write("\nPress enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Not finished yet, come back later.");
                Console.Write("\nPress enter to continue...");
                Console.ReadLine();
            }
        }

        private static void StarsWithManyHabitablePlanets(IObservatory observatory)
        {
            Console.WriteLine(":: STARS WITH MANY HABITABLE PLANETS ::");

            observatory.GetStarsWithManyHabitablePlanets().ToList().
                ForEach(x => Console.WriteLine(x));
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void SuperhabitablePlanets(IObservatory observatory)
        {
            Console.WriteLine(":: SUPERHABITABLE PLANETS ::");

            observatory.GetSuperhabitablePlanets().ToList().
                ForEach(x => Console.WriteLine(x));
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void NearestHabitableStar(IObservatory observatory)
        {
            Console.WriteLine(":: NEAREST HABITABLE STAR::");
            Console.WriteLine(observatory.NearestHabitableStar());
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void AddSolarSystem(ICatalog catalog)
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("\nDistance: ");
            float distance;
            bool parseResult0 = float.TryParse(Console.ReadLine(), out distance);

            Console.Write("\nAge: ");
            float age;
            bool parseResult1 = float.TryParse(Console.ReadLine(), out age);

            Console.Write("\nAlternative name (optional): ");
            string altName = Console.ReadLine();

            Console.Write("\nCatalog type (optional): ");
            string catalogType = Console.ReadLine();

            Console.WriteLine();
            if (parseResult0 && parseResult1)
            {
                int id = catalog.AddStarSystem(name, distance, age, altName, catalogType);
                Console.WriteLine("\nSolar System added with id: " + id);
            }
            else
            {
                Console.WriteLine("Failed, check that distance and age are floats.");
            }

            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void AddStar(ICatalog catalog)
        {
            // string name, StellarType type, float temp, float mass, int systemId
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("\nType: ");
            StellarType type = StellarType.A;
            bool parseResult3 = true;
            switch (Console.ReadLine().ToUpper(CultureInfo.CurrentCulture))
            {
                case "O": type = StellarType.O; break;
                case "B": type = StellarType.B; break;
                case "BLACKHOLE": type = StellarType.BlackHole; break;
                case "D": type = StellarType.D; break;
                case "F": type = StellarType.F; break;
                case "G": type = StellarType.G; break;
                case "K": type = StellarType.K; break;
                case "M": type = StellarType.M; break;
                case "NEUTRONSTAR": type = StellarType.NeutronStar; break;
                case "REDGIANT": type = StellarType.RedGiant; break;
                default:
                    parseResult3 = false;
                    break;
            }

            Console.Write("\nTemperature: ");
            float temp;
            bool parseResult0 = float.TryParse(Console.ReadLine(), out temp);

            Console.Write("\nMass: ");
            float mass;
            bool parseResult1 = float.TryParse(Console.ReadLine(), out mass);

            Console.Write("\nSystem id: ");
            int systemID;
            bool parseResult2 = int.TryParse(Console.ReadLine(), out systemID);

            Console.WriteLine();
            if (parseResult0 && parseResult1 && parseResult2 && parseResult3)
            {
                try
                {
                    int id = catalog.AddStar(name, type, temp, mass, systemID);
                    Console.WriteLine("\nStar added with id: " + id);
                }
                catch (DbUpdateException)
                {
                    Console.WriteLine("System id not found.");
                }
            }
            else
            {
                Console.WriteLine("Failed, check that temperature and mass are float, system id is int, type is an actual stellar type.");
            }

            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void AddPlanet(ICatalog catalog)
        {
            // string name, float mass, float distance, float diameter, string molecules, int hostId)
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("\nMass: ");
            float mass;
            bool parseResult0 = float.TryParse(Console.ReadLine(), out mass);

            Console.Write("\nDistance: ");
            float distance;
            bool parseResult1 = float.TryParse(Console.ReadLine(), out distance);

            Console.Write("\nDiameter: ");
            float diameter;
            bool parseResult2 = float.TryParse(Console.ReadLine(), out diameter);

            Console.Write("\nMolecules (optional): ");
            string molecules = Console.ReadLine();

            Console.Write("\nHost star's id: ");
            int hostId;
            bool parseResult3 = int.TryParse(Console.ReadLine(), out hostId);

            Console.WriteLine();
            if (parseResult0 && parseResult1 && parseResult2 && parseResult3)
            {
                try
                {
                    int id = catalog.AddPlanet(name, mass, distance, diameter, molecules, hostId);
                    Console.WriteLine("\nPlanet added with id: " + id);
                }
                catch (DbUpdateException)
                {
                    Console.WriteLine("Host id not found.");
                }
            }
            else
            {
                Console.WriteLine("Failed, check that mass, distance and diameter are floats, host id is int.");
            }

            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void RemoveSolarSystem(ICatalog catalog)
        {
            Console.Write("Id of the solar system: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Solar system {0}", catalog.RemoveStarSystem(id) ? "succesfully removed." : "not found.");
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void RemoveStar(ICatalog catalog)
        {
            Console.Write("Star's id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Star {0}", catalog.RemoveStar(id) ? "succesfully removed." : "not found.");
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void RemovePlanet(ICatalog catalog)
        {
            Console.Write("Planet's id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Planet {0}", catalog.RemovePlanet(id) ? "succesfully removed." : "not found.");
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void RunSimulation(Simulation sim)
        {
            Console.Write("Time (in billions of years): ");
            int dt;
            bool parseResult = int.TryParse(Console.ReadLine(), out dt);
            if (parseResult && dt >= 0)
            {
                Console.Write("Direction (forward/backward): ");
                if (Console.ReadLine().ToUpperInvariant() == "FORWARD")
                {
                    sim.RunForward(dt);
                }
                else
                {
                    sim.RunBackward(dt);
                }

                Console.WriteLine("Simulation done.");
            }
            else
            {
                Console.WriteLine("Failed, check that time is a non-negative whole number.");
            }

            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void GetOneStarSystem(ICatalog catalog)
        {
            Console.Write("\nSystem id: ");
            int systemID;
            bool parseResult = int.TryParse(Console.ReadLine(), out systemID);

            if (parseResult && systemID > 0)
            {
                Console.WriteLine(catalog.GetOneStarSystem(systemID));
            }
            else
            {
                Console.WriteLine("Failed, check that system id is a positive whole number.");
            }

            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void GetOneStar(ICatalog catalog)
        {
            Console.Write("\nStar id: ");
            int starID;
            bool parseResult = int.TryParse(Console.ReadLine(), out starID);

            if (parseResult && starID > 0)
            {
                Console.WriteLine(catalog.GetOneStar(starID));
            }
            else
            {
                Console.WriteLine("Failed, check that id is a positive whole number.");
            }

            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void GetOnePlanet(ICatalog catalog)
        {
            Console.Write("\nPlanet id: ");
            int planetID;
            bool parseResult = int.TryParse(Console.ReadLine(), out planetID);

            if (parseResult && planetID > 0)
            {
                Console.WriteLine(catalog.GetOnePlanet(planetID));
            }
            else
            {
                Console.WriteLine("Failed, check that id is a positive whole number.");
            }

            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void GetAllSystems(ICatalog catalog)
        {
            Console.WriteLine(":: ALL SOLAR SYSTEMS::");
            catalog.GetAllStarSystems().ToList().ForEach(system => Console.WriteLine(system));
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void GetAllStars(ICatalog catalog)
        {
            Console.WriteLine(":: ALL STARS::");
            catalog.GetAllStars().ToList().ForEach(star => Console.WriteLine(star));
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static void GetAllPlanets(ICatalog catalog)
        {
            Console.WriteLine(":: ALL PLANETS::");
            catalog.GetAllPlanets().ToList().ForEach(planet => Console.WriteLine(planet));
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }
    }
}
