using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ProjectAwesome
{
    class SpriteV2
    {
        //The current position of the Sprite
        public Vector2 position = new Vector2(0, 0);
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        //The current rotation of the Sprite
        public float Rotation = 0.0f;

        //The center of the Sprite is here
        public Vector2 center;
        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }
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

        //Draw the sprite to the screen
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
             theSpriteBatch.Draw(mSpriteTexture, position,
            new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
            Color.White, Rotation, Center, Scale, SpriteEffects.None, 0);
        }
        //Draw the sprite to the screen
        public virtual void Draw(SpriteBatch theSpriteBatch, Vector2 newPosition, SpriteEffects spriteEffects)
        {
            theSpriteBatch.Draw(mSpriteTexture, newPosition,
           new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
           Color.White, Rotation, Center, Scale, spriteEffects, 0);
        }
        //Draw the sprite to the screen overload for layer, currently only used by background for back layer
        //Scaled map*10
        public void DrawLayer(SpriteBatch theSpriteBatch, int layer)
        {
            theSpriteBatch.Draw(mSpriteTexture, position,
           new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
           Color.White, Rotation, Center, Scale*1.5f, SpriteEffects.None, layer);
        }
    }
}
