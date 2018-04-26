using Microsoft.Xna.Framework;
using SuperMario.LevelComponents;

namespace SuperMario
{
    /// <summary>
    /// Manages everything Physics related
    /// </summary>
    public class RigidBody
    {
        /// <summary>
        /// Shortcut for the collider's position in the world
        /// </summary>
        public Vector2 position
        {
            get => collider.Location.ToVector2();
            set
            {
                collider.Location = value.ToPoint();
            }
        }

        Vector2 tilePosition => collider.Center.ToVector2() / Block.TILE_SIZE;
        /// <summary>
        /// The vector containing the velocity of the <see cref="RigidBody"/>
        /// </summary>
        public Vector2 velocity;
        /// <summary>
        /// The <see cref="Rectangle"/> used to compute collisions
        /// </summary>
        public Rectangle collider;

        Vector2 maxSpeed;

        float gravityAcceleration = 1;
        float dragPercentage = 0.05f;
        /// <summary>
        /// Default constructor that creates and initiazlizes the collider
        /// </summary>
        /// <param name="colliderSize">Size of the collider</param>
        public RigidBody(Vector2 colliderSize)
        {
            position = Vector2.Zero;
            velocity = Vector2.Zero;

            collider.Location = position.ToPoint();
            collider.Size = colliderSize.ToPoint();

            maxSpeed = new Vector2(8, 10);
        }
        /// <summary>
        /// Creates a new Rigid Body with a collider of given size
        /// </summary>
        /// <param name="xSize">Width of the collider</param>
        /// <param name="ySize">The Height of the collider</param>
        /// <returns></returns>
        public RigidBody(int xSize, int ySize) : this(new Vector2(xSize, ySize))
        {

        }


        public void Update()
        {
            velocity = Vector2.Clamp(velocity, new Vector2(-maxSpeed.X, -20 * maxSpeed.Y), maxSpeed);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int tileX = i + (int)tilePosition.X;
                    int tileY = j + (int)tilePosition.Y;

                    if (tileX < 0 || tileY < 0) continue;

                    if (SuperMario.Main.tiles[tileX, tileY].Solid && SuperMario.Main.tiles[tileX, tileY].BlockId != -1)
                    {
                        Rectangle tileCollider = new Rectangle(tileX * Block.TILE_SIZE, tileY * Block.TILE_SIZE, Block.TILE_SIZE, Block.TILE_SIZE);
                        if (Collider.CheckSimpleCollision(collider, velocity, tileCollider, out var result))
                        {
                            position += new Vector2(result.Value.X, result.Value.Y);
                            velocity += new Vector2(result.Value.Z, result.Value.W);

                            return;
                        }
                    }
                }
            }

            velocity.Y += gravityAcceleration;

            position += velocity;

        }
    }
}
