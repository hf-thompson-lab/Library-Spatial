// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.RasterIO
{
    /// <summary>
    /// Gets the value for a particular band in a pixel as a specific data type.
    /// </summary>
    public interface IPixelBandGetter<T>
        where T : struct
    {
        T GetValue();
    }
}
