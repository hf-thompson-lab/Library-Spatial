// Contributors:
//   James Domingo, Green Code LLC

using Landis.SpatialModeling;
using System;

namespace Landis.RasterIO
{
    /// <summary>
    /// Accessor for getting a particular pixel band as another data type.
    /// </summary>
    public class PixelBandGetter<TPixelBand, TRasterBand> : PixelBandAccessor<TPixelBand>, IPixelBandGetter<TRasterBand>
        where TPixelBand : struct
        where TRasterBand : struct
    {
        private Converter<TPixelBand, TRasterBand> converter;

        public PixelBandGetter(PixelBand                          pixelBand,
                               Converter<TPixelBand, TRasterBand> converter)
            : base(pixelBand)
        {
            this.converter = converter;
        }

        public TRasterBand GetValue()
        {
            return converter(pixelBand.Value);
        }
    }
}
