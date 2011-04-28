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
        const int ENEMY_SPEED = 130;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        const float ROTATE_SPEED = 0.05f;

        public Boolean alive = false;
        int mSpeed = 10;
        int shotChance = 3;
        float mRotation = 0.0f;
        Vector2 mStartPosition = new Vector2(START_POSITION_X, START_POSITION_Y);

        //projectile array to hold bullets
        public List<Projectile> mBullets = new List<Projectile>();
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
            //UpdateMovement(gen);        //run the movement method with the generator
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
            //this should give them a .3% shot to shoot a projectile in a given frame
            if ((generator.Next() % 1000) < shotChance)
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
                        400, mRotation);
                    mBullets.Add(aProjectile);
                }
            }
        }
        /* OUTDATED MOVEMENT METHOD
         * I just commented this out for now --Dan
        //this should really be set to follow player but I wanted to implement this quickly
        private void UpdateMovement(Random generator)
        {
            mSpeed = 30;
            int tmp = generator.Next();
            // 75% of the time they should turn left
            if (tmp % 1000 <= 103)
            {
                mRotation -= ROTATE_SPEED;
                if (mRotation < -360.0f)
                    mRotation = mRotation % 360.0f;
            }
            else if (tmp % 1000 >= 950)
            {
                mRotation += ROTATE_SPEED;
                if (mRotation > 360.0f)
                    mRotation = mRotation % 360.0f;
            }
            else
            {
                mSpeed = ENEMY_SPEED;
            }
        }
         * */
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
            /*BROKEN... i dunno why
            //we're going to cast a 2dray in front of the enemy
            //and check to see if it connects with player
            float deltaX;
            float deltaY;
            Vector2 anEnd;
            int rayDistance = 700;
            
            //depending on rotation (pi/4 is 45 deg, 3pi/4 is 135 deg, etc)
            //figure out if delta x and y should be a sin or cos call
            //from sohcahtoa (google it if you forgot trig, i know i did)
            //betwen degree (315 and 45), (135 and 225), sin is x cos is y
            //and the other half is the other way (sin(angle) = opposite/hypotenuse)
            if ((Rotation <= (MathHelper.Pi / 4.0f)) || (Rotation > (7.0f * (MathHelper.Pi / 4.0f))))
            {
                //find the change in x and the change in y (sohcahtoa)
                deltaX = (float)rayDistance * (float)Math.Sin(Rotation);
                deltaY = (float)rayDistance * (float)Math.Cos(Rotation);
            }
            else if ((Rotation > (1.0f* MathHelper.Pi) / 4.0f) && (Rotation <= (3.0f* (MathHelper.Pi / 4.0f))))
            {
                //find the change in x and the change in y (sohcahtoa)
                deltaY = (float)rayDistance * (float)Math.Sin(Rotation);
                deltaX = (float)rayDistance * (float)Math.Cos(Rotation);
            }
            else if ((Rotation > (3.0f * MathHelper.Pi) / 4.0f) && (Rotation <= (5.0f * (MathHelper.Pi / 4.0f))))
            {
                //find the change in x and the change in y (sohcahtoa)
                deltaX = (float)rayDistance * (float)Math.Sin(Rotation);
                deltaY = (float)rayDistance * (float)Math.Cos(Rotation);
            }
            else
            {
                //find the change in x and the change in y (sohcahtoa)
                deltaY = (float)rayDistance * (float)Math.Sin(Rotation);
                deltaX = (float)rayDistance * (float)Math.Cos(Rotation);
            }
            //depending on rotation (0 is up, pi/2 rads is right, etc)
            //find the endpoint of the ray straight in front of enemy
            if (Rotation <= (MathHelper.Pi / 2.0f))
            {
                anEnd = new Vector2(Position.X + deltaX,
                                    Position.Y + deltaY);
            }
            else if ((Rotation > (MathHelper.Pi / 2.0f)) && (Rotation <= MathHelper.Pi))
            {
                anEnd = new Vector2(Position.X + deltaX,
                                    Position.Y + deltaY);
            }
            else if ((Rotation > (MathHelper.Pi)) && (Rotation <= (3.0f * MathHelper.Pi) / 2.0f))
            {
                anEnd = new Vector2(Position.X + deltaX,
                                    Position.Y + deltaY);
            }
            else
            {
                anEnd = new Vector2(Position.X + deltaX,
                                    Position.Y + deltaY);
            }

            //create the ray in front of enemy
            Ray2D aRay = new Ray2D(Position, anEnd);

            //create the rectangle for player
            Rectangle playerRect = new Rectangle((int)p.Position.X, (int)p.Position.Y,
                (int)p.Position.X + p.Size.Width,
                (int)p.Position.Y + p.Size.Height);
            //if the ray doesn't touch the player rectangle, turn rightdd
            if (!(aRay.Intersects(playerRect)))
            {
                rotate(0.08f, true);
            }
            */

            //2nd attempt
            //this time I'm going to try a different method.
            //i'm going to do it by comparing angles.
            //call the angle of the line from enemy to player angle A,
            //the angle made by the rotation of this enemy B,
            //and the difference between the 2 angles C.
            //if C is less than maybe 10 degrees (pi/20 rad?) don't do anything
            //but if B-A <= 180 degrees (pi rad), make the enemy turn left
            //else turn right.

            //if this method works, i'm going to completely get rid of ray2d.
            //I thought it was ugly and i don't think it works right.

            //at any rate, let's declare our variables required:
            float angleA, angleC;
            //first we figure out the angle of A
            angleA = findAngle(p.Center);
            //then we figure out the angle of C (b is just enemy rotation)
            angleC = mRotation - angleA;
            //if the absolute value of c is < about 10 degrees (pi/20 rad)
            //don't do anything
            if (angleC <= (MathHelper.Pi / 20))
            {
            }
            //else if b-a < pi rad turn left
            else if ((angleC < MathHelper.Pi) && (angleC > (MathHelper.Pi / 20)))
            {
                rotate(ROTATE_SPEED, false);
            }
            //else turn right
            else
            {
                rotate(ROTATE_SPEED, true);
            }
        }
        //returns the angle between this enemy and the destination spot
        public float findAngle(Vector2 destination)
        {
            float ans = 0.0f;
            // theta = arctan(x/y)
            ans = (float)Math.Atan((Math.Abs(Position.X-destination.X))/
                                    (Math.Abs(Position.Y-destination.Y)));
            return ans;
        }
    }
}
