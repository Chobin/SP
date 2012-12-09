using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Maze
{
    public sealed class WorldPosition : ISimpleWorldPosition, IReadOnlyWorldPosition
    {
        /// <summary> The underlying x/y position of the instance, relative to the top-left of the screen. Use the X/Y/Left/Right/Top/Bottom properties to set. </summary>
        private Point _position;

        public WorldPosition()
            : this(Point.Zero)
        {
        }

        public WorldPosition(Point position)
        {
            this._position = position;
        }

        public WorldPosition(ISimpleReadOnlyWorldPosition position)
        {
            this._position = new Point(position.X, position.Y);
        }

        /// <summary> Gets the WorldPosition instace as a Read Only instance. </summary>
        public IReadOnlyWorldPosition AsReadOnly()
        {
            return this;
        }

        /// <summary> Gets or sets the X position of the instance. (Same as Left) </summary>
        /// <remarks> By abstracting the Point struct beneath, we ensure we don't accidentally fall prey to struct manipulation issues. </remarks>
        public int X { get { return _position.X; } set { _position.X = value; } }

        /// <summary> Gets or sets the Y position of the instance. (Same as Top) </summary>
        /// <remarks> By abstracting the Point struct beneath, we ensure we don't accidentally fall prey to struct manipulation issues. </remarks>
        public int Y { get { return _position.Y; } set { _position.Y = value; } }

        /// <summary> Gets or sets the Top position of the instance. (Same as Y) </summary>
        /// <remarks> By abstracting the Point struct beneath, we ensure we don't accidentally fall prey to struct manipulation issues. </remarks>
        public int Top
        {
            get { return this.Y; }
            set { this.Y = Math.Max(0, value); }
        }

        /// <summary> Gets or sets the Bottom position of the instance. </summary>
        /// <remarks> By abstracting the Point struct beneath, we ensure we don't accidentally fall prey to struct manipulation issues. </remarks>
        public int Bottom
        {
            get { return this.Y + Constants.Tile.Height; }
            set { this.Y = Math.Max(0, value - Constants.Tile.Height); }
        }
        public int BottomP
        {
            get { return this.Y + Constants.Player.Height; }
            set { this.Y = Math.Max(0, value - Constants.Player.Height); }
        }

        /// <summary> Gets or sets the Left position of the instance. (Same as X) </summary>
        /// <remarks> By abstracting the Point struct beneath, we ensure we don't accidentally fall prey to struct manipulation issues. </remarks>
        public int Left
        {
            get { return this.X; }
            set { this.X = Math.Max(0, value); }
        }

        /// <summary> Gets or sets the Right position of the instance. </summary>
        /// <remarks> By abstracting the Point struct beneath, we ensure we don't accidentally fall prey to struct manipulation issues. </remarks>
        public int Right
        {
            get { return this.X + Constants.Tile.Width; }
            set { this.X = Math.Max(0, value - Constants.Tile.Width); }
        }
        public int RightP
        {
            get { return this.X + Constants.Player.Width; }
            set { this.X = Math.Max(0, value - Constants.Player.Width); }
        }
    }

    /// <summary> Defines a ReadOnly World Position. </summary>
    public interface IReadOnlyWorldPosition : ISimpleReadOnlyWorldPosition
    {
        /// <summary> Gets the Top position of the instance. (Same as Y) </summary>
        int Top { get; }

        /// <summary> Gets the Bottom position of the instance. </summary>
        int Bottom { get; }

        /// <summary> Gets the Left position of the instance. (Same as X) </summary>
        int Left { get; }

        /// <summary> Gets the Right position of the instance. </summary>
        int Right { get; }
    }

    public interface ISimpleWorldPosition : ISimpleReadOnlyWorldPosition
    {
        new int X { get; set; }
        new int Y { get; set; }
    }

    public interface ISimpleReadOnlyWorldPosition
    {
        int X { get; }
        int Y { get; }
    }

}
