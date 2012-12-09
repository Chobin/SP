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
       // private WorldPosition _oldPlayerPosition;
        private FullMaze _currentMaze;
        private Texture2D _fogOfWar;
        private SpriteFont _font;
        private int _numSpaces;
        private int _numMazeCompleted;
        private GameState _gameState;

        public Game1()
        {
            // This must be done in the constructor, otherwise the initialization stack of the game will not be invoked properly and the entire XNA Game stack will be compromised! Don't move it!
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 1000;
            _numMazeCompleted = 0;
            _gameState = new GameState();
            _gameState.gameState = GameState.InState.Menu;
            _gameState.menuState = GameState.MenuState.Start;
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

            _fogOfWar = Content.Load<Texture2D>("fogofwar");
            _font = Content.Load<SpriteFont>("Score");
            CreateAll();
        }
        
        /// <summary> UnloadContent will be called once per game and is the place to unload all content. </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void CreateAll()
        {
            // Create out Maze, which is the entire "world" the player will interact with.
            _currentMaze = new FullMaze(this, Constants.Maze.Width, Constants.Maze.Height);
            _currentMaze.Initialize();

            // Initialize the player, informing it of it's starting point, somewhere within the maze, as the randomly generated maze tells us.
            _player = new Player(this, _currentMaze.StartPosition);
            _player.Initialize();
            //_oldPlayerPosition = _player.Position;
            _numSpaces = 5;
        }

        private void Reload()
        {
            _player.Dispose();
            _player = null;
            _currentMaze.Dispose();
            _currentMaze = null;

            CreateAll();
        }

        protected override void Update(GameTime gameTime)
        {
            //check where we are in the game, menu, maze, etc
            GameKeyboard.UpdateKeyboardStates();
            switch(_gameState.gameState)
            {
                case GameState.InState.Started:

                    // At the start of every Update frame, the Game Keyboard must be updated so we know what has changed between frames.

                    // Allows the game to exit
                    if (GameKeyboard.PlayerOne.IsKeyDown(Keys.Escape))
                        _gameState.gameState = GameState.InState.Menu;
                    //if (GameKeyboard.PlayerOne.IsKeyDownFromUp(Keys.F5))
                    //    this.Reload();
                    if (GameKeyboard.PlayerOne.IsKeyDownFromUp(Keys.Space) && _numSpaces > 0)
                    {
                        int direction;
                        direction = _player.GetDirection();
                        _currentMaze.BreakTile(_player.Position, direction);
                        _numSpaces--;
                    }

                    _player.CheckInput();
                    CheckPlayerCollisions();
                    break;
                case GameState.InState.Menu:
                    if (GameKeyboard.PlayerOne.IsKeyUpFromDown(Keys.Up) || GameKeyboard.PlayerOne.IsKeyUpFromDown(Keys.W))
                    {
                        _gameState.menuState--;
                        if (_gameState.menuState < GameState.MenuState.Start)
                            _gameState.menuState = GameState.MenuState.Quit;
                    }
                    if (GameKeyboard.PlayerOne.IsKeyUpFromDown(Keys.Down) || GameKeyboard.PlayerOne.IsKeyUpFromDown(Keys.S))
                    {
                        _gameState.menuState++;
                        if (_gameState.menuState == GameState.MenuState.Off)
                            _gameState.menuState = GameState.MenuState.Start;

                    }
                    if (GameKeyboard.PlayerOne.IsKeyUpFromDown(Keys.Enter) || GameKeyboard.PlayerOne.IsKeyUpFromDown(Keys.Space))
                    {
                        switch (_gameState.menuState)
                        {
                            case GameState.MenuState.Start:
                                //start the game!
                                _gameState.gameState = GameState.InState.Started;
                                break;
                            case GameState.MenuState.Something:
                                //start the game!
                                break;
                            case GameState.MenuState.Quit:
                                //start the game!
                                _gameState.gameState = GameState.InState.Quit;
                                break;
                        }
                    }
                    break;
                case GameState.InState.Quit:
                    this.Exit();
                    break;
            }

            base.Update(gameTime);
        }
        

        // TODO: This entire method needs to be in the Player.Update method, to prevent the movement if we try to go over the bounds. Otherwise, we won't be able to scan the entire Tile Map to see
        // if the position we've moved to can somehow be rolled back. This is crucial for tile collision.
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
            //get closest tile
            int collisionNum = _currentMaze.IsThisCollided(_player.Position);
            if (collisionNum > 0)
            {
                Console.WriteLine("Collision!!");
                if (collisionNum == 1)//normal player collision so reset it!
                    _player.SetLastPosition();
                else if (collisionNum == 2)//hit the end position!
                {
                    _numMazeCompleted++;
                    this.Reload();
                }
            }
            //Console.WriteLine("Player Position x:" + _player.Position.X + " Y:" + _player.Position.Y);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            String TextToDraw;
            switch (_gameState.gameState)
            {
                case GameState.InState.Started:

                    _spriteBatch.Begin();
                    _currentMaze.Draw(_spriteBatch);
                    _player.Draw(_spriteBatch);
                    _spriteBatch.Draw(_fogOfWar,
                        new Rectangle(
                        (int)(_player.Position.X + Constants.Player.Width / 2) - (_fogOfWar.Width),
                        (int)_player.Position.Y - (_fogOfWar.Height),
                        _fogOfWar.Width * 2, _fogOfWar.Height * 2),
                        Color.White);
                    TextToDraw = "number of spaces=" + _numSpaces + "!!";
                    _spriteBatch.DrawString(_font, TextToDraw, new Vector2((float)_graphics.PreferredBackBufferWidth - 400, 50), new Color(255, 255, 255));
                    TextToDraw = "Number of mazes completed:" + _numMazeCompleted;
                    _spriteBatch.DrawString(_font, TextToDraw, new Vector2((float)_graphics.PreferredBackBufferWidth - 400, 100), new Color(255, 255, 255));
                    _spriteBatch.End();
                    break;
                case GameState.InState.Menu:
                    _spriteBatch.Begin();
                    Color textColor;
                    TextToDraw = "Start";
                    if (_gameState.menuState == GameState.MenuState.Start)
                    {
                        textColor = new Color(255, 0, 0);
                    }
                    else
                        textColor = new Color(255, 255, 255);
                    _spriteBatch.DrawString(_font, TextToDraw, new Vector2((float)_graphics.PreferredBackBufferWidth / 3, 400), textColor);

                    TextToDraw = "Something";
                    if (_gameState.menuState == GameState.MenuState.Something)
                    {
                        textColor = new Color(255, 0, 0);
                    }
                    else
                        textColor = new Color(255, 255, 255);
                    _spriteBatch.DrawString(_font, TextToDraw, new Vector2((float)_graphics.PreferredBackBufferWidth / 3, 500), textColor);

                    TextToDraw = "Quit";
                    if (_gameState.menuState == GameState.MenuState.Quit)
                    {
                        textColor = new Color(255, 0, 0);
                    }
                    else
                        textColor = new Color(255, 255, 255);
                    _spriteBatch.DrawString(_font, TextToDraw, new Vector2((float)_graphics.PreferredBackBufferWidth / 3, 600), textColor);
                    _spriteBatch.End();
                    break;
                case GameState.InState.Quit:
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
