// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab
//   James Domingo, Green Code LLC

namespace Landis.SpatialModeling
{
    /// <summary>
    /// A file with raster data.
    /// </summary>
    public interface IRaster
        : System.IDisposable
    {
        /// <summary>
        /// The path used to open/create the raster.
        /// </summary>
        string Path
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The dimensions of the raster.
        /// </summary>
        Dimensions Dimensions
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Closes the raster, releasing any unmanaged resources.
        /// </summary>
        void Close();
    }
}
