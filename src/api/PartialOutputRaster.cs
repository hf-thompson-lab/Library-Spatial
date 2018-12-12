// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

namespace Landis.SpatialModeling
{
    /// <summary>
    /// Provides events and methods for handling output rasters that are
    /// closed before all their data is written.
    /// </summary>
    public static class PartialOutputRaster
    {
        /// <summary>
        /// Signature for methods called with a partial output raster is
        /// closed.
        /// </summary>
        public delegate void CloseEventHandler(string     path,
                                               Dimensions dimensions,
                                               int        pixelsWritten);

        //---------------------------------------------------------------------

        /// <summary>
        /// The event when a partial output raster is called.
        /// </summary>
        public static event CloseEventHandler CloseEvent;

        //---------------------------------------------------------------------

        /// <summary>
        /// Called when a partial output raster is closed.
        /// </summary>
        public static void Closed(string     path,
                                  Dimensions dimensions,
                                  int        pixelsWritten)
        {
            if (CloseEvent != null)
                CloseEvent(path, dimensions, pixelsWritten);
        }
    }
}
