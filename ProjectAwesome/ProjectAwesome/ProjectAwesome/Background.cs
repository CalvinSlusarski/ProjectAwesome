using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace ProjectAwesome
{
    class Background: GameObject
    {
        // was const
        string ASSETNAME = "Background";
        const int START_POSITION_X = 0;
        const int START_POSITION_Y = 0;
        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, ASSETNAME);
        }
        new public void Draw(SpriteBatch theSpriteBatch)
        {
            base.sprite.DrawLayer(theSpriteBatch, 1);
        }
        public void assetName(string newName)
        {
             ASSETNAME = newName;
        }
    }
}
