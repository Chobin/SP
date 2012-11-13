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

namespace Maze
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private FullMaze _currentMaze;

        public Game1()
        {
            // This must be done in the constructor, otherwise the initialization stack of the game will not be invoked properly and the entire XNA Game stack will be compromised! Don't move it!
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary> LoadContent will be called once per game and is the place to load all of your content. </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures. A single instance is enough for the lifetime of the game, unless we need another.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create out Maze, which is the entire "world" the player will interact with.
            _currentMaze = new FullMaze(this, Constants.Maze.Width, Constants.Maze.Height);
            _currentMaze.Initialize();

            // Initialize the player, informing it of it's starting point, somewhere within the maze, as the randomly generated maze tells us.
            _player = new Player(this, _currentMaze.StartPosition);
            _player.Initialize();
        }
        
        /// <summary> UnloadContent will be called once per game and is the place to unload all content. </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GameKeyboard.PlayerOne.IsKeyDown(Keys.Escape))
                this.Exit();
            
            _player.CheckInput();
            CheckPlayerCollisions();

            base.Update(gameTime);
        }

        private void CheckPlayerCollisions()
        {
            // Ensure the player doesn't walk beyond the bounds of the game, and if so, force them back into the area.
            if (_player.Position.Right > Constants.Maze.WidthPixels)
                _player.Position.Right = Constants.Maze.WidthPixels;
            else if (_player.Position.Left < 0)
                _player.Position.Left = 0;
            if (_player.Position.Bottom > Constants.Maze.HeightPixels)
                _player.Position.Bottom = Constants.Maze.HeightPixels;
            else if (_player.Position.Top < 0)
                _player.Position.Top = 0;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _currentMaze.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
