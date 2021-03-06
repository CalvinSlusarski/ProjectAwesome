﻿using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace ProjectAwesome
{
    class Enemy: GameObject
    {
        // Constants for adjusting game variables
        const string ENEMY_ASSETNAME = "f22";
        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        const int ENEMY_SPEED = 130;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        const float ROTATE_SPEED = 0.05f;

        public bool alive = false;
        int mSpeed = 10;
        int shotChance = 3;
        float mRotation = 0.0f;
        Vector2 mStartPosition = new Vector2(START_POSITION_X, START_POSITION_Y);

        //ProjectileV2 array to hold bullets
        public List<Projectile> mBullets = new List<Projectile>();
        ContentManager mContentManager;

        //load up the content
        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            //load up the ProjectileV2 content for each bullet in the array
            foreach (Projectile aProjectile in mBullets)
            {
                aProjectile.LoadContent(theContentManager);
            }
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, ENEMY_ASSETNAME);
        }
        //update method for enemy
        //--I chose initially to handle everything with a random feel just for simplicity's sake
        public void Update(GameTime theGameTime, Player p)
        {                
            Random gen = new Random();  //init random gen, time based seed
            //UpdateMovement(gen);        //run the movement method with the generator
            UpdateProjectileV2(theGameTime, gen); //same thing, but for the ProjectileV2s
            follow(p);
            base.Update(theGameTime, mSpeed, mRotation);
        }
        
        //ProjectileV2 control method for enemy class
        private void UpdateProjectileV2(GameTime theGameTime, Random generator)
        {
            //for each ProjectileV2, update it
            foreach (Projectile aProjectile in mBullets)
            {
                aProjectile.Update(theGameTime);
            }
            //I initialized shotchance to like 3 up at the top, 
            //this should give them a .3% shot to shoot a ProjectileV2 in a given frame
            if ((generator.Next() % 1000) < shotChance)
            {
                ShootProjectileV2();
            }
        }
        //ProjectileV2 creation method for enemy class
        private void ShootProjectileV2()
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
                        //aProjectile.Fire(Position,  
                        //    400, mRotation);// adjusted fire speed
                        break;
                    }
                }

                //if you reach the end of the bullet list without changing the create flag
                if (aCreateNew == true)
                {
                    //create a new ProjectileV2, init it, add it to the list, and shoot it
                    Projectile aProjectile = new Projectile();
                    aProjectile.LoadContent(mContentManager);
                    //aProjectile.Fire(Position,
                    //    400, mRotation);
                    mBullets.Add(aProjectile);
                }
            }
        }

        public void Spawn(Vector2 theStartPosition, int theSpeed, float theDirection)
        {

            Position = theStartPosition;
            mStartPosition = theStartPosition;
            mSpeed = theSpeed;
            mRotation = theDirection;
            alive = true;

        }
        
        public override void Draw(SpriteBatch theSpriteBatch)
        {
            foreach (Projectile aProjectile in mBullets)
            {
                if(aProjectile.Visible)
                    aProjectile.Draw(theSpriteBatch);
            }
            base.Draw(theSpriteBatch);
        }
        //rotate the sprite from another class
        public void rotate(float change, bool plus)
        {
            if (plus)
                mRotation += change;
            else
                mRotation -= change;            
        }
        public void follow(Player p)
        {
            float angle = findAngle(p.Position);
            mRotation = angle;
        }
        //returns the angle between this enemy and the destination spot
        public float findAngle(Vector2 destination)
        {
            double ans = 0.0f;
            // theta = arctan(x/y)
            ans = Math.Atan((Math.Abs(Position.X-destination.X))/
                                    (Math.Abs(Position.Y-destination.Y)));

            return (float)ConvertRadiansToDegrees(ans);
        }
        private static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }
    }
}
