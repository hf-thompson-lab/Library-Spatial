// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.SpatialModeling
{
    /// <summary>
    /// Information about a particular raster format that a raster factory
    /// supports.
    /// </summary>
    public class RasterFormat
    {
        /// <summary>
        /// A short code identifier for the format.
        /// </summary>
        /// <remarks>
        /// A GDAL format code.  Format codes are listed in the "Code"
        /// column in the table at http://gdal.org/formats_list.html.
        /// GDAL codes are used even if the raster I/O is not implemented
        /// with GDAL.
        /// </remarks>
        public string Code { get; private set; }

        /// <summary>
        /// The format's name.
        /// </summary>
        /// <remarks>
        /// By convention, GDAL format names are used.  These names are
        /// shown in the "Long Format Name" column in the table at
        /// http://gdal.org/formats_list.html.  GDAL names are used even
        /// if the raster I/O is not implemented with GDAL.
        /// </remarks>
        public string Name { get; private set; }

        /// <summary>
        /// Can the raster factory write output rasters in this format?
        /// </summary>
        public bool CanWrite { get; private set; }

        /// <summary>
        /// Construct a new instance with information about a raster format.
        /// </summary>
        /// <param name="code">The format's code identifier.</param>
        /// <param name="name">The format's name.</param>
        /// <param name="canWrite">Can the raster factory write output rasters
        /// in this format?</param>
        public RasterFormat(string code,
                            string name,
                            bool canWrite)
        {
            Code = code;
            Name = name;
            CanWrite = canWrite;
        }
    }
}
