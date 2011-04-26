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
    class Player : Sprite
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
        Camera camera;

        //Enumerator states used to asking if player is moving
        enum State
        {
            Moving
        }
        State mCurrentState = State.Moving;

        int mSpeed = 0;
        float mRotation = 0.0f;

        //projectile array to hold bullets
        List<Projectile> mBullets = new List<Projectile>();
        ContentManager mContentManager;
        

        KeyboardState mPreviousKeyboardState;
        public Player(ref Camera camera)
        {
            this.camera = camera;
        }
        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            

            foreach (Projectile aProjectile in mBullets)
            {

                aProjectile.LoadContent(theContentManager);

            }
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, PLAYER_ASSETNAME);
        }

        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMovement(aCurrentKeyboardState);
            UpdateProjectile(theGameTime, aCurrentKeyboardState);
            mPreviousKeyboardState = aCurrentKeyboardState;

            base.Update(theGameTime, mSpeed, mRotation);
        }
        private void UpdateProjectile(GameTime theGameTime, KeyboardState aCurrentKeyboardState)
        {
            foreach (Projectile aProjectile in mBullets)
            {
                aProjectile.Update(theGameTime);
            }
            
            if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && mPreviousKeyboardState.IsKeyDown(Keys.Space) == false)
            {
                ShootProjectile();
            }
        }
        private void ShootProjectile()
        {
            if (mCurrentState == State.Moving)
            {
                bool aCreateNew = true;

                foreach (Projectile aProjectile in mBullets)
                {
                    if (aProjectile.Visible == false)
                    {
                        aCreateNew = false;
                        aProjectile.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                            200, 1.0f);
                        break;
                    }
                }

                if (aCreateNew == true)
                {
                    Projectile aProjectile = new Projectile();
                    aProjectile.LoadContent(mContentManager);
                    aProjectile.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                        200, mRotation);
                    mBullets.Add(aProjectile);
                }
            }
        }


        //Assume player is not moving
        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            mSpeed = 0;
            //Dan Reed added WASD support
            if (mCurrentState == State.Moving)
            {
                if ((aCurrentKeyboardState.IsKeyDown(Keys.Left) == true) ||
                    (aCurrentKeyboardState.IsKeyDown(Keys.A) == true))
                {
                    mRotation -= ROTATE_SPEED;
                }
                else if ((aCurrentKeyboardState.IsKeyDown(Keys.Right) == true) ||
                    (aCurrentKeyboardState.IsKeyDown(Keys.D) == true))
                {
                    mRotation += ROTATE_SPEED;
                }

                if ((aCurrentKeyboardState.IsKeyDown(Keys.Up) == true) ||
                    (aCurrentKeyboardState.IsKeyDown(Keys.W) == true))
                {
                    mSpeed = PLAYER_SPEED;
                    camera.Move(Position, true);


                }
                else if ((aCurrentKeyboardState.IsKeyDown(Keys.Down) == true) ||
                    (aCurrentKeyboardState.IsKeyDown(Keys.S) == true))
                {
                    mSpeed = PLAYER_SPEED * -1;
                    camera.Move(Position, true);
                }
                
            }
        }
        public override void Draw(SpriteBatch theSpriteBatch)
        {
            foreach (Projectile aProjectile in mBullets)
            {
                aProjectile.Draw(theSpriteBatch);
            }
            base.Draw(theSpriteBatch);
        }
    }
}

