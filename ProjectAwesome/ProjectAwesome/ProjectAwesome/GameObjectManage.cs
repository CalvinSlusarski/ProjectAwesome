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
        Background currentBackground = new Background();
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
            // BACKGROUND DURKA DO
            currentBackground.LoadContent(theContentManager);
            contentManager = theContentManager;
        }
        public void Update(GameTime theGameTime)
        {
            testPlayerProjectile();
            player.Update(theGameTime);
            foreach (Enemy enemy in enemyObjectList) { enemy.Update(theGameTime); }
            foreach (Projectile projectile in projectileObjectList) { projectile.Update(theGameTime); }//<-- fix in projectile class

        }
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            currentBackground.Draw(theSpriteBatch);
            player.Draw(theSpriteBatch);
            foreach (Enemy enemy in enemyObjectList) { enemy.Draw(theSpriteBatch); }
            foreach (Projectile projectile in projectileObjectList) { projectile.Draw(theSpriteBatch); }
        }
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
            Projectile tempProjectile = new Projectile();
            tempProjectile.Fire(/*player.Position*/player.sprite.center, 400, player.Rotation);// adjusted fire Speed
            tempProjectile.LoadContent(contentManager);
            projectileObjectList.Add(tempProjectile);
        }
        public void testPlayerProjectile()
        {
            if (player.Attack == true)
            {
                createPlayerProjectile();
            }
        }
        // SHOULD NEVER BE USED
        public void create() { }
        public void delete() { }
    }
}
