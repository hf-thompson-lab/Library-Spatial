// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.RasterIO.Gdal
{
    public static class RasterBandWriters
    {
        public static RasterBandWriter<byte> NewByteWriter(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandWriter<byte>(gdalBand, GdalBandIO.WriteByteBlock);
        }

        public static RasterBandWriter<short> NewShortWriter(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandWriter<short>(gdalBand, GdalBandIO.WriteShortBlock);
        }

        public static RasterBandWriter<int> NewIntWriter(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandWriter<int>(gdalBand, GdalBandIO.WriteIntBlock);
        }

        public static RasterBandWriter<float> NewFloatWriter(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandWriter<float>(gdalBand, GdalBandIO.WriteFloatBlock);
        }

        public static RasterBandWriter<double> NewDoubleWriter(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandWriter<double>(gdalBand, GdalBandIO.WriteDoubleBlock);
        }
    }
}
