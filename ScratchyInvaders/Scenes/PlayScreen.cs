#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
#endregion

namespace ScratchyXna
{

    public class PlayScreen : Scene
    {
        // Sprites on the screen
        public ShipSprite ship;
        MissileSprite missile;
        List<AlienMissileSprite> alienMissiles = new List<AlienMissileSprite>();
        UfoSprite ufo;
        BarrierSprite barrier1;
        BarrierSprite barrier2;
        BarrierSprite barrier3;
        BarrierSprite barrier4;
        //LineSprite line;
        List<AlienSprite> aliens = new List<AlienSprite>();
        List<LivesSprite> lifesprites = new List<LivesSprite>();
        float MoveWaitSeconds;
        float AlienMoveXStep = 10;
        float AlienMoveYStep = 10;
        float AlienHitBottomY = -70;
        float MoveWaitPercent = 0.97f;
        int level = 1;
        int alienMoveSound = 1;
        int AlienCount;
        public int ActivePlayer;
        public int Lives;
        AlienDirections alienDirection;

        // Text on the screen
        Text shipStatusText;
        Text DebugText;
        Text Player1ScoreText;
        Text Player2ScoreText;
        Text LevelText;
        Text LivesText;
        Text HighScoreText;

        /// <summary>
        /// Load the screen
        /// </summary>
        public override void Load()
        {
            BackgroundColor = Color.Black;
            FontName = "QuartzMS";
            AddSound("shoot");
            AddSound("ufo");
            AddSound("UfoDeath");
            AddSound("AlienDeath");
            AddSound("AlienMove1");
            AddSound("AlienMove2");
            AddSound("AlienMove3");
            AddSound("AlienMove4");

            // Create our sprites
            ship = AddSprite<ShipSprite>();
            barrier1 = AddSprite<BarrierSprite>();
            barrier2 = AddSprite<BarrierSprite>();
            barrier3 = AddSprite<BarrierSprite>();
            barrier4 = AddSprite<BarrierSprite>();
            missile = AddSprite<MissileSprite>();
            ufo = AddSprite<UfoSprite>();
            //line = AddSprite<LineSprite>();

            // Position the barriers
            barrier1.X = -60;
            barrier2.X = -20;
            barrier3.X = 20;
            barrier4.X = 60;

            // Create our texts
            Player1ScoreText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Left,
                VerticalAlign = VerticalAlignments.Top,
                Scale = .4f,
                Position = new Vector2(-100f, 100f),
                Color = Color.White,
                Value = "Player 1 Score: " + SpaceInvaders.Player1Score
            });

