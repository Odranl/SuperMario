using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperMario.LevelComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SuperMario
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SuperMario : Game
    {
        /// <summary>
        /// Singleton instance of the game
        /// </summary>
        public static SuperMario Main { get; private set; }
        //TODO: implement separate levels with custom sizes and properties
        Level level;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dictionary<int, Block> blocks;

        MouseState mouseState;
        MouseState oldMouseState;

        public KeyboardState KeyboardState { get; private set; }
        public KeyboardState OldKeyboardState { get; private set; }

        Player player;

        /// <summary>
        /// The level grid containing tiles infos
        /// </summary>
        public Tile[,] tiles;

        /// <summary>
        /// Initialize and start the game
        /// </summary>
        public SuperMario()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Main = this;
            base.Initialize();

            IsMouseVisible = true;

            Window.AllowUserResizing = true;
            Window.Title = "SuperMarioEngine Debug";

            string nameSpace = "SuperMario.LevelComponents";
            //Load all Block types present in the above namespace
            var q = (from t in Assembly.GetExecutingAssembly().GetTypes()
                     where t.IsClass && t.Namespace == nameSpace && !t.IsAbstract && t.BaseType == typeof(Block)
                     select t).ToList();

            blocks = new Dictionary<int, Block>();

            for (int i = 0; i < q.Count; i++)
            {
                //Load all the single instances of the various blocks in the Dictionary for easy
                //access through all the code
                Block block = (Block)Activator.CreateInstance(q.ElementAtOrDefault(i));
                blocks.Add(block.Id, block);
            }

            //TODO change to a dynamic size
            tiles = new Tile[255, 255];
            for (int i = 0; i < 255; i++)
            {
                for (int j = 0; j < 255; j++)
                {
                    tiles[i, j] = new Tile();
                }
            }

            //Testing code for new BitMap algorithm
            tiles[3, 3] = new Tile(ID.BlockId.Grass, -1);
            tiles[4, 3] = new Tile(ID.BlockId.Grass, -1);
            tiles[5, 3] = new Tile(ID.BlockId.Grass, -1);
            tiles[4, 4] = new Tile(ID.BlockId.Grass, -1);

            tiles[0, 6] = new Tile(ID.BlockId.Grass, -1);


            player = new Player();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState = Keyboard.GetState();
            MouseManagement();

            player.Update(gameTime);

            base.Update(gameTime);

            OldKeyboardState = KeyboardState;
        }

        private void MouseManagement()
        {
            mouseState = Mouse.GetState();

            var tilePosition = player.Camera.RelativeToAbsolutePosition(mouseState.Position.ToVector2()) / 16;
            if (tilePosition.X > 0 && tilePosition.Y > 0)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    tiles[(int)tilePosition.X, (int)tilePosition.Y].BlockId = 0;
                }
                else if (mouseState.RightButton == ButtonState.Pressed)
                {
                    tiles[(int)tilePosition.X, (int)tilePosition.Y].BlockId = -1;
                }
            }

            oldMouseState = mouseState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: player.Camera.GetMatrix(), samplerState: SamplerState.PointClamp);

            for (int x = 0; x < tiles.GetLength(0); ++x)
            {
                for (int y = 0; y < tiles.GetLength(1); ++y)
                {
                    if (tiles[x, y].BlockId != -1)
                        spriteBatch.Draw(blocks[tiles[x, y].BlockId].TileSet, new Vector2(x * Block.TILE_SIZE, y * Block.TILE_SIZE), Block.GetFrame(x, y, tiles[x, y].BlockId), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }

            var texture = new Texture2D(GraphicsDevice, 16, 16);
            texture.SetData(Enumerable.Range(0, texture.Width * texture.Height).Select(i => new Color(Color.Yellow, 0.5f)).ToArray());

            var tilePosition = player.Camera.RelativeToAbsolutePosition(mouseState.Position.ToVector2()) / 16;
            spriteBatch.Draw(texture, new Vector2((int)tilePosition.X * 16, (int)tilePosition.Y * 16), Color.Yellow);

            player.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
