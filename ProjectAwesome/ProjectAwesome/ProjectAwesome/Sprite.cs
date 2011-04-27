using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectAwesome
{
    class Sprite
    {
        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);

        //The current rotation of the Sprite
        public float Rotation = 0.0f;

        //The center of the Sprite is here
        public Vector2 Center;

        //The texture object used when drawing the sprite
        private Texture2D mSpriteTexture;

        //The asset name for the Sprite's Texture
        public string AssetName;

        //The Size of the Sprite (with scale applied)
        public Rectangle Size;

        //The amount to increase/decrease the size of the original sprite. When
        //modified throught he property, the Size of the sprite is recalculated
        //with the new scale applied.
        private float mScale = 1.0f;
        public float Scale
        {
            get { return mScale; }
            set
            {
                mScale = value;
                //Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            }
        }

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            Center = new Vector2(mSpriteTexture.Width / 2, mSpriteTexture.Height / 2);
        }

        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime theGameTime, int theSpeed, float theRotation)
        {
            Position.X += (float)(theSpeed * Math.Cos(Rotation))*(float)theGameTime.ElapsedGameTime.TotalSeconds;
            Position.Y += (float)(theSpeed * Math.Sin(Rotation))*(float)theGameTime.ElapsedGameTime.TotalSeconds;
            Rotation = theRotation;
        }

        //Draw the sprite to the screen
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position,
            new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
            Color.White, Rotation, Center, Scale, SpriteEffects.None, 0);
        }
        //checks if our sprite intersects with a sprite
        //added by Dan
        public Boolean intersect(Sprite sprite)
        {
            Boolean intersects = false;

            //get box coords for our called sprite
            Vector3 aMin = new Vector3(sprite.Position.X, sprite.Position.Y, 0.0f);
            Vector3 aMax = new Vector3(sprite.Position.X + sprite.Size.Width,
                sprite.Position.Y + sprite.Size.Height, 0.0f);

            //get box coords for our calling sprite
            Vector3 mMin = new Vector3(Position.X, Position.Y, 0.0f);
            Vector3 mMax = new Vector3(Position.X + Size.Width,
                Position.Y + Size.Height, 0.0f);

            //make boxes out of the coords
            BoundingBox aBox = new BoundingBox(aMin, aMax * new Vector3(0.99f, 0.99f, 0));
            BoundingBox mBox = new BoundingBox(mMin, mMax * new Vector3(0.99f, 0.99f, 0));

            //check if they intersect
            intersects = mBox.Intersects(aBox);

            return intersects;
        }
        public static double ToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }
    }
}