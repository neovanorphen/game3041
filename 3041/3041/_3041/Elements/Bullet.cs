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
    public class Bullet
    {
        public Texture2D texture;

        public Rectangle Area;

        public Vector2 origin;

        public Vector2 position;
        public int Speed { get; set; }

        public bool isVisible;
        public Direction Direction { get; set; }

        public Bullet(Texture2D texture)
        {
            this.Direction = Enums.Direction.North;
            this.texture = texture;
            this.Speed = 10;
            this.isVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            //spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture, position, null, Color.White, (float)Math.PI, new Vector2(texture.Width, texture.Height), 1, SpriteEffects.None, 0);

        }

    }
}
