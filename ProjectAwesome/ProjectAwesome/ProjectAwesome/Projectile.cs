using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectAwesome
{
    class Projectile: GameObject
    {
        //constants for adjusting game values
        const string PROJECTILE_ASSETNAME = "Projectile";
        const int projectileSpeed = 250;
        const int MAX_DISTANCE = 500;
        const int START_POSITION_X = 150;
        const int START_POSITION_Y = 150;
        
        float mRotation = 0.0f;
        public bool Visible = false;
        Vector2 mStartPosition = new Vector2(START_POSITION_X, START_POSITION_Y);
        int mSpeed = 300;
        
        //Constructor
        public Projectile()
        {
            // Musings from GameObject with Love
            base.Group = identity.Projectile;
            base.CanDraw = true;
        }
        //load content
        public void LoadContent(ContentManager theContentManager)
        {
            Visible = true;
            Sprite.position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, PROJECTILE_ASSETNAME);
        }
        //update object
        new public void Update(GameTime theGameTime)
        {
            if (Vector2.Distance(mStartPosition, Position) > MAX_DISTANCE)
            {
                Visible = false;
            }     
            if (Visible == true)
            {
                base.Update(theGameTime, mSpeed, mRotation);
            }
        }
        
        public override void Draw(SpriteBatch theSpriteBatch)
        {
            if (Visible == true)
            {
                base.Draw(theSpriteBatch);
            }
        }
        public void Fire(Vector2 theStartPosition, int theSpeed, float theDirection)
        {
            Position = theStartPosition;
            mStartPosition = theStartPosition;
            mSpeed = theSpeed;
            mRotation = theDirection;
            Visible = false;
        }
    }
}
