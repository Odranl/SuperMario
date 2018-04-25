using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario
{
    /// <summary>
    /// Base class for shared properties between NPC and characters
    /// </summary>
    public class Character
    {
        /// <summary>
        /// This RigidBody manages all the physics of the character
        /// </summary>
        public RigidBody RigidBody { get; protected set; }
    }
}
