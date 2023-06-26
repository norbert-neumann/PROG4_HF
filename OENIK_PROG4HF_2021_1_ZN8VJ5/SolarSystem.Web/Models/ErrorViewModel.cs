namespace SolarSystem.Web.Models
{
    /// <summary>
    /// Error VM.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// .
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// .
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}
