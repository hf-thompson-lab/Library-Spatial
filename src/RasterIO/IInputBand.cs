// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.RasterIO
{
    /// <summary>
    /// Input raster band
    /// </summary>
    public interface IInputBand
    {
        /// <summary>
        /// Read the value from the raster band into the corresponding band of
        /// the buffer pixel.
        /// </summary>
        void ReadValueIntoBufferPixel();
    }
}
