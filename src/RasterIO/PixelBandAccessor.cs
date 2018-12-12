// Contributors:
//   James Domingo, Green Code LLC

using Landis.SpatialModeling;

namespace Landis.RasterIO
{
    /// <summary>
    /// Accessor for getting or seting a particular pixel band.
    /// </summary>
    public abstract class PixelBandAccessor<T>
        where T : struct
    {
        protected Band<T> pixelBand;

        protected PixelBandAccessor(PixelBand pixelBand)
        {
            this.pixelBand = pixelBand as Band<T>;
            if (this.pixelBand == null)
                throw new System.ArgumentException("pixelBand is not a Band<T>");
        }
    }
}
