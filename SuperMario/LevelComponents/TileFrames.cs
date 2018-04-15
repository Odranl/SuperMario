using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario.LevelComponents
{
    /// <summary>
    /// This Enum is used to easily identify each tile from the tileset in a sorted way
    /// </summary>
    public enum TileMapping
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        TopLeftCorner,
        TopMiddleBorder,
        TopRightCorner,
        BottomRightInnerCorner,
        BottomLeftInnerCorner,
        TopLeftCornerBottomRightInnerCorner,
        TopBottomBorder,
        TopRightCornerBottomLeftInnerCorner,
        LeftBorder,
        Center,
        RightBorder,
        TopRightInnerCorner,
        TopLeftInnerCorner,
        LeftRightBorder,
        AllBorder,
        TopLeftRightBorder,
        BottomLeftCorner,
        BottomMiddleBorder,
        BottomRightCorner,
        TopLeftBottomBorder,
        TopRightBottomBorder,
        BottomLeftCornerTopRightInnerCorner,
        TopBottomBorder2,
        BottomRightCornerTopLeftInnerCorner,
        TopMiddleBorderBottomRightInnerCorner,
        TopMiddleBorderBottomLeftInnerCorner,
        LeftMiddleBorderBottomRightInnerCorner,
        RightMiddleBorderBottomLeftInnerCorner,
        TopMiddleBorderBottomLeftInnerCornerBottomRighInnerCorner,
        BottomLeftInnerCornerBottomRightInnerCorner,
        LeftMiddleBorderTopRightInnerCornerBottomRightInnerCorner,
        RightMiddleBorderTopLeftInnerCornerBottomLeftInnerCorner,
        BottomMiddleBorderTopRightInnerCorner,
        BottomMiddleBorderTopLeftInnerCorner,
        LeftMiddleBorderTopRightInnerCorner,
        RightMiddleBorderTopLeftInnerCorner,
        BottomMiddleBorderTopLeftInnerCornerTopRightInnerCorner,
        TopLeftInnerCornerTopRightInnerCorner,
        TopRightInnerCornerBottomRightInnerCorner,
        TopLeftInnerCornerBottomLeftInnerCorner,
        TopLeftInnerCornerTopRightInnerCornerBottomLeftInnerCorner,
        TopLeftInnerCornerTopRightInnerCornerBottomRightInnerCorner,
        TopRightInnerCornerBottomLeftInnerCornerBottomRightInnerCorner,
        TopLeftInnerCornerBottomLeftInnerCornerBottomRightInnerCorner,
        AllFourInnerCorner,
        TopRightInnerCornerBottomLeftInnerCorner,
        TopLeftInnerCornerBottomRightInnerCorner,
        BottomLeftRightBorder
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
