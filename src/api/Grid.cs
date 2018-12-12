// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

namespace Landis.SpatialModeling
{
    /// <summary>
    /// Base class for grids.
    /// </summary>
    public class Grid
        : IGrid
    {
        private Dimensions dimensions;
        private long count;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance with specific dimensions.
        /// </summary>
        protected Grid(Dimensions dimensions)
        {
            this.dimensions = dimensions;
            this.count = dimensions.Rows * dimensions.Columns;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance with specific dimensions.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// One or both of parameters is negative.
        /// </exception>
        protected Grid(int rows,
                       int columns)
            : this(new Dimensions(rows, columns))
        {
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The grid's dimensions (rows and columns).
        /// </summary>
        public Dimensions Dimensions
        {
            get {
                return dimensions;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The number of rows in the grid.
        /// </summary>
        public int Rows
        {
            get {
                return dimensions.Rows;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The number of columns in the grid.
        /// </summary>
        public int Columns
        {
            get {
                return dimensions.Columns;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The number of cells (elements) in the grid.
        /// </summary>
        public long Count
        {
            get {
                return count;
            }
        }
    }
}
