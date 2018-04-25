using Microsoft.Xna.Framework;
using SuperMario.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario
{
    /// <summary>
    /// Core methods of the collision engine
    /// </summary>
    public static class Collider
    {
        /// <summary>
        /// Check the collision between a dynamic object and a static one
        /// </summary>
        /// <param name="collider">The dynamic object</param>
        /// <param name="velocity">The object's velocity</param>
        /// <param name="other">The other item to check the collision against</param>
        /// <param name="colliderAdjustement">The result of the calculation, X and Y contain the position offset to apply while Z and W contain the velocity correction</param>
        /// <returns></returns>
        public static bool CheckSimpleCollision(Rectangle collider, Vector2 velocity, Rectangle other, out Vector4? colliderAdjustement)
        {
            Rectangle colliderMoved = collider.Translate((int)velocity.X, (int)velocity.Y);
            Vector4 result = Vector4.Zero;

            if (!IsColliding(colliderMoved, other))
            {
                /*Vector4 checkCollisionResult = Vector4.Zero;
                if (IsColliding(collider.Translate(1, 0), other))
                {
                    checkCollisionResult.Y = velocity.Y;
                }

                if (IsColliding(collider.Translate(0, 1), other))
                {
                    checkCollisionResult.Y = 0;
                }

                if (checkCollisionResult != Vector4.Zero)
                {
                    colliderAdjustement = checkCollisionResult;
                    return true;
                }
                else
                {
                    colliderAdjustement = null;
                    return false;
                }*/

                if (IsColliding(collider.Translate(0, 2), other))
                {
                    colliderAdjustement = new Vector4(velocity.X, 0, 0, 0);
                    return true;
                }
                else
                {
                    colliderAdjustement = null;
                    return false;
                }
            }

            Rectangle translatedX = collider.Translate((int)velocity.X, 0);

            if (IsColliding(translatedX, other))
            {
                if (velocity.X < 0)
                {
                    int clippingSizeX = other.Right - translatedX.Left;
                    result.X = velocity.X + clippingSizeX;
                    result.Z = -velocity.X;
                }
                else if (velocity.X > 0)
                {
                    int clippingSizeX = translatedX.Right - other.Left;
                    result.X = velocity.X - clippingSizeX;
                    result.Z = -velocity.X;
                }

            }

            Rectangle translatedY = collider.Translate((int)result.X, (int)velocity.Y);

            if (velocity.Y > 0)
            {
                /* if (collider.Bottom > other.Bottom)
                 {
                     result.Y = 0;
                     result.Z = 0;
                 }
                 else*/
                {
                    if (IsColliding(translatedY, other))
                    {
                        int clippingSizeY = translatedY.Bottom - other.Top + 1;
                        result.Y = velocity.Y - clippingSizeY;
                        result.W = -velocity.Y;
                    }
                }
            }
            else if (velocity.Y < 0)
            {
                if (collider.Top < other.Top)
                {
                    /*if (collider.Bottom > other.Bottom)
                    {
                        result.Y = 0;
                        result.Z = 0;
                    }
                    else*/
                    {
                        if (IsColliding(translatedY, other))
                        {
                            int clippingSizeY = other.Bottom - translatedY.Top + 1;
                            result.Y = velocity.Y + clippingSizeY;
                            result.W = -velocity.Y;
                        }
                    }
                }
            }

            colliderAdjustement = result;

            return true;
        }
        /// <summary>
        /// Check whether two rectangles intersect
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns></returns>
        public static bool IsColliding(Rectangle rect1, Rectangle rect2)
        {
            return rect1.Intersects(rect2);
        }
    }
}
