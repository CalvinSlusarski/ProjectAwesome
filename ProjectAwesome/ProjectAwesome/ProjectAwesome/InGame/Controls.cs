using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ProjectAwesome
{
    static class Controls
    {
        static KeyboardState mPreviousKeyboardState = Keyboard.GetState();
        static MouseState mouseStatePrevious = Mouse.GetState();
        // Control Flags
        public static bool playerRotateRight = false;
        public static bool playerRotateLeft = false;
        public static bool playerMoveForward = false;
        public static bool playerMoveBackward = false;
        public static bool playerShoot = false;
        public static bool playerPause = false;
        //MouseState mouseStateCurrent;
        // all keyboard actions take place here!
        // TODO: ADD METHODS AND INTERFACE FOR KEYBINDING!
        public static void Update()
        {
            set();
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            MouseState aMouseStateCurrent = Mouse.GetState();

            if ((aCurrentKeyboardState.IsKeyDown(Keys.Left) == true) ||
                (aCurrentKeyboardState.IsKeyDown(Keys.A) == true))
            {

                playerRotateLeft = true;
            }
            else if ((aCurrentKeyboardState.IsKeyDown(Keys.Right) == true) ||
                (aCurrentKeyboardState.IsKeyDown(Keys.D) == true))
            {
                playerRotateRight = true;
            }

            if ((aCurrentKeyboardState.IsKeyDown(Keys.Up) == true) ||
                (aCurrentKeyboardState.IsKeyDown(Keys.W) == true))
            {
                playerMoveForward = true;
            }
            else if ((aCurrentKeyboardState.IsKeyDown(Keys.Down) == true) ||
                (aCurrentKeyboardState.IsKeyDown(Keys.S) == true))
            {
                playerMoveBackward = true;
            }
            if ((aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && mPreviousKeyboardState.IsKeyDown(Keys.Space) == false) ||
                (aMouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton != ButtonState.Pressed))
            {
                playerShoot = true;
            }
            if ((aCurrentKeyboardState.IsKeyDown(Keys.Escape) == true))
            {
                playerPause = true;
            }
        }
        private static void set()
        {
            playerRotateRight = false;
            playerRotateLeft = false;
            playerMoveForward = false;
            playerMoveBackward = false;
            playerShoot = false;
            playerPause = false;
        }
    }
}
