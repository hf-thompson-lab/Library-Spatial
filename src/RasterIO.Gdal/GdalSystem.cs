// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.RasterIO.Gdal
{
    public static class GdalSystem
    {
        static GdalSystem()
        {
            OSGeo.GDAL.Gdal.AllRegister();
        }

        public static void Initialize()
        {
            // Do nothing.  The initialization occurs on the first call.
        }
    }
}
