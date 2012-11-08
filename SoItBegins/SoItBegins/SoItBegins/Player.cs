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


namespace SoItBegins
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Player : Microsoft.Xna.Framework.GameComponent
    {
        public Texture2D PlayerTexture;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position;
        // State of the player
        public bool Active;
        public TimeSpan nextAvailableShot;
        public TimeSpan SHOT_TIMELIMIT;
        private float speedX;
        public float speedY;
        static int MAX_PLAYER_MOVEMENT = 5;
        private int jumpNum;
        public TimeSpan nextAvailableJump;
        public TimeSpan JUMP_TIMELIMIT;
        public float XPosDiff;
        public Vector2 Speed
        {
            get
            {
                return new Vector2(speedX, speedY); 
            }
        }
        public enum playerState
        {
            POSTJUMP,
            MOVING,
            STATIC
        }
        public playerState currentPlayerState;

        // Amount of hit points that player has
        public int Health;

        // Get the width of the player ship
        public int Width
        {
            get { return PlayerTexture.Width; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return PlayerTexture.Height; }
        }
        public Player(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize(Texture2D tex, Vector2 pos)
        {
            // TODO: Add your initialization code here
            GameTime time;
            time = new GameTime();
            nextAvailableShot = time.ElapsedGameTime;
            nextAvailableJump = time.ElapsedGameTime;
            SHOT_TIMELIMIT = new TimeSpan(0, 0, 0, 0, 100);
            JUMP_TIMELIMIT = new TimeSpan(0, 0, 0, 0, 100);
            PlayerTexture = tex;
            Position = pos;
            Active = true;
            Health = 100;
            speedX = 0;
            speedY = 0;
            jumpNum = 0;
            XPosDiff = 0.0f;
            currentPlayerState = playerState.STATIC;
            base.Initialize();
        }
        public void SetTexture(Texture2D tex)
        {
            PlayerTexture = tex;
        }
        public bool Shoot(GameTime currentTime)
        {
            if (currentTime.TotalGameTime - nextAvailableShot > SHOT_TIMELIMIT)
            {
                nextAvailableShot = currentTime.TotalGameTime;
                return true;
            }
            return false;

        }
        public bool Jump(GameTime currentTime)
        {
            if (currentPlayerState != playerState.POSTJUMP || (jumpNum < 1 && currentTime.TotalGameTime - nextAvailableJump > JUMP_TIMELIMIT))
            {
                nextAvailableJump = currentTime.TotalGameTime;
                currentPlayerState = playerState.POSTJUMP;
                speedY = 3.5f;
                jumpNum++;
                if (jumpNum == 2)
                    jumpNum = 0;
                return true;
            }
            return false;

        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, float moveX)
        {
            // TODO: Add your update code here
            if (moveX < speedX && moveX < 0)
                speedX = speedX + moveX;
            else if (moveX < speedX && moveX == 0)
                speedX = speedX / 5;
            else if (moveX > speedX && moveX > 0)
                speedX = speedX + moveX;
            else if (moveX > speedX && moveX == 0)
                speedX = speedX / 5;
            //if (moveX > 0)
            //    currentPlayerState = playerState.MOVING;
            //else
            //    currentPlayerState = playerState.STATIC;
            if (currentPlayerState == Player.playerState.STATIC)
            {
                speedY = 0;
                if (speedX > -0.025 && speedX < 0.025)
                    speedX = 0;
            }
            else if (currentPlayerState == playerState.POSTJUMP)
            {
                speedY = speedY - (float)0.25;
                if (speedY < -5)
                    speedY = -5;
            }
            Position.Y -= speedY * MAX_PLAYER_MOVEMENT;
            Position.X += speedX * MAX_PLAYER_MOVEMENT;
            
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
