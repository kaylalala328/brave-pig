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
        private int HealthPoint;
        private int Damage = 5;
        public bool Dead = false;
        private Vector2 PushedVector = Vector2.Zero;
        bool IsDamaged = false;
        #region Constructor
        public Enemy(ContentManager content, string MonsterName, string Contentname, int width, int height, int cellX, int cellY, int HP)
        {
            IsEnemy = true;
            
            ///idle 애니메이션
            animations.Add("idle",
                new AnimationStrip(
                    content.Load<Texture2D>(
                        "Monsters/"+Contentname+"_idle"),
                    width,
                    "idle"));
            animations["idle"].FrameLength = 0.3f;
            animations["idle"].LoopAnimation = true;

            ///attack 애니메이션
            animations.Add("attack",
                new AnimationStrip(
                    content.Load<Texture2D>(
                        "Monsters/" + Contentname + "_attack"),
                    width,
                    "attack"));
            animations["attack"].FrameLength = 0.1f;
            animations["attack"].LoopAnimation = false;

            ///죽음 애니메이션
            animations.Add("dead",
                new AnimationStrip(
                    content.Load<Texture2D>(
                       "Monsters/" + Contentname + "_dead"),
                    width,
                    "dead"));

            animations["dead"].FrameLength = 0.2f;
            animations["dead"].LoopAnimation = false;
            
            this.HealthPoint = HP;

            frameWidth = width;
            frameHeight = height;
            CollisionRectangle = new Rectangle(3, 3, frameWidth-6, frameHeight-6);

            worldLocation = new Vector2(
                cellX * TileMap.TileWidth,
                cellY * TileMap.TileHeight);

            enabled = true;

            codeBasedBlocks = true;
            PlayAnimation("idle");
        }
        #endregion

        public int healthPoint
        {
            get { return HealthPoint; }
            set { HealthPoint = value; }
        }
        public void Pushed()
        {
            velocity.Y = -300;
        }
        #region Update
        public override void Update(GameTime gameTime)
        {
            
            Vector2 oldLocation = worldLocation;
            /// Monster AI : 좌우로 움직임
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
        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            //HP Draw

            base.Draw(spriteBatch);
        }


        #endregion
        #endregion
    }

}
