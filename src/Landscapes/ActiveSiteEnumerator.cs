// Copyright 2005-2006 University of Wisconsin
// All rights reserved. 
//
// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

using Landis.SpatialModeling;
using System.Collections;
using System.Collections.Generic;

namespace Landis.Landscapes
{
    /// <summary>
    /// Enumerator for the active sites in a landscape.
    /// </summary>
    public class ActiveSiteEnumerator
        : IEnumerator<ActiveSite>
    {
        private ILandscape landscape;
        private ActiveSite currentSite;
        private DataIndexes.Enumerator activeLocationEtor;

        //---------------------------------------------------------------------

        public ActiveSite Current
        {
            get {
                return currentSite;
            }
        }

        //---------------------------------------------------------------------

        object IEnumerator.Current
        {
            get {
                return currentSite;
            }
        }

        //---------------------------------------------------------------------

        internal ActiveSiteEnumerator(ILandscape             landscape,
                                      DataIndexes.Collection dataIndexes)
        {
            this.landscape = landscape;
            this.activeLocationEtor = dataIndexes.GetActiveLocationEnumerator();
        }

        //---------------------------------------------------------------------

        public bool MoveNext()
        {
            if (activeLocationEtor.MoveNext()) {
                currentSite = new ActiveSite(landscape,
                                             activeLocationEtor.CurrentLocation,
                                             activeLocationEtor.CurrentDataIndex);
                return true;
            }
            else {
                currentSite = new ActiveSite();
                return false;
            }
        }

        //---------------------------------------------------------------------

        public void Reset()
        {
            currentSite = new ActiveSite();
            activeLocationEtor.Reset();
        }

        //---------------------------------------------------------------------

        void System.IDisposable.Dispose()
        {
        }
    }
}
