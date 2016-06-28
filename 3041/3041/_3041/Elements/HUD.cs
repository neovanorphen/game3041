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
    public class HUD
    {
        public int screenWidth, screenHeight;
        public int score;
        public SpriteFont scoreFont;

        public Vector2 scorePosition;
        public bool showHUD;

        public HUD()
        {
            this.score = 0;
            this.showHUD = true;
            this.screenHeight = 800;
            this.screenWidth = 600;
            this.scorePosition = new Vector2(screenWidth-200,30);

        }

        public void LoadContent(ContentManager content)
        {
            this.scoreFont = content.Load<SpriteFont>("arcadeclasic");
        }



        public void Update(GameTime gameTime)
        {
            //keystate to show scoreboard

            KeyboardState keyState = Keyboard.GetState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(this.showHUD ==true)
            {
                spriteBatch.DrawString(this.scoreFont, "Score :      " + this.score, scorePosition, Color.White*0.9f);
            }
        }

    }
}
