using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maze
{
    class Player : Microsoft.Xna.Framework.GameComponent
    {
        public Texture2D PlayerTexture;
        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position;
        // State of the player
        public bool Active;
        public float speedX;
        public float speedY;
        int MOVEMENTSPEED;
        public Vector2 Speed
        {
            get
            {
                return new Vector2(speedX, speedY);
            }
        }
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
        public void Initialize(Texture2D tex, Vector2 pos)
        {
            PlayerTexture = tex;
            Position = pos;
            Active = true;
            speedX = 0;
            speedY = 0;
            MOVEMENTSPEED = 5;//5 pixels
            base.Initialize();
        }
        public void SetTexture(Texture2D tex)
        {
            PlayerTexture = tex;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void CheckInput()
        {
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
            {
                Position.Y -= MOVEMENTSPEED;
            }
            else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
            {
                Position.Y += MOVEMENTSPEED;
            }
            else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
            {
                Position.X -= MOVEMENTSPEED;
            }
            else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
            {
                Position.X += MOVEMENTSPEED;
            }
        }
    }
}
