using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Brave_Pig.Character
{
    public class skill : Status
    {
        public void useSkill()
        {
            if(UseSword == 0)
            {
                //기본 검에 대한 스킬        
            }
            else if(UseSword == 1)
            {
                //첫번째 검에 대한 스킬
            }
            else if(UseSword == 2)
            {
                //두번째 검에 대한 스킬
            }
            else if(UseSword == 3)
            {
                //세번째 검에 대한 스킬
            }
        }
    }
}
