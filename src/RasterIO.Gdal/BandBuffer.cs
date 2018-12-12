// Contributors:
//   James Domingo, Green Code LLC

using Landis.SpatialModeling;
using System;
using System.Collections.Generic;

namespace Landis.RasterIO.Gdal
{
    /// <summary>
    /// A buffer to hold data for a raster band.
    /// </summary>
    public class BandBuffer<T>
        where T : struct
    {
        private BlockDimensions blockDimensions;
        private Dimensions rasterDimensions;
        private BandBlock<T>[] blocks;
        private int xOffset;          // X offset of the current data value relative to left edge of whole raster band
        private int yOffset;          // Y offset of the current data value relative to top edge of whole raster band
        private int yOffsetInBuffer;  // Y offset of the current data value relative to top edge of band buffer
        private int currentBlock;
        private int currentIndexInBlock;
        private bool atEnd;

        public BandBuffer(BlockDimensions blockDimensions,
                          Dimensions      rasterDimensions)
        {
            this.blockDimensions = blockDimensions;
            this.rasterDimensions = rasterDimensions;
            
            int nBlocks = (int) Math.Ceiling((double) rasterDimensions.Columns / blockDimensions.XSize);
            blocks = new BandBlock<T>[nBlocks];
            for (int i = 0; i < nBlocks; ++i) {
                BandBlock<T> block = new BandBlock<T>(blockDimensions);
                blocks[i] = block;
                block.XOffset = i * blockDimensions.XSize;
                block.YOffset = 0;
                if (i == nBlocks - 1)  // last block
                    block.UsedPortionXSize = rasterDimensions.Columns - block.XOffset;
                else
                    block.UsedPortionXSize = blockDimensions.XSize;
            }

            yOffset = 0;
            Reset();
        }

        public void Reset()
        {
            xOffset = 0;
            yOffsetInBuffer = 0;
            currentBlock = 0;
            currentIndexInBlock = 0;
            atEnd = false;

            int usedPortionYSize = Math.Min(blockDimensions.YSize, rasterDimensions.Rows - yOffset);
            foreach (BandBlock<T> block in blocks) {
                block.YOffset = yOffset;
                block.UsedPortionYSize = usedPortionYSize;
            }
        }

        public IEnumerable< BandBlock<T> > Blocks
        {
            get {
                return blocks;
            }
        }

        public void WriteValue(T dataValue)
        {
            if (atEnd)
                throw new InvalidOperationException("Writing value past end of buffer");
            blocks[currentBlock].Buffer[currentIndexInBlock] = dataValue;
            AdvanceIndexes();
        }

        public T ReadValue()
        {
            if (atEnd)
                throw new InvalidOperationException("Reading value past end of buffer");
            T dataValue = blocks[currentBlock].Buffer[currentIndexInBlock];
            AdvanceIndexes();
            return dataValue;
        }

        private void AdvanceIndexes()
        {
            if (atEnd)
                throw new InvalidOperationException("Trying to advance indexes back end of buffer");

            xOffset++;
            if (xOffset >= rasterDimensions.Columns) {
                // Start at the left edge of next row.
                xOffset = 0;
                yOffset++;
                yOffsetInBuffer++;
                if (yOffsetInBuffer >= blocks[0].UsedPortionYSize)
                    atEnd = true;
            }

            if (! atEnd) {
                currentBlock = (int) (xOffset / blockDimensions.XSize);
                currentIndexInBlock = (xOffset - blocks[currentBlock].XOffset) + (yOffsetInBuffer * blockDimensions.XSize);
            }
        }

        public bool AtEnd
        {
            get {
                return atEnd;
            }
        }
    }
}
