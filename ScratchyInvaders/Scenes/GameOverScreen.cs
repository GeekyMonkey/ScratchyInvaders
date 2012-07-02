#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion


namespace ScratchyXna
{
    class GameOverScreen : Scene
    {
        // Texts on the game over screen
        Text GameOverText;
        Text ScoreText;
        Text RestartText;

        /// <summary>
        /// Load the game over screen
        /// </summary>
        public override void Load()
        {
            BackgroundColor = Color.Black;
            FontName = "QuartzMS";

            // Add the game over text
            GameOverText = new Text
            {
                Value = "Game Over",
                Color = Color.Red,
                Alignment = HorizontalAlignments.Center,
                AnimationType = TextAnimations.Throb,
                AnimationSeconds = 0.5,
                AnimationIntensity = 0.5
            };
            AddText(GameOverText);

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
        }


        /// <summary>
        /// Start the game over screen
        /// </summary>
        public override void StartScreen()
        {
            // Display the final score
            ScoreText.Value = "Score: " + SpaceInvaders.score;
            
            // Wait 2 seconds
            Wait(2, () =>
            {
                // then flash the game over text orange and blue 
                Forever(0.4,
                    () =>
                    {
                        if (GameOverText.Color == Color.Blue)
                        {
                            GameOverText.Color = Color.Orange;
                        }
                        else
                        {
                            GameOverText.Color = Color.Blue;
                        }
                    });
            }
            );
        }


        /// <summary>
        /// Update the game over screen
        /// </summary>
        /// <param name="gameTime">Time since the last update</param>
        public override void Update(GameTime gameTime)
        {
            // Space key to play again
            if (Keyboard.KeyPressed(Keys.Space) || Mouse.Button1Pressed())
            {
                //todo: also do this for phone tap
                //todo: also do this for xbox a button
                ShowScreen("play");
            }
        }

    }
}
