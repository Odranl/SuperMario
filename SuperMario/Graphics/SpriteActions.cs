using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario.Graphics
{
    /// <summary>
    /// Contains the various type of actions that can be performed by a character
    /// </summary>
    public enum SpriteActions
    {
        /// <summary>
        /// The default when no other is selected
        /// </summary>
        Default,
        /// <summary>
        /// The character is still
        /// </summary>
        Idle,
        /// <summary>
        /// The character is walking at normal speed
        /// </summary>
        Walking,
        /// <summary>
        /// The character is running fast
        /// </summary>
        Running,
        /// <summary>
        /// Character's Y velocity is not 0
        /// </summary>
        Falling,
        /// <summary>
        /// The character is crouching
        /// </summary>
        Croucing
    }
}
