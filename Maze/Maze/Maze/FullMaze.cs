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
        int width;
        int height;
        int[,] mazeDigits;//aparently this is a 2d array?

        MazeTile[,] mazeTiles;//aparently this is a 2d array?
        public FullMaze(Game game)
            : base(game)
        {
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

            mazeTiles = new MazeTile[w, h];//initializes all positions to null, so we need to create them all!
            Random rnd = new Random(w * h);//seed for some reason
            Vector2 startPosition = new Vector2(rnd.Next(0, w), rnd.Next(0, h));//creates the randomized start position (between 0-width and 0-height)
            Vector2 endPosition = new Vector2(rnd.Next(0, w), rnd.Next(0, h));//creates the randomized start position (between 0-width and 0-height)
            while (IsFirstToCloseToSecond(startPosition,endPosition,2))
                endPosition = new Vector2(rnd.Next(0, w), rnd.Next(0, h));//create a new ending position because this one is too close!

            
            base.Initialize();
        }

        private bool IsFirstToCloseToSecond(Vector2 first, Vector2 second, int within)
        {
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
    }
}