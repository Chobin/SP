using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Maze
{
    public class GameKeyboard
    {
        public struct Presets
        {
            public static readonly Keys[] UpKeys = new Keys[] { Keys.W, Keys.Up };
            public static readonly Keys[] LeftKeys = new Keys[] { Keys.A, Keys.Left };
            public static readonly Keys[] DownKeys = new Keys[] { Keys.S, Keys.Down };
            public static readonly Keys[] RightKeys = new Keys[] { Keys.D, Keys.Right };
        }

        private static GameKeyboard _playerOne;

        /// <summary> Gets the singleton instance of the Game Keyboard interface. </summary>
        public static GameKeyboard PlayerOne
        {
            get { return (_playerOne == null) ? _playerOne = new GameKeyboard(PlayerIndex.One) : _playerOne; }
        }

        private GameKeyboard(PlayerIndex playerIndex)
        {
            _playerIndex = playerIndex;
        }

        /// <summary> The PlayerIndex for this keyboard instance. </summary>
        private readonly PlayerIndex _playerIndex;

        /// <summary> Gets the if any of the specified keys are down for this instance. </summary>
        public bool IsKeyDown(params Keys[] keys)
        {
            var keyboardState = Keyboard.GetState(_playerIndex);
            return (from key in keys
                    where keyboardState.IsKeyDown(key)
                    select key).Any();
        }
    }
}
