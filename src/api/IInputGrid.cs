// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

namespace Landis.SpatialModeling
{
    /// <summary>
    /// An input grid of data values that are read from the upper-left corner
    /// to the lower-right corner in row-major order.
    /// </summary>
    public interface IInputGrid<TValue>
        : IGrid, System.IDisposable
    {
        /// <summary>
        /// Reads the next data value from the grid.
        /// </summary>
        /// <exception cref="System.IO.EndOfStreamException">
        /// Thrown when there are no more data values left to read.
        /// </exception>
        TValue ReadValue();

        //---------------------------------------------------------------------

        /// <summary>
        /// Closes the input grid.
        /// </summary>
        void Close();
    }
}
