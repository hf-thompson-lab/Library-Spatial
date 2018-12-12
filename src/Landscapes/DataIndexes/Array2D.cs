// Copyright 2006 University of Wisconsin
// All rights reserved. 
//
// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

#if LOG4NET
using log4net;
#endif
using Landis.SpatialModeling;
using System.Reflection;
using System.Text;

namespace Landis.Landscapes.DataIndexes
{
    /// <summary>
    /// A collection of the data indexes implemented using a 2-dimensional
    /// array.
    /// </summary>
    public class Array2D
        : Collection
    {
        private uint[,] indexes;

#if LOG4NET
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly bool isDebugEnabled = log.IsDebugEnabled;
#endif

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance from an input grid of boolean values.
        /// </summary>
        /// <param name="activeSites">
        /// An input grid of boolean values where true values indicates active
        /// sites.
        /// </param>
        public Array2D(IInputGrid<bool> activeSites)
            : base(activeSites.Dimensions)
        {
            int rows = activeSites.Rows;
            int columns = activeSites.Columns;

            indexes = new uint[rows, columns];
            uint count = 0;
            for (int row = 0; row < rows; ++row) {
                for (int column = 0; column < columns; ++column) {
                    uint dataIndex = InactiveSite.DataIndex;
                    if (activeSites.ReadValue()) {
                        count++;
                        dataIndex = count;
                    }
                    indexes[row, column] = dataIndex;
                }
            }
            ActiveLocationCount = count;
            InactiveLocationCount = (rows * columns) - ActiveLocationCount;

#if LOG4NET
            if (isDebugEnabled) {
                log.Debug("Active Site Locations");
                log.Debug("");
                log.DebugFormat("Grid dimensions: {0}", activeSites.Dimensions);
                log.Debug("");

                StringBuilder line = new StringBuilder(8 * (int) columns);
                line.Append("Column:");
                for (int column = 1; column <= columns; column++)
                    line.Append('\t').Append(column);
                log.Debug(line.ToString());

                log.Debug("Row");
                for (int row = 0; row < rows; row++) {
                    line.Remove(0, line.Length);
                    line.Append(row + 1);
                    for (int column = 0; column < columns; column++)
                        line.Append('\t').Append(indexes[row, column]);
                    log.Debug(line.ToString());
                }
            }
#endif
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
        public override uint this[Location location]
        {
            get {
                return indexes[location.Row-1, location.Column-1];
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// An enumerator for all the active locations in the collection.
        /// </summary>
        /// <remarks>
        /// Traverses the landscape from upper left corner to lower right
        /// corner in row-major order.
        /// </remarks>
        public class ActiveLocationEnumerator
            : DataIndexes.Enumerator
        {
            private Array2D locations;

            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            internal ActiveLocationEnumerator(Array2D locations)
                : base(locations.LandscapeDimensions)
            {
                this.locations = locations;
            }

            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public override bool MoveNext()
            {
                if (CurrentState == State.AfterLast)
                    return false;

                if (CurrentState == State.BeforeFirst) {
                    if (locations.ActiveLocationCount == 0)
                        return MoveNextReachedEnd();
                    CurrentLocation = new Location(1, 1);
                }
                else {
                    //  CurrentState == State.InCollection
                    CurrentLocation = RowMajor.Next(CurrentLocation, Columns);
                }

                while (CurrentLocation.Row <= Rows) {
                    CurrentDataIndex = locations.indexes[CurrentLocation.Row-1,
                                                         CurrentLocation.Column-1];
                    if (CurrentDataIndex != InactiveSite.DataIndex)
                        return MoveNextSucceeded();
                    CurrentLocation = RowMajor.Next(CurrentLocation, Columns);
                }

                ResetLocationAndIndex();
                return MoveNextReachedEnd();
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Returns an enumerator for all the active locations in the
        /// collection.
        /// </summary>
        public override DataIndexes.Enumerator GetActiveLocationEnumerator()
        {
            return new ActiveLocationEnumerator(this);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// An enumerator for all the locations in the collection.
        /// </summary>
        /// <remarks>
        /// Traverses the landscape from upper left corner to lower right
        /// corner in row-major order.
        /// </remarks>
        public class AllLocationsEnumerator
            : DataIndexes.Enumerator
        {
            private Array2D locations;

            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            internal AllLocationsEnumerator(Array2D locations)
                : base(locations.LandscapeDimensions)
            {
                this.locations = locations;
            }

            //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public override bool MoveNext()
            {
                if (CurrentState == State.AfterLast)
                    return false;

                if (CurrentState == State.BeforeFirst) {
                    if (locations.ActiveLocationCount == 0 &&
                            locations.InactiveLocationCount == 0)
                        return MoveNextReachedEnd();
                    CurrentLocation = new Location(1, 1);
                }
                else {
                    //  CurrentState == State.InCollection
                    CurrentLocation = RowMajor.Next(CurrentLocation, Columns);
                }

                if (CurrentLocation.Row <= Rows) {
                    CurrentDataIndex = locations.indexes[CurrentLocation.Row-1,
                                                         CurrentLocation.Column-1];
                    return MoveNextSucceeded();
                }

                ResetLocationAndIndex();
                return MoveNextReachedEnd();
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Returns an enumerator for all the locations in the collection.
        /// </summary>
        public override DataIndexes.Enumerator GetEnumerator()
        {
            return new AllLocationsEnumerator(this);
        }
    }
}
