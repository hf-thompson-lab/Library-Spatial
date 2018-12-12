// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.SpatialModeling
{
    /// <summary>
    /// A raster factory whose configuration can be changed.
    /// </summary>
    public interface IConfigurableRasterFactory : IRasterFactory
    {
        /// <summary>
        /// Get information about a raster format.
        /// </summary>
        /// <param name="code">The format's code.</param>
        /// <remarks>
        /// If the raster factory does not recognize or support the format,
        /// then null is returned.
        /// </remarks>
        RasterFormat GetFormat(string code);

        /// <summary>
        /// Bind a file extension with a raster format.
        /// </summary>
        /// <param name="fileExtension">
        /// Must start with a period.
        /// </param>
        /// <param name="format">
        /// A raster format supported by the raster factory.
        /// </param>
        void BindExtensionToFormat(string       fileExtension,
                                   RasterFormat format);
    }
}
