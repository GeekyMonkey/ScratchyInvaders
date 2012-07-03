#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScratchyXna;
using Microsoft.Xna.Framework;
#endregion


namespace ScratchyXna
{
    public class UfoSprite : Sprite
    {
        public UfoStates State;
        public float UfoScale = 1f / 2f;

        public override void Load()
        {
            SetCostume("Ufo");
            AddCostume("AlienDeath");
            AddCostume("UfoScore");
            SpriteColor = Color.Red;
            Scale = UfoScale;
        }

        public void Reset()
        {
            GoHome();
        }

        public void Launch()
        {
            SetCostume("ufo");
            Show();
            GhostEffect = 0;
            Scale = UfoScale;
            X = -100; // Scene.MinX - Width;
            State = UfoStates.Flying;
            Direction = 0;
            Speed = 0.5f;
            PlaySound("ufo", loop: true);
        }

        public void GoHome()
        {
            Hide();
            State = UfoStates.Waiting;
            X = Scene.MinX - Width;
            Y = 90;
            Speed = 0;
            StopSound("ufo");
            Wait(10, Launch);
        }

        public void Explode()
        {
            State = UfoStates.Exploding;
            SetCostume("AlienDeath");
            Speed = 0;
            StopSound("ufo");
            PlaySound("UfoDeath");
            Wait(0.5, () =>
            {
                SetCostume("UfoScore");
                Wait(1, GoHome);
            });
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (State == UfoStates.Flying)
            {
                RotateTowards(((PlayScreen)Scene).ship, -90);
                if (X > 100 + Width /*GameScene.MaxX + Width*/)
                {
                    GoHome();
                }
            }
            if (State == UfoStates.Exploding)
            {
                Scale += ((float)gameTime.ElapsedGameTime.TotalSeconds * 2.5f);
                GhostEffect += ((float)gameTime.ElapsedGameTime.TotalSeconds * 70f);
            }
        }

    }
}
