using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectAwesome
{

    /// <summary>
    /// Lots of sweet gui action taking place in this class
    /// </summary>
    class GUI: GameObject
    {
        const string ASSETUPPERFRAME = "gui";
        const int START_POSITION_X = 408;
        const int START_POSITION_Y = 300;
        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, ASSETUPPERFRAME);
            sprite.Scale = 1.6f;
        }
        new public void Draw(SpriteBatch theSpriteBatch)
        {
            // top left corner of gui
            base.sprite.Draw(theSpriteBatch);

            // top right
            //base.sprite.Draw(theSpriteBatch,new Vector2(700,100), SpriteEffects.FlipHorizontally);
        }
    }
}
