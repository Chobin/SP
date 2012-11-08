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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GroundElement : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Texture2D groundTexture;
        public Vector2 Position;

        public GroundElement(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize(Texture2D tex, Vector2 pos)
        {
            // TODO: Add your initialization code here

            groundTexture = tex;
            Position = pos;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(float deltaX)
        {
            if (deltaX > 1)
                Position.X += deltaX ;
            else if (deltaX < -1)
                Position.X += deltaX ;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(groundTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
