using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario
{
    /// <summary>
    /// All the mechanics related to the player go here (e.g. Custom sprites, Camera...)
    /// </summary>
    public class Player : Character
    {
        Graphics.MarioSmallSpriteDictionary marioSmallSprites;
        public Camera Camera { get; private set; }


        bool drawBoundingBox = true;

        public Player()
        {
            marioSmallSprites = new Graphics.MarioSmallSpriteDictionary();
            RigidBody = new RigidBody(marioSmallSprites.FrameWidth, marioSmallSprites.FrameHeight);

            Camera = new Camera();
        }

        public void Update(GameTime gameTime)
        {
            if (SuperMario.Main.KeyboardState.IsKeyDown(Keys.Right))
            {
                marioSmallSprites.FacingRight = true;
                ++RigidBody.velocity.X;
            }
            if (SuperMario.Main.KeyboardState.IsKeyDown(Keys.Left))
            {
                marioSmallSprites.FacingRight = false;
                --RigidBody.velocity.X;
            }

            RigidBody.Update();

            if (RigidBody.velocity.Y != 0)
            {
                marioSmallSprites.ChoosenAction = Graphics.SpriteActions.Falling;
            }
            else if (RigidBody.velocity.X != 0 )
            {
                marioSmallSprites.ChoosenAction = Graphics.SpriteActions.Walking;
            }
            else
            {
                marioSmallSprites.ChoosenAction = Graphics.SpriteActions.Idle;
            }
            marioSmallSprites.UpdateAction(gameTime);
            
            Camera.Position = (RigidBody.position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            marioSmallSprites.DrawAction(spriteBatch, RigidBody.position);
            if (drawBoundingBox)
            {
                var texture = new Texture2D(spriteBatch.GraphicsDevice, 16, 16);
                texture.SetData(Enumerable.Range(0, texture.Width * texture.Height).Select(i => new Color(Color.Yellow, 0.5f)).ToArray());

                spriteBatch.Draw(texture, RigidBody.collider, Color.Yellow);
            }
        }
    }
}
