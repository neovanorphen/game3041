using _3041.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3041.Elements
{
    public class EnviromentElement
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float rotationAngle;
        public bool isVisible;

        public int Speed { get; set; }

        public Rectangle Area;

        Random random = new Random();
        public float randX, randY;

        public Direction Direction { get; set; }



        public EnviromentElement( Texture2D texture, Vector2 position)
        {
            this.texture = texture;

            this.Direction = Enums.Direction.South;

            this.position = position;
            Speed = 4;

            this.randX = random.Next(25, 500);
            this.randY = random.Next(-600, -50);
            this.isVisible = true;
        }


        public void LoadContent(ContentManager content)
        {
            //this.texture = content.Load<Texture2D>("spaceship");
        }

        

        public void Update(GameTime gameTime)
        {
            this.Area = new Rectangle((int)position.X, (int)position.Y,50,50);

            //update Movement
            position.Y = position.Y + Speed;

            if(position.Y>= 800)
            {
                position.Y = -50;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (isVisible)
            {
                spriteBatch.Draw(texture, position, null, Color.White, (float)Math.PI, new Vector2(texture.Width, texture.Height), 1, SpriteEffects.None, 0);
                
                //spriteBatch.Draw(texture, position, Color.White);

            }

            
            

        }




    }
}
