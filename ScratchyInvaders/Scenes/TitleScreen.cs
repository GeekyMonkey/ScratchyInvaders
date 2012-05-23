#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace ScratchyXna
{
    /// <summary>
    /// The Title Screen
    /// </summary>
    public class TitleScreen : Scene
    {
        // Title screen variables
        int HighScore = 0;

        // Title screen text objects
        Text HighScoreText;
        Text StartText;
        Text AlienScoreText;

        /// <summary>
        /// Load the title screen
        /// </summary>
        public override void Load()
        {
            BackgroundColor = Color.Black;
            FontName = "QuartzMS";
        }


        /// <summary>
        /// Start the title screen
        /// This happens at the beginning of the game, and when you're done playing
        /// </summary>
        public override void StartScreen()
        {
            Texts.Clear();
            Sprites.Clear();

            // Add the title text
            AddText(new Text
            {
                Value = "Space Invaders",
                Position = new Vector2(0f, 80f),
                Alignment = HorizontalAlignments.Center,
                AnimationType = TextAnimations.Typewriter,
                AnimationSeconds = 0.15,
                Scale = 1f,
                //Font = FancyFont,
                OnAnimationComplete = (Text text) =>
                {
                    // When the title text is done typing, show the high score
                    HighScoreText = AddText(new Text
                    {
                        Value = "High Score:",
                        Position = new Vector2(80f, 90f),
                        Alignment = HorizontalAlignments.Center,
                        VerticalAlign = VerticalAlignments.Top,
                        AnimationType = TextAnimations.SeeSaw,
                        AnimationSeconds = 0.2,
                        AnimationIntensity = 0.15,
                        Scale = 0.4f,
                    });

                    UfoSprite ufo = AddSprite<UfoSprite>();
                    ufo.X = -49;
                    ufo.Y = 45;

                    Wait(2.3f, () =>
                    {
                        AlienSprite alien = AddSprite<AlienSprite>();
                        alien.X = -40;
                        alien.Y = 13;
                        alien.Setup(1);
                    });
                    Wait(5.2f, () =>
                    {
                        AlienSprite alien = AddSprite<AlienSprite>();
                        alien.X = -40;
                        alien.Y = -13;
                        alien.Setup(2);
                    });
                    Wait(8.7, () =>
                    {
                        AlienSprite alien = AddSprite<AlienSprite>();
                        alien.X = -40;
                        alien.Y = -43;
                        alien.Setup(3);
                    });


                    AlienScoreText = AddText(new Text
                    {
                        Value = "= ? POINTS" + Text.NewLine + Text.NewLine +
                        "= 10 POINTS" + Text.NewLine + Text.NewLine +
                        "= 20 POINTS" + Text.NewLine + Text.NewLine +
                        "= 30 POINTS",
                        Position = new Vector2(0, 0),
                        Alignment = HorizontalAlignments.Center,
                        VerticalAlign = VerticalAlignments.Center,
                        AnimationType = TextAnimations.Typewriter,
                        AnimationSeconds = 0.2,
                        Scale = 0.8f,
                        Color = Color.White
                    });


                }
            });

            // Add the start key text
            StartText = AddText(new Text
            {
                Value = "Press SPACE to Play",
                Position = new Vector2(0f, -90f),
                Alignment = HorizontalAlignments.Center,
                VerticalAlign = VerticalAlignments.Bottom,
                AnimationType = TextAnimations.Typewriter,
                AnimationSeconds = 0.2,
                Scale = 0.5f,
                Color = Color.Lime
            });

            if (Game.Platform == GamePlatforms.XBox)
            {
                StartText.Value = "Press START to Play Again";
            }
            else if (Game.Platform == GamePlatforms.WindowsPhone)
            {
                StartText.Value = "TAP to Play Again";
            }
        }

        /// <summary>
        /// Update the title screen
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Space key starts the game
            if (Keyboard.KeyPressed(Keys.Space) || Mouse.Button1Pressed())
            {
                //todo: tap to play
                //todo: xbox a or start button to play
                ShowScreen("Play");
            }

            // Give us something interesting to look at
            // by just making the high score go mad
            // (wait until it has been added to the screen)
            if (HighScoreText != null)
            {
                HighScore += 1;
                HighScore = (int)(HighScore * 1.005);
                HighScoreText.Value = "High Score: " + HighScore;
            }
        }

    }
}
