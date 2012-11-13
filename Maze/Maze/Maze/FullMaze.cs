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
using Maze.Extensions;

namespace Maze
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class FullMaze : Microsoft.Xna.Framework.GameComponent
    {
        private readonly int _width;
        private readonly int _height;

        int[,] mazeDigits;
        public ISimpleWorldPosition StartPosition { get; private set; }
        public ISimpleWorldPosition EndPosition { get; private set; }

        /// <summary> 2D Array of tiles, as [column,row]. </summary>
        private MazeTile[,] mazeTiles;

        public FullMaze(Game game, int width, int height)
            : base(game)
        {
            MazeTile.TileTextures.Add( MazeTile.ETileType.No, game.Content.Load<Texture2D>("no"));
            MazeTile.TileTextures.Add( MazeTile.ETileType.Yes, game.Content.Load<Texture2D>("yes"));
            MazeTile.TileTextures.Add( MazeTile.ETileType.Maybe, game.Content.Load<Texture2D>("maybe"));
            MazeTile.TileSubTextures.Add( MazeTile.ETileSubType.Start, game.Content.Load<Texture2D>("start"));
            MazeTile.TileSubTextures.Add( MazeTile.ETileSubType.End, game.Content.Load<Texture2D>("end"));

            _width = width;
            _height = height;

            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // TODO: Add your initialization code here
            mazeDigits = new int[_width, _height];//initializes all positions to 0!

            // Initialize the Maze Tiles, all with the "No" texture, specifying each of their positions.
            mazeTiles = new MazeTile[_width, _height];
            for (int row = 0; row < mazeTiles.GetLength(0); row++)
            {
                for (int col = 0; col < mazeTiles.GetLength(1); col++)
                {
                    mazeTiles[row, col] = new MazeTile(base.Game, MazeTile.ETileType.No, new Point(col * Constants.Tile.Width, row * Constants.Tile.Height));
                }
            }

            Random rnd = new Random(_width * _height * DateTime.Now.Millisecond);//seed for some reason
            Point startPosition = new Point(rnd.Next(0, _width), rnd.Next(0, _height));//creates the randomized start position (between 0-width and 0-height)
            Point endPosition = new Point(rnd.Next(0, _width), rnd.Next(0, _height));//creates the randomized start position (between 0-width and 0-height)
            while (IsFirstTooCloseToSecond(startPosition, endPosition, 2))
            {
                endPosition = new Point(rnd.Next(0, _width), rnd.Next(0, _height));//create a new ending position because this one is too close!
            }

            // We have a good start and end position, so go create the maze from start to finish!
            StartPosition = new WorldPosition(startPosition.Inflate(Constants.Tile.Width, Constants.Tile.Height));
            EndPosition = new WorldPosition(endPosition.Inflate(Constants.Tile.Width, Constants.Tile.Height));

            mazeTiles[startPosition.Y, startPosition.X].TileSubType = MazeTile.ETileSubType.Start;
            mazeTiles[endPosition.Y, endPosition.X].TileSubType = MazeTile.ETileSubType.End;

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
