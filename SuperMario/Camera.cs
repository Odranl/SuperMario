using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario
{
    /// <summary>
    /// Manages everything camera related
    /// </summary>
    public class Camera
    {
        Rectangle Bounds => SuperMario.Main.Window.ClientBounds;

        float scale;
        float rotation;

        /// <summary>
        /// Position of the camera on the level
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Creates the Camera with some default values
        /// </summary>
        public Camera()
        {
            scale = 2f;
            rotation = 0f;
            Position = Vector2.Zero;
        }

        /// <summary>
        /// Updates the matrix used in the spritebatch function
        /// </summary>
        public Matrix GetMatrix()
        {
            return Matrix.CreateRotationZ(rotation) * 
                   Matrix.CreateTranslation(new Vector3(-Position, 0)) * 
                   Matrix.CreateScale(scale) * 
                   Matrix.CreateTranslation(new Vector3(Bounds.Size.ToVector2() / 2, 0));
        }

        /// <summary>
        /// Converts the position of an object on the screen to the absolute position in the world
        /// </summary>
        /// <param name="relativePosition"></param>
        /// <returns></returns>
        public Vector2 RelativeToAbsolutePosition(Vector2 relativePosition) => Vector2.Transform(relativePosition, Matrix.Invert(GetMatrix()));
    }
}
