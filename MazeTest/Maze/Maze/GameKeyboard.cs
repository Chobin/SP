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
            public static readonly Keys[] ActionKeys = new Keys[] { Keys.E, Keys.Space };
        }

        private static GameKeyboard _playerOne;

        /// <summary> Gets the singleton instance of the Game Keyboard interface. </summary>
        public static GameKeyboard PlayerOne
        {
            get { return (_playerOne == null) ? _playerOne = new GameKeyboard(PlayerIndex.One) : _playerOne; }
        }

        /// <summary> The PlayerIndex for this keyboard instance. </summary>
        private readonly PlayerIndex _playerIndex;

        /// <summary> The state of the keyboard in the previous 'frame'. </summary>
        private KeyboardState objPreviousState;

        /// <summary> The state of the keyboard in the current 'frame'. </summary>
        private KeyboardState objCurrentState;

        private GameKeyboard(PlayerIndex playerIndex)
        {
            _playerIndex = playerIndex;
            UpdateKeyboardState();
        }

        /// <summary> Gets if any of the specified keys are down and were up in the previous state. </summary>
        public bool IsKeyDownFromUp(params Keys[] objKeys)
        {
            return objKeys.Any(objKey => (objCurrentState.IsKeyDown(objKey) && objPreviousState.IsKeyUp(objKey)));
        }

        /// <summary> Gets if any of the specified keys are down. </summary>
        public bool IsKeyDown(params Keys[] objKeys)
        {
            return objKeys.Any(objKey => objCurrentState.IsKeyDown(objKey));
        }

        /// <summary> Gets if any of the specified keys are up and were down in the previous state. </summary>
        public bool IsKeyUpFromDown(params Keys[] objKeys)
        {
            return objKeys.Any(objKey => (objCurrentState.IsKeyUp(objKey) && objPreviousState.IsKeyDown(objKey)));
        }

        /// <summary> Ges if any of the specified keys are up. </summary>
        public bool IsKeyUp(params Keys[] objKeys)
        {
            return objKeys.Any(objKey => objCurrentState.IsKeyUp(objKey));
        }
        
        /// <summary> Updates the previous and current keyboard states. </summary>
        public static void UpdateKeyboardStates()
        {
            if (_playerOne != null)
                _playerOne.UpdateKeyboardState();
        }

        /// <summary> Updates the previous and current keyboard states. </summary>
        public void UpdateKeyboardState()
        {
            objPreviousState = objCurrentState;
            objCurrentState = Keyboard.GetState(_playerIndex);
        }        
    }
}
