#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
#endregion

namespace ScratchyXna
{
    class BarrierSprite : Sprite
    {
        public override void Load()
        {
            SetCostume("Barrier");
            Costume.YCenter = VerticalAlignments.Center;
            Y = -50;
            Scale = 1f / 2f;
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
