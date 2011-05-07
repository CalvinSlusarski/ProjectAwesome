using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
/// <summary>
/// This Class will Manage All inGame Object's: Creation, Deletion, Movement, and Collisions
/// First Object will always be player subject to change
/// </summary>

namespace ProjectAwesome
{
    class GameObjectManage
    {
        // USER INTERFACE CONTROLLER CLASS DEALS DIRECTLY WITH THE PLAY CLASS
        //Controls controls = new Controls();// now static
        // All ingame Objects are stored here
        //List<GameObject> gameObjectList = new List<GameObject>();
        public Player player;
        List<Enemy> enemyObjectList = new List<Enemy>();
        List<Projectile> projectileObjectList = new List<Projectile>();
        //Ocean currentBackground = new Ocean();
        GUI GUI = new GUI();
        ContentManager contentManager;
        // Create GameObject
        // i want create objects using methods from this class!
        public GameObjectManage()
        {
            // create player!
            createPlayer();

        }
        public void LoadContent(ContentManager theContentManager)
        {

            player.LoadContent(theContentManager);
            foreach (Enemy enemy in enemyObjectList) { enemy.LoadContent(theContentManager); }
            foreach (Projectile projectile in projectileObjectList) { projectile.LoadContent(theContentManager); }
            // BACKGROUND DURKA DO and sweetness!
            //currentBackground.LoadContent(theContentManager);
            GUI.LoadContent(theContentManager);
            contentManager = theContentManager;
        }
        public void Update(GameTime theGameTime)
        {
            player.Update(theGameTime);
            foreach (Enemy enemy in enemyObjectList) { enemy.Update(theGameTime); }
            foreach (Projectile projectile in projectileObjectList) { projectile.Update(theGameTime); }//<-- fix in projectile class
            testPlayerProjectile();
            GUI.Update(theGameTime);
        }
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            //currentBackground.Draw(theSpriteBatch);
            player.Draw(theSpriteBatch);
            foreach (Enemy enemy in enemyObjectList) { enemy.Draw(theSpriteBatch); }
            foreach (Projectile projectile in projectileObjectList) { projectile.Draw(theSpriteBatch); }   
        }
        public virtual void DrawGUI(SpriteBatch theSpriteBatch)
        {
            GUI.Draw(theSpriteBatch);
        }
        #region create Methods...
        public void createPlayer()
        {
            player = new Player();
            //gameObjectList.Add(player);
        }
        public void createEnemy()
        {
            //we want param's, like location
            enemyObjectList.Add(new Enemy());
        }
        public void createPlayerProjectile()
        {
            //projectileObjectList.Clear();
            Projectile tempProjectile = new Projectile(player.Position, player.Rotation);
            tempProjectile.LoadContent(contentManager);
            projectileObjectList.Add(tempProjectile);
        }
        public void createGUI()
        {
        }
        #endregion

        public void testPlayerProjectile()
        {
            if (player.Attack == true)
            {
                createPlayerProjectile();
            }
        }
        // temp dunno why i still have these
        public void create() { }
        public void delete() { }
    }
}
