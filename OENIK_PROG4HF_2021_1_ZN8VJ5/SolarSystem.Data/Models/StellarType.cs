namespace SolarSystem.Data.Models
{
    /// <summary>
    /// All posible stellar types. These classes will behave differently in the simulation.
    /// </summary>
    public enum StellarType
    {
        /// <summary>
        /// Star where temperature is in [60k - 30k] Kelvin.
        /// </summary>
        O,

        /// <summary>
        /// Star where temperature is in [30k - 10k] Kelvin.
        /// </summary>
        B,

        /// <summary>
        /// Star where temperature is in [10k - 7,5k] Kelvin.
        /// </summary>
        A,

        /// <summary>
        /// Star where temperature is in [7,5k - 6k] Kelvin.
        /// </summary>
        F,

        /// <summary>
        /// Star where temperature is in [6k - 5k] Kelvin.
        /// </summary>
        G,

        /// <summary>
        /// Star where temperature is in [5k - 3,5k] Kelvin.
        /// </summary>
        K,

        /// <summary>
        /// Star where temperature is in [3,5k - 0k] Kelvin.
        /// </summary>
        M,

        /// <summary>
        /// White dwarf (stellar remnant).
        /// </summary>
        D,

        /// <summary>
        /// Star which is in the red giant phase.
        /// </summary>
        RedGiant,

        /// <summary>
        /// Neutron star (stellar remnant).
        /// </summary>
        NeutronStar,

        /// <summary>
        /// Black hole (stellar remnant).
        /// </summary>
        BlackHole,
    }
}
