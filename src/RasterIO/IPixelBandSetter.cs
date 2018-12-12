// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.RasterIO
{
    /// <summary>
    /// Sets the value for a particular band in a pixel with a specific data type.
    /// </summary>
    public interface IPixelBandSetter<T>
        where T : struct
    {
        void SetValue(T newValue);
    }
}
