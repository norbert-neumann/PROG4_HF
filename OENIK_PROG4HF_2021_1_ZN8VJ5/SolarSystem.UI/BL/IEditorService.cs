namespace SolarSystem.UI.BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SolarSystem.UI.Data;

    /// <summary>
    /// Edits an existing VM planet somehow.
    /// </summary>
    [CLSCompliant(false)]
    public interface IEditorService
    {
        /// <summary>
        /// Edits and existing VM planet.
        /// </summary>
        /// <param name="p">Planet to be modified.</param>
        /// <returns>True if the modification was succesful, false otherwise.</returns>
        bool EditPlanet(VMPlanet p);
    }
}
