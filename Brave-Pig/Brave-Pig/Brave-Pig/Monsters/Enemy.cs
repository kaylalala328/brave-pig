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
using Brave_Pig.BasicObject;
using Tile_Engine;

namespace Brave_Pig.Monsters
{
    class Enemy : GameObject
    {
        private Vector2 fallSpeed = new Vector2(0, 20);
        private float walkSpeed = 60.0f;
        private bool facingLeft = true;
        private float HealthPoint;
        private int Damage;
        public bool Dead = false;
        
        #region Constructor
        public Enemy(ContentManager content, string MonsterName, string Contentname, int cellX, int cellY, int HP)
        {
            IsEnemy = true;
            
            ///idle 애니메이션
            animations.Add("idle",
                new AnimationStrip(
                    content.Load<Texture2D>(
                        "Monsters/"+Contentname+"_idle"),
                    48,
                    "idle"));
           
            animations["idle"].LoopAnimation = true;

            ///attack 애니메이션
            animations.Add("attack",
                new AnimationStrip(
                    content.Load<Texture2D>(
                        "Monsters/" + Contentname + "_attack"),
                    48,
                    "attack"));
            animations["attack"].FrameLength = 0.1f;
            animations["attack"].LoopAnimation = true;

            ///죽음 애니메이션
            animations.Add("dead",
                new AnimationStrip(
                    content.Load<Texture2D>(
                       "Monsters/" + Contentname + "_dead"),
                    48,
                    "dead"));
            animations["dead"].LoopAnimation = false;
            
            HealthPoint = HP;
            frameWidth = 48;
            frameHeight = 48;
            CollisionRectangle = new Rectangle(8, 2, frameWidth - 10, frameWidth-5);

            worldLocation = new Vector2(
                cellX * TileMap.TileWidth,
                cellY * TileMap.TileHeight);

            enabled = true;

            codeBasedBlocks = true;
            //PlayAnimation("run");
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 oldLocation = worldLocation;

            if (!Dead)
            {
                velocity = new Vector2(0, velocity.Y);

                Vector2 direction = new Vector2(1, 0);
                flipped = true;

                if (facingLeft)
                {
                    direction = new Vector2(-1, 0);
                    flipped = false;
                }

                direction *= walkSpeed;
                velocity += direction;
                velocity += fallSpeed;
            }

            base.Update(gameTime);

            if (!Dead)
            {
                if (oldLocation == worldLocation)
                {
                    facingLeft = !facingLeft;
                }
            }
            else
            {
                if (animations[currentAnimation].FinishedPlaying)
                {
                    enabled = false;
                }
            }
        }
        #endregion
    }

}
