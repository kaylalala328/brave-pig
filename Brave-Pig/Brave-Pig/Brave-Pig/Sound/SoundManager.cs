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

namespace Brave_Pig.Sound
{
    class SoundManager
    {
        private Dictionary<string, SoundEffect> Sound;
        private SoundEffect background;
        private SoundEffectInstance backgroundInstance;

        public void Initialize()
        {
            Sound = new Dictionary<string, SoundEffect>();
        }
        public void LoadContent ( ContentManager content )
        {
            try
            {
                background = content.Load<SoundEffect>("Sound/backgroundSound");

                backgroundInstance = background.CreateInstance();
                backgroundInstance.IsLooped = true;

                Sound.Add("background", background);
            }
            catch
            {   }
        }
        public void SoundPlay ( string name )
        {
            try
            {
                Sound[name].Play(1.0f, 0.5f, 1.0f);
            }
            catch
            {
            }
        }
        public void PlayBackground ( )
        {
            backgroundInstance.Play();
        }

        public void PauseBackground ( )
        {
            backgroundInstance.Stop();
        }
    }
}
