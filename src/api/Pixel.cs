// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.SpatialModeling
{
    /// <summary>
    /// Base class for types of pixels.
    /// <example>
    /// </example>
    /// </summary>
    public abstract class Pixel
    {
        PixelBand[] bands;

        /// <summary>
        /// Sets the bands for the pixel.  Derived classes should call this
        /// method in their constructors.
        /// </summary>
        /// <remarks>
        /// <example>
        /// <code><![CDATA[
        /// public class MyPixel : Pixel
        /// {
        ///     public Band<float> Slope  = "slope : tangent of inclination angle (rise / run)";
        ///     public Band<short> Aspect = "aspect : degrees clockwise from north (0 to 359)";
        ///
        ///     public MyPixel()
        ///     {
        ///         SetBands(Slope, Aspect);
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        /// </remarks>
        /// <param name="bands">
        /// An array of <see cref="PixelBand"/>
        /// </param>
        protected void SetBands(params PixelBand[] bands)
        {
            this.bands = (PixelBand[]) bands.Clone();
            // Assign band numbers
            for (int i = 0; i < this.bands.Length; i++) {
                this.bands[i].Number = i + 1;
            }
        }

        /// <summary>
        /// The number of bands in the pixel.
        /// </summary>
        public int Count {
            get { return bands.Length; }
        }

        /// <summary>
        /// Gets the pixel band by its band number.
        /// </summary>
        /// <param name="bandNumber">
        /// A <see cref="System.Int32"/>
        /// </param>
        public PixelBand this[int bandNumber]
        {
            get { return bands[bandNumber - 1]; }
        }
    }
}
