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
        public Point Position;
        // State of the player
        public bool Active;
        public float speedX;
        public float speedY;
        int MOVEMENTSPEED;
        float PLAYERSCALE;
        public Vector2 Speed
        {
            get
            {
                return new Vector2(speedX, speedY);
            }
        }
        public int Width
        {
            get { return (int)(PlayerTexture.Width * PLAYERSCALE); }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return (int)(PlayerTexture.Height * PLAYERSCALE); }
        }
        public Player(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }
        public void Initialize(Texture2D tex, Point pos, float playerScale)
        {
            PlayerTexture = tex;
            Position = pos;
            Active = true;
            speedX = 0;
            speedY = 0;
            MOVEMENTSPEED = 5;//5 pixels
            PLAYERSCALE = playerScale;
            base.Initialize();
        }
        public void SetTexture(Texture2D tex)
        {
            PlayerTexture = tex;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)(PlayerTexture.Width * PLAYERSCALE), (int)(PlayerTexture.Height * PLAYERSCALE));
            spriteBatch.Draw(PlayerTexture, rectangle, Color.White);
        }

        public void CheckInput()
        {
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
            {
                Position.Y -= MOVEMENTSPEED;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
            {
                Position.Y += MOVEMENTSPEED;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
            {
                Position.X -= MOVEMENTSPEED;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
            {
                Position.X += MOVEMENTSPEED;
            }
        }
    }
}
