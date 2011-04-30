using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ProjectAwesome
{
    class Player: GameObject
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
        // adjustable variables
        int mSpeed = 0;
        float mRotation = 0.0f;
        bool attack;
        // Projectile attack
        //public List<Projectile> mBullets = new List<Projectile>();

        public Player()
        {
            // Musings from GameObject with Love
            attack = false;
            base.Group = identity.Player;
            base.CanDraw = true;
        }


        public void LoadContent(ContentManager theContentManager)
        {

            //foreach (Projectile aProjectile in mBullets) { aProjectile.LoadContent(theContentManager);}
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, PLAYER_ASSETNAME);
        }
        // new hides old update
        new public void Update(GameTime theGameTime)
        {
            UpdateMovement();
            base.Update(calcMovementVect(theGameTime), mRotation);
        }
        // TODO create physics class
        public Vector2 calcMovementVect(GameTime theGameTime)
        {
            Rotation = mRotation;
            return new Vector2((Position.X + (float)(mSpeed * Math.Cos(sprite.Rotation)) * (float)theGameTime.ElapsedGameTime.TotalSeconds),
                (Position.Y + (float)(mSpeed * Math.Sin(sprite.Rotation)) * (float)theGameTime.ElapsedGameTime.TotalSeconds));
            //OLD logic possible to directly change variables too
            //Position.X += (float)(mSpeed * Math.Cos(sprite.Rotation)) * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            //Position.Y += (float)(mSpeed * Math.Sin(sprite.Rotation)) * (float)theGameTime.ElapsedGameTime.TotalSeconds;

        }

        //Assume player is not moving...
        private void UpdateMovement() { mSpeed = 0; }
        public void RotateLeft() { mRotation -= ROTATE_SPEED; }
        public void RotateRight() { mRotation += ROTATE_SPEED; }
        public void MoveForward() { mSpeed = PLAYER_SPEED; }
        public void MoveBackward() { mSpeed = PLAYER_SPEED * -1; }
        public bool Attack { get { return attack; } set { attack = value; }}
        //public float MRotation { get { return mRotation; } }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch);
        }
    }
}





