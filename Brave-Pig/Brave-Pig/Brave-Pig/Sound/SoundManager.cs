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
    public static class SoundManager
    {
        private static Dictionary<string, SoundEffect> Sound;
        private static SoundEffect background;
        private static SoundEffectInstance backgroundInstance;

        public static void Initialize()
        {
            Sound = new Dictionary<string, SoundEffect>();
        }
        public static void LoadContent ( ContentManager content )
        {
            try
            {
                background = content.Load<SoundEffect>("Sound/일반전투");

                backgroundInstance = background.CreateInstance();
                backgroundInstance.IsLooped = true;

                Sound.Add("background", background);
            }
            catch
            {   }
        }
        public static void SoundPlay ( string name )
        {
            try
            {
                Sound[name].Play(1.0f, 0.5f, 1.0f);
            }
            catch
            {
            }
        }
        public static void PlayBackground ( )
        {
            backgroundInstance.Play();
        }

        public static void PauseBackground ( )
        {
            backgroundInstance.Stop();
        }
    }
}
