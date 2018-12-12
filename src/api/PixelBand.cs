// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.SpatialModeling
{
    /// <summary>
    /// Base class for the bands in a pixel.
    /// </summary>
    public abstract class PixelBand
    {
        /// <summary>
        /// Description of what the pixel band represents.  Should include
        /// the band's name, what it represents, and its units.
        /// <example>
        /// "slope : tangent of inclination angle (rise / run)"
        /// </example>
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The band's position in the pixel.  The first band is numbered 1,
        /// the second band is numbered 2, and so on.
        /// </summary>
        public int Number { get; internal set; }

        /// <summary>
        /// The code representing the type of data the pixel band contains.
        /// </summary>
        public System.TypeCode TypeCode { get; private set; }

        /// <summary>
        /// The size of the pixel band in bytes.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="description">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="typeCode">
        /// A <see cref="System.TypeCode"/>
        /// </param>
        protected PixelBand(string          description,
                            System.TypeCode typeCode)
        {
            Description = description;
            TypeCode = typeCode;
            Size = ComputeSize(typeCode);
        }

        /// <summary>
        /// Computes the size in bytes for a pixel band's data type.
        /// </summary>
        /// <param name="typeCode">
        /// A <see cref="System.TypeCode"/> representing the pixel band's data
        /// type.
        /// </param>
        /// <returns>
        /// A <see cref="System.Int32"/>
        /// </returns>
        /// <exception cref="System.ArgumentException">Thrown if
        /// <paramref name="typeCode"/> is not Byte, SByte, Int16, UInt16,
        /// Int32, UInt32, Single or Double.
        /// </exception>
        public static int ComputeSize(System.TypeCode typeCode)
        {
            switch (typeCode)
            {
                case System.TypeCode.Byte:
                case System.TypeCode.SByte:
                    return 1;

                case System.TypeCode.Int16:
                case System.TypeCode.UInt16:
                    return 2;

                case System.TypeCode.Int32:
                case System.TypeCode.UInt32:
                case System.TypeCode.Single:
                    return 4;

                case System.TypeCode.Double:
                    return 8;

                default:
                    throw new System.ArgumentException("Invalid type for pixel band");
            }
        }
    }
}
