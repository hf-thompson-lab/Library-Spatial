// Contributors:
//   James Domingo, Green Code LLC

using Landis.SpatialModeling;
using System;

namespace Landis.RasterIO.Gdal
{
    public class InputBand<T> : IInputBand
        where T : struct
    {
        private RasterBandReader<T> rasterBandReader;
        private IPixelBandSetter<T> pixelBandSetter;
        private BlockDimensions blockDimensions;
        private BandBuffer<T> bandBuffer;
        private bool hasData;

        public InputBand(RasterBandReader<T> rasterBandReader,
                         IPixelBandSetter<T> pixelBandSetter)
        {
            this.rasterBandReader = rasterBandReader;
            this.pixelBandSetter = pixelBandSetter;
            blockDimensions = rasterBandReader.BlockSize;
            bandBuffer = new BandBuffer<T>(blockDimensions, new Dimensions(rasterBandReader.Rows, rasterBandReader.Columns));
            hasData = false;
        }

        /// <summary>
        /// Read the value from the raster band into the corresponding band of
        /// the buffer pixel.
        /// </summary>
        public void ReadValueIntoBufferPixel()
        {
            // Get the band value from the raster band
            if (bandBuffer.AtEnd)
                hasData = false;
            if (! hasData) {
                ReadBuffer();
                hasData = true;
            }
            T bandValue = bandBuffer.ReadValue();
            pixelBandSetter.SetValue(bandValue);
        }

        private void ReadBuffer()
        {
            bandBuffer.Reset();
            foreach (BandBlock<T> block in bandBuffer.Blocks)
                rasterBandReader.ReadBlock(block);
        }
    }
}
