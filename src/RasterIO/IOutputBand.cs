// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.RasterIO
{
    /// <summary>
    /// Output raster band
    /// </summary>
    public interface IOutputBand
    {
        /// <summary>
        /// Write the value from the corresponding band in the buffer pixel
        /// to the raster band.
        /// </summary>
        void WriteValueFromBufferPixel();

        /// <summary>
        /// Flush any data that has not yet been written to the filesystem.
        /// </summary>
        void Flush();
    }
}
