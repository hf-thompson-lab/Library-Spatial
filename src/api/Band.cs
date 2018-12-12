// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.SpatialModeling
{
    /// <summary>
    /// A pixel band of a particular data type.
    /// </summary>
    /// <typeparam name="T">The data type for band's value.
    /// </typeparam>
    public class Band<T> : PixelBand
        where T : struct
    {
        /// <summary>
        /// The value of the pixel band.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Constructs a new pixel band from its description.
        /// </summary>
        /// <remarks>
        /// <example>
        /// <code><![CDATA[
        ///   Band<float> Slope = "slope : tangent of inclination angle (rise/run)";
        /// ]]></code>
        /// </example>
        /// </remarks>
        /// <param name="description">
        /// A <see cref="System.String"/>
        /// </param>
        public static implicit operator Band<T>(string description)
        {
            return new Band<T>(description);
        }

        private Band(string description)
            : base(description, System.Type.GetTypeCode(typeof(T)))
        {
        }

        /// <summary>
        /// Computes the size in bytes for a pixel band's data type.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Int32"/>
        /// </returns>
        /// <exception cref="System.ArgumentException">Thrown if
        /// <paramref name="typeCode"/> is not Byte, SByte, Int16, UInt16,
        /// Int32, UInt32, Single or Double.
        /// </exception>
        public static int ComputeSize()
        {
            return PixelBand.ComputeSize(System.Type.GetTypeCode(typeof(T)));
        }
    }
}
