// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab
//   James Domingo, Green Code LLC

namespace Landis.SpatialModeling
{
    /// <summary>
    /// A location of a site on a landscape.
    /// </summary>
    /// <remarks>
    /// This value type is semantically equivalent to its counterpart in the
    /// Grids module of this library.  This type is defined in this module
    /// as a convenience to developers using this module, so they don't have
    /// to reference the Grids module if they are working with site locations.
    /// </remarks>
    public struct Location
    {
        private int row;
        private int column;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="row">
        /// The row where the site is located.
        /// </param>
        /// <param name="column">
        /// The column where the site is located.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// The row or column is negative.
        /// </exception>
        public Location(int row,
                        int column)
        {
            if (row < 0)
                throw new System.ArgumentException("row parameter is negative");
            if (column < 0)
                throw new System.ArgumentException("column parameter is negative");
            this.row    = row;
            this.column = column;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The row where the site is located.
        /// </summary>
        public int Row
        {
            get {
                return row;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The column where the site is located.
        /// </summary>
        public int Column
        {
            get {
                return column;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Compares two locations for equality.
        /// </summary>
        public static bool operator ==(Location location1,
                                       Location location2)
        {
            return (location1.row == location2.row) && (location1.column == location2.column);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Compares two locations for inequality.
        /// </summary>
        public static bool operator !=(Location location1,
                                       Location location2)
        {
            return !(location1 == location2);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Converts a location to a bool.
        /// </summary>
        /// <returns>
        /// true if the location's row and column are both positive (not zero);
        /// false otherwise.
        /// </returns>
        public static implicit operator bool(Location location)
        {
            return (location.row > 0) && (location.column > 0);
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
            Location loc = (Location)obj;
            return this == loc;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Computes a hash code for the current object.
        /// </summary>
        /// <returns>A hash code</returns>
        public override int GetHashCode()
        {
            return (int)(row ^ column);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string formatted as "(row, column)".</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1})", row, column);
        }
    }
}
