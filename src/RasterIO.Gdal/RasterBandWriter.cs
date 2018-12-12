// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.RasterIO.Gdal
{
    /// <summary>
    /// A writer for a particular GDAL raster band.
    /// </summary>
    public class RasterBandWriter<T>
        where T : struct
    {
        private OSGeo.GDAL.Band gdalBand;
        private BlockDimensions blockDimensions;
        private GdalBandIO.WriteBlock<T> writeBlock;

        public RasterBandWriter(OSGeo.GDAL.Band          gdalBand,
                                GdalBandIO.WriteBlock<T> writeBlock)
        {
            this.gdalBand = gdalBand;
            this.writeBlock = writeBlock;

            gdalBand.GetBlockSize(out blockDimensions.XSize, out blockDimensions.YSize);
        }

        public BlockDimensions BlockSize
        {
            get {
                return blockDimensions;
            }
        }

        public int Rows
        {
            get {
                return gdalBand.YSize;
            }
        }

        public int Columns
        {
            get {
                return gdalBand.XSize;
            }
        }

        public void WriteBlock(BandBlock<T> block)
        {
            writeBlock(gdalBand, block);
        }
    }
}
