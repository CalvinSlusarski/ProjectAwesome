using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectAwesome
{
    class Player: Sprite
    {
        const string PLAYER_ASSETNAME = "Boat";
        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        const int PLAYER_SPEED = 160;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        const float ROTATE_SPEED = 0.025f;

        enum State
        {
            Moving
        }
        State mCurrentState = State.Moving;

        int mSpeed = 0;
        float mRotation = 0.0f;

        KeyboardState mPreviousKeyboardState;

        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, PLAYER_ASSETNAME);
        }

        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMovement(aCurrentKeyboardState);

            mPreviousKeyboardState = aCurrentKeyboardState;

            base.Update(theGameTime, mSpeed, mRotation);
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Moving)
            {

                mSpeed = 0;

                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
                {
                    mRotation -= ROTATE_SPEED;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
                {
                    mRotation += ROTATE_SPEED;
                }

                if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true)
                {
                    mSpeed = PLAYER_SPEED;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
                {
                    mSpeed = PLAYER_SPEED*-1;
                }
            }
        }
    }
}

