using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario
{
    /// <summary>
    /// Class that store infos about a particular tile
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// The id of the foreground object, used as key to access the access the blocks Dictionary.
        /// Set to -1 to set as empty
        /// </summary>
        public int BlockId { get; set; }

        /// <summary>
        /// The id of the background object, used as key to access the access the walls Dictionary.
        /// Set to -1 to set as empty
        /// </summary>
        public int WallId;

        /// <summary>
        /// Determines whether the player can pass through it
        /// </summary>
        public bool Solid = true;

        /// <summary>
        /// Determines if the block should behave as a platform 
        /// </summary>
        public bool Platform;

        /// <summary>
        /// Default constructor used to initialize the main array with empty tiles
        /// </summary>
        public Tile()
        {
            BlockId = -1;
            WallId = -1;
        }

        /// <summary>
        /// Constructor to set a non-default tile
        /// </summary>
        /// <param name="blockId">The Id of the <see cref="LevelComponents.Block"/></param>
        /// <param name="wallId">The id of the <see cref="LevelComponents.Wall"/></param>
        public Tile(int blockId, int wallId)
        {
            this.BlockId = blockId;
            this.WallId = wallId;
        }
    }
}
