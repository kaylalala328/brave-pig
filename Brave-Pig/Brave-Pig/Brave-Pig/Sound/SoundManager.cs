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
        private static Dictionary<string, SoundEffectInstance> backgroundSound;
        private static SoundEffect sword;
        private static SoundEffect citySound;
        private static SoundEffect battleSound;
        private static SoundEffect bossSound;
        private static SoundEffectInstance cityInstance;
        private static SoundEffectInstance battleInstance;
        private static SoundEffectInstance bossInstance;
        private static string currentSound;

        public static void Initialize()
        {
            Sound = new Dictionary<string, SoundEffect>();
            backgroundSound = new Dictionary<string, SoundEffectInstance>();
        }
        public static void LoadContent ( ContentManager content )
        {
            try
            {
                citySound = content.Load<SoundEffect>("Sound/city");
                battleSound = content.Load<SoundEffect>("Sound/field");
                bossSound = content.Load<SoundEffect>("Sound/mainfinal");
                sword = content.Load<SoundEffect>("Sound/changeSW");

                cityInstance = citySound.CreateInstance();
                cityInstance.IsLooped = true;
                battleInstance = battleSound.CreateInstance();
                battleInstance.IsLooped = true;
                bossInstance = bossSound.CreateInstance();
                bossInstance.IsLooped = true;

                backgroundSound.Add("citySound", cityInstance);
                backgroundSound.Add("battleSound", battleInstance);
                backgroundSound.Add("bossSound", bossInstance);

                Sound.Add("sword", sword);
            }
            catch
            {   }
        }
        public static void UpdateSound ( )
        {
            switch ( LevelManager.CurrentLevel )
            {
                case 0:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 1:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 2:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 3:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 4:
                    SoundManager.PlayBackground("citySound");
                    break;
                case 5:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 6:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 7:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 8:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 9:
                    SoundManager.PlayBackground("citySound");
                    break;
                case 10:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 11:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 12:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 13:
                    SoundManager.PlayBackground("battleSound");
                    break;
                case 14:
                    SoundManager.PlayBackground("citySound");
                    break;
                case 15:
                    SoundManager.PlayBackground("bossSound");
                    break;
            }
        }
        public static void SoundPlay ( string name )
        {
            try
            {
                Sound[name].Play(1.0f, 1.0f, 1.0f);
            }
            catch
            {
            }
        }
        public static void PlayBackground ( string name )
        {
            try
            {
                if(currentSound != name)
                    backgroundSound[currentSound].Stop();
            }
            catch ( Exception e )
            {
            }

            backgroundSound[name].Play();
            currentSound = name;
        }
    }
}
