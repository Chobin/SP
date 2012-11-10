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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player(this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //load player texture here, for now it will be random guy from my other game
            Vector2 playerStartPos = new Vector2(60, 60);
            Texture2D playerTexture = Content.Load<Texture2D>("player");

            player.Initialize(playerTexture, playerStartPos);
            GenerateMaze(20, 20);
            //load our maze textures here

            // TODO: use this.Content to load your game content here
        }
        public FullMaze GenerateMaze(int width, int height)
        {
            FullMaze maze = new FullMaze(this);
            maze.Initialize(width, height);
            //do mazelols.
            return maze;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();


            // TODO: Add your update logic here
            player.CheckInput();
            CheckPlayerCollisions();

            base.Update(gameTime);
        }

        private void CheckPlayerCollisions()
        {
            //player position is the top left of the texture so add size of texture to keep his whole body on screen
            if ((player.Position.X + player.Width) > GraphicsDevice.Viewport.TitleSafeArea.Width)
                player.Position.X = GraphicsDevice.Viewport.TitleSafeArea.Width - player.Width;
            else if (player.Position.X < 0)
                player.Position.X = 0;
            if ((player.Position.Y + player.Height) > GraphicsDevice.Viewport.TitleSafeArea.Height)
                player.Position.Y = GraphicsDevice.Viewport.TitleSafeArea.Height - player.Height;
            else if (player.Position.Y < 0)
                player.Position.Y = 0;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
