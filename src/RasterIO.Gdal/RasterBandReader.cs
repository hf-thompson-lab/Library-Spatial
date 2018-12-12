// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.RasterIO.Gdal
{
    /// <summary>
    /// A reader for a particular GDAL raster band.
    /// </summary>
    public class RasterBandReader<T>
        where T : struct
    {
        private OSGeo.GDAL.Band gdalBand;
        private BlockDimensions blockDimensions;
        private GdalBandIO.ReadBlock<T> readBlock;

        public RasterBandReader(OSGeo.GDAL.Band         gdalBand,
                                GdalBandIO.ReadBlock<T> readBlock)
        {
            this.gdalBand = gdalBand;
            this.readBlock = readBlock;

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

        public void ReadBlock(BandBlock<T> block)
        {
            readBlock(gdalBand, block);
        }
    }
}
