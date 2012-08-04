#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
#endregion

namespace ScratchyXna
{
    class AlienMissileSprite : Sprite
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
            Costume HitCostume = AddCostume("MissileHitBarrier");
            HitCostume.YCenter = VerticalAlignments.Center;
            AddCostume("AlienBullets/BoltRight");
            AddCostume("AlienBullets/AlienMissileHitBarrier");
            SetCostume("AlienBullets/BoltLeft");
            Costume.YCenter = VerticalAlignments.Center;
            Scale = 0.5f;
            State = MissileStates.Loaded;
            Hide();
        }

        public void HitBarrier(BarrierSprite Barrier)
        {
            SetCostume("AlienBullets/AlienMissileHitBarrier");
            Barrier.Stamp(this, StampMethods.Cutout);
            State = MissileStates.Destroy; 
        }

        /// <summary>
        /// Update the missile
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // If it hit the bottom, then re-load it
            if (Position.Y < -100 && State == MissileStates.Flying)
            {
                Hide();
                Speed = 0;
                State = MissileStates.Destroy;
                //Scene.ShowScene("gameover");
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
                Direction = 270 + rotation;
                Speed = 2f;
                PlaySound("shoot");
            }
        }


    }
}
