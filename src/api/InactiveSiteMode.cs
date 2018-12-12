// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

namespace Landis.SpatialModeling
{
    /// <summary>
    /// Indicates how values at inactive sites are handled for a site variable.
    /// </summary>
    public enum InactiveSiteMode
    {
        /// <summary>
        /// All the inactive sites share one value (memory efficient).
        /// </summary>
        Share1Value,

        /// <summary>
        /// Each inactive site has its own distinct (separate) value.
        /// </summary>
        DistinctValues
    }
}
