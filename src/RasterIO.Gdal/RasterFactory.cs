
// Copyright 2010-2011 Green Code LLC
// All rights reserved.
// //
// The copyright holders license this file under the New (3-clause) BSD
// License (the "License").  You may not use this file except in
// compliance with the License.  A copy of the License is available at
//
//   http://www.opensource.org/licenses/BSD-3-Clause
//
// and is included in the NOTICE.txt file distributed with this work.
//
// Contributors:
//   James Domingo, Green Code LLC

using Landis.SpatialModeling;
using OSGeo.GDAL;
using System;
using System.Collections.Generic;

namespace Landis.RasterIO.Gdal
{
    public class RasterFactory : IConfigurableRasterFactory
    {
        private IDictionary<string, Driver> extToDriver;
 
        //---------------------------------------------------------------------

        static RasterFactory()
        {
            GdalSystem.Initialize();
        }
 
        //---------------------------------------------------------------------

        public RasterFactory ()
        {
            extToDriver = new Dictionary<string, Driver>(StringComparer.InvariantCultureIgnoreCase);
        }

        //---------------------------------------------------------------------

        public RasterFormat GetFormat(string code)
        {
            if (code == null)
                throw new ArgumentNullException();
            Driver driver = OSGeo.GDAL.Gdal.GetDriverByName(code);
            if (driver == null)
                return null;
            bool canWrite = driver.GetMetadataItem("DCAP_CREATE", "") == "YES";
            return new RasterFormat(driver.ShortName,
                                    driver.LongName,
                                    canWrite);
        }
 
        //---------------------------------------------------------------------

        public void BindExtensionToFormat(string       fileExtension,
                                          RasterFormat format)
        {
            if (fileExtension == null)
                throw new ArgumentNullException("fileExtension");
            if (fileExtension.Length == 0 || fileExtension[0] != '.')
                throw new ArgumentException("fileExtension does not start with a period");

            if (format == null) {
                extToDriver.Remove(fileExtension);
            } else {
                Driver driver = OSGeo.GDAL.Gdal.GetDriverByName(format.Code);
                if (driver == null)
                    throw new ArgumentException(string.Format("Unknown format code: \"{0}\"", format.Code));
                extToDriver[fileExtension] = driver;
            }
        }
 
        //---------------------------------------------------------------------

        public IInputRaster<TPixel> OpenRaster<TPixel>(string path)
            where TPixel : Pixel, new()
        {
            return new GdalInputRaster<TPixel>(path);
        }
 
        //---------------------------------------------------------------------

        public IOutputRaster<TPixel> CreateRaster<TPixel>(string     path,
                                                          Dimensions dimensions)
            where TPixel : Pixel, new()
        {
            // Fetch extension from path.
            // Get the GDAL driver associated with that extension.
            string extension = System.IO.Path.GetExtension(path);
            if (extension == null)
                throw new ArgumentNullException("path");
            if (extension == string.Empty)
                throw new ArgumentException("path has no extension");
            Driver driver;
            if (! extToDriver.TryGetValue(extension, out driver))
                throw new ApplicationException(string.Format("Unknown file extension: \"{0}\"", extension));

            return new GdalOutputRaster<TPixel>(path, dimensions, driver);
        }
    }
}
