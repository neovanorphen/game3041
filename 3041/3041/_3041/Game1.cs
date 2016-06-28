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
using _3041.Elements;
using _3041.Enums;
using _3041.Sounds;

namespace _3041
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Random random = new Random();

        Player player = new Player();
        Map map = new Map();
        HUD hud = new HUD();
        


        private List<EnviromentElement> enviromentElements = new List<EnviromentElement>();
        private List<Enemy> enemyList = new List<Enemy>();
        private List<Explosion> explosionList = new List<Explosion>();

        SoundManager soundManager = new SoundManager();
  

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 800;
            this.Window.Title = "3041";
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            soundManager.LodadContent(Content);
            hud.LoadContent(Content);
            player.LoadContent(Content);
            map.LoadContent(Content);
            MediaPlayer.Play(soundManager.mapSong);

        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //update enemys
            foreach (Enemy enemy in enemyList)
            {
                
                if (enemy.Area.Intersects(player.Area)) // collision  enemy - player
                {
                    soundManager.playerCollision.Play();
                    player.health -= 10;
                    enemy.isVisible = false;
                }
                //collision with enemy bulets
                for (int i = 0; i < enemy.bulletList.Count; i++)
                {
                    if (player.Area.Intersects(enemy.bulletList[i].Area))
                    {
                        soundManager.playerCollision.Play();
                        player.health = player.health - enemy.enemyBulletDamage;
                        enemy.bulletList[i].isVisible = false;
                    }
                }

                //collision player bullet - enemy ship
                for (int i = 0; i < player.bulletList.Count; i++)
                {
                    if (player.bulletList[i].Area.Intersects(enemy.Area))
                    {
                        soundManager.enemyExplosionSound.Play();
                        this.explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion_sheet2"), new Vector2(enemy.position.X + enemy.texture.Width / 2, enemy.position.Y + enemy.texture.Height / 2)));
                        this.hud.score = this.hud.score + 100;
                        player.bulletList[i].isVisible = false; //destroy the bullet
                        enemy.isVisible = false; // destroy the enemy

                    }
                }

                enemy.Update(gameTime);
            }

            foreach(Explosion explosion in explosionList)
            {
                explosion.Update(gameTime);
            }

            //update enviroment element - asteroids
            foreach (EnviromentElement asteroid in enviromentElements )
            {
                
                if (asteroid.Area.Intersects(player.Area)) // collision  asteroid - player
                {
                    soundManager.playerCollision.Play();
                    player.health -=10;
                    asteroid.isVisible = false;
                }
                for (int i = 0; i < player.bulletList.Count; i++)
                {
                    if (asteroid.Area.Intersects(player.bulletList[i].Area))
                    {
                        soundManager.asteroidExplosionSound.Play();
                        this.explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion_sheet1"), new Vector2(asteroid.position.X + asteroid.texture.Width / 2, asteroid.position.Y + asteroid.texture.Height / 2)));
                        
                        this.hud.score = this.hud.score + 50;
                        asteroid.isVisible = false;
                        player.bulletList.ElementAt(i).isVisible = false;
                    }
                }

                asteroid.Update(gameTime);
            }

            //iterate throguh our bulletList if any plane come in contantcs with bullet - destroy the 

            ManageExplotions();
            LoadElements( Content);
            LoadEnemies(Content);
            //hud.Update(gameTime);
            player.Update(gameTime);
            map.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


            map.Draw(spriteBatch);

            foreach (Explosion explosion in explosionList)
            {
                explosion.Draw(spriteBatch);
            }

            foreach(EnviromentElement element in enviromentElements)
            {
                element.Draw(spriteBatch);
            }

            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);
            hud.Draw(spriteBatch);
  
            spriteBatch.End();
            base.Draw(gameTime);
        }


        //Load elements
        public void LoadElements(ContentManager content)
        {
            // create random variables for  our x , y and type  and generate the position random for a elements
            int randY = random.Next(-600, -50);
            int randX = random.Next(25, 500);
            int randType = random.Next(1, 3);

            //load random planes textures
            Texture2D elementTexture;
            switch (randType)
            {
                case 1:
                    elementTexture = content.Load<Texture2D>("asteroid1");
                    break;
                case 2:
                    elementTexture = content.Load<Texture2D>("asteroid2");
                    break;
                case 3:
                    elementTexture = content.Load<Texture2D>("asteroid3");
                    break;

                default:
                    elementTexture = content.Load<Texture2D>("asteroid3");
                    break;
            }


            if (enviromentElements.Count < 2)
            {
                enviromentElements.Add(new _3041.Elements.EnviromentElement(elementTexture,new Vector2(randX,randY)));
            }


            //remove al planes invisibles
            for (int i = 0; i < enviromentElements.Count; i++)
            {
                if (!enviromentElements[i].isVisible)
                {
                    enviromentElements.RemoveAt(i);
                    i--;
                }
            }
        }


        //Load Enemies
        public void LoadEnemies(ContentManager content)
        {
            // create random variables for  our x , y and type  and generate the position random for a elements
            int randY = random.Next(-600, -50);
            int randX = random.Next(25, 500);
            int randType = random.Next(1, 4);

            //load random planes textures
            Texture2D enemyTexture, bulletTexture;
            switch (randType)
            {
                case 1:
                    enemyTexture = content.Load<Texture2D>("ship1");
                    bulletTexture = content.Load<Texture2D>("enemyB2");
                    break;
                case 2:
                    enemyTexture = content.Load<Texture2D>("ship2");
                    bulletTexture = content.Load<Texture2D>("enemyB3");
                    break;
                case 3:
                    enemyTexture = content.Load<Texture2D>("ship3");
                    bulletTexture = content.Load<Texture2D>("enemyB4");
                    break;

                default:
                    enemyTexture = content.Load<Texture2D>("ship4");
                    bulletTexture = content.Load<Texture2D>("enemyB5");
                    break;
            }


            if (enemyList.Count < 3)
            {
                enemyList.Add(new Enemy(enemyTexture, new Vector2(randX, randY),bulletTexture));
            }


            //remove all enemies invisibles
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ManageExplotions()
        {
            for (int i = 0; i < explosionList.Count; i++)
            {
                if (!explosionList[i].isVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }
            }
        }

    }
}
