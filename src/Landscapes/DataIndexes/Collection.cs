// Copyright 2006 University of Wisconsin
// All rights reserved. 
//
// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

using Landis.SpatialModeling;

namespace Landis.Landscapes.DataIndexes
{
    /// <summary>
    /// A collection of data indexes for all the locations on a landscape.
    /// </summary>
    public abstract class Collection
    {
        private Dimensions landscapeDimensions;
        private uint activeCount;
        private long inactiveCount;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="landscapeDimensions">
        /// The dimensions of the landscape.
        /// </param>
        protected Collection(Dimensions landscapeDimensions)
        {
            this.landscapeDimensions = landscapeDimensions;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The dimensions of the landscape.
        /// </summary>
        public Dimensions LandscapeDimensions
        {
            get {
                return landscapeDimensions;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The # of active locations on the landscape.
        /// </summary>
        /// <remarks>
        /// Since each active location may be assigned a unique data index, and
        /// a data index is an unsigned integer, the maximum number of active
        /// locations is the maximum value for an unsigned integer.
        /// </remarks>
        public uint ActiveLocationCount
        {
            get {
                return activeCount;
            }

            protected set {
                activeCount = value;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The # of inactive locations on the landscape.
        /// </summary>
        /// <remarks>
        /// Since it's possible that all the locations on a landscape are
        /// inactive and that the number of locations (sites, cells) on a
        /// landscape is a long, then this property is a long too.
        /// </remarks>
        public long InactiveLocationCount
        {
            get {
                return inactiveCount;
            }

            protected set {
                if (value < 0)
                    throw new System.ArgumentOutOfRangeException();
                inactiveCount = value;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The data index for a particular location.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">
        /// The location's row is 0 or > the # of rows in the landscape, or
        /// or the location's column is 0 or > the # of columns in the
        /// landscape.
        /// </exception>
        public abstract uint this[Location location]
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Returns an enumerator for all the locations and data indexes in
        /// the collection.
        /// </summary>
        public abstract Enumerator GetEnumerator();

        //---------------------------------------------------------------------

        /// <summary>
        /// Returns an enumerator for all the active locations and their data
        /// indexes in the collection.
        /// </summary>
        public abstract Enumerator GetActiveLocationEnumerator();
    }
}
