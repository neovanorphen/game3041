using _3041.Enums;
using _3041.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3041.Elements
{
  
        public class Player
        {
            public Texture2D texture , bulletTexture;

            public Vector2 position;
            public int Speed { get; set; }

            public Rectangle Area;

            public float bulletDelay;

            public List<Bullet> bulletList;

            SoundManager soundManager = new SoundManager();

            //Health
            public int health;
            public Rectangle healthRectangle , healthBarRectangle;
            public Texture2D healthTextureFull, healthTextureLow,healthTextureMedium, healthBar;
            public Vector2 healthPosition;

            public Player()
            {
                texture = null;
                bulletList = new List<Bullet>();
                this.Direction = Enums.Direction.North;
                this.bulletDelay = 1;
                this.position = new Vector2(300,800-50);
                Speed = 5;

                this.health = 200;
                this.healthPosition = new Vector2(50,750);
            }



            public Direction Direction { get; set; }

            public void LoadContent(ContentManager content)
            {
                this.texture = content.Load<Texture2D>("spaceship");
                this.bulletTexture = content.Load<Texture2D>("bullet1");

                this.healthBarRectangle = new Rectangle((int)healthPosition.X-10, (int)healthPosition.Y-10, 220, 40);
                this.healthTextureLow = content.Load<Texture2D>("healthred");
                this.healthTextureFull = content.Load<Texture2D>("healthgreen");
                this.healthTextureMedium = content.Load<Texture2D>("healthyellow");
                this.healthBar = content.Load<Texture2D>("healthbar");
                this.soundManager.LodadContent(content);

            }

            public void Draw(SpriteBatch spriteBatch)
            {
                
                spriteBatch.Draw(texture,position,Color.White);

                spriteBatch.Draw(healthBar, healthBarRectangle, Color.White * 0.5f);
                if(this.health>=150)
                {
                    spriteBatch.Draw(healthTextureFull, healthRectangle, Color.White * 0.5f);
                }
                else if(health<150 && health >=75)
                {
                    spriteBatch.Draw(healthTextureMedium, healthRectangle, Color.White * 0.5f);
                }
                else
                {
                    spriteBatch.Draw(healthTextureLow, healthRectangle, Color.White * 0.5f);
                }
                


                //Draw all bullets
                foreach (Bullet bullet in bulletList)
                {
                    bullet.Draw(spriteBatch);
                    
                }
                
            }

            public void Update(GameTime gameTime)
            {


                KeyboardState keyState = Keyboard.GetState();

                //set Area
                this.Area = new Rectangle((int)position.X,(int)position.Y,texture.Width,texture.Height);
                //set rectable for lives bar
                this.healthRectangle = new Rectangle((int)healthPosition.X, (int)healthPosition.Y,health,20);


                // Player Movement
                if (keyState.IsKeyDown(Keys.Right))
                    position.X = position.X + Speed;
                if (keyState.IsKeyDown(Keys.Left))
                    position.X = position.X - Speed;
                if (keyState.IsKeyDown(Keys.Up))
                    position.Y = position.Y - Speed;
                if (keyState.IsKeyDown(Keys.Down))
                    position.Y = position.Y + Speed;

              
                if (keyState.IsKeyDown(Keys.Space))
                {
                    this.Shoot();
                }

                this.UpdateBullets();


                //keep player in the screen 
                if (this.position.X <= 0)
                    this.position.X = 0;

                if (this.position.X >= 600 - texture.Width)
                    this.position.X = 600 - texture.Width;

                if (this.position.Y <= 0)
                    this.position.Y = 0;

                if (this.position.Y >= 800 - texture.Height)
                    this.position.Y = 800 - texture.Height;

            }


            //Shoot Method - used to set starting position of our bullet
            public void Shoot()
            {
                // shot only if bullet delay resets

                
                if (bulletDelay >= 0)
                {
                    bulletDelay--;
                }

                // if bulletdelay is 0 create a new bullet at player position, make it visible and add that bullet to bulletlist
                if (bulletDelay <= 0)
                {
                    soundManager.playerBulletSound.Play();
                    Bullet bullet = new Bullet( bulletTexture);
                    

                    //bullet.position = new Vector2(position.X +32 -bulletTexture.Width/2, position.Y+30);
                    bullet.position.X = (int)position.X + texture.Width/2 -bulletTexture.Width/2;
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
                    bulletDelay = 20;
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
                    bullet.position.Y = bullet.position.Y - bullet.Speed;

                    // if bullet hit the top of the screen destroy it
                    if (bullet.position.Y <= 0)
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
                
            

    }
}
