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
        // Constants for adjusting game variables
        const string PLAYER_ASSETNAME = "Boat";
        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        const int PLAYER_SPEED = 160;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        const float ROTATE_SPEED = 0.025f;
        //Enumerator states used to asking if player is moving
        enum State
        {
            Moving
        }
        State mCurrentState = State.Moving;

        int mSpeed = 0;
        float mRotation = 0.0f;

        //projectile array to hold bullets
        public Projectile[] mBulletArr = new Projectile[5];
        int bulletCount = 0;

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
        //Assume player is not moving
        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            mSpeed = 0;
            //Dan Reed added WASD support
            if (mCurrentState == State.Moving)
            {
                if ((aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)||
                    (aCurrentKeyboardState.IsKeyDown(Keys.A) == true))
                {
                    mRotation -= ROTATE_SPEED;
                }
                else if ((aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)||
                    (aCurrentKeyboardState.IsKeyDown(Keys.D) == true))
                {
                    mRotation += ROTATE_SPEED;
                }

                if ((aCurrentKeyboardState.IsKeyDown(Keys.Up) == true)||
                    (aCurrentKeyboardState.IsKeyDown(Keys.W) == true))
                {
                    mSpeed = PLAYER_SPEED;
                }
                else if ((aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)||
                    (aCurrentKeyboardState.IsKeyDown(Keys.S) == true))
                {
                    mSpeed = PLAYER_SPEED*-1;
                }
                //test code for projectile --DR
                if ((aCurrentKeyboardState.IsKeyDown(Keys.Space)==true))
                {
                    mBulletArr[bulletCount] = new Projectile();
                    bulletCount++;
                }
            }
        }
    }
}

