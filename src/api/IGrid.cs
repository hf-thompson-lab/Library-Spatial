// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

namespace Landis.SpatialModeling
{
    /// <summary>
    /// A rectangular grid of cells (elements).
    /// </summary>
    public interface IGrid
    {
        /// <summary>
        /// The grid's dimensions (rows and columns).
        /// </summary>
        Dimensions Dimensions
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The number of rows in the grid.
        /// </summary>
        int Rows
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The number of columns in the grid.
        /// </summary>
        int Columns
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The number of cells in the grid.
        /// </summary>
        long Count
        {
            get;
        }
    }
}
