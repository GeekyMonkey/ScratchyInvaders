#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
#endregion

namespace ScratchyXna
{
    class LineSprite : Sprite
    {
        public override void Load()
        {
            SetCostume("GreenLine");
            Costume.YCenter = VerticalAlignments.Center;
            Y = -90;
            Scale = 1;
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
