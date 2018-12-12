// Contributors:
//   James Domingo, Green Code LLC

using Landis.SpatialModeling;

namespace Landis.Landscapes
{
    public class LandscapeFactory : ILandscapeFactory
    {
        /// <summary>
        /// Creates a new landscape using an input grid of active sites.
        /// </summary>
        /// <param name="activeSites">
        /// A grid that indicates which sites are active.
        /// </param>
        public ILandscape CreateLandscape(IInputGrid<bool> activeSites)
        {
            return new Landscape(activeSites);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Creates a new landscape using an indexable grid of active sites.
        /// </summary>
        /// <param name="activeSites">
        /// A grid that indicates which sites are active.
        /// </param>
        public ILandscape CreateLandscape(IIndexableGrid<bool> activeSites)
        {
            return new Landscape(activeSites);
        }
    }
}
