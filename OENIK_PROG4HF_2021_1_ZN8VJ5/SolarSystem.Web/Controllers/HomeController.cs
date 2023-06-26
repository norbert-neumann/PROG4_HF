namespace SolarSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using SolarSystem.Web.Models;

    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// GET index method.
        /// </summary>
        /// <returns>Main view.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// GET index method.
        /// </summary>
        /// <returns>Main view.</returns>
        public IActionResult Privacy()
        {
            return this.View();
        }

        /// <summary>
        /// Calls error view.
        /// </summary>
        /// <returns>Error viewmodel.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
