#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace ScratchyXna
{
    public class ShipSprite : Sprite
    {
        public float ShipY = -80f;

        /// <summary>
        /// Load the ship
        /// </summary>
        public override void Load()
        {
            SetCostume("ship").YCenter = VerticalAlignments.Top;
            AddCostume("ShipDeath").YCenter = VerticalAlignments.Top;
            AddSound("ShipDeath");
            Position = new Vector2(0f, ShipY);
            Scale = 1f / 2f;
        }

        /// <summary>
        /// Ship explode
        /// </summary>
        public void Explode()
        {
            SetCostume("ShipDeath");
            PlaySound("ShipDeath");
        }

        /// <summary>
        /// Ship back to life
        /// </summary>
        public void ShipLive()
        {
            X = 0;
            SetCostume("ship");
            Show();
        }


        /// <summary>
        /// Update the Ship
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            if (CostumeName != "ShipDeath")
            {
                // Left and Right
                if (Scene.Keyboard.KeyDown(Keys.Left) && (IsTouchingLeft() == false))
                {
                    Direction = 180;
                    Speed = 1.0f;
                }
                else if (Scene.Keyboard.KeyDown(Keys.Right) && (IsTouchingRight() == false))
                {
                    Direction = 0;
                    Speed = 1.0f;
                }
                else
                {
                    Speed = 0.0f;
                }
                /*
                This is how the ship would use mouse input
                GoTo(Mouse.Position);
                 */
            }
        }
    }
}
