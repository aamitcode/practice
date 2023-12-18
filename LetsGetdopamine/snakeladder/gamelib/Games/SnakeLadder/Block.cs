/******************************************************************************
 * Author : Amit Chauhan
 * Block of the Board
 ******************************************************************************/
namespace gamelib.V2
{
    /// <summary>
    /// Block of board
    /// </summary>
    public class Block(BlockType blockType, int des) : IBlock
    {
        private readonly BlockType blockType = blockType;
        private readonly int destination = des;

        public int Destination
        {
            get
            {
                return this.destination;
            }
        }

        public BlockType BlockType
        {
            get
            {
                return this.blockType;
            }
        }
    }
}
