using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _3041.Sounds
{
    public class SoundManager
    {
        public SoundEffect playerBulletSound;
        public SoundEffect playerCollision;
        public SoundEffect enemyExplosionSound;
        public SoundEffect asteroidExplosionSound;
        public SoundEffect gameOver;
        public SoundEffect speed;
        public Song mapSong;
        public Song menuSong;
        public Song bossSong;

        public SoundManager()
        {

        }

        public void LodadContent(ContentManager content)
        {
            this.mapSong = content.Load<Song>("Afterburner");
            this.bossSong = content.Load<Song>("BossSong");
            this.playerCollision = content.Load<SoundEffect>("hit");
            this.asteroidExplosionSound = content.Load<SoundEffect>("explosion1");
            this.enemyExplosionSound = content.Load<SoundEffect>("explosion2");
            this.speed = content.Load<SoundEffect>("start");
            this.playerBulletSound = content.Load<SoundEffect>("Laser_Shoot_Player");
            

        }
    }
}
