using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3041.Elements
{
    public class Explosion
    {
        public Texture2D texture;
        public Vector2 position , origin;
        public Rectangle rectangle;
        public int currentFrame, spriteWidth, spriteHeight;
        public float timer, interval;
        public bool isVisible;

        public Explosion(Texture2D texture, Vector2 position)
        {
            this.spriteWidth = 128;
            this.spriteHeight = 128;
            this.texture = texture;
            this.position = position;
            this.timer = 0;
            this.interval = 40f;
            this.currentFrame = 1;   
            this.isVisible = true;
        }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            this.timer = this.timer + (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(this.timer > this.interval)
            {
                this.currentFrame++;
                this.timer = 0f;
            }

            if(this.currentFrame ==9)
            {
                this.isVisible = false;
                this.currentFrame = 0;
            }

            this.rectangle = new Rectangle(this.currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            this.origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(this.isVisible)
            {
                spriteBatch.Draw(texture,position,rectangle,Color.White,0f,origin,1.0f,SpriteEffects.None,0);
            }
        }
    }
}
