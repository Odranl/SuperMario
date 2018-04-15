using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario.Graphics
{
    public abstract class AnimatedSpriteDictionary : IEnumerable<KeyValuePair<SpriteActions, AnimatedSprite>>, IDictionary<SpriteActions, AnimatedSprite>
    {
        Dictionary<SpriteActions, AnimatedSprite> animatedSprites;

        public AnimatedSpriteDictionary()
        {
            animatedSprites = new Dictionary<SpriteActions, AnimatedSprite>();
        }

        protected abstract void AddAnimatedSprites();

        #region interface implementation
        public AnimatedSprite this[SpriteActions key] { get => animatedSprites[key]; set => animatedSprites[key] = value; }

        public ICollection<SpriteActions> Keys => animatedSprites.Keys;

        public ICollection<AnimatedSprite> Values => animatedSprites.Values;

        public int Count => animatedSprites.Count;

        public bool IsReadOnly => false;

        public void Add(SpriteActions key, AnimatedSprite value)
        {
            animatedSprites.Add(key, value);
        }

        public void Add(KeyValuePair<SpriteActions, AnimatedSprite> item)
        {
            animatedSprites.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            animatedSprites.Clear();
        }

        public bool Contains(KeyValuePair<SpriteActions, AnimatedSprite> item)
        {
            return animatedSprites.Contains(item);
        }

        public bool ContainsKey(SpriteActions key)
        {
            return animatedSprites.Keys.Contains(key);
        }

        public void CopyTo(KeyValuePair<SpriteActions, AnimatedSprite>[] array, int arrayIndex)
        {
            
        }

        public IEnumerator<KeyValuePair<SpriteActions, AnimatedSprite>> GetEnumerator()
        {
            return animatedSprites.GetEnumerator();
        }

        public bool Remove(SpriteActions key)
        {
            return animatedSprites.Remove(key);
        }

        public bool Remove(KeyValuePair<SpriteActions, AnimatedSprite> item)
        {
            return animatedSprites.Remove(item.Key);
        }

        public bool TryGetValue(SpriteActions key, out AnimatedSprite value)
        {
            return animatedSprites.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return animatedSprites.GetEnumerator();
        }
        #endregion
    }

    //TODO: Start working on this
    public class MarioSmallSpriteDictionary : AnimatedSpriteDictionary
    {
        protected override void AddAnimatedSprites()
        {
            
        }
    }
}
