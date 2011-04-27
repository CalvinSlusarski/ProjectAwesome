using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ProjectAwesome
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //public static Camera camera;
        // Added By Calvin for drawing the boat
        Player mPlayerSprite;
        Camera camera = new Camera();

        //added by Dan to get enemies in there
        List<Enemy> Enemies = new List<Enemy>();

        //added by dan for text on screen
        SpriteFont debugFont;
        Vector2 FontPos;

        //added by dan for player collision math
        int timesShot = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Draw your game here
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //init spritefont for debugging
            debugFont = Content.Load<SpriteFont>("debugFont");
            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                                    graphics.GraphicsDevice.Viewport.Height / 2);


            // init Camera
           // camera = new Camera(graphics.GraphicsDevice.Viewport) { Limits = new Rectangle(0, 0, 3200, 600) };
            foreach (Enemy aEnemy in Enemies)
            {

                aEnemy.LoadContent(this.Content);
            }

            mPlayerSprite = new Player(ref camera);
            mPlayerSprite.LoadContent(this.Content);
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Added by calvin
            mPlayerSprite.Update(gameTime);
            //Added by calvin moves camera to player position
            camera.Position = mPlayerSprite.Position;

            //added by Dan, spawns enemies
            ///*******************************************************************
            // * ----------uncomment to reenable enemy spawns---------------------
            //UpdateEnemies(gameTime, new Random());
            // * */

            //added by Dan, 
            //handles player bullet->enemy
            //and enemy bullet ->player
            //collisions using the intersect method I wrote for sprite
            UpdateCollisions();
            base.Update(gameTime);
        }
        private void UpdateCollisions()
        {
            //for each enemy and for each player's bullet, check collision
            foreach (Enemy aEnemy in Enemies)
            {
                foreach (Projectile p in mPlayerSprite.mBullets)
                {
                    //if they intersect and the bullet is visible
                    if ((p.intersect(aEnemy) == true)&& p.Visible)
                    {
                        //kill the enemy and kill the bullet
                        aEnemy.alive = false;
                        p.Visible = false;
                    }
                }
            }
            //for each enemies bullet check against player for collision
            foreach (Enemy anEnemy in Enemies)
            {
                foreach(Projectile pr in anEnemy.mBullets)
                {
                    if(pr.intersect(mPlayerSprite) && pr.Visible)
                    {
                        //increment the times the player has been shot, kill bullet
                        timesShot ++;
                        pr.Visible = false;
                    }
                }
            }            
        }
        //updates the enemy list
        private void UpdateEnemies(GameTime theGameTime, Random generator)
        {
            foreach (Enemy aEnemy in Enemies)
            {
                aEnemy.Update(theGameTime);
            }
            int tmp = generator.Next();
            if (tmp%1000 <= 2)
            {
                SpawnEnemy();
            }
        }
        //spawns enemies to the list
        private void SpawnEnemy()
        {
            Random gen = new Random();
            //if (mCurrentState == State.Moving)
            //{
                bool aCreateNew = true;

                foreach (Enemy aEnemy in Enemies)
                {
                    if (aEnemy.alive == false)
                    {
                        aCreateNew = false;
                        aEnemy.Spawn(mPlayerSprite.Position,
                            100, (gen.Next() % 360.0f));// adjusted fire Speed
                        break;
                    }
                }

                if (aCreateNew == true)
                {
                    Enemy aEnemy = new Enemy();
                    aEnemy.LoadContent(this.Content);
                    aEnemy.Spawn(mPlayerSprite.Position,
                        100, (gen.Next()%360.0f));
                    Enemies.Add(aEnemy);
                }
            //}
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Added by Calvin to draw boat
            //spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                          BlendState.AlphaBlend,
                          null,
                          null,
                          null,
                          null,
                          camera.Transform(GraphicsDevice));
            mPlayerSprite.Draw(this.spriteBatch);
            spriteBatch.DrawString(debugFont, timesShot.ToString(), FontPos, Color.Black);
            foreach (Enemy e in Enemies)
            {
                if(e.alive)
                    e.Draw(this.spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
