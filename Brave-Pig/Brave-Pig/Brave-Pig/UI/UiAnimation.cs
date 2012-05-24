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
using Brave_Pig.Character;
using Brave_Pig.BasicObject;
using Brave_Pig.Monsters;

namespace Brave_Pig.UI
{
    class UiAnimation
    {
        protected Dictionary<string, AnimationStrip> animations =
            new Dictionary<string, AnimationStrip>();
        protected string currentAnimation;

        public void AddAnimation(ContentManager content)
        {
            animations.Add("normal", new AnimationStrip(content.Load<Texture2D>("UI/MP"), 44, "normal"));
            animations["normal"].LoopAnimation = true;
            animations["normal"].FrameLength = 0.08f;
            animations.Add("event", new AnimationStrip(content.Load<Texture2D>("UI/MPanimation3"), 44, "event"));
            animations["event"].LoopAnimation = false;
            animations["event"].FrameLength = 0.1f;
        }
        public void updateAnimation ( GameTime gameTime )
        {
            //Key를 갖고 잇어야함. 즉 애니메이션이 존재하면,
            if ( animations.ContainsKey(currentAnimation) )
            {
                //애니메이션이 끝났다면
                if ( animations[currentAnimation].FinishedPlaying )
                {
                    //애니메이션은 다음 애니메이션을 가르키게 한다.
                    PlayAnimation(animations[currentAnimation].NextAnimation);
                }
                else
                {
                    //애니메이션이 안끝낫다면
                    animations[currentAnimation].Update(gameTime);
                }
            }
        }
        public void PlayAnimation ( string name )
        {
            if ( !( name == null ) && animations.ContainsKey(name) )
            {
                currentAnimation = name;
                animations[name].Play();
            }
        }
    }
}
