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
    public class AlienSprite : Sprite
    {
        public AlienStates State;
        public int AlienType;
        public int ScoreValue;
        public int Row;
        public int Col;

        public override void Load()
        {
            AddCostume("Alien1A");
            AddCostume("Alien1B");
            AddCostume("Alien2A");
            AddCostume("Alien2B");
            AddCostume("Alien3A");
            AddCostume("Alien3B");
            AddCostume("AlienDeath");
            Scale = 1f / 2.5f;
        }

        public void Explode()
        {
            State = AlienStates.Exploding;
            SetCostume("AlienDeath");
            PlaySound("AlienDeath");
            Wait(0.6, GoAway);
        }

        public void Setup(int alienType)
        {
            State = AlienStates.Alive;
            AlienType = alienType;
            SetCostume("Alien" + alienType + "A");
            ScoreValue = alienType * 10;
        }

        void GoAway()
        {
            State = AlienStates.Dead;
            Hide();
        }

        public void alienMove()
        {
            if (CostumeName.EndsWith("A"))
            { 
                SetCostume("Alien" + AlienType + "B");
            }
            else
            {
                SetCostume("Alien" + AlienType + "A");
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (State == AlienStates.Alive)
            {
                // Freaky color mode
                // SpriteColor = new Color(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
            }
        }

    }
}