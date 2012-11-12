﻿using System;
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
        private Point _position;
        // State of the player
        public bool Active;
        public float speedX;
        public float speedY;
        int MOVEMENTSPEED;
        float PLAYERSCALE;

        public int X { get { return _position.X; } set { _position.X = value; } }
        public int Y { get { return _position.Y; } set { _position.Y = value; } }

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
            _position = pos;
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
            Rectangle rectangle = new Rectangle(_position.X, _position.Y, (int)(PlayerTexture.Width * PLAYERSCALE), (int)(PlayerTexture.Height * PLAYERSCALE));
            spriteBatch.Draw(PlayerTexture, rectangle, Color.White);
        }

        public void CheckInput()
        {
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
            {
                this.Y -= MOVEMENTSPEED;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
            {
                this.Y += MOVEMENTSPEED;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
            {
                this.X -= MOVEMENTSPEED;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D) || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
            {
                this.X += MOVEMENTSPEED;
            }
        }
    }
}
