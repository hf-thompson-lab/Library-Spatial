// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.SpatialModeling
{
    /// <summary>
    /// A factory that produces data structures for landscapes.
    /// </summary>
    public interface ILandscapeFactory
    {
        /// <summary>
        /// Creates a new landscape using an input grid of active sites.
        /// </summary>
        /// <param name="activeSites">
        /// A grid that indicates which sites are active.
        /// </param>
        ILandscape CreateLandscape(IInputGrid<bool> activeSites);

        //---------------------------------------------------------------------

        /// <summary>
        /// Creates a new landscape using an indexable grid of active sites.
        /// </summary>
        /// <param name="activeSites">
        /// A grid that indicates which sites are active.
        /// </param>
        ILandscape CreateLandscape(IIndexableGrid<bool> activeSites);
    }
}
