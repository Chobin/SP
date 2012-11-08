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
    ///         
    public enum BulletDirection
	        {
            UP,
            DOWN,
            LEFT,
	        RIGHT,
            UPLEFT,
            UPRIGHT,
            DOWNLEFT,
            DOWNRIGHT
	        }
    public class BasicProjectile : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Vector2 Position;
        public bool Active;
        public Texture2D BulletTexture;
        private Viewport gameViewport;
        private float directionX;
        private float directionY;
        private float bulletSpeed;
        private float gravity;


        public BasicProjectile(Game game)
            : base(game)
        {
            Active = false;
            gravity = 0.0f;
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize(Viewport viewport, Vector2 bulletPos, float directionX, float directionY, Texture2D bulletTexture, float bulSpeed)
        {
            // TODO: Add your initialization code here
            BulletTexture = bulletTexture;
            Position = bulletPos;
            gameViewport = viewport;
            this.directionX = directionX;
            this.directionY = directionY;
            Active = true;
            bulletSpeed = bulSpeed;
            base.Initialize();
        }
        // Get the width of the player ship
        public int Width
        {
            get { return BulletTexture.Width; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return BulletTexture.Height; }
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            gravity += 0.1f;
            if (gravity < -50.0f)
                gravity = -50.0f;
            Position.X += bulletSpeed * directionX - gravity/10;
            Position.Y += bulletSpeed * (-directionY) + gravity;
            if (Position.X > gameViewport.Width || Position.X < 0 || Position.Y > gameViewport.Height || Position.Y < 0)
                Active = false;
            base.Update(gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BulletTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            base.Draw(gameTime);
        }
    }
}
