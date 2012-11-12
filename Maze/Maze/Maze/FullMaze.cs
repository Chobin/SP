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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class FullMaze : Microsoft.Xna.Framework.GameComponent
    {
        private readonly Texture2D _noTexture;
        private readonly Texture2D _yesTexture;
        private readonly Texture2D _startTexture;
        private readonly Texture2D _endTexture;

        int width;
        int height;
        int[,] mazeDigits;
        public Point StartPosition;
        Point EndPosition;

        /// <summary> 2D Array of tiles, as [row,column]. </summary>
        private MazeTile[,] mazeTiles;

        public FullMaze(Game game)
            : base(game)
        {
            _noTexture = game.Content.Load<Texture2D>("no");
            _yesTexture = game.Content.Load<Texture2D>("yes");
            _startTexture = game.Content.Load<Texture2D>("start");
            _endTexture = game.Content.Load<Texture2D>("end");

            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize(int w, int h)
        {
            // TODO: Add your initialization code here
            width = w;
            height = h;
            mazeDigits = new int[w, h];//initializes all positions to 0!

            // Initialize the Maze Tiles, all with the "No" texture, specifying each of their positions.
            mazeTiles = new MazeTile[w, h];
            for (int row = 0; row < mazeTiles.GetLength(0); row++)
            {
                for (int col = 0; col < mazeTiles.GetLength(1); col++)
                {
                    mazeTiles[row, col] = new MazeTile(base.Game)
                    {
                        Position = new Point(col * _noTexture.Width, row * _noTexture.Height),
                        Texture = _noTexture,
                    };
                }
            }

            Random rnd = new Random(w * h * DateTime.Now.Millisecond);//seed for some reason
            Point startPosition = new Point(rnd.Next(0, w), rnd.Next(0, h));//creates the randomized start position (between 0-width and 0-height)
            Point endPosition = new Point(rnd.Next(0, w), rnd.Next(0, h));//creates the randomized start position (between 0-width and 0-height)
            while (IsFirstTooCloseToSecond(startPosition,endPosition,2))
                endPosition = new Point(rnd.Next(0, w), rnd.Next(0, h));//create a new ending position because this one is too close!

            //we have a good start and end position, so go create the maze from start to finish!
            StartPosition = startPosition;
            EndPosition = endPosition;

            // Start/End Tiles can use the Yes texture.
            mazeTiles[startPosition.X, startPosition.Y].Texture = _startTexture;
            mazeTiles[endPosition.X, endPosition.Y].Texture = _endTexture;

            base.Initialize();
        }

        private bool IsFirstTooCloseToSecond(Point first, Point second, int within)
        {
            if (first.X + within > second.X && first.X - within < second.X)//then the x point is within! so check the Y
            {
                if (first.Y + within > second.Y && first.Y - within < second.Y)//it is within the y also? oh shit!
                {
                    return true;
                }
            }
            
            return false;
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var mazeTile in mazeTiles)
            {
                mazeTile.Draw(spriteBatch);
            }
        }
    }
}
