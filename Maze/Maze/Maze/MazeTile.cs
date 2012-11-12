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
        int Type;
        public Point Position { get; set; }
        public Texture2D Texture { get; set; }
        bool walkable;
        
        public MazeTile(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            walkable = false;
        }
        public int Width
        {
            get { return Texture.Width; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return Texture.Height; }
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize(int type, Point pos)
        {
            // TODO: Add your initialization code here
            switch (Type)
            {
                case 0://load a texture?
                    walkable = false;
                    break;
                case 1:
                    walkable = true;
                    break;
                case 2:
                    walkable = true;
                    //and something else!
                    break;
            }
            Position = pos;
            base.Initialize();
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
            spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
