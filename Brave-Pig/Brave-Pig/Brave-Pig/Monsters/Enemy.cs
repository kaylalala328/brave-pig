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
        protected Vector2 fallSpeed = new Vector2(0, 20);
        protected float walkSpeed = 60.0f;
        protected bool facingLeft;
        protected int HealthPoint;
        protected int MaxHP;
        protected int Damage;
        public bool Dead = false;
        protected Vector2 PushedVector = Vector2.Zero;
        protected bool IsDamaged = false;
        protected Texture2D backhp;
        protected Texture2D forehp;
        protected static Random rand = new Random();

        #region Constructor

        public Enemy()
        {
            IsDamaged = false;
        }
        public Enemy(ContentManager content, string MonsterName, string Contentname, int width, int height, int cellX, int cellY, int HP, int damage)
        {
            IsEnemy = true;
            animations2.Add("default", new AnimationStrip(content.Load<Texture2D>("Player/default"), width, "damage"));
            currentAnimation2 = "default";
            
            ///idle 애니메이션
            animations.Add("idle",
                new AnimationStrip(
                    content.Load<Texture2D>(
                        "Monsters/"+Contentname+"_idle"),
                    width,
                    "idle"));
            animations["idle"].FrameLength = 0.3f;
            animations["idle"].LoopAnimation = true;

            ///죽음 애니메이션
            animations.Add("dead",
                new AnimationStrip(
                    content.Load<Texture2D>(
                       "Monsters/" + Contentname + "_dead"),
                    width,
                    "dead"));

            animations["dead"].FrameLength = 0.2f;
            animations["dead"].LoopAnimation = false;

            int r = rand.Next(-10, 10);
            if (r <= 0)
            {
                facingLeft = true;
            }
            else
            {
                facingLeft = false;
            }
            ///피격 애니메이션
            animations.Add("damage",
                new AnimationStrip(
                    content.Load<Texture2D>(
                       "Monsters/" + Contentname + "_damage"),
                    width,
                    "damage"));

            animations["damage"].FrameLength = 0.3f;
            animations["damage"].LoopAnimation = false;

            this.HealthPoint = HP;
            this.MaxHP = HP;
            this.Damage = damage;

            frameWidth = width;
            frameHeight = height;
            CollisionRectangle = new Rectangle(3, 3, frameWidth-6, frameHeight-6);

            worldLocation = new Vector2(
                cellX * TileMap.TileWidth,
                cellY * TileMap.TileHeight);

            enabled = true;
            backhp = content.Load<Texture2D>("Monsters/backhp");
            forehp = content.Load<Texture2D>("Monsters/monhp");

            codeBasedBlocks = true;
            PlayAnimation("idle");
        }
        #endregion

        public int healthPoint
        {
            get { return HealthPoint; }
            set { HealthPoint = value; }
        }
        public int Damege
        {
            get { return Damage; }
            set { Damage = value; }
        }
        public void Pushed()
        {
            velocity.Y = -200;
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
                if (animations["damage"].FinishedPlaying == true)
                {
                    PlayAnimation("idle");
                }
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
            Rectangle back = Camera.WorldToScreen(WorldRectangle);
            Rectangle fore = Camera.WorldToScreen(WorldRectangle);
            back.Y -= 10;
            back.X += frameWidth/2 - 16;
            back.Height = 7;
            back.Width = 32;

            fore.Y -= 9;
            fore.X += frameWidth/2 - 15;
            fore.Height = 5;
            fore.Width = (int)(((float)HealthPoint / (float)MaxHP) * 30);
            if (!Dead)
            {
                spriteBatch.Draw(backhp, back, Color.White);
                spriteBatch.Draw(forehp, fore, Color.White);
            }
            base.Draw(spriteBatch);
        }


        #endregion
        #endregion
    }

}
