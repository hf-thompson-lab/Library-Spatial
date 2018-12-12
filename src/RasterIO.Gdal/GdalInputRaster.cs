// Contributors:
//   James Domingo, Green Code LLC

using Landis.SpatialModeling;
using OSGeo.GDAL;
using GdalBand = OSGeo.GDAL.Band;
using System;

namespace Landis.RasterIO.Gdal
{
    public class GdalInputRaster<TPixel> : InputRaster, IInputRaster<TPixel>
        where TPixel : Pixel, new()
    {
        private TPixel bufferPixel;
        public TPixel BufferPixel {
            get {
                return bufferPixel;
            }
        }

        private Dataset dataset;
        private IInputBand[] rasterBands;

        static GdalInputRaster()
        {
            GdalSystem.Initialize();
        }

        public GdalInputRaster(string path)
            : base(path)
        {
            // This overload of the ctor uses the built-in GDAL method of trying all known drivers.
            // Another overload would allow client code to specify the driver by its Short Name (code).

            bufferPixel = new TPixel();
            int nBands = bufferPixel.Count;

            dataset = OSGeo.GDAL.Gdal.OpenShared(path, Access.GA_ReadOnly);
            if (dataset == null)
                throw new ApplicationException("Cannot open raster file for reading");

            Dimensions = new Dimensions(dataset.RasterYSize, dataset.RasterXSize);

            if (dataset.RasterCount != nBands)
                throw new ApplicationException("Wrong # of bands in input raster");

            rasterBands = new IInputBand[nBands];
            for (int i = 0; i < nBands; ++i) {
                int bandNum = i + 1;
                //  TO DO: catch exception that represents type mismatch (RasterBand dataType is too big for bufferPixel dataType)
                rasterBands[i] = NewInputBand(dataset.GetRasterBand(bandNum), bufferPixel[bandNum]);
            }
        }


        public void ReadBufferPixel()
        {
            foreach (IInputBand rasterBand in rasterBands) {
                 rasterBand.ReadValueIntoBufferPixel();
            }
        }


        public static IInputBand NewInputBand(GdalBand  gdalBand,
                                              PixelBand pixelBand)
        {
            switch (gdalBand.DataType) {
                case DataType.GDT_Byte:
                    return NewByteBand(gdalBand, pixelBand);

                case DataType.GDT_Int16:
                    return NewShortBand(gdalBand, pixelBand);

                case DataType.GDT_Int32:
                    return NewIntBand(gdalBand, pixelBand);

                case DataType.GDT_Float32:
                    return NewFloatBand(gdalBand, pixelBand);

                case DataType.GDT_Float64:
                    return NewDoubleBand(gdalBand, pixelBand);

                default:
                    throw new ArgumentException("Raster band is not byte, short, int, float, double");
            }
        }


        public static InputBand<byte> NewByteBand(GdalBand  gdalBand,
                                                  PixelBand pixelBand)
        {
            RasterBandReader<byte> rasterBandReader = RasterBandReaders.NewByteReader(gdalBand);

            switch (pixelBand.TypeCode) {
                case TypeCode.Byte:
                    return new InputBand<byte>(rasterBandReader, new PixelBandSetter<byte, byte>(pixelBand, Convert.ToByte));

                case TypeCode.SByte:
                    return new InputBand<byte>(rasterBandReader, new PixelBandSetter<sbyte, byte>(pixelBand, Convert.ToSByte));

                case TypeCode.UInt16:
                    return new InputBand<byte>(rasterBandReader, new PixelBandSetter<ushort, byte>(pixelBand, Convert.ToUInt16));

                case TypeCode.Int16:
                    return new InputBand<byte>(rasterBandReader, new PixelBandSetter<short, byte>(pixelBand, Convert.ToInt16));

                case TypeCode.UInt32:
                    return new InputBand<byte>(rasterBandReader, new PixelBandSetter<uint, byte>(pixelBand, Convert.ToUInt32));

                case TypeCode.Int32:
                    return new InputBand<byte>(rasterBandReader, new PixelBandSetter<int, byte>(pixelBand, Convert.ToInt32));

                case TypeCode.Single:
                    return new InputBand<byte>(rasterBandReader, new PixelBandSetter<float, byte>(pixelBand, Convert.ToSingle));

                case TypeCode.Double:
                    return new InputBand<byte>(rasterBandReader, new PixelBandSetter<double, byte>(pixelBand, Convert.ToDouble));

                default:
                    throw new ArgumentException("pixelBand.TypeCode is not byte, sbyte, ushort, short, uint, int, float, double");
            }
        }


        public static InputBand<short> NewShortBand(GdalBand  gdalBand,
                                                    PixelBand pixelBand)
        {
            RasterBandReader<short> rasterBandReader = RasterBandReaders.NewShortReader(gdalBand);

            switch (pixelBand.TypeCode) {
                case TypeCode.UInt16:
                    return new InputBand<short>(rasterBandReader, new PixelBandSetter<ushort, short>(pixelBand, Convert.ToUInt16));

                case TypeCode.Int16:
                    return new InputBand<short>(rasterBandReader, new PixelBandSetter<short, short>(pixelBand, Convert.ToInt16));

                case TypeCode.UInt32:
                    return new InputBand<short>(rasterBandReader, new PixelBandSetter<uint, short>(pixelBand, Convert.ToUInt32));

                case TypeCode.Int32:
                    return new InputBand<short>(rasterBandReader, new PixelBandSetter<int, short>(pixelBand, Convert.ToInt32));

                case TypeCode.Single:
                    return new InputBand<short>(rasterBandReader, new PixelBandSetter<float, short>(pixelBand, Convert.ToSingle));

                case TypeCode.Double:
                    return new InputBand<short>(rasterBandReader, new PixelBandSetter<double, short>(pixelBand, Convert.ToDouble));

                default:
                    throw new ArgumentException("pixelBand.TypeCode is not ushort, short, uint, int, float, double");
            }
        }


        public static InputBand<int> NewIntBand(GdalBand  gdalBand,
                                                PixelBand pixelBand)
        {
            RasterBandReader<int> rasterBandReader = RasterBandReaders.NewIntReader(gdalBand);

            switch (pixelBand.TypeCode) {
                case TypeCode.UInt32:
                    return new InputBand<int>(rasterBandReader, new PixelBandSetter<uint, int>(pixelBand, Convert.ToUInt32));

                case TypeCode.Int32:
                    return new InputBand<int>(rasterBandReader, new PixelBandSetter<int, int>(pixelBand, Convert.ToInt32));

                case TypeCode.Single:
                    return new InputBand<int>(rasterBandReader, new PixelBandSetter<float, int>(pixelBand, Convert.ToSingle));

                case TypeCode.Double:
                    return new InputBand<int>(rasterBandReader, new PixelBandSetter<double, int>(pixelBand, Convert.ToDouble));

                default:
                    throw new ArgumentException("pixelBand.TypeCode is not uint, int, float, double");
            }
        }


        public static InputBand<float> NewFloatBand(GdalBand  gdalBand,
                                                    PixelBand pixelBand)
        {
            RasterBandReader<float> rasterBandReader = RasterBandReaders.NewFloatReader(gdalBand);

            switch (pixelBand.TypeCode) {
                case TypeCode.Single:
                    return new InputBand<float>(rasterBandReader, new PixelBandSetter<float, float>(pixelBand, Convert.ToSingle));

                case TypeCode.Double:
                    return new InputBand<float>(rasterBandReader, new PixelBandSetter<double, float>(pixelBand, Convert.ToDouble));

                default:
                    throw new ArgumentException("pixelBand.TypeCode is not float or double");
            }
        }


        public static InputBand<double> NewDoubleBand(GdalBand  gdalBand,
                                                      PixelBand pixelBand)
        {
            RasterBandReader<double> rasterBandReader = RasterBandReaders.NewDoubleReader(gdalBand);

            switch (pixelBand.TypeCode) {
                case TypeCode.Double:
                    return new InputBand<double>(rasterBandReader, new PixelBandSetter<double, double>(pixelBand, Convert.ToDouble));

                default:
                    throw new ArgumentException("pixelBand.TypeCode is not double");
            }
        }
    }
}
