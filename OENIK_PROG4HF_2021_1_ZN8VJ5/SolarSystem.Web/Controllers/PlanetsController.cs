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
    /// Controller for Planets page.
    /// </summary>
    public class PlanetsController : Controller
    {
        private ICatalog logic;
        private IMapper mapper;
        private PlanetsViewModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetsController"/> class.
        /// </summary>
        /// <param name="logic">Interaction logic with the database.</param>
        /// <param name="mapper">Data.Planet to Web.Planet mapper.</param>
        public PlanetsController(ICatalog logic, IMapper mapper)
        {
            this.logic = logic;
            this.mapper = mapper;

            this.model = new PlanetsViewModel();
            this.model.EditedPlanet = new Planet();
            this.model.EditedPlanet.Stars = this.logic.GetAllStars();

            IList<Data.Models.Planet> planets = this.logic.GetAllPlanets();
            this.model.ListOfPlanets = this.mapper.Map<IList<Data.Models.Planet>, List<Web.Models.Planet>>(planets);
        }

        /// <summary>
        /// GET: index page.
        /// </summary>
        /// <returns>PlanetsIndex view.</returns>
        public IActionResult Index()
        {
            this.ViewData["editAction"] = "AddNew";
            return this.View("PlanetsIndex", this.model);
        }

        /// <summary>
        /// GET for planet datasheet.
        /// </summary>
        /// <param name="id">Id of the planet.</param>
        /// <returns>PlanetDatasheet view.</returns>
        public ActionResult Details(int id)
        {
            return this.View("PlanetDatasheet", this.GetWebPlanet(id));
        }

        /// <summary>
        /// Removes a planet from the database.
        /// </summary>
        /// <param name="id">Id of the planet.</param>
        /// <returns>PlanetsIndex view.</returns>
        public ActionResult Remove(int id)
        {
            this.TempData["editResult"] = "Delete FAIL";
            if (this.logic.RemovePlanet(id))
            {
                this.TempData["editResult"] = "Delete OK";
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        /// <summary>
        /// GET
        /// Edits a planet in the database.
        /// </summary>
        /// <param name="id">Id of the planet.</param>
        /// <returns>PlanetsIndex view.</returns>
        public ActionResult Edit(int id)
        {
            this.ViewData["editAction"] = "Edit";
            this.model.EditedPlanet = this.GetWebPlanet(id);
            return this.View("PlanetsIndex", this.model);
        }

        /// <summary>
        /// POST
        /// Edits a planet in the database.
        /// </summary>
        /// <param name="planet">New or modified planet.</param>
        /// <param name="editAction">Indicates whether the planet should be added or updated.</param>
        /// <returns>PlanetsIndex view.</returns>
        [HttpPost]
        public ActionResult Edit(Web.Models.Planet planet, string editAction)
        {
            if (this.ModelState.IsValid && planet != null)
            {
                this.TempData["editResult"] = "Edit OK";
                if (editAction == "AddNew")
                {
                    try
                    {
                        // TODO: hostId?
                        this.logic.AddPlanet(planet.Name, planet.Mass, planet.Distance, planet.Distance, planet.Molecules, planet.HostId);
                    }
                    catch (ArgumentException ex)
                    {
                        this.TempData["editResult"] = "Insert FAIL: " + ex.Message;
                    }
                }
                else
                {
                    try
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
                    catch (ArgumentException)
                    {
                        this.TempData["editResult"] = "Edit FAIL";
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }
            else
            {
                this.ViewData["editAction"] = "Edit";
                if (planet != null)
                {
                    planet.Stars = this.logic.GetAllStars();
                }

                this.model.EditedPlanet = planet;
                return this.View("PlanetsIndex", this.model);
            }
        }

        private Web.Models.Planet GetWebPlanet(int id)
        {
            Planet planet = this.mapper.Map<Data.Models.Planet, Web.Models.Planet>(this.logic.GetOnePlanet(id));
            planet.Stars = this.logic.GetAllStars();
            return planet;
        }
    }
}
