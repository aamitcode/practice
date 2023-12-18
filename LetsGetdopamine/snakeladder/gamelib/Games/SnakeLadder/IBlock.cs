/******************************************************************************
 * Author : Amit Chauhan
 * Interface Block of the Board
 ******************************************************************************/
namespace gamelib.V2
{
    /// <summary>
    /// Interface for Block
    /// </summary>
    public interface IBlock
    {
        /// <summary>
        /// Destination of block
        /// </summary>
        public int Destination { get;}

        /// <summary>
        /// Block Type
        /// </summary>
        public BlockType BlockType { get; }
    }
}
