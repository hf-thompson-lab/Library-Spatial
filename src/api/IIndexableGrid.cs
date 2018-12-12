// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

namespace Landis.SpatialModeling
{
    /// <summary>
    /// A grid whose cells can be accessed by their locations.
    /// </summary>
    public interface IIndexableGrid<TCell>
        : IGrid
    {
        /// <summary>
        /// Gets or sets the cell specified by its row and column.
        /// </summary>
        /// <param name="row">The cell's row.</param>
        /// <param name="column">The cell's column</param>
        TCell this [int row,
                    int column]
        {
            get;
            set;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the cell specified by its location.
        /// </summary>
        /// <param name="location">The cell's location.</param>
        TCell this [Location location]
        {
            get;
            set;
        }
    }
}
