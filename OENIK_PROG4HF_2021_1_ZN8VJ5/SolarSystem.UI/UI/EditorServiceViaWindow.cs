namespace SolarSystem.UI.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SolarSystem.UI.BL;
    using SolarSystem.UI.Data;

    /// <summary>
    /// Implement editor service using EditorWindow.
    /// </summary>
    [CLSCompliant(false)]
    public class EditorServiceViaWindow : IEditorService
    {
        /// <inheritdoc/>
        public bool EditPlanet(VMPlanet p)
        {
            EditorWindow win = new EditorWindow(p);
            return win.ShowDialog() ?? false;
        }
    }
}
