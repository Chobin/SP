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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool isPaused;
        bool isStarted;
        Player player;
        static float BULLET_MOVEMENT = 15;
        Texture2D[] playerTextures;
        Texture2D bulletTexture;
        Texture2D[] groundTextures;
        static int NUM_PLAYER_TEXTURES = 30;
        static int NUM_GROUND_TEXTURES = 45;
        List<BasicProjectile> projectiles;
        GameTime fullTime;
        List<GroundElement> groundElements;
        static int NUM_GROUND_ELEMENTS = 45;
        bool jump;
        Level lvl1;
        //static double GRAVITY = 9.51;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferHeight = 1050;
            this.graphics.PreferredBackBufferWidth = 1680;
            //this.graphics.IsFullScreen = true;
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
            fullTime = new GameTime();
            isPaused = false;
            isStarted = false;
            player = new Player(this);
            projectiles = new List<BasicProjectile>();
            groundElements = new List<GroundElement>();
            jump = true;
            lvl1 = new Level(this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Create a new SpriteBatch, which can be used to draw textures.
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,GraphicsDevice.Viewport.TitleSafeArea.Y +GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            playerTextures = new Texture2D[NUM_PLAYER_TEXTURES];
            playerTextures[0] = Content.Load<Texture2D>("player");
            playerTextures[1] = Content.Load<Texture2D>("player-l");
            playerTextures[2] = Content.Load<Texture2D>("player-r");
            playerTextures[3] = Content.Load<Texture2D>("player-u");
            playerTextures[4] = Content.Load<Texture2D>("player-d");
            bulletTexture = Content.Load<Texture2D>("bullet");
            groundTextures = new Texture2D[NUM_GROUND_TEXTURES];
            groundTextures[0] = Content.Load<Texture2D>("grassL");
            groundTextures[1] = Content.Load<Texture2D>("grassM");
            groundTextures[2] = Content.Load<Texture2D>("grassR");
            player.Initialize(playerTextures[0], playerPosition);
            lvl1.Load(this.graphics.GraphicsDevice, Content.Load<Texture2D>("icebg"));
            for(int i=0;i < NUM_GROUND_ELEMENTS;i++)
            {
                GroundElement elem;
                elem = new GroundElement(this);
                elem.Initialize(groundTextures[i % 3], new Vector2((i+3) * 30, 950 ));//- (i*20)));
                groundElements.Add(elem);
            }

            // TODO: use this.Content to load your game content here
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
        //float vibrationAmount = 0.0f;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            float MovementX;
            float MovementY;
            float FireX;
            float FireY;
            float playerisinmiddle;
            Vector2 OldPlayerPos = player.Position;
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            if (currentState.Buttons.Back == ButtonState.Pressed)
                this.Exit();
            MovementX = currentState.ThumbSticks.Left.X;
            MovementY = currentState.ThumbSticks.Left.Y;
            FireX = currentState.ThumbSticks.Right.X;
            FireY = currentState.ThumbSticks.Right.Y;
            if (currentState.IsConnected && currentState.Buttons.A == ButtonState.Pressed && jump)
            {
                jump = false;
                if (player.Jump(gameTime) == false)
                    jump = true;
            }
            else if (currentState.IsConnected && currentState.Buttons.A == ButtonState.Released && !jump)
                jump = true;

            player.Update(gameTime, MovementX);
            //player.Position.Y -= MovementY * MOVEMENT;
            UpdateProjectiles(gameTime);

            // TODO: Add your update logic here
            //if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D) )
            //{
            //    player.Position.X += MOVEMENT;
            //    player.SetTexture(playerTextures[2]);
            //}
            //if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
            //{
            //    player.Position.X -= MOVEMENT;
            //    player.SetTexture(playerTextures[1]);
            //}
            //if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            //{
            //    player.Position.Y -= MOVEMENT;
            //    player.SetTexture(playerTextures[3]);
            //}
            //if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            //{
            //    player.Position.Y += MOVEMENT;
            //    player.SetTexture(playerTextures[4]);
            //}
            CheckPlayerColisions();
            CheckProjectileColisions();
            
            //vibrations stuff!
            //if (currentState.IsConnected && currentState.Buttons.A ==
            //    ButtonState.Pressed)
            //{
            //    // Button A is currently being pressed; add vibration.
            //    vibrationAmount =
            //        MathHelper.Clamp(vibrationAmount + 0.03f, 0.0f, 1.0f);
            //    GamePad.SetVibration(PlayerIndex.One,
            //        vibrationAmount, vibrationAmount);
            //}
            //else
            //{
            //    // Button A is not being pressed; subtract some vibration.
            //    vibrationAmount =
            //        MathHelper.Clamp(vibrationAmount - 0.05f, 0.0f, 1.0f);
            //    GamePad.SetVibration(PlayerIndex.One,
            //        vibrationAmount, vibrationAmount);
            //}

            //player.Update(gameTime);

            //(FireX < 0.5 && FireX > -0.5) && (FireY < 0.5 && FireY > -0.5)
            if (currentState.Triggers.Right > 0.5 )
            {
                if (((FireX < 0.5 && FireX > -0.5) && (FireY < 0.5 && FireY > -0.5)))
                {
                    if (MovementX < -0.25)
                        FireX = -1;
                    else 
                        FireX = 1;
                }
                if (player.Shoot(gameTime))
                {
                    
                    BasicProjectile projectile = new BasicProjectile(this);
                    projectile.Initialize(GraphicsDevice.Viewport, player.Position + new Vector2(player.Width / 2, 0), FireX, FireY, bulletTexture, BULLET_MOVEMENT);
                    projectiles.Add(projectile);
                }
            }
            if (player.Position.X >= (GraphicsDevice.Viewport.Width / 2))
            {
                playerisinmiddle = player.Position.X - OldPlayerPos.X;
                player.XPosDiff += playerisinmiddle;
                player.Position.X = OldPlayerPos.X;
            }
            else if (player.Position.X <= (GraphicsDevice.Viewport.Width / 2) && player.XPosDiff > 0.0f)
            {

                playerisinmiddle = player.Position.X - OldPlayerPos.X;
                player.XPosDiff += playerisinmiddle;
                player.Position.X = OldPlayerPos.X;
            }
            else
                playerisinmiddle = 0.0f;
            lvl1.Update((OldPlayerPos.X - player.Position.X) - playerisinmiddle);
            for (int i = 0; i <= groundElements.Count - 1; i++)
            {
                groundElements[i].Update((OldPlayerPos.X - player.Position.X) - playerisinmiddle);
            }
            base.Update(gameTime);
        }

        private void CheckProjectileColisions()
        {
            for (int i = 0; i <= projectiles.Count - 1; i++)
            {
                for (int j = 0; j < groundElements.Count - 1; j++)
                {
                    int gWidth = groundElements[j].groundTexture.Width;
                    int gHeight = groundElements[j].groundTexture.Height;
                    float x = groundElements[j].Position.X;
                    float y = groundElements[j].Position.Y;
                    if ((projectiles[i].Position.Y + projectiles[i].Height > y && projectiles[i].Position.Y <= y) && ((projectiles[i].Position.X >= x) && (projectiles[i].Position.X <= x + gWidth)))
                    {
                        projectiles[i].Active = false;
                    }
                    else if ((projectiles[i].Position.Y < (y + gHeight) && projectiles[i].Position.Y + projectiles[i].Height > y + gHeight) && (projectiles[i].Position.X >= x) && (projectiles[i].Position.X <= x + gWidth))
                    {
                        projectiles[i].Active = false;
                    }
                }
            }

        }

        private void CheckPlayerColisions()
        {
            //off screen
            bool hitOne = false;
            for (int i = 0; i < groundElements.Count - 1; i++)
            {
                int gWidth = groundElements[i].groundTexture.Width;
                int gHeight = groundElements[i].groundTexture.Height;
                float x = groundElements[i].Position.X;
                float y = groundElements[i].Position.Y;
                if ((player.Position.Y + player.Height > y && player.Position.Y  <= y) && ((player.Position.X + player.Width >= x) && (player.Position.X <= x + gWidth)))
                {
                    hitOne = true;
                    //if (player.Position.Y+player.Height < y)
                        player.Position.Y = y - player.Height+1;
            //        if (player.Position.Y < y + gHeight && player.Position.Y + player.Height > y)
              //          player.Position.Y = y+gHeight;
                    player.currentPlayerState = Player.playerState.STATIC;
                    //if (player.Position.X + player.Width >= x)
                    //    player.Position.X = x - player.Width;
                    //else if (player.Position.X < x + gWidth)
                    //    player.Position.X = x + gWidth;
                }
                else if ((player.Position.Y < (y + gHeight) && player.Position.Y+player.Height > y+gHeight) && (player.Position.X + player.Width >= x) && (player.Position.X  <= x + gWidth))
                {
                    hitOne = false;
                    player.Position.Y = y + gHeight;
                    player.speedY = 0;
                    //if (player.Position.X + player.Width >= x)
                    //    player.Position.X = x - player.Width;
                    //else if (player.Position.X < x + gWidth)
                    //    player.Position.X = x + gWidth;


                }
                else//not under or over, on the side?
                {

                }
                //if (player.Position.Y > GraphicsDevice.Viewport.TitleSafeArea.Height - player.Height)
                    //player.Position.Y = GraphicsDevice.Viewport.TitleSafeArea.Height - player.Height;
            }
            if (player.Position.Y <= GraphicsDevice.Viewport.TitleSafeArea.Y)
                player.Position.Y = GraphicsDevice.Viewport.TitleSafeArea.Y;
            if (player.Position.Y > GraphicsDevice.Viewport.TitleSafeArea.Height - player.Height)
            {
                hitOne = true;
                player.currentPlayerState = Player.playerState.STATIC;
                player.Position.Y = GraphicsDevice.Viewport.TitleSafeArea.Height - player.Height - 1;
            }
            if (player.Position.X <= GraphicsDevice.Viewport.TitleSafeArea.X)
                player.Position.X = GraphicsDevice.Viewport.TitleSafeArea.X;
            if (player.Position.X > GraphicsDevice.Viewport.TitleSafeArea.Width - player.Width)
                player.Position.X = GraphicsDevice.Viewport.TitleSafeArea.Width - player.Width;
            //with ground elements
            if (!hitOne)
                player.currentPlayerState = Player.playerState.POSTJUMP;

        }
        public void UpdateProjectiles(GameTime gameTime)
        {
            for (int i = 0; i <= projectiles.Count -1 ; i++)
            {
                projectiles[i].Update(gameTime);
                if (projectiles[i].Active == false)
                    projectiles.RemoveAt(i);
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            lvl1.Draw(spriteBatch);

            // Draw the Player
            player.Draw(spriteBatch);

            for (int i = 0; i <= projectiles.Count - 1; i++)
            {
                projectiles[i].Draw(gameTime, spriteBatch);
            }
            for (int i = 0; i <= groundElements.Count - 1; i++)
            {
                groundElements[i].Draw(spriteBatch);
            }

            // Stop drawing
            spriteBatch.End();
            if (!isStarted)
            {
                //Draw main menu
            }
            else
            {
                if (isPaused)
                {
                    //draw paused screen
                }
            }

            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }
    }
}
