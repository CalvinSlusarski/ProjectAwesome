using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ProjectAwesome
{
    class GameObject
    {
        // Variables
        private int UniqueID = UID.getID();     // this is primary key of sorts each gameObject should have a unique id.
        public SpriteV2 sprite = new SpriteV2();                 // what do i look like?
        public Collider collider;               // Can i collide and if so what about it
        public bool canDraw = false;                    // draw me on the screen if true, else dont
        //int expireIn;                         // If i am not alive destory me
        public Vector2 currentLocation;         // Where do i currently reside?
        public identity group = identity.UnSet; // Who and what kind of gameObject am I?

        // Constructor
        public GameObject() { }
        #region LoadContent, UnloadContent, Update, and Draw
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            sprite.LoadContent(theContentManager, theAssetName);
            //CanDraw = true;
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent()
        {

        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
        }
        //Update overload the Sprite's position, and rotation
        public void Update(Vector2 position, float rotation)
        {
            sprite.position = position;
            sprite.Rotation = rotation;
        }
        // TEMP TILL WE have phy system
        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime theGameTime, int theSpeed, float theRotation)
        {
            sprite.position.X += (float)(theSpeed * Math.Cos(Rotation)) * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            sprite.position.Y += (float)(theSpeed * Math.Sin(Rotation)) * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            Rotation = theRotation;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            if (canDraw == true)
            {
                // Awesome drawing action takes place here
                sprite.Draw(theSpriteBatch);
            }
        }
        #endregion
        #region properties
        public enum identity
        {
            Player,
            Projectile,
            Enemy,
            UnSet
        }
        public identity Group
        {
            get { return group; }
            set { group = value; }
        }
        public SpriteV2 Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        public Collider Collider
        {
            get { return collider; }
            set { collider = value; }
        }
        public bool CanDraw
        {
            get { return canDraw; }
            set { canDraw = value; }
        }
        public Vector2 Position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
        }
        public float Rotation
        {
            get { return sprite.Rotation; }
            set { sprite.Rotation = value; }
        }
        public Vector2 CurrentLocation
        {
            get { return currentLocation; }
            set
            {
                currentLocation = value;
            }
        }
        #endregion
    }
}
