// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

using System.Diagnostics;

namespace Landis.SpatialModeling
{
    /// <summary>
    /// An individual site on a landscape.
    /// </summary>
    public struct Site
    {
        private ILandscape landscape;
        private Location location;
        private uint dataIndex;

        //---------------------------------------------------------------------

        /// <summary>
        /// The data index assigned to inactive sites.
        /// </summary>
        public const uint InactiveSiteDataIndex = 0;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance for a site on a landscape.
        /// </summary>
        /// <param name="landscape">
        ///  The landscape where the site is located.
        /// </param>
        /// <param name="location">
        ///  The location of the site.
        /// </param>
        /// <param name="dataIndex">
        ///  The index of the site's data for site variables.
        /// </param>
        internal Site(ILandscape landscape,
                      Location   location,
                      uint       dataIndex)
        {
            Debug.Assert( landscape.IsValid(location) );
            this.landscape = landscape;
            this.location = location;
            this.dataIndex = dataIndex;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The landscape on which the site is located.
        /// </summary>
        public ILandscape Landscape
        {
            get {
                return landscape;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The location of the site.
        /// </summary>
        public Location Location
        {
            get {
                return location;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        ///  The index of the site's data for site variables.
        /// </summary>
        public uint DataIndex
        {
            get {
                return dataIndex;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Is the site active?
        /// </summary>
        public bool IsActive
        {
            get {
                return dataIndex != InactiveSiteDataIndex;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Converts a site to a bool.
        /// </summary>
        /// <returns>
        /// true if the site's location can be converted to true (both row and
        /// column are positive) and the landscape is not null.
        /// </returns>
        public static implicit operator bool(Site site)
        {
            return site.location && (site.landscape != null);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Are two sites the same site?
        /// </summary>
        public static bool operator ==(Site site1,
                                       Site site2)
        {
            return (site1.location == site2.location) &&
                   (site1.landscape == site2.landscape);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Are two sites different?
        /// </summary>
        /// <remarks>
        /// The sites are different if they are on the same landscape, but
        /// their locations are different, or if they are on different
        /// landscapes.
        /// </remarks>
        public static bool operator !=(Site site1,
                                       Site site2)
        {
            return !(site1 == site2);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets a neighboring site.
        /// </summary>
        /// <param name="neighborLocation">
        /// The location of the neighboring site relative to the site.
        /// </param>
        /// <returns>
        /// a site that converts to false if the neighbor's location is not
        /// a valid location for the site's landscape.
        /// </returns>
        public Site GetNeighbor(RelativeLocation neighborLocation)
        {
            Location? neighborAbsoluteLocation = location + neighborLocation;
            if (neighborAbsoluteLocation.HasValue)
                return landscape.GetSite(neighborAbsoluteLocation.Value);
            return new Site();
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
            Site site = (Site)obj;
            return this == site;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Computes a hash for a site.
        /// </summary>
        /// <remarks>
        /// A site's hash code is the hash code of its location.
        /// </remarks>
        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Returns a string representation of a site.
        /// </summary>
        /// <returns>
        /// The string representation of the site's location: "(row, column)".
        /// </returns>
        public override string ToString()
        {
            return Location.ToString();
        }
    }
}
