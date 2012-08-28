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
        // Title screen text objects
        Text HighScoreText;
        Text Player1StartText;
        Text Player2StartText;
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
        public override void StartScene()
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

            // Add the 1p start key text
            Player1StartText = AddText(new Text
            {
                Value = "1 Player Start",
                Position = new Vector2(-65f, -85f),
                Alignment = HorizontalAlignments.Center,
                VerticalAlign = VerticalAlignments.Center,
                AnimationType = TextAnimations.None,
                AnimationIntensity = 0.2,
                Scale = 0.6f,
                Color = Color.Lime
            });

            // Add the 2p start key text
            Player2StartText = AddText(new Text
            {
                Value = "2 Player Start",
                Position = new Vector2(65f, -85f),
                Alignment = HorizontalAlignments.Center,
                VerticalAlign = VerticalAlignments.Center,
                AnimationType = TextAnimations.None,
                AnimationIntensity = 0.2,
                Scale = 0.6f,
                Color = Color.Lime
            });

            if (SpaceInvaders.NumberOfPlayers == 1)
            {
                Player1StartText.AnimationType = TextAnimations.Throb;
                Player1StartText.Scale = 0.6f;
                Player1StartText.Start();
            }
            else
            {
                Player2StartText.AnimationType = TextAnimations.Throb;
                Player2StartText.Scale = 0.6f;
                Player2StartText.Start();
            }
        }

        /// <summary>
        /// Update the title screen
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.KeyPressed(Keys.Right))
            {
                SpaceInvaders.NumberOfPlayers = 2;
                Player1StartText.AnimationType = TextAnimations.None;
                Player1StartText.Scale = 0.6f;
                Player2StartText.AnimationType = TextAnimations.Throb;
                Player2StartText.Scale = 0.6f;
                Player2StartText.Start();
            }

            if (Keyboard.KeyPressed(Keys.Left))
            {
                SpaceInvaders.NumberOfPlayers = 1;
                Player1StartText.AnimationType = TextAnimations.Throb;
                Player1StartText.Scale = 0.6f;
                Player2StartText.AnimationType = TextAnimations.None;
                Player2StartText.Scale = 0.6f;
                Player1StartText.Start();
            }

            // Space key starts the game
            if (Keyboard.KeyPressed(Keys.Space) || Mouse.Button1Pressed())
            {
                ((SpaceInvaders)Game).CreatePlayerScreens();
                ShowScene("Player1");
            }

            // Give us something interesting to look at
            // by just making the high score go mad
            // (wait until it has been added to the screen)
            if (HighScoreText != null)
            {
                HighScoreText.Value = "High Score: " + SpaceInvaders.HighScore;
            }
        }

    }
}
