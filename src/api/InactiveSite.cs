// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

namespace Landis.SpatialModeling
{
    /// <summary>
    /// Provides information about inactive sites on a landscape.
    /// </summary>
    public static class InactiveSite
    {
        /// <summary>
        /// The data index assigned to inactive sites.
        /// </summary>
        public const uint DataIndex = 0;

        //---------------------------------------------------------------------

        /// <summary>
        /// Creates a new inactive site on a landscape.
        /// </summary>
        /// <param name="landscape">
        ///  The landscape where the site is located.
        /// </param>
        /// <param name="location">
        ///  The location of the site.
        /// </param>
        internal static Site Create(ILandscape landscape,
                                    Location   location)
        {
            return new Site(landscape, location, DataIndex);
        }

    }
}
