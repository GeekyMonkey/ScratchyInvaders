#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace ScratchyXna
{
    public enum MissileStates
    {
        /// <summary>
        /// Missile is ready to go
        /// </summary>
        Loaded,

        /// <summary>
        /// Missile is flying
        /// </summary>
        Flying,
        
        /// <summary>
        /// Alien missile has hit something and needs to go away
        /// </summary>
        Destroy
    }
}
