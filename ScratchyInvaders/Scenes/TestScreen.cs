#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion


namespace ScratchyXna.Screens
{
    class TestScreen : Scene
    {
        Text ScoreText;
        Text RestartText;
        MissileSprite missile;
        UfoSprite ufo;
        BarrierSprite barrier;

        /// <summary>
        /// Load the game over screen
        /// </summary>
        public override void Load()
        {
            GridStyle = GridStyles.Ticks;
            BackgroundColor = Color.Black;
            FontName = "QuartzMS";

            // Add the score text
            ScoreText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Left,
                VerticalAlign = VerticalAlignments.Top,
                Scale = .4f,
                Position = new Vector2(-100f, 100f),
                Color = Color.White
            });

            // Add the start key text
            RestartText = AddText(new Text
            {
                Value = "Press SPACE to Play Again",
                Position = new Vector2(0f, -100f),
                Alignment = HorizontalAlignments.Center,
                VerticalAlign = VerticalAlignments.Bottom,
                AnimationType = TextAnimations.Typewriter,
                AnimationSeconds = 0.2,
                Scale = 0.5f,
                Color = Color.Lime
            });
            if (Game.Platform == GamePlatforms.XBox)
            {
                RestartText.Value = "Press START to Play Again";
            }
            else if (Game.Platform == GamePlatforms.WindowsPhone)
            {
                RestartText.Value = "TAP to Play Again";
            }

            missile = AddSprite<MissileSprite>();
            missile.SetCostume("ufo");
            missile.Costume.YCenter = VerticalAlignments.Center;
            missile.Scale = 2;
            missile.Show();
            missile.X = 0;
            missile.Y = 50;

            barrier = AddSprite<BarrierSprite>();
            barrier.Scale = 2.5f;
            barrier.Y = -70;

            ufo = AddSprite<UfoSprite>();
            ufo.Position = Vector2.Zero;
            ufo.Costume.YCenter = VerticalAlignments.Top;
            ufo.Scale = 1;
            ufo.Show();
        }


        /// <summary>
        /// Start the game over screen
        /// </summary>
        public override void StartScreen()
        {
            // Display the final score
            ScoreText.Value = "Score: Kickass"; // +SpaceInvaders.score;
        }


        /// <summary>
        /// Update the game over screen
        /// </summary>
        /// <param name="gameTime">Time since the last update</param>
        public override void Update(GameTime gameTime)
        {
            if (Mouse.Button1Pressed())
            {
                ufo.RotateTowards(Mouse.Position, 90);
                ufo.GlideTo(Mouse.Position, 2f);
            }

            barrier.SpriteColor = barrier.IsTouching(Mouse.Position) ? 
                Color.Green : Color.Aqua;

            // Space key to play again
            if (Keyboard.KeyPressed(Keys.Space))
            {
                //todo: also do this for phone tap
                //todo: also do this for xbox a button
                ShowScreen("play");
            }

            if (missile != null)
            {
                missile.Rotation += .5f;
                missile.SpriteColor = ufo.IsTouching(missile) ? Color.Red : Color.Green;
            }
            if (ufo != null)
            {
                ufo.DirectionFrom(Keyboard, 2);
                if (Keyboard.KeyDown(Keys.Z))
                {
                    ufo.Rotation += 1f;
                }
                if (Keyboard.KeyDown(Keys.X))
                {
                    ufo.Rotation -= 1f;
                }
            }
            
        }

    }
}
