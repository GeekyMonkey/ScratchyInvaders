#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
#endregion

namespace ScratchyXna
{
    class LivesSprite : Sprite
    {
        public override void Load()
        {
            Y = -98;
            Scale = 0.2f;
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
