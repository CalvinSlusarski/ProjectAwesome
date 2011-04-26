using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectAwesome
{
    class Projectile: Sprite
    {
        //constants for adjusting game values
        const string PROJECTILE_ASSETNAME = "Projectile";
        const int projectileSpeed = 250;
        const int START_POSITION_X = 150;
        const int START_POSITION_Y = 150;

        //enum to detect state
        enum State
        {
            Moving
        }
        //init state to moving
        State mCurrentState = State.Moving;

        int mSpeed = 0;
        float mRotation = 0.0f;

        KeyboardState mPreviousKeyboardState;
        public Projectile(int mx, int my)
        {
            Position.X = mx;
            Position.Y = my;
        }
        //load content
        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, PROJECTILE_ASSETNAME);
        }

        //update object
        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMovement();

            mPreviousKeyboardState = aCurrentKeyboardState;

            base.Update(theGameTime, mSpeed, mRotation);
        }
        //updates movement of the bullet while on screen
        private void UpdateMovement()
        {
            Position.Y++;
        }
        
    }
}
