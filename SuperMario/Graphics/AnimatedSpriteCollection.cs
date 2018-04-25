using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario.Graphics
{
    /// <summary>
    /// This class is used to manage multiple sets of animations that can be accessed through a <see cref="SpriteActions"/> value
    /// </summary>
    public abstract class AnimatedSpriteDictionary : IEnumerable<KeyValuePair<SpriteActions, AnimatedSprite>>, IDictionary<SpriteActions, AnimatedSprite>
    {
        /// <summary>
        /// The dictionary containing the various sets of animations
        /// </summary>
        public Dictionary<SpriteActions, AnimatedSprite> AnimatedSprites { get; private set; }
        /// <summary>
        /// The currently selected action from the animations dictionary
        /// </summary>
        public SpriteActions ChoosenAction { get; set; }
        /// <summary>
        /// Used to determine whether to flip the sprite that is flipped if set to <see langword="true"/>
        /// </summary>
        public bool FacingRight { get; set; }
        /// <summary>
        /// Shortcut to access the frame width from the animation sprite
        /// </summary>
        public int FrameWidth => this[ChoosenAction].FrameWidth;
        /// <summary>
        /// Shortcut to access the frame height from the animation sprite
        /// </summary>
        public int FrameHeight => this[ChoosenAction].FrameHeight;
        /// <summary>
        /// The spritesheet that will be used by the animation set
        /// </summary>
        protected Texture2D spriteSheet;

        /// <summary>
        /// Default constructor, initializes the dictionary and calls the custom code from child classes
        /// </summary>
        public AnimatedSpriteDictionary()
        {
            AnimatedSprites = new Dictionary<SpriteActions, AnimatedSprite>();
            AddAnimatedSprites();
        }
        /// <summary>
        /// This method is used by child classes to load the animations in the dictionary
        /// </summary>
        protected abstract void AddAnimatedSprites();

        /// <summary>
        /// Updates the counter of the animation
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateAction(GameTime gameTime)
        {
            this[ChoosenAction].Update(gameTime);
        }
        /// <summary>
        /// Draws the frame on the screen at the chosen position
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position">The position in the world</param>
        /// <param name="action">The selected animation</param>
        public void DrawAction(SpriteBatch spriteBatch, Vector2 position, SpriteActions action)
        {
            this[action].Draw(spriteBatch, position);
        }
        /// <summary>
        /// Draws the frame on the screen at the chosen position 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position">The position in the world</param>
        public void DrawAction(SpriteBatch spriteBatch, Vector2 position)
        {
            this[ChoosenAction].Draw(spriteBatch, position, FacingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        #region interface implementation
        /// <summary>
        /// Returns an <see cref="AnimatedSprite"/> assigned to the selected key
        /// </summary>
        /// <param name="key">The key of the value to access</param>
        /// <returns></returns>
        public AnimatedSprite this[SpriteActions key] { get => AnimatedSprites[key]; set => AnimatedSprites[key] = value; }
        /// <summary>
        /// The collection of Keys contained in the dictionary
        /// </summary>
        public ICollection<SpriteActions> Keys => AnimatedSprites.Keys;
        /// <summary>
        /// The collection of values contained in the dictionary
        /// </summary>
        public ICollection<AnimatedSprite> Values => AnimatedSprites.Values;
        /// <summary>
        /// The number of objects stored in the dictionary
        /// </summary>
        public int Count => AnimatedSprites.Count;
        /// <summary>
        /// Whether the dictionary is read-only
        /// </summary>
        public bool IsReadOnly => false;
        /// <summary>
        /// Add a new <see cref="AnimatedSprite"/> to the Dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(SpriteActions key, AnimatedSprite value)
        {
            AnimatedSprites.Add(key, value);
        }
        /// <summary>
        /// Add a new <see cref="AnimatedSprite"/> to the Dictionary
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<SpriteActions, AnimatedSprite> item)
        {
            AnimatedSprites.Add(item.Key, item.Value);
        }
        /// <summary>
        /// Deletes all items
        /// </summary>
        public void Clear()
        {
            AnimatedSprites.Clear();
        }
        /// <summary>
        /// Used to check if an item is contained in the dictionary
        /// </summary>
        /// <param name="item">The item to check for</param>
        /// <returns>Whether the element has been found</returns>
        public bool Contains(KeyValuePair<SpriteActions, AnimatedSprite> item)
        {
            return AnimatedSprites.Contains(item);
        }
        /// <summary>
        /// Determines whether the specified element is contained in the collection of keys
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(SpriteActions key)
        {
            return AnimatedSprites.Keys.Contains(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        [Obsolete("This method has not been implemented and probably will never be", true)]
        public void CopyTo(KeyValuePair<SpriteActions, AnimatedSprite>[] array, int arrayIndex)
        {
   
        }
        /// <summary>
        /// Returns the Dictionary's enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<SpriteActions, AnimatedSprite>> GetEnumerator()
        {
            return AnimatedSprites.GetEnumerator();
        }
        /// <summary>
        /// Removes an item with the specified key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(SpriteActions key)
        {
            return AnimatedSprites.Remove(key);
        }
        /// <summary>
        /// Removes the specified Pair from the <see cref="Dictionary{SpriteActions, AnimatedSprite}"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<SpriteActions, AnimatedSprite> item)
        {
            return AnimatedSprites.Remove(item.Key);
        }
        /// <summary>
        /// Get the values associated with the specified key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(SpriteActions key, out AnimatedSprite value)
        {
            return AnimatedSprites.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return AnimatedSprites.GetEnumerator();
        }
        #endregion
    }

    //TODO: Start working on this
    /// <summary>
    /// Custom implementation of the AnimationDictionatry for the player
    /// </summary>
    public class MarioSmallSpriteDictionary : AnimatedSpriteDictionary
    {
        /// <summary>
        /// Small Mario's animations get loaded here
        /// </summary>
        protected override void AddAnimatedSprites()
        {
            spriteSheet = SuperMario.Main.Content.Load<Texture2D>("Player/MarioSmall");
            Add(SpriteActions.Idle, new AnimatedSprite(spriteSheet, 16, 16, 1, 1));
            Add(SpriteActions.Walking, new AnimatedSprite(spriteSheet, 16, 16, 1, 0, 1));
            Add(SpriteActions.Falling, new AnimatedSprite(spriteSheet, 16, 16, 1, 4));
        }
    }
}
