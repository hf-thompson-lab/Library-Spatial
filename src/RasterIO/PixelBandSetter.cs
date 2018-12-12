// Contributors:
//   James Domingo, Green Code LLC

using Landis.SpatialModeling;
using System;

namespace Landis.RasterIO
{
    /// <summary>
    /// Accessor for setting a particular pixel band with another data type.
    /// </summary>
    public class PixelBandSetter<TPixelBand, TRasterBand> : PixelBandAccessor<TPixelBand>, IPixelBandSetter<TRasterBand>
        where TPixelBand : struct
        where TRasterBand : struct
    {
        private Converter<TRasterBand, TPixelBand> converter;  // TRasterBand -> TPixelBand

        public PixelBandSetter(PixelBand                          pixelBand,
                               Converter<TRasterBand, TPixelBand> converter)
            : base(pixelBand)
        {
            this.converter = converter;
        }

        public void SetValue(TRasterBand newValue)
        {
            pixelBand.Value = converter(newValue);
        }
    }
}
