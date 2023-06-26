namespace SolarSystem.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Extendable result class.
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// Indicates whether or not the given operation was successful.
        /// </summary>
        public bool OperationResult { get; set; }
    }
}
