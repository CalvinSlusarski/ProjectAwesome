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
        const int MAX_DISTANCE = 500;
        const int START_POSITION_X = 150;
        const int START_POSITION_Y = 150;

        //enum to detect state
        //enum State
        //{
        //    Moving
        //}
        //init state to moving
        //State mCurrentState = State.Moving;

        
        float mRotation = 0.0f;
        public Boolean Visible = false;
        Vector2 mStartPosition = new Vector2(START_POSITION_X, START_POSITION_Y);
        int mSpeed = 300;
        

        //KeyboardState mPreviousKeyboardState;
       
        //load content
        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, PROJECTILE_ASSETNAME);
        }

        //update object
        public void Update(GameTime theGameTime)
        {
            if (Vector2.Distance(mStartPosition, Position) > MAX_DISTANCE)
            {
                Visible = false;
            }
            
            if (Visible == true)
            {

                base.Update(theGameTime, mSpeed, mRotation);
            }
        }
        
        public override void Draw(SpriteBatch theSpriteBatch)
        {

            if (Visible == true)
            {

                base.Draw(theSpriteBatch);

            }

        }
        public void Fire(Vector2 theStartPosition, int theSpeed, float theDirection)
        {

            Position = theStartPosition;

            mStartPosition = theStartPosition;

            mSpeed = theSpeed;

            mRotation = theDirection;

            Visible = true;

        }


        
    }
}