            Player2ScoreText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Right,
                VerticalAlign = VerticalAlignments.Top,
                Scale = .4f,
                Position = new Vector2(100f, 100f),
                Color = Color.White,
                Value = "Player 2 Score: " + SpaceInvaders.Player2Score
            });

            HighScoreText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Center,
                VerticalAlign = VerticalAlignments.Top,
                Scale = .4f,
                Position = new Vector2(0, 100f),
                Color = Color.White
            });

            LevelText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Right,
                VerticalAlign = VerticalAlignments.Bottom,
                Scale = .4f,
                Position = new Vector2(100f, -100f),
                Color = Color.White
            });

            shipStatusText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Left,
                VerticalAlign = VerticalAlignments.Bottom,
                Scale = 0.5f,
                Position = new Vector2(-100f, -100f),
                Color = new Color(1.0f, .2f, .9f, .5f)
            });

            DebugText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Right,
                VerticalAlign = VerticalAlignments.Top,
                Scale = 0.4f,
                Position = new Vector2(100f, 100f),
                Color = Color.YellowGreen
            });

            LivesText = AddText(new Text
            {
                Alignment = HorizontalAlignments.Left,
                VerticalAlign = VerticalAlignments.Bottom,
                Scale = .4f,
                Position = new Vector2(-100f, -100f),
                Color = Color.White,
                Value = "Lives: "
            });
        }


        /// <summary>
        /// Create all of the aliens when a level starts
        /// </summary>
        public void CreateAliens()
        {
            Sprites.RemoveAll(s => s is AlienSprite);
            aliens.Clear();
            AlienCount = 0;

            // Create 55 alien sprites in 5 rows of 11
            for (int row = 1; row <= 5; row++)
            {
                for (int col = 1; col <= 11; col++)
                {
                    AlienSprite newAlien = AddSprite<AlienSprite>();
                    newAlien.X = -100 + col * 14;
                    newAlien.Y = 80 - row * 10;
                    newAlien.Col = col;
                    newAlien.Row = row;
                    if (row == 1)
                    {
                        newAlien.Setup(3);
                    }
                    if (row == 2 || row == 3)
                    {
                        newAlien.Setup(2);
                    }
                    if (row == 4 || row == 5)
                    {
                        newAlien.Setup(1);
                    }
                    aliens.Add(newAlien);
                    AlienCount++;
                }
            }
        }


        /// <summary>
        /// Start the play screen
        /// </summary>
        public override void StartScene()
        {
            Lives = 3;
            lifesprites.Clear();
            Sprites.RemoveAll(s => s is LivesSprite);
            Costume lifecostume = new Costume();
            lifecostume.Load(this, Content, "Ship");
            lifecostume.YCenter = VerticalAlignments.Bottom;
            float lifeSpacing = 10;
            for (int i = 1; i <= Lives - 1; i++)
            {
                LivesSprite newLifeSprite = AddSprite<LivesSprite>(); 
                newLifeSprite.X = LivesText.Position.X + 21 + ((i-1) * lifeSpacing);
                newLifeSprite.Costume = lifecostume;
                lifesprites.Add(newLifeSprite);
            }
            Score = 0;
            Level = 0;
            StartLevel();
            HighScoreText.Value = "High Score: " + SpaceInvaders.HighScore;
            ufo.Reset();
            MoveWaitSeconds = 1;
            alienDirection = AlienDirections.Right;
            Wait(MoveWaitSeconds, Move);
            ScheduleAlienShoot();
        }

        /// <summary>
        /// Start the next level
        /// </summary>
        private void StartLevel()
        {
            Level += 1;
            CreateAliens();
            barrier1.Load();
            barrier2.Load();
            barrier3.Load();
            barrier4.Load();
            MoveWaitSeconds = 1.1f - ((float)Level / 10.0f);
        }

        /// <summary>
        /// Move all of the aliens
        /// </summary>
        void Move()
        {
            // Move all of the aliens left or right, and keep track if any touched an edge
            bool TouchedEdge = false;
            foreach (AlienSprite alien in aliens)
            {
                if (alien.State == AlienStates.Alive)
                {
                    if (alienDirection == AlienDirections.Right)
                    {
                        alien.X += AlienMoveXStep;
                        if (alien.IsTouchingRight())
                        {
                            TouchedEdge = true;
                        }
                    }
                    else
                    {
                        alien.X -= AlienMoveXStep;
                        if (alien.IsTouchingLeft())
                        {
                            TouchedEdge = true;
                        }
                    }
                    alien.alienMove();
                }
            }

            // Did any aliens hit an edge
            if (TouchedEdge)
            {
                // We touched an edge, so next time we'll use a different direction
                if (alienDirection == AlienDirections.Right)
                {
                    alienDirection = AlienDirections.Left;
                }
                else
                {
                    alienDirection = AlienDirections.Right;
                }

                // Schedule the next move to be down
                Wait(MoveWaitSeconds, MoveDown);

                // The time after next, continue moving across
                Wait(MoveWaitSeconds * 2, Move);
            }
            else
            {
                // Didn't hit an edge, just schedule the next move
                Wait(MoveWaitSeconds, Move);
            }

            playMoveSound();
        }

        /// <summary>
        /// Play the sound of the aliens moving
        /// </summary>
        void playMoveSound()
        {
            if (alienMoveSound == 5)
            {
                alienMoveSound = 1;
            }
            if (alienMoveSound == 1)
            {
                PlaySound("AlienMove1");
            }
            if (alienMoveSound == 2)
            {
                PlaySound("AlienMove2");
            }
            if (alienMoveSound == 3)
            {
                PlaySound("AlienMove3");
            }
            if (alienMoveSound == 4)
            {
                PlaySound("AlienMove4");
            }
            alienMoveSound += 1;
        }

        /// <summary>
        /// Move all of the aliens down
        /// </summary>
        void MoveDown()
        {
            bool HitBottom = false;
            playMoveSound();
            foreach (AlienSprite alien in aliens)
            {
                // Only move the alive ones, because we don't want the explosions to move
                if (alien.State == AlienStates.Alive)
                {
                    alien.alienMove();
                    alien.Y -= AlienMoveYStep;
                    if (alien.Y < AlienHitBottomY)
                    {
                        HitBottom = true;
                    }
                }
            }
            if (HitBottom == true)
            {
                ShipDie();
                MoveWaitSeconds = 2.1f; // stop moving
                Wait(2, StartLevel);
            }
        }

        /// <summary>
        /// Ship has been hit
        /// </summary>
        void ShipDie()
        {
            Lives -= 1;
            ship.Explode();
            if (Lives == 0)
            {
                Wait(1, () => ShowScene("GameOver"));
            }
            else
            {
                Wait(1, () =>
                {
                    if (Lives != 0)
                    {
                        lifesprites[Lives - 1].GlideTo(new Vector2(0, ship.ShipY - 10), 1);
                    }
                });
                Wait(2, () =>
                {
                    if (Lives != 0)
                    {
                        lifesprites[Lives - 1].Hide();
                    }
                });
            }
        }


        /// <summary>
        /// Stop the play screen
        /// </summary>
        public override void StopScene()
        {
            ufo.GoHome();
        }

        void CheckHighScore()
        {
            if (Score > SpaceInvaders.HighScore)
            {
                SpaceInvaders.HighScore = Score;
                HighScoreText.Value = "High Score: " + SpaceInvaders.HighScore;
            }
        }

        /// <summary>
        /// Update the play screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Use this to use the mouse to rotate the ship
            // ship.RotateTowards(Mouse.Position);

            if (ship.CostumeName != "ShipDeath")
            {
                // Space key to fire a missile 
                if (Keyboard.KeyPressed(Keys.Space))
                {
                    missile.Fire(ship.Position, ship.Rotation);
                }
            }

            foreach (AlienMissileSprite AlienMissile in alienMissiles.Where(a => a.State == MissileStates.Destroy).ToList())
            {
                Sprites.Remove(AlienMissile);
                alienMissiles.Remove(AlienMissile);
            }

            AlienMissileCollisions();

            // Missile collisions
            if (missile.State == MissileStates.Flying)
            {
                // Missile collsion with UFO
                if (ufo.State == UfoStates.Flying)
                {
                    if (missile.IsTouching(ufo))
                    {
                        ufo.Explode();
                        missile.Load();
                        Score += 100;
                    }
                }

                Sprite HitBarrier = null;
                if (missile.IsTouching(barrier1))
                {
                    HitBarrier = barrier1;
                }
                if (missile.IsTouching(barrier2))
                {
                    HitBarrier = barrier2;
                }
                if (missile.IsTouching(barrier3))
                {
                    HitBarrier = barrier3;
                }
                if (missile.IsTouching(barrier4))
                {
                    HitBarrier = barrier4;
                }
                if(HitBarrier != null)
                {
                    missile.SetCostume("MissileHitBarrier");
                    missile.Scale = 0.5f;
                    HitBarrier.Stamp(missile,StampMethods.Cutout);
                    missile.Load();
                }

                // Missile collision with each alien
                foreach (AlienSprite alien in aliens)
                {
                    // Only the ones who are alive, not the exploding or dead ones
                    if (alien.State == AlienStates.Alive)
                    {
                        if (missile.IsTouching(alien))
                        {
                            alien.Explode();
                            AlienCount -= 1;
                            missile.Load();
                            MoveWaitSeconds = MoveWaitSeconds * MoveWaitPercent;
                            Score += alien.ScoreValue;
                            //Checking to see if all the aliens are dead
                            if (AlienCount == 0)
                            {
                                StartLevel();
                                break;
                            }
                        }
                    }
                }

            }

            // Debug Text
            if (DebugTextVisible)
            {
                ShowShipStatus();
                ShowDebugText();
            }
        }


        /// <summary>
        /// Show some debugging text
        /// </summary>
        private void ShowDebugText()
        {
            DebugText.Value = "pxs=" + PixelScale.ToString("0.0000") + Text.NewLine +
              "minX=" + MinX.ToString("0.00") + Text.NewLine +
              "missiles=" + alienMissiles.Count();
        }


        /// <summary>
        /// Draw the ship status in text
        /// </summary>
        private void ShowShipStatus()
        {
            RectangleF shipRect = ship.Rect;
            shipStatusText.Value =
              "X=" + ship.Position.X.ToString("0.00") + Text.NewLine +
              "Y=" + ship.Position.Y.ToString("0.00") + Text.NewLine +
              "Rot=" + ship.Rotation.ToString("0.00") + Text.NewLine +
              "CenX=" + ship.Costume.Center.X.ToString("0.00") + Text.NewLine +
              "CenY=" + ship.Costume.Center.Y.ToString("0.00") + Text.NewLine +
              // "D=" + ship.Depth + " / " + shipStatusText.Depth + Text.NewLine +
              shipRect.Left.ToString("0.00") + " x " + shipRect.Top.ToString("0.00") + Text.NewLine +
              shipRect.Right.ToString("0.00") + " x " + shipRect.Bottom.ToString("0.00");
        }

        /// <summary>
        /// Score variable, but when you set it, it automatically updates the score text
        /// </summary>
        public int Score
        {
            get
            {
                if (ActivePlayer == 1)
                {
                    return SpaceInvaders.Player1Score;
                }
                else
                {
                    return SpaceInvaders.Player2Score;
                }
            }
            set
            {
                if (ActivePlayer == 1)
                {
                    SpaceInvaders.Player1Score = value;
                    Player1ScoreText.Value = "Player 1 Score: " + SpaceInvaders.Player1Score;
                }
                else
                {
                    SpaceInvaders.Player2Score = value;
                    Player2ScoreText.Value = "Player 2 Score: " + SpaceInvaders.Player2Score;
                }
                CheckHighScore();
            }
        }

        /// <summary>
        /// The level that the player is on. Setting it automatically updades the level text
        /// </summary>
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                LevelText.Value = "Level " + level;
            }
        }

        public override void Draw()
        {
            DrawLine(new Vector2(-100, -90), new Vector2(100, -90), 1f, Color.Lime);
        }

        void AlienShoot()
        {
            List<AlienSprite> ShootingAliens = new List<AlienSprite>();
            for (int col = 1; col <= 11; col++)
            {
                AlienSprite Alien;
                Alien = FindBottomAlienInCol(col);
                if (Alien != null)
                {
                    ShootingAliens.Add(Alien);
                }
            }
            if (ShootingAliens.Count > 0)
            {
                int ShooterIndex = Random.Next(0, ShootingAliens.Count() - 1);
                AlienSprite ShootingAlien = ShootingAliens[ShooterIndex];
                /*
                foreach (AlienSprite alien in aliens)
                {
                    alien.GhostEffect = 0;
                }
                foreach (AlienSprite alien in ShootingAliens)
                {
                    alien.GhostEffect = 50;
                }
                ShootingAlien.GhostEffect = 70;
                */
                AlienShoot(ShootingAlien);
                ScheduleAlienShoot();
            }
        }

        void AlienShoot(AlienSprite ShootingAlien)
        {
            AlienMissileSprite AlienMissile;
            AlienMissile = new AlienMissileSprite();
            alienMissiles.Add(AlienMissile);
            AddSprite(AlienMissile);
            AlienMissile.Fire(ShootingAlien.Position, ShootingAlien.Rotation);
        }

        AlienSprite FindBottomAlienInCol(int col)
        {
            AlienSprite Found = null;
            Found = aliens.Where(a => a.Col == col && a.State == AlienStates.Alive).OrderByDescending(a => a.Row).FirstOrDefault();
            return Found;
        }

        /// <summary>
        /// Calculate number of seconds until next alien shoot
        /// </summary>
        /// <returns>Seconds</returns>
        double CalculateAlienShootDelay()
        {
            double shootDelay = 1.0 / Level;
            double randomPercent = Random.NextDouble();
            return (shootDelay / 2) + (shootDelay * 1.8 * randomPercent);
        }

        /// <summary>
        /// Schedule the next alien shot
        /// </summary>
        void ScheduleAlienShoot()
        {
            Wait(CalculateAlienShootDelay(), AlienShoot);
        }

        void AlienMissileCollisions()
        {
            foreach (AlienMissileSprite AlienMissile in alienMissiles)
            {
                if (AlienMissile.IsTouching(barrier1))
                {
                    AlienMissile.HitBarrier(barrier1);
                }
                if (AlienMissile.IsTouching(barrier2))
                {
                    AlienMissile.HitBarrier(barrier2);
                }
                if (AlienMissile.IsTouching(barrier3))
                {
                    AlienMissile.HitBarrier(barrier3);
                }
                if (AlienMissile.IsTouching(barrier4))
                {
                    AlienMissile.HitBarrier(barrier4);
                }
                if (AlienMissile.IsTouching(ship))
                {
                    ShipDie();
                    AlienMissile.State = MissileStates.Destroy;
                }
            }    
        }
    }
}