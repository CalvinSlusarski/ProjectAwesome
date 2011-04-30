using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectAwesome
{
    class Collisions
    {
        enum State
        {
            Player,
            Enemy,
            Projectile,
            UnSet
        }
        /// <summary>
        /// Coordinates Of All Objects In 2d Vector Space
        /// </summary>
        List<MyTypeAndLocation> CoordAndType = new List<MyTypeAndLocation>();

        private void UpdateCollisions()
        {
            ////for each enemy and for each player's bullet, check collision
            //foreach (Enemy aEnemy in Enemies)
            //{
            //    foreach (Projectile p in mPlayerSprite.mBullets)
            //    {
            //        //if they intersect and the bullet is visible
            //        if ((p.intersect(aEnemy) == true) && p.Visible)
            //        {
            //            //kill the enemy and kill the bullet
            //            aEnemy.alive = false;
            //            p.Visible = false;
            //        }
            //    }
            //}
            ////for each enemies bullet check against player for collision
            //foreach (Enemy anEnemy in Enemies)
            //{
            //    foreach (Projectile pr in anEnemy.mBullets)
            //    {
            //        if (pr.intersect(mPlayerSprite) && pr.Visible)
            //        {
            //            //increment the times the player has been shot, kill bullet
            //            timesShot++;
            //            pr.Visible = false;
            //        }
            //    }
            //}
        }

        // Method is designed to detect collisions of a vector2
        public bool detect(Vector2 myLocation)
        {
            bool didIDetectACollision = false;

            // Compare against all objects in space for collisions
            foreach (MyTypeAndLocation didWeCollide in CoordAndType)
            {
                
            }
            return didIDetectACollision;
        }
        public void newObject(string ObjectType, Vector2 location)
        {
            MyTypeAndLocation newObject = new MyTypeAndLocation(State.UnSet,new Vector2(0,0));
            switch (ObjectType)
            {
                case "Enemy":
                    newObject.WhatAmI = State.Enemy;
                    break;
                case "Player":
                    newObject.WhatAmI = State.Player;
                    break;
                case "Projectile":
                    newObject.WhatAmI = State.Projectile;
                    break;
            }

        }

        // This inner class is designed to store locations and type of objects floating in 2d space...
        class MyTypeAndLocation
        {
            // Constructor
            public MyTypeAndLocation(State mCurrentState, Vector2 coordinates)
            {
                WhatAmI = mCurrentState;
                Coordinates = coordinates;
            }

            //Enumerator states used to determine what the object im colliding with is
            State objectType = State.UnSet;
            Vector2 coordinates;

            #region props
            // Declare what i am! one of the enum states please...
            public State WhatAmI
            {
                get
                {
                    return objectType;
                }
                set
                {
                    objectType = value;
                }
            }
            // Where do i currently reside?
            public Vector2 Coordinates
            {
                get
                {
                    return coordinates;
                }
                set
                {
                    coordinates = value;
                }
            }
            #endregion
        }
    }
}
