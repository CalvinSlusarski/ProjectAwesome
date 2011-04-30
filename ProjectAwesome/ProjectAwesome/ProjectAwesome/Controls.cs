using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ProjectAwesome
{
    class Controls
    {
        KeyboardState mPreviousKeyboardState;
        MouseState mouseStatePrevious;
        //MouseState mouseStateCurrent;
        // all keyboard actions take place here!
        // TODO: ADD METHODS AND INTERFACE FOR KEYBINDING!
        public void Update(Player player)
        {
            player.Attack = false;
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            MouseState aMouseStateCurrent = Mouse.GetState(); 

            if ((aCurrentKeyboardState.IsKeyDown(Keys.Left) == true) ||
                (aCurrentKeyboardState.IsKeyDown(Keys.A) == true)) {
                    player.RotateLeft();
            }
            else if ((aCurrentKeyboardState.IsKeyDown(Keys.Right) == true) ||
                (aCurrentKeyboardState.IsKeyDown(Keys.D) == true)) {
                    player.RotateRight();
            }

            if ((aCurrentKeyboardState.IsKeyDown(Keys.Up) == true) ||
                (aCurrentKeyboardState.IsKeyDown(Keys.W) == true)) {
                    player.MoveForward();
            }
            else if ((aCurrentKeyboardState.IsKeyDown(Keys.Down) == true) ||
                (aCurrentKeyboardState.IsKeyDown(Keys.S) == true)) {
                    player.MoveBackward();
            }
            if ((aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && mPreviousKeyboardState.IsKeyDown(Keys.Space) == false) ||
                (aMouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton != ButtonState.Pressed)) {
                    player.Attack = true;
            }
        }
    }
}
