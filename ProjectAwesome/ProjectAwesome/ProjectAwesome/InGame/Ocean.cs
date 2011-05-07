using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace ProjectAwesome
{
    class Ocean: GameObject
    {
        // was const
        string ASSETNAME = "Ocean";
        const int START_POSITION_X = 400;
        const int START_POSITION_Y = 400;
        // Effects Make water look like its moving!
        Effect refractionEffect;

        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, ASSETNAME);
            // moving effect
            refractionEffect = theContentManager.Load<Effect>("refraction");
        }
        new public void Draw(SpriteBatch theSpriteBatch)
        {
            //base.sprite.DrawLayer(theSpriteBatch, 1);
            base.sprite.Draw(theSpriteBatch);
        }
        public void assetName(string newName)
        {
             ASSETNAME = newName;
        }
        /// <summary>
        // Effect uses a scrolling displacement texture to offset the position of
        // the main texture.
        /// </summary>
        public void DrawRefract(GameTime gameTime, SpriteBatch theSpriteBatch, GraphicsDevice graphics)
        {
            refractionEffect.Parameters["DisplacementScroll"].SetValue(
                                            MoveInCircle(gameTime, 0.005f));
            // Set the displacement texture.
            graphics.Textures[1] = Sprite.mSpriteTexture;
            // Begin the sprite batch.
            //theSpriteBatch.End();
            theSpriteBatch.Begin(0, null, null, null, null, refractionEffect);
            // Draw the sprite.
            //spriteBatch.Draw(catTexture,
            //                 MoveInCircle(gameTime, catTexture, 1),
            //                 Color.White);
            Draw(theSpriteBatch);
            // End the sprite batch.
            theSpriteBatch.End();
           //theSpriteBatch.Draw(mSpriteTexture, position,
           //new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
           //Color.White, Rotation, Center, Scale*1.5f, SpriteEffects.None, layer);
        }
        /// <summary>
        /// Helper for moving a value around in a circle.
        /// </summary>
        static Vector2 MoveInCircle(GameTime gameTime, float speed)
        {
            double time = gameTime.TotalGameTime.TotalSeconds * speed;

            float x = (float)Math.Cos(time);
            float y = (float)Math.Sin(time);

            return new Vector2(x, y);
        }
    }
}
