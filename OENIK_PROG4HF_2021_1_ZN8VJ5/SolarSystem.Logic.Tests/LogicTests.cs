using System;

[assembly: CLSCompliant(true)]

namespace SolarSystem.Logic.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using SolarSystem.Data.Models;
    using SolarSystem.Repository;

    /// <summary>
    /// Contains all tests done on Logic layer.
    /// </summary>
    [TestFixture]
    public class LogicTests
    {
        private Mock<IStarSystemRepository> mockedSystemRepo;
        private Mock<IStarRepository> mockedStarRepo;
        private Mock<IPlanetRepository> mockedPlanetRepo;

        /// <summary>
        /// Tests if we add new entity then we get back it's id.
        /// Add method should only run once.
        /// </summary>
        [Test]
        public void TestAddPlanet()
        {
            // Arrange
            Mock<IStarSystemRepository> mockedSystemRepo = new Mock<IStarSystemRepository>(MockBehavior.Loose);
            Mock<IStarRepository> mockedStarRepo = new Mock<IStarRepository>(MockBehavior.Loose);
            Mock<IPlanetRepository> mockedPlanetRepo = new Mock<IPlanetRepository>(MockBehavior.Loose);

            mockedPlanetRepo.Setup(repo => repo.Add(
                It.IsAny<string>(),
                It.IsAny<float>(),
                It.IsAny<float>(),
                It.IsAny<float>(),
                It.IsAny<string>(),
                It.IsAny<int>())).Returns(99);

            Catalog catalog = new Catalog(mockedSystemRepo.Object, mockedStarRepo.Object, mockedPlanetRepo.Object);

            // Act
            var id = catalog.AddPlanet(string.Empty, 0f, 0f, 0f, string.Empty, 1);

            // Assert
            Assert.That(id, Is.EqualTo(99));
            mockedPlanetRepo.Verify(
                repo => repo.Add(
                It.IsAny<string>(),
                It.IsAny<float>(),
                It.IsAny<float>(),
                It.IsAny<float>(),
                It.IsAny<string>(),
                It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Tests that Remove method only runs once.
        /// </summary>
        [Test]
        public void TestRemoveStar()
        {
            // Arrange
            Mock<IStarSystemRepository> mockedSystemRepo = new Mock<IStarSystemRepository>(MockBehavior.Loose);
            Mock<IStarRepository> mockedStarRepo = new Mock<IStarRepository>(MockBehavior.Loose);
            Mock<IPlanetRepository> mockedPlanetRepo = new Mock<IPlanetRepository>(MockBehavior.Loose);

            mockedStarRepo.Setup(repo => repo.Remove(It.IsAny<int>())).Returns(true);
            Catalog catalog = new Catalog(mockedSystemRepo.Object, mockedStarRepo.Object, mockedPlanetRepo.Object);

            // Act
            var result = catalog.RemoveStar(99);

            // Assert
            mockedStarRepo.Verify(repo => repo.Remove(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// During simulation stars can change type, mass, temperature. There is a random factor
        /// to that change, but with the given parameters the transition should always happen.
        /// We will simulation 2 stars. Each star has 2 transitions, so there should be 4 transitions in total.
        /// repo.ChangeType gets called when a transition happens, so repo.ChangeType should run exactly 4 times.
        /// </summary>
        [Test]
        public void TestUpdateStar_TestSimulationRunForward()
        {
            Mock<IStarRepository> mockedStarRepo = new Mock<IStarRepository>(MockBehavior.Loose);

            List<Star> stars = new List<Star>()
            {
                new Star() { Id = 1, Mass = 100 },
                new Star() { Id = 2, Mass = 50 },
            };

            mockedStarRepo.Setup(repo => repo.GetAll()).Returns(stars.AsQueryable());
            mockedStarRepo.Setup(repo => repo.IsMainSequence(It.IsAny<Star>())).Returns(true);
            mockedStarRepo.Setup(repo => repo.InRedGiantPhase(It.IsAny<Star>())).Returns(true);
            mockedStarRepo.Setup(repo => repo.ChangeType(It.IsAny<int>(), It.IsAny<StellarType>()));

            Simulation sim = new Simulation(mockedStarRepo.Object);

            // Act
            sim.RunForward(1000);

            // Assert
            mockedStarRepo.Verify(repo => repo.ChangeType(It.IsAny<int>(), It.IsAny<StellarType>()), Times.Exactly(4));
        }

        /// <summary>
        /// This test is the same as TestSimulationRunForward() but it is testing Simulation.RunBackward().
        /// </summary>
        [Test]
        public void TestUpdateStar_TestSimulationRunBackward()
        {
            Mock<IStarRepository> mockedStarRepo = new Mock<IStarRepository>(MockBehavior.Loose);

            List<Star> stars = new List<Star>()
            {
                new Star() { Id = 1, Mass = 100 },
                new Star() { Id = 2, Mass = 50 },
            };

            mockedStarRepo.Setup(repo => repo.GetAll()).Returns(stars.AsQueryable());
            mockedStarRepo.Setup(repo => repo.IsStellarRemnant(It.IsAny<Star>())).Returns(true);
            mockedStarRepo.Setup(repo => repo.InRedGiantPhase(It.IsAny<Star>())).Returns(true);
            mockedStarRepo.Setup(repo => repo.ChangeType(It.IsAny<int>(), It.IsAny<StellarType>()));

            Simulation sim = new Simulation(mockedStarRepo.Object);

            // Act
            sim.RunBackward(1000);

            // Assert
            mockedStarRepo.Verify(repo => repo.ChangeType(It.IsAny<int>(), It.IsAny<StellarType>()), Times.Exactly(4));
        }

        /// <summary>
        /// Tests that GetOne only runs one.
        /// </summary>
        [Test]
        public void TestGetOneStarSystem()
        {
            // Arrange
            Mock<IStarSystemRepository> mockedSystemRepo = new Mock<IStarSystemRepository>(MockBehavior.Loose);
            Mock<IStarRepository> mockedStarRepo = new Mock<IStarRepository>(MockBehavior.Loose);
            Mock<IPlanetRepository> mockedPlanetRepo = new Mock<IPlanetRepository>(MockBehavior.Loose);

            mockedSystemRepo.Setup(repo => repo.GetOne(It.IsAny<int>())).Returns(It.IsAny<StarSystem>());

            Catalog catalog = new Catalog(mockedSystemRepo.Object, mockedStarRepo.Object, mockedPlanetRepo.Object);

            // Act
            var result = catalog.GetOneStarSystem(2);

            // Assert
            mockedSystemRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Tests if GetAllStars really returns all entities from the repo.
        /// And it only gets called once.
        /// </summary>
        [Test]
        public void TestGetAllStars()
        {
            // Arrange
            Mock<IStarSystemRepository> mockedSystemRepo = new Mock<IStarSystemRepository>(MockBehavior.Loose);
            Mock<IStarRepository> mockedStarRepo = new Mock<IStarRepository>(MockBehavior.Loose);
            Mock<IPlanetRepository> mockedPlanetRepo = new Mock<IPlanetRepository>(MockBehavior.Loose);

            List<Star> stars = new List<Star>()
            {
                new Star() { Id = 1, Name = "Kepler-127A" },
                new Star() { Id = 2, Name = "Kepler-127B" },
                new Star() { Id = 3, Name = "KOI-4955" },
            };

            List<Star> expected = new List<Star>()
            {
               new Star() { Id = 2, Name = "Kepler-127B" },
               new Star() { Id = 3, Name = "KOI-4955" },
               new Star() { Id = 1, Name = "Kepler-127A" },
            };

            mockedStarRepo.Setup(repo => repo.GetAll()).Returns(stars.AsQueryable());

            ICatalog catalog = new Catalog(
                mockedSystemRepo.Object,
                mockedStarRepo.Object,
                mockedPlanetRepo.Object);

            // Act
            var result = catalog.GetAllStars();

            // Assert
            Assert.That(result, Is.EquivalentTo(expected));
            mockedStarRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// Tests if GetHabitablePlanets() returns the correct results.
        /// </summary>
        [Test]
        public void TestGetHabitablePlanets()
        {
            // Arrange
            var logic = this.CreateLogic();
            List<Planet> expected = new List<Planet>()
            {
                 new Planet() { Name = "KOI-4955Aa", Distance = 0.55f, HostId = 3, Molecules = string.Empty },
                 new Planet() { Name = "Kepler-128Ba", Distance = 1.27f, HostId = 2, Molecules = "H2O, CH4" },
                 new Planet() { Name = "KOI-4955Ab", Distance = 0.57f, HostId = 3, Molecules = "H2O, O, NH3, CH4" },
            };

            // Act
            var result = logic.GetHabitablePlanets();

            // Assert
            Assert.That(result, Is.EquivalentTo(expected));
            this.mockedStarRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.mockedPlanetRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// Tests if GetSuperhabitablePlanets() returns the correct results.
        /// </summary>
        [Test]
        public void TestGetSuperhabitablePlanets()
        {
            // Arrange
            var logic = this.CreateLogic();
            List<Planet> expected = new List<Planet>()
            {
                 new Planet() { Name = "Kepler-128Ba", Distance = 1.27f, HostId = 2, Molecules = "H2O, CH4" },
                 new Planet() { Name = "KOI-4955Ab", Distance = 0.57f, HostId = 3, Molecules = "H2O, O, NH3, CH4" },
            };

            // Act
            var result = logic.GetSuperhabitablePlanets();

            // Assert
            Assert.That(result, Is.EquivalentTo(expected));
            this.mockedStarRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.mockedPlanetRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// Tests if GetStarsWithManyHabitablePlanets() returns the correct results.
        /// </summary>
        [Test]
        public void TestGetStarsWithManyHabitablePlanets()
        {
            // Arrange
            var logic = this.CreateLogic();
            List<Star> expected = new List<Star>()
            {
                new Star() { Id = 3, Name = "KOI-4955A", HabitableZone = 0.56f },
            };

            // Act
            var result = logic.GetStarsWithManyHabitablePlanets();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
            this.mockedStarRepo.Verify(repo => repo.GetAll(), Times.Exactly(3));
            this.mockedPlanetRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// Tests if NearestHabitableStar() returns the correct results.
        /// </summary>
        [Test]
        public void TestGetNearestHabitableStar()
        {
            // Arrange
            var logic = this.CreateLogic();
            List<Star> expected = new List<Star>()
            {
                new Star() { Id = 2, Name = "Kepler-128B", HabitableZone = 1.3f },
            };

            // Act
            var result = logic.NearestHabitableStar();

            // Assert
            Assert.That(result, Is.EqualTo(result));
            this.mockedStarRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            this.mockedPlanetRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.mockedSystemRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// This method tests what happens when there are more then one habitable stars with the same distance.
        /// </summary>
        [Test]
        public void TestNearestHabitableStar_WhenThereAreTwoStarsWithTheSameDistance()
        {
            // Arrange
            Mock<IStarSystemRepository> mockedSystemRepo = new Mock<IStarSystemRepository>(MockBehavior.Loose);
            Mock<IStarRepository> mockedStarRepo = new Mock<IStarRepository>(MockBehavior.Loose);
            Mock<IPlanetRepository> mockedPlanetRepo = new Mock<IPlanetRepository>(MockBehavior.Loose);

            List<StarSystem> systems = new List<StarSystem>()
            {
                new StarSystem() { Name = "Kepler-128", Distance = 5.4f },
            };

            List<Star> stars = new List<Star>()
            {
                new Star() { Id = 2, Name = "Kepler-128B", HabitableZone = 1.3f },
                new Star() { Id = 1, Name = "Kepler-128A", HabitableZone = 2.2f },
            };

            List<Planet> planets = new List<Planet>()
            {
                new Planet() { Name = "Kepler-128Ba", Distance = 1.27f, HostId = 2, Molecules = "H2O, CH4" },
                new Planet() { Name = "Kepler-128Bb", Distance = 9.4f, HostId = 2, Molecules = "NH3, O, H2O" },
                new Planet() { Name = "Kepler-128Aa", Distance = 0.4f, HostId = 1, Molecules = string.Empty },
                new Planet() { Name = "Kepler-128Ab", Distance = 2.2f, HostId = 1, Molecules = "H2O, CH4" },
            };

            Star expectedStar = new Star() { Id = 1, Name = "Kepler-128A", HabitableZone = 2.2f };
            StarWithDistance expected = new StarWithDistance(expectedStar, 5.4f);

            mockedSystemRepo.Setup(repo => repo.GetAll()).Returns(systems.AsQueryable());
            mockedStarRepo.Setup(repo => repo.GetAll()).Returns(stars.AsQueryable());
            mockedPlanetRepo.Setup(repo => repo.GetAll()).Returns(planets.AsQueryable());

            var logic = new Observatory(
                mockedSystemRepo.Object,
                mockedStarRepo.Object,
                mockedPlanetRepo.Object);

            // Act
            var result = logic.NearestHabitableStar();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
            mockedStarRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            mockedPlanetRepo.Verify(repo => repo.GetAll(), Times.Once);
            mockedSystemRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// Creates logic with mocked repos for testing methods in IObservatory.
        /// </summary>
        /// <returns>IObservatory implementation with mocked repositories.</returns>
        private IObservatory CreateLogic()
        {
            this.mockedSystemRepo = new Mock<IStarSystemRepository>(MockBehavior.Loose);
            this.mockedStarRepo = new Mock<IStarRepository>(MockBehavior.Loose);
            this.mockedPlanetRepo = new Mock<IPlanetRepository>(MockBehavior.Loose);

            List<StarSystem> systems = new List<StarSystem>()
            {
                new StarSystem() { Name = "Kepler-128", Distance = 5.4f },
                new StarSystem() { Name = "KOI-4955", Distance = 9.4f },
                new StarSystem() { Name = "EPIC-77014581", Distance = 3.4f },
            };

            List<Star> stars = new List<Star>()
            {
                new Star() { Id = 1, Name = "Kepler-128A", HabitableZone = 2.2f },
                new Star() { Id = 2, Name = "Kepler-128B", HabitableZone = 1.3f },
                new Star() { Id = 3, Name = "KOI-4955A", HabitableZone = 0.56f },
                new Star() { Id = 4, Name = "EPIC-77014581A", HabitableZone = 4.8f },
            };

            List<Planet> planets = new List<Planet>()
            {
                new Planet() { Name = "Kepler-128Aa", Distance = 0.4f, HostId = 1, Molecules = string.Empty },
                new Planet() { Name = "Kepler-128Ab", Distance = 7.2f, HostId = 1, Molecules = "H2O, CH4" },
                new Planet() { Name = "Kepler-128Ba", Distance = 1.27f, HostId = 2, Molecules = "H2O, CH4" },
                new Planet() { Name = "Kepler-128Bb", Distance = 9.4f, HostId = 2, Molecules = "NH3, O, H2O" },
                new Planet() { Name = "Kepler-128c", Distance = 11.3f, HostId = 2, Molecules = "H2O" },
                new Planet() { Name = "KOI-4955Aa", Distance = 0.55f, HostId = 3, Molecules = string.Empty },
                new Planet() { Name = "KOI-4955Ab", Distance = 0.57f, HostId = 3, Molecules = "H2O, O, NH3, CH4" },
                new Planet() { Name = "EPIC-77014581Aa", Distance = 7.5f, HostId = 4, Molecules = string.Empty },
            };

            this.mockedSystemRepo.Setup(repo => repo.GetAll()).Returns(systems.AsQueryable());
            this.mockedStarRepo.Setup(repo => repo.GetAll()).Returns(stars.AsQueryable());
            this.mockedPlanetRepo.Setup(repo => repo.GetAll()).Returns(planets.AsQueryable());

            Observatory observatory = new Observatory(
                this.mockedSystemRepo.Object,
                this.mockedStarRepo.Object,
                this.mockedPlanetRepo.Object);

            return observatory;
        }
    }
}
