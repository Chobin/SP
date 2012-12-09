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

// Phil is the greatest human being alive and would survive the zombie apocalypse.
namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        double PADDLE_REG_SIZE;//25% of screen  = paddle regular size
        double PADDLE_SMALL_SIZE;//% of paddle regular size of screen
        double PADDLE_HEIGHT;
        double MOVEMENT;
        double playerPositionX;
        double playerPositionY;
        double player2PositionX;
        double player2PositionY;
        Point ballPosition;
        double BALL_SPEED_X;
        double BALL_SPEED_Y;
        double BALL_SPEED_Y_BASE;
        SpriteFont font;
        int p1Score;
        int p2Score;
        TimeSpan timeSinceLastGoal;
        TimeSpan highestTime;


        Texture2D t;
        Texture2D b;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 820;
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
            MOVEMENT = GraphicsDevice.Viewport.Width * 0.05;
            PADDLE_REG_SIZE = GraphicsDevice.Viewport.Width * 0.05;
            PADDLE_SMALL_SIZE = GraphicsDevice.Viewport.Width * 0.05;
            PADDLE_HEIGHT = GraphicsDevice.Viewport.Width * 0.05;
            BALL_SPEED_X = GraphicsDevice.Viewport.Width * 0.01;
            BALL_SPEED_Y_BASE = BALL_SPEED_Y = GraphicsDevice.Viewport.Height * 0.01;
            ballPosition.X = (int)(graphics.GraphicsDevice.Viewport.Width / 2.0) ;
            ballPosition.Y = (int)(25);
            playerPositionX = (graphics.GraphicsDevice.Viewport.Width / 2.0) - PADDLE_REG_SIZE / 2;//center of screen
            playerPositionY = graphics.GraphicsDevice.Viewport.Height - graphics.GraphicsDevice.Viewport.Height * 0.02;//
            player2PositionX = (graphics.GraphicsDevice.Viewport.Width / 2.0) - PADDLE_REG_SIZE / 2;//center of screen
            player2PositionY = graphics.GraphicsDevice.Viewport.Height * 0.02;//
            GameTime time;
            time = new GameTime();
            timeSinceLastGoal = time.TotalGameTime;
            highestTime = new TimeSpan(0,0,0,0);
            p1Score = 0;
            p2Score = 0;
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
            t = new Texture2D(GraphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });
            b = new Texture2D(GraphicsDevice, 1, 1);
            b.SetData(new[] { Color.White });
            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("Score");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            //if viewport changed update the sizes
            MOVEMENT = GraphicsDevice.Viewport.Width * 0.02;
            PADDLE_REG_SIZE = GraphicsDevice.Viewport.Width * 0.15;
            PADDLE_SMALL_SIZE = GraphicsDevice.Viewport.Width * 0.05;
            PADDLE_HEIGHT = GraphicsDevice.Viewport.Width * 0.01;

            //BALL_SPEED_X = GraphicsDevice.Viewport.Width * 0.01;
            //BALL_SPEED_Y = GraphicsDevice.Viewport.Height * 0.01;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
            {
                playerPositionX += MOVEMENT;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
            {
                playerPositionX -= MOVEMENT;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            {
                //playerPositionY -= MOVEMENT;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
               // playerPositionY += MOVEMENT;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
            {
                player2PositionX += MOVEMENT;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
            {
                player2PositionX -= MOVEMENT;
            }
            ballPosition.X = (int)(ballPosition.X + BALL_SPEED_X);
            ballPosition.Y = (int)(ballPosition.Y + BALL_SPEED_Y);

            if (playerPositionX < 0)
                playerPositionX = 0;
            if (playerPositionX > (GraphicsDevice.Viewport.Width - PADDLE_REG_SIZE))
                playerPositionX = (GraphicsDevice.Viewport.Width - PADDLE_REG_SIZE);


            if (player2PositionX < 0)
                player2PositionX = 0;
            if (player2PositionX > (GraphicsDevice.Viewport.Width - PADDLE_REG_SIZE))
                player2PositionX = (GraphicsDevice.Viewport.Width - PADDLE_REG_SIZE);

            if (ballPosition.Y + 20 >= playerPositionY)//ball has hit bottom player's position (paddle or score)
            {
                if (ballPosition.X + 30 > playerPositionX && ballPosition.X - 10 < playerPositionX + PADDLE_REG_SIZE)//hit the paddle so reverse!
                {
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
                    {
                        if (BALL_SPEED_X < -2)
                            BALL_SPEED_X *= 0.75;
                        else if (BALL_SPEED_X > 2)
                            BALL_SPEED_X *= 0.75;
                        else if (BALL_SPEED_X < 0)
                            BALL_SPEED_X *= -0.25;
                        else if (BALL_SPEED_X > 0)
                            BALL_SPEED_X *= -0.25;
                    }
                    else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
                    {
                        if (BALL_SPEED_X < -2)
                            BALL_SPEED_X *= 0.75;
                        else if (BALL_SPEED_X > 2)
                            BALL_SPEED_X *= 0.75;
                        else if (BALL_SPEED_X < 0)
                            BALL_SPEED_X *= -0.25;
                        else if (BALL_SPEED_X > 0)
                            BALL_SPEED_X *= -0.25;
                    }
                    else
                        BALL_SPEED_X *= 1.2;
                    BALL_SPEED_Y *= -1.05;
                    ballPosition.Y -= 5;
                }
                else {
                    //LOSE!
                    ballPosition.Y = (int)(playerPositionY - 30);//position the ball on the players paddle
                    p2Score++;
                    ballPosition.X = (int)(playerPositionX + PADDLE_REG_SIZE/2);
                    BALL_SPEED_X = new Random().NextDouble() * (GraphicsDevice.Viewport.Width * 0.01);
                    BALL_SPEED_Y = BALL_SPEED_Y_BASE * -1;
                    timeSinceLastGoal = gameTime.TotalGameTime;
                    //BALL_SPEED_Y *= -1;
                }
            }
            else if (ballPosition.Y <= player2PositionY + PADDLE_HEIGHT)
            {
                //ballPosition.X + 30 < player2PositionX || ballPosition.X - 10 > player2PositionX + PADDLE_REG_SIZE
                if (ballPosition.X +30 > player2PositionX && ballPosition.X -10 < player2PositionX + PADDLE_REG_SIZE)//hit the paddle so reverse!
                {
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
                    {
                        if (BALL_SPEED_X < -2)
                            BALL_SPEED_X *= 0.75;
                        else if (BALL_SPEED_X > 2)
                            BALL_SPEED_X *= 0.75;
                        else if (BALL_SPEED_X < 0)
                            BALL_SPEED_X *= -0.25;
                        else if (BALL_SPEED_X > 0)
                            BALL_SPEED_X *= -0.25;
                    }
                    else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
                    {
                        if (BALL_SPEED_X < -2)
                            BALL_SPEED_X *= 0.75;
                        else if (BALL_SPEED_X > 2)
                            BALL_SPEED_X *= 0.75;
                        else if (BALL_SPEED_X < 0)
                            BALL_SPEED_X *= -0.25;
                        else if (BALL_SPEED_X > 0)
                            BALL_SPEED_X *= -0.25;
                    }
                    else
                        BALL_SPEED_X *= 1.2;
                    BALL_SPEED_Y *= -1.05;
                    ballPosition.Y += 5;
                }
                else
                {
                    //LOSE!
                    ballPosition.Y = (int)(player2PositionY + PADDLE_HEIGHT + 30);
                    p1Score++;
                    ballPosition.X = (int)(player2PositionX + PADDLE_REG_SIZE / 2);
                    BALL_SPEED_X = new Random().NextDouble() * (GraphicsDevice.Viewport.Width * 0.01);
                    BALL_SPEED_Y = BALL_SPEED_Y_BASE;
                    timeSinceLastGoal = gameTime.TotalGameTime;
                }
            }

            if (ballPosition.X < 0)
            {
                ballPosition.X = 0;
                BALL_SPEED_X *= -1;
            }
            if (ballPosition.X > (GraphicsDevice.Viewport.Width - 20))
            {
                ballPosition.X = (int)(GraphicsDevice.Viewport.Width - 20);
                BALL_SPEED_X *= -1;
                ballPosition.X = (int)(ballPosition.X + BALL_SPEED_X);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Rectangle rectangle = new Rectangle((int)playerPositionX, (int)playerPositionY, (int)PADDLE_REG_SIZE, (int)PADDLE_HEIGHT);
            Rectangle rectangle2 = new Rectangle((int)player2PositionX, (int)player2PositionY, (int)PADDLE_REG_SIZE, (int)PADDLE_HEIGHT);
            Rectangle rectangle3 = new Rectangle(ballPosition.X, ballPosition.Y, 20, 20);
            spriteBatch.Begin();
            spriteBatch.Draw(t, rectangle, Color.White);
            spriteBatch.Draw(t, rectangle2, Color.White);
            String p1Text;
            p1Text = "Player 1 Score:" + p1Score;

            String p2Text;
            p2Text = "Player 2 Score:" + p2Score;
            TimeSpan sinceLast = (gameTime.TotalGameTime - timeSinceLastGoal);
            String timerText;
            String timer2Text;
            timerText = "Time Since Last Score:" + sinceLast.Hours + ":" + sinceLast.Minutes + ":" + sinceLast.Seconds + ":" + sinceLast.Milliseconds;
            Color Timercolor = new Color(128, 128, 128);

            spriteBatch.DrawString(font, p1Text, new Vector2((float)20, (float)playerPositionY - 50), new Color(128, 128, 255));
            spriteBatch.DrawString(font, p2Text, new Vector2((float)20, (float)player2PositionY + 50), new Color(128, 128, 255));
            spriteBatch.DrawString(font, timerText, new Vector2(GraphicsDevice.Viewport.Width / 3, GraphicsDevice.Viewport.Height / 2), Timercolor);

            if (highestTime < sinceLast)
            {
                highestTime = sinceLast;
                Random rand = new Random();
                Timercolor = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
            }
            timer2Text = "Longest volley:" + highestTime.Hours + ":" + highestTime.Minutes + ":" + highestTime.Seconds + ":" + highestTime.Milliseconds;

            spriteBatch.DrawString(font, timer2Text, new Vector2(GraphicsDevice.Viewport.Width / 3, GraphicsDevice.Viewport.Height / 2 + 30), Timercolor);

            spriteBatch.Draw(b, rectangle3, Color.White);

            spriteBatch.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
