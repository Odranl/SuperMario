using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario.Extensions
{
    /// <summary>
    /// Contains extension methods for Xna's <see cref="Rectangle"/>
    /// </summary>
    public static class RectangleExtension
    {
        /// <summary>
        /// returns a new Rectangle with the same size but translated
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="X">Horizontal translation vector</param>
        /// <param name="Y">Vertical translation vector</param>
        /// <returns></returns>
        public static Rectangle Translate(this Rectangle rectangle, int X, int Y)
        {
            return new Rectangle(rectangle.X + X, rectangle.Y + Y, rectangle.Size.X, rectangle.Size.Y);
        }
        /// <summary>
        /// returns a new Rectangle with the same size but translated
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="translation">The translation vector</param>
        /// <returns></returns>
        public static Rectangle Translate(this Rectangle rectangle, Vector2 translation) => Translate(rectangle, (int)translation.X, (int)translation.Y);
    }
}
