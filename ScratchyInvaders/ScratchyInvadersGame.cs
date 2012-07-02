#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace ScratchyXna
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SpaceInvaders : ScratchyXnaGame
    {
        // Game variables
        public static int score;

        /// <summary>
        /// Load the screens needed for the game
        /// The first one added is where the game will start
        /// </summary>
        public override void LoadGameScreens()
        {
            AddScreen<TitleScreen>();
            AddScreen<PlayScreen>();
            AddScreen<GameOverScreen>();
            AddScreen<TestScreen>();
        }

    }
}
