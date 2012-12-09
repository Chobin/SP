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
        public static IDictionary<ETileType, Texture2D> TileTextures { get; private set; }
        public static IDictionary<ETileSubType, Texture2D> TileSubTextures { get; private set; }

        public enum ETileType
        {
            Yes,
            No,
            Maybe,
        }

        public enum ETileSubType
        {
            None,
            Start,
            End,
        }

        /// <summary> Gets or sets the primary type in use. </summary>
        public ETileType TileType { get; set; }

        /// <summary> Gets the position of the tile. </summary>
        public IReadOnlyWorldPosition Position { get; private set; }

        /// <summary> Gets or sets the sub type in use, if any. </summary>
        public ETileSubType TileSubType { get; set; }

        private Texture2D Texture
        {
            get
            {
                switch (TileSubType)
                {
                    case ETileSubType.None:
                        return TileTextures[TileType];
                    case ETileSubType.Start:
                        return TileSubTextures[TileSubType];
                    case ETileSubType.End:
                        return TileSubTextures[TileSubType];
                    default:
                        throw new NotSupportedException(string.Format("TileSubType '{0}' is not supported.", TileSubType));
                }                
            }
        }

        static MazeTile()
        {
            TileTextures = new Dictionary<ETileType, Texture2D>();
            TileSubTextures = new Dictionary<ETileSubType, Texture2D>();
        }

        public MazeTile(Game game, Point position)
            : base(game)
        {
            TileType = ETileType.No;
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
