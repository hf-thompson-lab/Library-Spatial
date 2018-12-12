// Contributors:
//   James Domingo, Green Code LLC

using System;
using OSGeo.GDAL;

namespace Landis.SpatialModeling.CoreServices
{
    public class DriverInfo
    {
        private string shortName;
        private string longName;
        private bool hasCreate;
        private bool hasCreateCopy;

        public string ShortName { get { return shortName; } }

        public string LongName { get { return longName; } }

        public bool HasCreate { get { return hasCreate; } }

        public bool HasCreateCopy { get { return hasCreateCopy; } }

        public DriverInfo(Driver gdalDriver)
        {
            shortName = gdalDriver.ShortName;
            longName = gdalDriver.LongName;
            hasCreate = GetMetadataItem(gdalDriver, "DCAP_CREATE") == "YES";
            hasCreateCopy = GetMetadataItem(gdalDriver, "DCAP_CREATECOPY") == "YES";
        }

        private string GetMetadataItem(Driver gdalDriver,
                                       string itemName)
        {
            string itemValue = gdalDriver.GetMetadataItem(itemName, "");
            return itemValue;
        }

        public static int CompareShortName(DriverInfo x,
                                           DriverInfo y)
        {
            return x.ShortName.CompareTo(y.ShortName);
        }
    }
}
