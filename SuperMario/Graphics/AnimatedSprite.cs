using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario.Graphics
{
    /// <summary>
    /// Manages a single animation set for an entity, to be used with <see cref="AnimatedSpriteDictionary"/>
    /// </summary>
    public class AnimatedSprite
    {
        Texture2D spriteSheet;

        /// <summary>
        /// The total number of frames in the spriteSheet
        /// </summary>
        public int TotalFrames { get; private set; }

        private int currentFrame;

        private int columns;
        private int rows;
        /// <summary>
        /// The Width of the frame calculated on the spritesheet, used for collision bounding calculations
        /// </summary>
        public int FrameWidth { get; private set; }
        /// <summary>
        /// The Height of the frame calculated on the spritesheet, used for collision bounding calculations
        /// </summary>
        public int FrameHeight { get; private set; }

        private int waitTime = 1000 / 12;
        private float timeSinceLastFrame = 0;

        int[] actionFrames;

        /// <summary>
        /// Creates a new Animated Sprite
        /// </summary>
        /// <param name="texture">The spriteSheet containing the frames</param>
        /// <param name="totalFrames">The total number of frames in the spritesheet</param>
        /// <param name="columns">The number of columns on the sheet</param>
        /// <param name="rows">The number of rows in the sheet</param>
        /// <param name="actionFrames">The position of the chosen frames</param>
        public AnimatedSprite(Texture2D texture, int totalFrames, int columns, int rows, params int[] actionFrames)
        {
            spriteSheet = texture;
            TotalFrames = totalFrames;
            this.columns = columns;
            this.rows = rows;

            FrameWidth = texture.Width / columns;
            FrameHeight = texture.Height / rows;

            this.actionFrames = actionFrames;
        }

        /// <summary>
        /// Manages the animation speed through the use of a counter
        /// </summary>
        /// <param name="gameTime">GameTime from the main class</param>
        public void Update(GameTime gameTime)
        {
            if (actionFrames.Length == 1)
            {
                currentFrame = 0;
                return;
            }

            timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeSinceLastFrame > waitTime)
            {
                if (++currentFrame == actionFrames.Length)
                {
                    currentFrame = 0;
                }

                timeSinceLastFrame = 0;
            }
        }

        /// <summary>
        /// Draws the currently selected frame on the screen at the set position with the set <see cref="SpriteEffects"/>
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/></param>
        /// <param name="position">The position it will be drawn to</param>
        /// <param name="effects">The spriteEffects</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects effects = SpriteEffects.None)
        {
            int choosenFrame = actionFrames[currentFrame];

            int frameColumn = choosenFrame % columns;
            int frameRow = choosenFrame / columns;

            spriteBatch.Draw(texture: spriteSheet, position: position, sourceRectangle: new Rectangle(frameColumn * FrameWidth, frameRow * FrameHeight, FrameWidth, FrameHeight), color: Color.White, rotation: 0f, origin: Vector2.Zero, scale: 1f, effects: effects, layerDepth: 0f);
        }
    }
}
