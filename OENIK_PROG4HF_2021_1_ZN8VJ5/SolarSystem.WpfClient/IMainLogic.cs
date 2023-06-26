namespace SolarSystem.WpfClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Lists all ApiCrud methods.
    /// </summary>
    public interface IMainLogic
    {
        /// <summary>
        /// Calls the API and retursn all planets in db.
        /// </summary>
        /// <returns>All planets in db as VM planet.</returns>
        public IReadOnlyCollection<VMPlanet> ApiGetAllPlanets();

        /// <summary>
        /// Calls the API to delte a planet form the db.
        /// </summary>
        /// <param name="planet">Planet to be deleted.</param>
        public void ApiDeletePlanet(VMPlanet planet);

        /// <summary>
        /// Edits a planet with the given edit function.
        /// </summary>
        /// <param name="planet">Planet to be edited.</param>
        /// <param name="editor">Edit function that carries out the modification.</param>
        public void EditPlanet(VMPlanet planet, Func<VMPlanet, bool> editor);
    }
}
