// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

namespace Landis.SpatialModeling
{
    /// <summary>
    /// The dimensions (rows and columns) of a 2-dimensional rectangular grid.
    /// </summary>
    public struct Dimensions
    {
        private int rows;
        private int columns;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// The number of rows or columns is negative.
        /// </exception>
        public Dimensions(int rows,
                          int columns)
        {
            if (rows < 0)
                throw new System.ArgumentException("rows parameter is negative");
            if (columns < 0)
                throw new System.ArgumentException("columns parameter is negative");
            this.rows    = rows;
            this.columns = columns;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The number of rows.
        /// </summary>
        public int Rows
        {
            get {
                return rows;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The number of columns.
        /// </summary>
        public int Columns
        {
            get {
                return columns;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Tests two sets of dimensions for equality.
        /// </summary>
        /// <param name="x">
        /// First set of dimensions
        /// </param>
        /// <param name="y">
        /// Second set of dimensions
        /// </param>
        /// <returns>
        /// true if the two set of dimensions are equal; false otherwise.
        /// </returns>
        public static bool operator ==(Dimensions x,
                                       Dimensions y)
        {
            return (x.rows == y.rows) && (x.columns == y.columns);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Tests two sets of dimensions for inequality.
        /// </summary>
        /// <param name="x">
        /// First set of dimensions
        /// </param>
        /// <param name="y">
        /// Second set of dimensions
        /// </param>
        /// <returns>
        /// true if the two set of dimensions are not equal; false otherwise.
        /// </returns>
        public static bool operator !=(Dimensions x,
                                       Dimensions y)
        {
            return !(x == y);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Determines whether the specified object equals the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.
        /// </param>
        /// <returns><b>true</b> if the specified object is equal to the
        /// current object; otherwise, <b>false</b>.</returns>
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;
            Dimensions loc = (Dimensions)obj;
            return this == loc;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Computes a hash code for the current object.
        /// </summary>
        /// <returns>A hash code</returns>
        public override int GetHashCode()
        {
            return (int)(rows ^ columns);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string formatted as "# rows by # columns".</returns>
        public override string ToString()
        {
            return string.Format("{0:#,##0} row{1} by {2:#,##0} column{3}",
                                 rows, (rows == 1 ? "" : "s"),
                                 columns, (columns == 1 ? "" : "s"));
        }
    }
}
