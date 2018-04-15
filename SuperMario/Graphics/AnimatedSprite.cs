using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario.Graphics
{
    class AnimatedSprite
    {
        Texture2D spriteSheet;
        public int TotalFrames { get; set; }
        private int currentFrame;

        private int columns;
        private int rows;

        private int frameWidth;
        private int frameHeight;

        private int waitTime;
        private long oldTime = 0;

        int[] actionFrames;

        public AnimatedSprite(Texture2D texture, int totalFrames, int columns, int rows, params int[] actionFrames)
        {
            spriteSheet = texture;
            TotalFrames = totalFrames;
            this.columns = columns;
            this.rows = rows;

            frameWidth = texture.Width / columns;
            frameHeight = texture.Height / rows;

            this.actionFrames = actionFrames;
        }

        public void Update(GameTime gameTime)
        {
            if (oldTime == 0)
            {
                oldTime = gameTime.TotalGameTime.Ticks;
            }

            if (gameTime.TotalGameTime.Ticks - oldTime == waitTime)
            {
                if (++currentFrame == actionFrames.Length)
                {
                    currentFrame = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int choosenFrame = actionFrames[currentFrame];

            int frameColumn = choosenFrame % columns;
            int frameRow = choosenFrame / columns;

            spriteBatch.Draw(spriteSheet, position, new Rectangle(frameColumn * frameWidth, frameRow * frameHeight, frameWidth, frameHeight), Color.White);
        }
    }
}
