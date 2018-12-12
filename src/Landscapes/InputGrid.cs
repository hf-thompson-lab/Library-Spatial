// Copyright 2005-2006 University of Wisconsin
// All rights reserved. 
//
// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

using Landis.SpatialModeling;
using System.Collections.Generic;

namespace Landis.Landscapes
{
    /// <summary>
    /// An input grid with data values.
    /// </summary>
    public class InputGrid<TData>
        : Grid, IInputGrid<TData>
    {
        private IIndexableGrid<TData> data;
        private Location currentLocation;
        private bool disposed = false;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance using an indexable data grid.
        /// </summary>
        public InputGrid(IIndexableGrid<TData> dataGrid)
            : base(dataGrid.Dimensions)
        {
            this.data = dataGrid;
            //  Initialize current location such that RowMajor.Next will return
            //  the upper left location (1,1).
            this.currentLocation = new Location(1, 0);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The type of data in the grid.
        /// </summary>
        public System.Type DataType {
            get {
                return typeof(TData);
            }
        }

        //---------------------------------------------------------------------

        public TData ReadValue()
        {
            if (disposed)
                throw new System.ObjectDisposedException(GetType().FullName);
            currentLocation = RowMajor.Next(currentLocation, Columns);
            if (currentLocation.Row > Rows)
                throw new System.IO.EndOfStreamException();
            return data[currentLocation];
        }

        //---------------------------------------------------------------------

        public void Close()
        {
            Dispose();
        }

        //---------------------------------------------------------------------

        public void Dispose()
        {
            disposed = true;
        }
    }
}
