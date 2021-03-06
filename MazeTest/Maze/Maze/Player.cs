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
        public WorldPosition LastPosition { get; private set; }

        /// <summary> Gets or sets the state of the player. </summary>
        /// <remarks> TODO: Phil: Not sure what this is used for... </remarks>
        public bool Active { get; set; }
                
        public Player(Game game, ISimpleReadOnlyWorldPosition startingPoint)
            : base(game)
        {
            _playerTexture = game.Content.Load<Texture2D>("player");
            this.Position = new WorldPosition(startingPoint);
            this.LastPosition = new WorldPosition(startingPoint);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new Rectangle(Position.X, Position.Y, Constants.Player.Width, Constants.Player.Height);
            spriteBatch.Draw(_playerTexture, rectangle, Color.White);
        }
        public void SetLastPosition()
        {
            this.Position.X = this.LastPosition.X;
            this.Position.Y = this.LastPosition.Y;
        }
        public int GetDirection()
        {
            if (GameKeyboard.PlayerOne.IsKeyDown(GameKeyboard.Presets.UpKeys))
            {
                return 1;
            }
            if (GameKeyboard.PlayerOne.IsKeyDown(GameKeyboard.Presets.DownKeys))
            {
                return 2;
            }
            if (GameKeyboard.PlayerOne.IsKeyDown(GameKeyboard.Presets.LeftKeys))
            {
                return 3;
            }
            if (GameKeyboard.PlayerOne.IsKeyDown(GameKeyboard.Presets.RightKeys))
            {
                return 4;
            }
            return 0;
        }
        public void CheckInput()
        {
            this.LastPosition.X = this.Position.X;
            this.LastPosition.Y = this.Position.Y;
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
            if (GameKeyboard.PlayerOne.IsKeyDown(GameKeyboard.Presets.ActionKeys))
            {

            }
        }
    }
}
