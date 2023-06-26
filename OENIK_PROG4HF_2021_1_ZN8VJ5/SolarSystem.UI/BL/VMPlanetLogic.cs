using System;

[assembly: CLSCompliant(true)]

namespace SolarSystem.UI.BL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight.Messaging;
    using SolarSystem.Data.Models;
    using SolarSystem.Logic;
    using SolarSystem.UI.Data;

    /// <summary>
    /// Executes the CRUD operations on the VM collection, and syncs it with the Data kayer.
    /// </summary>
    [CLSCompliant(false)]
    public class VMPlanetLogic : IPlanetLogic
    {
        /// <summary>
        /// Sercie used for modification.
        /// </summary>
        private IEditorService editorService;

        /// <summary>
        /// Messaging service.
        /// </summary>
        private IMessenger messengerService;

        /// <summary>
        /// Reference to the Logic layer, which will execute the CRUD operation in the Data layer.
        /// </summary>
        private ICatalog logic;

        /// <summary>
        /// Initializes a new instance of the <see cref="VMPlanetLogic"/> class.
        /// </summary>
        /// <param name="service">Service for modification.</param>
        /// <param name="messenger">Service for messaging.</param>
        /// <param name="logic">Logic for database access.</param>
        public VMPlanetLogic(IEditorService service, IMessenger messenger, ICatalog logic)
        {
            this.editorService = service;
            this.messengerService = messenger;
            this.logic = logic;
        }

        /// <inheritdoc/>
        public void AddPlanet(IList<VMPlanet> list)
        {
            VMPlanet newPlanet = new VMPlanet();
            if (this.editorService.EditPlanet(newPlanet) == true && list != null)
            {
                list.Add(newPlanet);
                this.logic.AddPlanet(newPlanet.Name, newPlanet.Mass, newPlanet.Distance, newPlanet.Diameter, newPlanet.Molecules, newPlanet.Host.Id);
                this.messengerService.Send("ADD OK", "LogicResult");
            }
            else
            {
                this.messengerService.Send("ADD CANCEL", "LogicResult");
            }
        }

        /// <inheritdoc/>
        public void DelPlanet(IList<VMPlanet> list, VMPlanet planet)
        {
            if (planet != null && list != null && list.Remove(planet))
            {
                this.logic.RemovePlanet(planet.Id);
                this.messengerService.Send("DELETE OK", "LogicResult");
            }
            else
            {
                this.messengerService.Send("DELETE FAILED", "LogicResult");
            }
        }

        /// <inheritdoc/>
        public IList<VMPlanet> GetAllPlanets()
        {
            return this.logic.GetAllPlanets().Select(dbPlanet => new VMPlanet(dbPlanet)).ToList();
        }

        /// <inheritdoc/>
        public void ModPlanet(VMPlanet planetToModify)
        {
            if (planetToModify == null)
            {
                this.messengerService.Send("EDIT FAILED", "LogicResult");
                return;
            }

            VMPlanet clone = new VMPlanet();
            clone.CopyFrom(planetToModify);

            if (this.editorService.EditPlanet(clone) == true)
            {
                planetToModify.CopyFrom(clone);
                this.logic.UpdatePlanet(planetToModify.AsDbPlanet());
                this.messengerService.Send("EDIT OK", "LogicResult");
            }
            else
            {
                this.messengerService.Send("EDIT CANCEL", "LogicResult");
            }
        }

        /// <inheritdoc/>
        public IList<Star> GetAllHosts()
        {
            return this.logic.GetAllStars().ToList();
        }
    }
}
