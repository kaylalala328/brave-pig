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
using Brave_Pig.Character;
using Tile_Engine;

namespace Brave_Pig.Monsters
{
    class Boss : Enemy
    {
        public bool IsBoss;
        double totalSecond;
        double LimitSecond;
        int width2;
        int width;
        private bool Isattack;
        SpriteFont BossName;
        private string Name;
        #region Constructor
        public Boss(ContentManager content, string MonsterName, string Contentname, int width, int height, int cellX, int cellY, int HP, int damage, int width2, bool isboss)
        {
            Name = MonsterName;
            IsBoss = isboss;
            IsEnemy = true;
            animations2.Add("default", new AnimationStrip(content.Load<Texture2D>("Player/default"), width, "damage"));
            currentAnimation2 = "default";

            ///idle 애니메이션
            animations.Add("idle",
                new AnimationStrip(
                    content.Load<Texture2D>(
                        "Monsters/" + Contentname + "_idle"),
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

            if (isboss)
            {
                ///피격 애니메이션
                animations.Add("attack",
                    new AnimationStrip(
                        content.Load<Texture2D>(
                           "Monsters/" + Contentname + "_attack"),
                        width2,
                        "attack"));

                animations["attack"].FrameLength = 0.3f;
                animations["attack"].LoopAnimation = false;

            }

            this.HealthPoint = HP;
            this.MaxHP = HP;
            this.Damage = damage;
            this.width2 = width2;
            this.width = width;
            frameWidth = width;
            frameHeight = height;
            CollisionRectangle = new Rectangle(3, 3, frameWidth - 6, frameHeight - 6);

            worldLocation = new Vector2(
                cellX * TileMap.TileWidth,
                cellY * TileMap.TileHeight);

            enabled = true;
            backhp = content.Load<Texture2D>("Monsters/backhp");
            forehp = content.Load<Texture2D>("Monsters/monhp");
            BossName = content.Load<SpriteFont>("Font/BossName");
            codeBasedBlocks = true;
            PlayAnimation("idle");
            LimitSecond = 0;
            Isattack = false;
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {

            totalSecond = gameTime.TotalGameTime.TotalSeconds;
            int i = rand.Next(-2, 100);
            if (!Dead && totalSecond >= LimitSecond)
            {
                walkSpeed = 60f;
                frameWidth = width;
                Isattack = false;
                
                if (i > 80)
                {
                    LimitSecond = gameTime.TotalGameTime.TotalSeconds + 5;
                    velocity.Y = -700;
                }
                else if (i > 60)
                {
                    
                    LimitSecond = gameTime.TotalGameTime.TotalSeconds + 6;
                        if (IsBoss)
                        {
                            walkSpeed = 150;
                            frameWidth = width2;
                            PlayAnimation("attack");
                            Isattack = true;
                        }
                }
                else if (i < 10)
                {
                    LimitSecond = gameTime.TotalGameTime.TotalSeconds + 3;
                    walkSpeed = 300f;
                }
                else
                {
                    LimitSecond = gameTime.TotalGameTime.TotalSeconds + 2;
                }
            }
            base.Update(gameTime);
            if (IsBoss &&Isattack == true && animations["attack"].FinishedPlaying == true)
            {
                PlayAnimation("idle");
                Isattack = false;
                frameWidth = width;
            }
        }
        #endregion
        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            //HP Draw
            Rectangle back = new Rectangle(200, 30, 802, 82);
            Rectangle fore = new Rectangle(201, 31, (int)(((float)HealthPoint / (float)MaxHP) * 800), 80);

           
            if (!Dead)
            {
                spriteBatch.Draw(backhp, back, Color.White);
                spriteBatch.Draw(forehp, fore, Color.White);

                spriteBatch.DrawString(BossName, Name, new Vector2(500, 60), Color.Black);
            }
            base.Draw(spriteBatch);
        }

        #endregion
    }
}
