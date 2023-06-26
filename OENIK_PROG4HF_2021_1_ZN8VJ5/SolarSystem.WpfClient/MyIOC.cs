namespace SolarSystem.WpfClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Ioc;

    /// <summary>
    /// Custom simpleIoc implementation.
    /// </summary>
    public class MyIOC : SimpleIoc, IServiceLocator
    {
        /// <summary>
        /// Singleton instance(?).
        /// </summary>
        public static MyIOC Instance { get; private set; } = new MyIOC();
    }
}
