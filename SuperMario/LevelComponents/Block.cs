using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SuperMario.LevelComponents
{
    /// <summary>
    /// This class contains all the data for foreground Tile objects
    /// </summary>
    public abstract class Block
    {
        /// <summary>
        /// The tileset's number of frame per row
        /// </summary>
        public const int COLUMNS = 8;
        /// <summary>
        /// The number of frames per column
        /// </summary>
        public const int ROWS = 6;
        /// <summary>
        /// Standard size of a single frame
        /// </summary>
        public const int TILE_SIZE = 16;
        /// <summary>
        /// Padding between each frame in the tileset
        /// </summary>
        /// TODO: fully implement or scrap this feature
        [Obsolete("Currently there's no padding setting, it's useless", true)]
        public const int TILE_PADDING = 1;

        /// <summary>
        /// The actual TextureAtlas
        /// </summary>
        public readonly Texture2D TileSet;

        /// <summary>
        /// The Id used to identify this block, used in <see cref="Tile"/>
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Initializes the custom properties from <see cref="OnInitialize"/>
        /// and loads the tileset from "Blocks/ClassName" path in content pipeline
        /// </summary>
        protected Block()
        {
            OnInitialize();
            TileSet = SuperMario.Main.Content.Load<Texture2D>($"Blocks/{this.GetType().Name.ToString()}");
        }

        /// <summary>
        /// This method is used to specify particular properties of the block
        /// (e.g. Id, Solid...)
        /// </summary>
        public abstract void OnInitialize();

        /// <summary>
        /// Each tile set is made of a 8x6 grid and between each frame
        /// </summary>
        /// <param name="frame">The particular frame to select (e.g. a corner or a border)</param>
        /// <returns>The <see cref="Rectangle"/> coordinates on the tileSet</returns>
        public static Rectangle GetFrame(TileMapping frame)
        {
            int column = ((int)frame % COLUMNS);
            int row = (int)frame / COLUMNS;

            return new Rectangle(
                (column * TILE_SIZE),
                (row * TILE_SIZE),
                TILE_SIZE,
                TILE_SIZE
            );
        }
        /// <summary>
        /// Calculate the correct frame to display on screen from the tileset
        /// </summary>
        /// <param name="x">x-coord of the tile in SuperMario.Main.tiles</param>
        /// <param name="y">y-coord of the tile in SuperMario.Main.tiles array</param>
        /// <param name="tileId">the tile's id to check against</param>
        /// <returns></returns>
        public static Rectangle GetFrame(int x, int y, int tileId) => GetFrame(GetTileFromBit(GetBitMapping(x, y, tileId)));

        /// <summary>
        /// Calculates the bit-mapping for the tileset to choose the correct frame
        /// refer to  <see href="https://gamedevelopment.tutsplus.com/tutorials/how-to-use-tile-bitmasking-to-auto-tile-your-level-layouts--cms-25673">this</see> for more infos
        /// </summary>
        /// <param name="x">x-coord of the tile in SuperMario.Main.tiles</param>
        /// <param name="y">y-coord of the tile in SuperMario.Main.tiles array</param>
        /// <param name="tileId">the tile's id to check against</param>
        /// <returns>The bitmap value to be used in GetTileFromBit method</returns>
        public static byte GetBitMapping(int x, int y, int tileId)
        {
            byte value = 0;
            //Loop through all the adjacent tiles to check if there are other similar
            //i and j are set to 0 instead of -1 in order to calculate each tile's value in a simpler way
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (i == 1 && j == 1 || x + i - 1 < 0 || y + j - 1 < 0) continue;
                    try
                    {
                        if (SuperMario.Main.tiles[x + i - 1, y + j - 1].BlockId == tileId)
                        {
                            //Each tile's value is expressed in power of 2
                            int pow = (j * 3) + i;
                            //We want to ignore the middle tile which is the original one
                            if (pow >= 5) --pow;
                            //Check if it's a corner, this way we can reduce drastically the output
                            if (pow == 0 || pow == 2 || pow == 5 || pow == 7)
                            {
                                //Top-Left corner
                                if (pow == 0)
                                {
                                    if (SuperMario.Main.tiles[x, y - 1].BlockId == ID.BlockId.Air || SuperMario.Main.tiles[x - 1, y].BlockId == ID.BlockId.Air)
                                    {
                                        continue;
                                    }
                                }
                                //Top-Right corner
                                else if (pow == 2)
                                {
                                    if (SuperMario.Main.tiles[x, y - 1].BlockId == ID.BlockId.Air || SuperMario.Main.tiles[x + 1, y].BlockId == ID.BlockId.Air)
                                    {
                                        continue;
                                    }
                                }
                                //Bottom-Left corner
                                else if (pow == 5)
                                {
                                    if (SuperMario.Main.tiles[x - 1, y].BlockId == ID.BlockId.Air || SuperMario.Main.tiles[x, y + 1].BlockId == ID.BlockId.Air)
                                    {
                                        continue;
                                    }
                                }
                                //Bottom-Right corner
                                else if (pow == 7)
                                {
                                    if (SuperMario.Main.tiles[x + 1, y].BlockId == ID.BlockId.Air || SuperMario.Main.tiles[x, y + 1].BlockId == ID.BlockId.Air)
                                    {
                                        continue;
                                    }
                                }
                            }

                            value += (byte)Math.Pow(2, pow);
                        }
                    }
                    //TODO Optimize the code to not raise an expensive Exception
                    catch (IndexOutOfRangeException)
                    {

                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Returns the correct <see cref="TileMapping"/> element from a BitMap input value
        /// </summary>
        /// <param name="value">The BitMap value resulted from <see cref="GetBitMapping(int, int, int)"/></param>
        /// <returns></returns>
        public static TileMapping GetTileFromBit(byte value)
        {
            switch (value)
            {
                case 255: return TileMapping.Center;
                case 254: return TileMapping.TopLeftInnerCorner;
                case 251: return TileMapping.TopRightInnerCorner;
                case 250: return TileMapping.TopLeftInnerCornerTopRightInnerCorner;
                case 248: return TileMapping.TopMiddleBorder;
                case 223: return TileMapping.BottomLeftInnerCorner;
                case 222: return TileMapping.TopLeftInnerCornerBottomLeftInnerCorner;
                case 219: return TileMapping.TopRightInnerCornerBottomLeftInnerCorner;
                case 218: return TileMapping.TopLeftInnerCornerTopRightInnerCornerBottomLeftInnerCorner;
                case 216: return TileMapping.TopMiddleBorderBottomLeftInnerCorner;
                case 214: return TileMapping.LeftBorder;
                case 210: return TileMapping.LeftMiddleBorderTopRightInnerCorner;
                case 208: return TileMapping.TopLeftCorner;
                case 127: return TileMapping.BottomRightInnerCorner;
                case 126: return TileMapping.TopLeftInnerCornerBottomRightInnerCorner;
                case 123: return TileMapping.TopRightInnerCornerBottomRightInnerCorner;
                case 122: return TileMapping.TopLeftInnerCornerTopRightInnerCornerBottomRightInnerCorner;
                case 120: return TileMapping.TopMiddleBorderBottomRightInnerCorner;
                case 107: return TileMapping.RightBorder;
                case 106: return TileMapping.RightMiddleBorderTopLeftInnerCorner;
                case 104: return TileMapping.TopRightCorner;
                case 95: return TileMapping.BottomLeftInnerCornerBottomRightInnerCorner;
                case 94: return TileMapping.TopLeftInnerCornerBottomLeftInnerCornerBottomRightInnerCorner;
                case 91: return TileMapping.TopRightInnerCornerBottomLeftInnerCornerBottomRightInnerCorner;
                case 90: return TileMapping.AllFourInnerCorner;
                case 88: return TileMapping.TopMiddleBorderBottomLeftInnerCornerBottomRighInnerCorner;
                case 86: return TileMapping.LeftMiddleBorderBottomRightInnerCorner;
                case 82: return TileMapping.LeftMiddleBorderTopRightInnerCornerBottomRightInnerCorner;
                case 80: return TileMapping.TopLeftCornerBottomRightInnerCorner;
                case 75: return TileMapping.RightMiddleBorderBottomLeftInnerCorner;
                case 74: return TileMapping.RightMiddleBorderTopLeftInnerCornerBottomLeftInnerCorner;
                case 72: return TileMapping.TopRightCornerBottomLeftInnerCorner;
                case 66: return TileMapping.LeftRightBorder;
                case 64: return TileMapping.TopLeftRightBorder;
                case 31: return TileMapping.BottomMiddleBorder;
                case 30: return TileMapping.BottomMiddleBorderTopLeftInnerCorner;
                case 27: return TileMapping.BottomMiddleBorderTopRightInnerCorner;
                case 26: return TileMapping.BottomMiddleBorderTopLeftInnerCornerTopRightInnerCorner;
                case 24: return TileMapping.TopBottomBorder;
                case 22: return TileMapping.BottomLeftCorner;
                case 18: return TileMapping.BottomLeftCornerTopRightInnerCorner;
                case 16: return TileMapping.TopLeftBottomBorder;
                case 11: return TileMapping.BottomRightCorner;
                case 10: return TileMapping.BottomRightCornerTopLeftInnerCorner;
                case 8: return TileMapping.TopRightBottomBorder;
                case 2: return TileMapping.BottomLeftRightBorder;
                case 0: return TileMapping.AllBorder;
            }
            return TileMapping.AllBorder;
        }
    }

    class Dirt : Block
    {
        public override void OnInitialize()
        {
            Id = ID.BlockId.Grass;

        }
    }
}
