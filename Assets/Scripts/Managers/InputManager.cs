using UnityEngine;

namespace Managers
{
    public static class InputManager
    {
        #region Enums

        public enum Axis
        {
            Horizontal,
            Vertical
        }

        public enum Buttons
        {
            A,
            B,
            X,
            Y
        }

        public enum Player
        {
            P1,
            P2,
            P3,
            P4
        }

        #endregion

        #region Public Methods

        public static float GetPlayerAxis(Player playerNumber, Axis axis)
        {
            var value = Input.GetAxis(string.Format("{0}{1}", axis, playerNumber));
            value = (value > 0 && value < 0.1) || (value < 0 && value > -0.1) ? value : 0;
            return value;
        }

        public static bool GetPlayerButton(Player playerNumber, Buttons button)
        {
            return Input.GetButton(string.Format("{0}{1}", button, playerNumber));
        }

        public static bool GetPlayerButtonUp(Player playerNumber, Buttons button)
        {
            return Input.GetButtonUp(string.Format("{0}{1}", button, playerNumber));
        }

        public static bool GetPlayerButtonDown(Player playerNumber, Buttons button)
        {
            return Input.GetButtonDown(string.Format("{0}{1}", button, playerNumber));
        }

        #endregion
    }
}
