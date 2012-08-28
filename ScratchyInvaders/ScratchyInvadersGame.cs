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
        public static int Player1Score;
        public static int Player2Score;
        public static int HighScore;
        public static int NumberOfPlayers = 1;

        /// <summary>
        /// Load the screens needed for the game
        /// The first one added is where the game will start
        /// </summary>
        public override void LoadScenes()
        {
            PlayerData.Load();
            HighScore = PlayerData.GetInt("HighScore", 0);
            AddScene<TitleScreen>();
            AddScene<GameOverScreen>();
            AddScene<TestScreen>();
            CreatePlayerScreens();
        }

        public void CreatePlayerScreens()
        {
            PlayScreen Player1Screen = AddScene<PlayScreen>("player1");
            PlayScreen Player2Screen = AddScene<PlayScreen>("player2");
            Player1Screen.ActivePlayer = 1;
            Player2Screen.ActivePlayer = 2;
            Player1Screen.Init(this);
            Player2Screen.Init(this);
        }
    }
}
