using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _3041.Enums;

namespace _3041.Elements
{
    public class Enemy
    {
        public Texture2D texture, bulletTexture;
        public Vector2 position;
        public Rectangle Area;
        public int health;
        public bool isVisible;
        public int Speed { get; set; }
        public Direction Direction { get; set; }
        public int bulletDelay;
        public int difficulty;
        public int enemyBulletDamage;

        public List<Bullet> bulletList;

        public Enemy(Texture2D texture, Vector2 position, Texture2D bulletTexture)
        {
            this.texture = texture;
            this.bulletTexture = bulletTexture;
            this.bulletList = new List<Bullet>();
            this.Direction = Enums.Direction.North;
            this.bulletDelay = 50;
            this.position = position;
            this.Speed = 6;
            this.health = 5;
            this.difficulty = 1;
            this.isVisible = true;
            this.enemyBulletDamage = 10;
                
        }


        public void LoadContent(ContentManager content)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            this.Area = new Rectangle((int)position.X,(int)position.Y,texture.Width,texture.Height);

            this.position.Y = this.position.Y + this.Speed;

            if(position.Y >=800)
            {
                position.Y = -75;
            }

            this.Shoot();
            this.UpdateBullets();

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            //Enemy
            //spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture, position, null, Color.White, (float)Math.PI, new Vector2(texture.Width, texture.Height), 1, SpriteEffects.None, 0);

            //Bullets

            foreach(Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }


        //update bullet method
        public void UpdateBullets()
        {
            // for each bullet in our bulletlist: update the movement and if the bullet hits the top of the screen remove it from te list
            foreach (Bullet bullet in bulletList)
            {
                //bullet Area
                bullet.Area = new Rectangle((int)bullet.position.X, (int)bullet.position.Y, bulletTexture.Width, bulletTexture.Height);

                //set movement for bullet
                bullet.position.Y = bullet.position.Y + bullet.Speed;

                // if bullet hit the top of the screen destroy it
                if (bullet.position.Y >= 800)
                {
                    bullet.isVisible = false;
                }
            }

            // remove the invisible bullets
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }

        }

        //Enemy Shoot Method - used to set starting position of our bullet
        public void Shoot()
        {
 

            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }

            // if bulletdelay is 0 create a new bullet at player position, make  visible and add that bullet to bulletlist
            if (bulletDelay <= 0)
            {
                Bullet bullet = new Bullet(bulletTexture);


                //bullet.position = new Vector2(position.X +32 -bulletTexture.Width/2, position.Y+30);
                bullet.position.X = (int)position.X + texture.Width / 2 - bulletTexture.Width / 2;
                bullet.position.Y = (int)position.Y + 30;

                bullet.isVisible = true;

                if (bulletList.Count() < 20)
                {
                    bulletList.Add(bullet);

                }

            }

            // reset bulletDelay
            if (bulletDelay == 0)
            {
                bulletDelay = 50;
            }

        }
    }
}
