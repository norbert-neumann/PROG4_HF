namespace SolarSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using SolarSystem.Logic;
    using SolarSystem.Web.Models;

    /// <summary>
    /// API CRUD controller.
    /// </summary>
    public class PlanetsApiController : Controller
    {
        private ICatalog logic;
        private IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetsApiController"/> class.
        /// </summary>
        /// <param name="logic">Logic to operate on.</param>
        /// <param name="mapper">Web.Planet -> Data.Planet mapper.</param>
        public PlanetsApiController(ICatalog logic, IMapper mapper)
        {
            this.logic = logic;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns all planets in the db as Web.Planet.
        /// </summary>
        /// <returns>All present planets in the db.</returns>
        [HttpGet]
        [ActionName("all")]
        public IEnumerable<Web.Models.Planet> GetAll()
        {
            var planets = this.logic.GetAllPlanets();
            return this.mapper.Map<IList<Data.Models.Planet>, List<Web.Models.Planet>>(planets);
        }

        /// <summary>
        /// Returns all stars form the db. This is used when the user selects a star.
        /// </summary>
        /// <returns>All stars present in db.</returns>
        [HttpGet]
        [ActionName("stars")]
        public IEnumerable<Data.Models.Star> GetAllStars()
        {
            var stars = this.logic.GetAllStars();
            return stars.ToList();
        }

        /// <summary>
        /// Deletes one planet from the db.
        /// </summary>
        /// <param name="id">Id of the planet that needs to be deleted.</param>
        /// <returns>ApiResult - true if the operation was successful.</returns>
        [HttpGet]
        [ActionName("delete")]
        public ApiResult DeleteOnePlanet(int id)
        {
            return new ApiResult() { OperationResult = this.logic.RemovePlanet(id) };
        }

        /// <summary>
        /// Adds a new planet to the database.
        /// </summary>
        /// <param name="planet">Planet that needs to be added to the db.</param>
        /// <returns>ApiResult-true if the operation was succesful.</returns>
        [HttpPost]
        [ActionName("add")]
        public ApiResult AddOnePlanet(Models.Planet planet)
        {
            bool success = true;
            try
            {
                if (planet != null)
                {
                    this.logic.AddPlanet(planet.Name, planet.Mass, planet.Distance, planet.Distance, planet.Molecules, planet.HostId);
                }
                else
                {
                    success = false;
                }
            }
            catch
            {
                success = false;
            }

            return new ApiResult() { OperationResult = success };
        }

        /// <summary>
        /// Changes an existing planet's properties.
        /// </summary>
        /// <param name="planet">Changed planet instance.</param>
        /// <returns>ApiResult-true if the operation was successful.</returns>
        [HttpPost]
        [ActionName("update")]
        public ApiResult UpdateOnePlanet(Models.Planet planet)
        {
            bool success = true;
            try
            {
                if (planet != null)
                {
                    this.logic.UpdatePlanet(new Data.Models.Planet()
                    {
                        Id = planet.Id,
                        Name = planet.Name,
                        Diameter = planet.Diameter,
                        Distance = planet.Distance,
                        Mass = planet.Mass,
                        Molecules = planet.Molecules,
                        Host = this.logic.GetOneStar(planet.HostId),
                    });
                }
                else
                {
                    success = false;
                }
            }
            catch
            {
                success = false;
            }

            return new ApiResult() { OperationResult = success };
        }
    }
}
