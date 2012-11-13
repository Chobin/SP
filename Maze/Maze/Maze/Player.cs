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
        /// <summary> The texture to use for the player. </summary>
        private Texture2D _playerTexture;

        /// <summary> Gets the position of the player. </summary>
        public WorldPosition Position { get; private set; }

        /// <summary> Gets or sets the state of the player. </summary>
        /// <remarks> TODO: Phil: Not sure what this is used for... </remarks>
        public bool Active { get; set; }
                
        public Player(Game game, ISimpleReadOnlyWorldPosition startingPoint)
            : base(game)
        {
            _playerTexture = game.Content.Load<Texture2D>("player");
            this.Position = new WorldPosition(startingPoint);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new Rectangle(Position.X, Position.Y, Constants.Tile.Width, Constants.Tile.Height);
            spriteBatch.Draw(_playerTexture, rectangle, Color.White);
        }

        public void CheckInput()
        {
            if (GameKeyboard.PlayerOne.IsKeyDown(GameKeyboard.Presets.UpKeys))
            {
                Position.Y -= Constants.Player.MoveSpeed;
            }
            if (GameKeyboard.PlayerOne.IsKeyDown(GameKeyboard.Presets.DownKeys))
            {
                Position.Y += Constants.Player.MoveSpeed;
            }
            if (GameKeyboard.PlayerOne.IsKeyDown(GameKeyboard.Presets.LeftKeys))
            {
                Position.X -= Constants.Player.MoveSpeed;
            }
            if (GameKeyboard.PlayerOne.IsKeyDown(GameKeyboard.Presets.RightKeys))
            {
                Position.X += Constants.Player.MoveSpeed;
            }
        }
    }
}
