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
        float scale;
        float rotation;

        /// <summary>
        /// Position of the camera on the level
        /// </summary>
        public Vector2 position;

        /// <summary>
        /// Transformation matrix that can be used for particular effects
        /// </summary>
        public Matrix Matrix { get; set; }

        /// <summary>
        /// Creates the Camera with some default values
        /// </summary>
        public Camera()
        {
            scale = 2f;
            rotation = 0f;
            position = Vector2.Zero;
        }

        /// <summary>
        /// Updates the matrix used in the spritebatch function
        /// </summary>
        public void Update()
        {
            Matrix = Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(scale) * Matrix.CreateTranslation(new Vector3(position, 0));
        }
    }
}
