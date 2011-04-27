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
    class Enemy : Sprite
    {
        // Constants for adjusting game variables
        const string ENEMY_ASSETNAME = "f22";
        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        const int ENEMY_SPEED = 160;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        const float ROTATE_SPEED = 0.08f;

        Boolean alive = false;
        int mSpeed = 0;
        int shotChance = 3;
        float mRotation = 0.0f;

        //projectile array to hold bullets
        List<Projectile> mBullets = new List<Projectile>();
        ContentManager mContentManager;

        //load up the content
        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            //load up the projectile content for each bullet in the array
            foreach (Projectile aProjectile in mBullets)
            {
                aProjectile.LoadContent(theContentManager);
            }
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, ENEMY_ASSETNAME);
        }
        //update method for enemy
        //--I chose initially to handle everything with a random feel just for simplicity's sake
        public void Update(GameTime theGameTime)
        {                
            Random gen = new Random();  //init random gen, time based seed
            UpdateMovement(gen);        //run the movement method with the generator
            UpdateProjectile(theGameTime, gen); //same thing, but for the projectiles
            
            base.Update(theGameTime, mSpeed, mRotation);
        }
        //projectile control method for enemy class
        private void UpdateProjectile(GameTime theGameTime, Random generator)
        {
            //for each projectile, update it
            foreach (Projectile aProjectile in mBullets)
            {
                aProjectile.Update(theGameTime);
            }
            //I initialized shotchance to like 3 up at the top, 
            //this should give them a 3% shot to shoot a projectile
            if ((generator.Next() % 100) < shotChance)
            {
                ShootProjectile();
            }
        }
        //projectile creation method for enemy class
        private void ShootProjectile()
        {
            //if the enemy is alive
            if (alive == true)
            {
                //set this create new flag to true
                bool aCreateNew = true;

                //and then for each bullet in the bullet list
                foreach (Projectile aProjectile in mBullets)
                {
                    //if it's not currently being shot
                    if (aProjectile.Visible == false)
                    {
                        aCreateNew = false;     //set the flag back to false to avoid a loop
                        aProjectile.Fire(Position,  
                            400, mRotation);// adjusted fire speed
                        break;
                    }
                }

                //if you reach the end of the bullet list without changing the create flag
                if (aCreateNew == true)
                {
                    //create a new projectile, init it, add it to the list, and shoot it
                    Projectile aProjectile = new Projectile();
                    aProjectile.LoadContent(mContentManager);
                    aProjectile.Fire(Position,
                        200, mRotation);
                    mBullets.Add(aProjectile);
                }
            }
        }
        //this should really be set to follow player but I wanted to implement this quickly
        private void UpdateMovement(Random generator)
        {
            mSpeed = 30;
            int tmp = generator.Next();
            // 75% of the time they should turn left
            if (tmp%4 <= 3)
            {
                mRotation -= ROTATE_SPEED;
                if (mRotation < -360.0f)
                    mRotation = mRotation % 360.0f;
            }
            else
            {
                mRotation -= ROTATE_SPEED;
                if (mRotation > 360.0f)
                    mRotation = mRotation % 360.0f;                
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
