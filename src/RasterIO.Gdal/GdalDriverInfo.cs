// Contributors:
//   James Domingo, Green Code LLC

using System;
using System.Collections.Generic;
using OSGeo.GDAL;

namespace Landis.SpatialModeling.CoreServices
{
    public static class GdalDriverInfo
    {
        public static void PrintAll()
        {
            try {
                Gdal.AllRegister();
                Console.WriteLine("Registered GDAL drivers:");
                List<DriverInfo> drivers = new List<DriverInfo>(Gdal.GetDriverCount());
                for (int i = 0; i <     Gdal.GetDriverCount(); ++i) {
                    Driver driver = Gdal.GetDriver(i);
                    DriverInfo driverInfo = new DriverInfo(driver);
                    drivers.Add(driverInfo);
                }
                drivers.Sort(DriverInfo.CompareShortName);
                PrintCreateCapabilities(drivers);
            }
            catch (ApplicationException exc) {
                Console.WriteLine("Application exception: {0}", exc);
            }
        }

        public static void PrintCreateCapabilities(List<DriverInfo> driverInfos)
        {
            Console.WriteLine("Create,CreateCopy,Driver Code,Driver Description");
            foreach (DriverInfo driverInfo in driverInfos) {
                string createStr = driverInfo.HasCreate ? "Y" : "";
                string createCopyStr = driverInfo.HasCreateCopy ? "Y" : "";
                Console.WriteLine("{0},{1},{2},\"{3}\"", createStr, createCopyStr, driverInfo.ShortName, driverInfo.LongName);
            }
        }

        public static void PrintMetadata(Driver driver,
                                         string prefix)
        {
            string[] metadata = driver.GetMetadata("");
            foreach (string metadataItem in metadata)
                Console.WriteLine("{0}{1}", prefix, metadataItem);
        }
    }
}
