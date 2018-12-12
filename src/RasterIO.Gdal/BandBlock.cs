// Contributors:
//   James Domingo, Green Code LLC

using Landis.SpatialModeling;
using System;

namespace Landis.RasterIO.Gdal
{
    /// <summary>
    /// A block of data for a raster band.
    /// </summary>
    public class BandBlock<T>
        where T : struct
    {
        public T[] Buffer           { get; private set; }
        public BlockDimensions Size { get; private set; }
        public int XOffset          { get; set; }
        public int YOffset          { get; set; }
        public int UsedPortionXSize { get; set; }
        public int UsedPortionYSize { get; set; }
        public int PixelSpace       { get; private set; }
        public int LineSpace        { get; private set; }

        public BandBlock(BlockDimensions blockDimensions)
        {
            int bufferLength = blockDimensions.XSize * blockDimensions.YSize;
            Buffer = new T[bufferLength];

            Size = blockDimensions;

            PixelSpace = Band<T>.ComputeSize();
            LineSpace = PixelSpace * blockDimensions.XSize;
        }
    }
}
