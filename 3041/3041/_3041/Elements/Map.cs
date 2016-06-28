using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3041.Elements
{
    public class Map
    {
        public Texture2D texture;

        public Vector2 position1 , position2;

        public int Speed { get; set; }

        public Map()
        {
            this.position1 = new Vector2(0,0);
            this.position1 = new Vector2(0, -800);
            this.texture = null;
            this.Speed = 5;

        }


        public void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("space1");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position1, Color.White);
            spriteBatch.Draw(texture, position2, Color.White);
            //spriteBatch.Begin(SpriteSortMode.Deferred,
            //                null,
            //                SamplerState.LinearWrap,
            //                null,
            //                null,
            //                null);
            //spriteBatch.Draw(texture, new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height), new Rectangle((int)position1.X, (int)position1.Y, 800, 600), Color.White);

            //spriteBatch.End();

        }

        public void Update(GameTime gameTime)
        {
            position1.Y = position1.Y + Speed;
            position2.Y = position2.Y + Speed;

            if (position1.Y >=800 || position2.Y>=0)
            {
                position1.Y = 0;
                position2.Y = -800;
            }

            
        }
    }
}
