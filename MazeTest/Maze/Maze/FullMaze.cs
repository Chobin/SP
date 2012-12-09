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

        /// <summary> 2D Array of tiles, as [col,row]. </summary>
        private MazeTile[,] mazeTiles;

        public FullMaze(Game game, int width, int height)
            : base(game)
        {
            MazeTile.TileTextures.Clear();
            MazeTile.TileTextures[MazeTile.ETileType.No] = game.Content.Load<Texture2D>("no");
            MazeTile.TileTextures[MazeTile.ETileType.Yes] = game.Content.Load<Texture2D>("yes");
            MazeTile.TileTextures[MazeTile.ETileType.Maybe] = game.Content.Load<Texture2D>("maybe");
            MazeTile.TileSubTextures[MazeTile.ETileSubType.Start] = game.Content.Load<Texture2D>("start");
            MazeTile.TileSubTextures[MazeTile.ETileSubType.End] = game.Content.Load<Texture2D>("end");

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
            Random rnd = new Random(_width * _height * DateTime.Now.Millisecond);//seed for some reason
            mazeTiles = new MazeTile[_width, _height];
            for (int col = 0; col < mazeTiles.GetLength(0); col++)
            {
                for (int row = 0; row < mazeTiles.GetLength(1); row++)
                {
                    mazeTiles[row, col] = new MazeTile(base.Game, new Point(col * Constants.Tile.Width, row * Constants.Tile.Height));
                    int ran = rnd.Next(3);
                    if(ran == 0)
                        mazeTiles[row, col].TileType = MazeTile.ETileType.No;
                    else
                        mazeTiles[row, col].TileType = MazeTile.ETileType.Yes;
                }
            }

            Point startPosition = new Point(rnd.Next(0, _width), rnd.Next(0, _height));//creates the randomized start position (between 0-width and 0-height)
            Point endPosition = new Point(rnd.Next(0, _width), rnd.Next(0, _height));//creates the randomized start position (between 0-width and 0-height)
            while (IsFirstTooCloseToSecond(startPosition, endPosition, 2))
            {
                endPosition = new Point(rnd.Next(0, _width), rnd.Next(0, _height));//create a new ending position because this one is too close!
            }

            // We have a good start and end position, so go create the maze from start to finish!
            {
                var startLeftOfEnd = (startPosition.X < endPosition.X);
                int delta = (startLeftOfEnd) ? 1 : -1;
                Func<int, bool> condition = (startLeftOfEnd) ? (Func<int, bool>)((x) => x <= endPosition.X) : (Func<int, bool>)((x) => x >= endPosition.X);
                for (int x = startPosition.X; condition(x); x += delta)
                {
                    // Make all tiles from Start to End walkable for now, as a straight line.
                 //   mazeTiles[startPosition.Y, x].TileType = MazeTile.ETileType.Yes;
                }
            }

            {
                var startAboveEnd = (startPosition.Y < endPosition.Y);
                int delta = (startAboveEnd) ? 1 : -1;
                Func<int, bool> condition = (startAboveEnd) ? (Func<int, bool>)((y) => y <= endPosition.Y) : (Func<int, bool>)((y) => y >= endPosition.Y);
                for (int y = startPosition.Y; condition(y); y += delta)
                {
                    // Make all tiles from Start to End walkable for now, as a straight line.
                 //   mazeTiles[y, endPosition.X].TileType = MazeTile.ETileType.Yes;
                }
            }

            mazeTiles[startPosition.Y, startPosition.X].TileType = MazeTile.ETileType.Yes;
            mazeTiles[startPosition.Y, startPosition.X].TileSubType = MazeTile.ETileSubType.Start;
            mazeTiles[endPosition.Y, endPosition.X].TileType = MazeTile.ETileType.Yes;
            mazeTiles[endPosition.Y, endPosition.X].TileSubType = MazeTile.ETileSubType.End;

            // Inform the outside world of the real-world coordiantes (pixels) for the Start/End tiles.
            StartPosition = new WorldPosition(startPosition.Inflate(Constants.Tile.Width, Constants.Tile.Height));
            EndPosition = new WorldPosition(endPosition.Inflate(Constants.Tile.Width, Constants.Tile.Height));

            //why was this being done twice? lol
           /// base.Initialize();
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
        public void BreakTile(WorldPosition Position, int direction)
        {
            WorldPosition TileToSwitch = new WorldPosition();
            switch (direction)
            {
                case 0://none
                    return;
                case 1://up
                    TileToSwitch.X = (Position.X / Constants.Tile.Width);
                    TileToSwitch.Y = (Position.Y / Constants.Tile.Height) - 1;
                    break;
                case 2://down
                    TileToSwitch.X = (Position.X / Constants.Tile.Width);
                    TileToSwitch.Y = (Position.Y / Constants.Tile.Height) + 1;
                    break;
                case 3://left
                    TileToSwitch.X = (Position.X / Constants.Tile.Width) - 1;
                    TileToSwitch.Y = (Position.Y / Constants.Tile.Height);
                    break;
                case 4://right
                    TileToSwitch.X = (Position.X / Constants.Tile.Width) + 1;
                    TileToSwitch.Y = (Position.Y / Constants.Tile.Height);
                    break;
            }
            if (TileToSwitch.X >= _width || TileToSwitch.Y >= _height || TileToSwitch.X < 0 || TileToSwitch.Y < 0)
                return;//do nothing if tile is out of bounds
            if (mazeTiles[TileToSwitch.Y, TileToSwitch.X].TileType == MazeTile.ETileType.No)
                mazeTiles[TileToSwitch.Y, TileToSwitch.X].TileType = MazeTile.ETileType.Yes;
            return;
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
        public int IsThisCollided(WorldPosition pos)
        {
            float xFloat = ((float)pos.Left / (float)Constants.Tile.Width);
            float yFloat = ((float)pos.Top / (float)Constants.Tile.Height);
            int x = (pos.Left / Constants.Tile.Width) -1;//get the closest width row
            int y = (pos.Top / Constants.Tile.Height) -1;//get the closest height row
            int xEnd = x + 3;
            int yEnd = y + 3;
            if(x < 0)
                x = 0;
            if(xEnd > mazeTiles.GetLength(0))
                xEnd = mazeTiles.GetLength(0);
            if(y < 0)
                y = 0;
            if(yEnd > mazeTiles.GetLength(1))
                yEnd = mazeTiles.GetLength(1);
            for (int col = y; col < yEnd; col++)
            {
                for (int row = x; row < xEnd; row++)
                {
                    MazeTile currentTile = mazeTiles[col, row];
                    Rectangle mazeRect = new Rectangle(currentTile.Position.X, currentTile.Position.Y, currentTile.Position.Right - currentTile.Position.Left, currentTile.Position.Bottom - currentTile.Position.Top);
                    Rectangle playerRect = new Rectangle(pos.X, pos.Y, pos.RightP - pos.Left, pos.BottomP - pos.Top);
                    if (currentTile.TileType == MazeTile.ETileType.No)
                    {
                        if (playerRect.Intersects(mazeRect))
                            return 1;
                    }
                    else if (currentTile.TileType == MazeTile.ETileType.Yes && currentTile.TileSubType == MazeTile.ETileSubType.End)
                    {
                        if (playerRect.Intersects(mazeRect))
                            return 2;
                    }
                }
            }
            return 0;
        }
    }
}
