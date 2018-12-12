// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.RasterIO.Gdal
{
    public static class GdalBandIO
    {
        /// <summary>
        /// Method for reading a block from a raster band.
        /// </summary>
        /// <remarks>
        /// Assumes pixelSpace = 0 and lineSpace = 0
        /// </remarks>
        public delegate void ReadBlock<T>(OSGeo.GDAL.Band rasterBand,
                                          BandBlock<T>    block)
            where T : struct;

        public static void ReadByteBlock(OSGeo.GDAL.Band rasterBand,
                                         BandBlock<byte> block)
        {
            rasterBand.ReadRaster(block.XOffset, block.YOffset,
                                  block.UsedPortionXSize, block.UsedPortionYSize,
                                  block.Buffer,
                                  block.UsedPortionXSize, block.UsedPortionYSize,
                                  block.PixelSpace, block.LineSpace);
        }   

        public static void ReadShortBlock(OSGeo.GDAL.Band  rasterBand,
                                          BandBlock<short> block)
        {
            rasterBand.ReadRaster(block.XOffset, block.YOffset,
                                  block.UsedPortionXSize, block.UsedPortionYSize,
                                  block.Buffer,
                                  block.UsedPortionXSize, block.UsedPortionYSize,
                                  block.PixelSpace, block.LineSpace);
        }   

        public static void ReadIntBlock(OSGeo.GDAL.Band rasterBand,
                                        BandBlock<int>  block)
        {
            rasterBand.ReadRaster(block.XOffset, block.YOffset,
                                  block.UsedPortionXSize, block.UsedPortionYSize,
                                  block.Buffer,
                                  block.UsedPortionXSize, block.UsedPortionYSize,
                                  block.PixelSpace, block.LineSpace);
        }   

        public static void ReadFloatBlock(OSGeo.GDAL.Band  rasterBand,
                                          BandBlock<float> block)
        {
            rasterBand.ReadRaster(block.XOffset, block.YOffset,
                                  block.UsedPortionXSize, block.UsedPortionYSize,
                                  block.Buffer,
                                  block.UsedPortionXSize, block.UsedPortionYSize,
                                  block.PixelSpace, block.LineSpace);
        }   

        public static void ReadDoubleBlock(OSGeo.GDAL.Band   rasterBand,
                                           BandBlock<double> block)
        {
            rasterBand.ReadRaster(block.XOffset, block.YOffset,
                                  block.UsedPortionXSize, block.UsedPortionYSize,
                                  block.Buffer,
                                  block.UsedPortionXSize, block.UsedPortionYSize,
                                  block.PixelSpace, block.LineSpace);
        }   

        /// <summary>
        /// Method for writing a block to a raster band.
        /// </summary>
        /// <remarks>
        /// Assumes pixelSpace = 0 and lineSpace = 0
        /// </remarks>
        public delegate void WriteBlock<T>(OSGeo.GDAL.Band rasterBand,
                                           BandBlock<T>    block)
            where T : struct;

        public static void WriteByteBlock(OSGeo.GDAL.Band rasterBand,
                                          BandBlock<byte> block)
        {
            rasterBand.WriteRaster(block.XOffset, block.YOffset,
                                   block.UsedPortionXSize, block.UsedPortionYSize,
                                   block.Buffer,
                                   block.UsedPortionXSize, block.UsedPortionYSize,
                                   block.PixelSpace, block.LineSpace);
        }   

        public static void WriteShortBlock(OSGeo.GDAL.Band  rasterBand,
                                           BandBlock<short> block)
        {
            rasterBand.WriteRaster(block.XOffset, block.YOffset,
                                   block.UsedPortionXSize, block.UsedPortionYSize,
                                   block.Buffer,
                                   block.UsedPortionXSize, block.UsedPortionYSize,
                                   block.PixelSpace, block.LineSpace);
        }   

        public static void WriteIntBlock(OSGeo.GDAL.Band rasterBand,
                                         BandBlock<int>  block)
        {
            rasterBand.WriteRaster(block.XOffset, block.YOffset,
                                   block.UsedPortionXSize, block.UsedPortionYSize,
                                   block.Buffer,
                                   block.UsedPortionXSize, block.UsedPortionYSize,
                                   block.PixelSpace, block.LineSpace);
        }   

        public static void WriteFloatBlock(OSGeo.GDAL.Band  rasterBand,
                                           BandBlock<float> block)
        {
            rasterBand.WriteRaster(block.XOffset, block.YOffset,
                                   block.UsedPortionXSize, block.UsedPortionYSize,
                                   block.Buffer,
                                   block.UsedPortionXSize, block.UsedPortionYSize,
                                   block.PixelSpace, block.LineSpace);
        }   

        public static void WriteDoubleBlock(OSGeo.GDAL.Band   rasterBand,
                                            BandBlock<double> block)
        {
            rasterBand.WriteRaster(block.XOffset, block.YOffset,
                                   block.UsedPortionXSize, block.UsedPortionYSize,
                                   block.Buffer,
                                   block.UsedPortionXSize, block.UsedPortionYSize,
                                   block.PixelSpace, block.LineSpace);
        }   
    }
}
