namespace SolarSystem.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Ioc;

    /// <summary>
    /// This class extends SimpleIoc with IServiceLocator.
    /// </summary>
    [CLSCompliant(false)]
    public class MyIOC : SimpleIoc, IServiceLocator
    {
        /// <summary>
        /// Singleton instance(?).
        /// </summary>
        public static MyIOC Instance { get; private set; } = new MyIOC();
    }
}
