// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

namespace Landis.SpatialModeling
{
    /// <summary>
    /// The location of a site relative to another site (known as the origin
    /// site).
    /// </summary>
    public struct RelativeLocation
    {
        private int row;
        private int column;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new relative location.
        /// </summary>
        public RelativeLocation(int row,
                                int column)
        {
            this.row    = row;
            this.column = column;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The row where the site is located, relative to the row of the
        /// origin site.
        /// </summary>
        public int Row
        {
            get {
                return row;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The column where the site is located, relative to the column of
        /// the origin site.
        /// </summary>
        public int Column
        {
            get {
                return column;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Compares two relative locations for equality.
        /// </summary>
        public static bool operator ==(RelativeLocation location1,
                                       RelativeLocation location2)
        {
            return (location1.row == location2.row) && (location1.column == location2.column);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Compares two locations for inequality.
        /// </summary>
        public static bool operator !=(RelativeLocation location1,
                                       RelativeLocation location2)
        {
            return !(location1 == location2);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Computes the absolute location that corresponds to a relative
        /// location.
        /// </summary>
        /// <returns>
        /// null if the relative location refers to an invalid location (row or
        /// column is negative or greater than 2,147,483,647).
        /// </returns>
        public static Location? operator +(Location         origin,
                                           RelativeLocation relativeLocation)
        {
            long row = origin.Row + relativeLocation.Row;
            long column = origin.Column + relativeLocation.Column;
            if (row < 0 || row > int.MaxValue ||
                column < 0 || column > int.MaxValue)
                return null;
            else
                return new Location((int) row, (int) column);
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
            RelativeLocation loc = (RelativeLocation)obj;
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
            return "(" + row + ", " + column + ")";
        }
    }
}
