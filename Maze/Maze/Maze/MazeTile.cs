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
    public class MazeTile : Microsoft.Xna.Framework.GameComponent
    {
        public enum TileType
        {
            Yes,
            No,
            Maybe
        }

        private readonly TileType _tileType;

        /// <summary> Gets the position of the tile. </summary>
        public IReadOnlyWorldPosition Position { get; private set; }

        public Texture2D Texture { get; set; }

        public MazeTile(Game game, TileType tileType, Point position)
            : base(game)
        {
            _tileType = tileType;

            switch (_tileType)
            {
                case TileType.Yes:
                    break;
                case TileType.No:
                    break;
                case TileType.Maybe:
                    break;
            }

            Position = new WorldPosition(position);
        }
                
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //doesnt really need an update.. depends on the type i guess
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(Position.X, Position.Y, Constants.Tile.Width, Constants.Tile.Height), Color.White);
        }
    }
}
