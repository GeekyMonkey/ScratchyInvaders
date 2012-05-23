#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
#endregion

namespace ScratchyXna
{
    class MissileSprite : Sprite
    {
        /// <summary>
        /// Is the missile loaded, or flying
        /// </summary>
        public MissileStates State;


        /// <summary>
        /// Load the missile sprite
        /// </summary>
        public override void Load()
        {
            SetCostume("Missile");
            Costume.YCenter = VerticalAlignments.Center;
            Scale = 1;
            State = MissileStates.Loaded;
            Hide();
        }


        /// <summary>
        /// Updaate the missile
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // If it hit the top, then re-load it
            if (Position.Y > 100 && State == MissileStates.Flying)
            {
                Hide();
                Speed = 0;
                State = MissileStates.Loaded;
                //GameScreen.ShowScreen("gameover");
            }
        }

        /// <summary>
        /// Fire the missile!
        /// </summary>
        /// <param name="position">Where should it start from</param>
        /// <param name="rotation">How should it be rotated</param>
        public void Fire(Vector2 position, float rotation)
        {
            if (State == MissileStates.Loaded)
            {
                State = MissileStates.Flying;
                Show();
                Position = position;
                Rotation = rotation;
                Direction = 90 - rotation;
                Speed = 2f;
                PlaySound("shoot");
            }
        }


    }
}
