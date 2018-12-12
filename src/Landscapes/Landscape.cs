// Copyright 2004-2006 University of Wisconsin
// All rights reserved. 
//
// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

using Landis.SpatialModeling;
using System.Collections;
using System.Collections.Generic;

namespace Landis.Landscapes
{
    internal class Landscape
        : Grid, ILandscape, IEnumerable<Site>
    {
        private DataIndexes.Collection dataIndexes;
        private int inactiveSiteCount;

        //---------------------------------------------------------------------

        public new Dimensions Dimensions
        {
            get {
                return base.Dimensions;
            }
        }

        //---------------------------------------------------------------------

        public int ActiveSiteCount
        {
            get {
                return (int) dataIndexes.ActiveLocationCount;
            }
        }

        //---------------------------------------------------------------------

        public int InactiveSiteCount
        {
            get {
                return inactiveSiteCount;
            }
        }

        //---------------------------------------------------------------------

        public Location FirstInactiveSite
        {
            get {
                throw new System.NotImplementedException();
            }
        }

        //---------------------------------------------------------------------

        public uint InactiveSiteDataIndex
        {
            get {
                if (inactiveSiteCount == 0)
                    throw new System.InvalidOperationException("No inactive sites");
                return InactiveSite.DataIndex;
            }
        }

        //---------------------------------------------------------------------

        public int SiteCount
        {
            get {
                return (int) Count;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance using an input grid of active sites.
        /// </summary>
        /// <param name="activeSites">
        /// A grid that indicates which sites are active.
        /// </param>
        internal Landscape(IInputGrid<bool> activeSites)
            : base(activeSites.Dimensions)
        {
            Initialize(activeSites);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance using an indexable grid of active sites.
        /// </summary>
        /// <param name="activeSites">
        /// A grid that indicates which sites are active.
        /// </param>
        internal Landscape(IIndexableGrid<bool> activeSites)
            : base(activeSites.Dimensions)
        {
            Initialize(new InputGrid<bool>(activeSites));
        }

        //---------------------------------------------------------------------

        private void Initialize(IInputGrid<bool> activeSites)
        {
            if (Count > int.MaxValue) {
                string mesg = string.Format("Landscape dimensions are too big; maximum # of sites = {0:#,###}",
                                            int.MaxValue);
                throw new System.ApplicationException(mesg);
            }
            dataIndexes = new Landscapes.DataIndexes.Array2D(activeSites);
            activeSites.Close();
            inactiveSiteCount = SiteCount - (int) dataIndexes.ActiveLocationCount;
        }

        //---------------------------------------------------------------------

        public ActiveSite this[Location location]
        {
            get {
                Site site = GetSite(location);
                if (site && site.IsActive)
                    return (ActiveSite) site;
                else
                    return new ActiveSite();
            }
        }

        //---------------------------------------------------------------------

        public ActiveSite this[int row,
                               int column]
        {
            get {
                return this[new Location(row, column)];
            }
        }

        //---------------------------------------------------------------------

        public IEnumerable<ActiveSite> ActiveSites
        {
            get {
                return this;
            }
        }

        //---------------------------------------------------------------------

        public IEnumerator<ActiveSite> GetEnumerator()
        {
            return GetActiveSiteEnumerator();
        }

        //---------------------------------------------------------------------

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetActiveSiteEnumerator();
        }

        //---------------------------------------------------------------------

        public ActiveSiteEnumerator GetActiveSiteEnumerator()
        {
            return new ActiveSiteEnumerator(this, dataIndexes);
        }

        //---------------------------------------------------------------------

        public IEnumerable<Site> AllSites
        {
            get {
                return this;
            }
        }

        //---------------------------------------------------------------------

        IEnumerator<Site> IEnumerable<Site>.GetEnumerator()
        {
            return GetSiteEnumerator();
        }

        //---------------------------------------------------------------------

        public SiteEnumerator GetSiteEnumerator()
        {
            return new SiteEnumerator(this);
        }

        //---------------------------------------------------------------------

        public bool IsValid(Location location)
        {
            return (1 <= location.Row) && (location.Row <= Rows) &&
                   (1 <= location.Column) && (location.Column <= Columns);
        }

        //---------------------------------------------------------------------

        public Site GetSite(Location location)
        {
            if (! IsValid(location))
                return new Site();
            uint index = dataIndexes[location];
            return new Site(this, location, index);
        }

        //---------------------------------------------------------------------

        public Site GetSite(int row,
                            int column)
        {
            return GetSite(new Location(row, column));
        }

        //---------------------------------------------------------------------

        public ISiteVar<T> NewSiteVar<T>(InactiveSiteMode mode)
        {
            if (mode == InactiveSiteMode.Share1Value)
                return new SiteVarShare<T>(this);
            else
                return new SiteVarDistinct<T>(this);
        }

        //---------------------------------------------------------------------

        public ISiteVar<T> NewSiteVar<T>()
        {
            return NewSiteVar<T>(InactiveSiteMode.Share1Value);
        }
    }
}
