﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Tile_Engine;
using Brave_Pig.BasicObject;
using Brave_Pig.Items;
using Brave_Pig.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brave_Pig.Elements
{
    class NPC : GameObject
    {
        private int NPCtype;
        private Texture2D[] DialogWindow;
        public bool IsDrawWindow;
        private SpriteFont DialogFont;
        private Vector2 fallSpeed;

        #region constructor
        public NPC(ContentManager content, int NPCtype, string Contentname, int width, int height, int cellX, int cellY)
        {
            DialogWindow = new Texture2D[9];
            IsDrawWindow = false;
            DialogFont = content.Load<SpriteFont>("Font/NPCfont");
            fallSpeed = new Vector2(0, 20);

            this.NPCtype = NPCtype;

            DialogWindow[0] = content.Load<Texture2D>("NPC/JK");    //무기
            DialogWindow[1] = content.Load<Texture2D>("NPC/DH");    //힐
            DialogWindow[2] = content.Load<Texture2D>("NPC/HK");    //방어구
            DialogWindow[3] = content.Load<Texture2D>("NPC/JK");    //무기
            DialogWindow[4] = content.Load<Texture2D>("NPC/DH");    //힐
            DialogWindow[5] = content.Load<Texture2D>("NPC/HK");    //방어구
            DialogWindow[6] = content.Load<Texture2D>("NPC/JK");    //무기
            DialogWindow[7] = content.Load<Texture2D>("NPC/DH");    //힐
            DialogWindow[8] = content.Load<Texture2D>("NPC/HK");    //방어구

            //idle 애니메이션
            animations.Add("idle", new AnimationStrip(content.Load<Texture2D>("NPC/" + Contentname), width, "idle"));
            animations["idle"].FrameLength = 0.5f;
            animations["idle"].LoopAnimation = true;

            frameWidth = width;
            frameHeight = height;

            worldLocation = new Vector2((cellX * TileMap.TileWidth) - width / 2, (cellY * TileMap.TileHeight) - height);
            collisionRectangle = new Rectangle(0, 0, width, height);
            enabled = true;

            codeBasedBlocks = true;
            PlayAnimation("idle");
        }
        #endregion

        #region Update
        public void Update(GameTime gameTime, Player p)
        {
            if (Game1.currentKeyState.IsKeyDown(Keys.Up) && this.CollisionRectangle.Intersects(p.CollisionRectangle))
            {
                IsDrawWindow = true;

                if (NPCtype == 0)
                {
                    ItemManager.gainSword("Blue");
                }
                if (NPCtype == 1)
                {
                    int healing;
                    healing = p.stat.getMaxHeal();
                    if (ItemManager.getCurrentArmor() == "none")
                    {
                        p.stat.healPoint = 50;
                    }
                    else if (ItemManager.getCurrentArmor() == "Armor")
                    {
                        p.stat.healPoint = 100;
                    }
                    else if (ItemManager.getCurrentArmor() == "Boots")
                    {
                        p.stat.healPoint = 200;
                    }
                    else if (ItemManager.getCurrentArmor() == "Shield")
                    {
                        p.stat.healPoint = 500;
                    }
                }
                if (NPCtype == 2)
                {
                    ItemManager.gainArmor("Armor");
                }
                if (NPCtype == 3)
                {
                    ItemManager.gainSword("Red");
                }
                if (NPCtype == 4)
                {
                    if (ItemManager.getCurrentArmor() == "none")
                    {
                        p.stat.healPoint = 50;
                    }
                    else if (ItemManager.getCurrentArmor() == "Armor")
                    {
                        p.stat.healPoint = 100;
                    }
                    else if (ItemManager.getCurrentArmor() == "Boots")
                    {
                        p.stat.healPoint = 200;
                    }
                    else if (ItemManager.getCurrentArmor() == "Shield")
                    {
                        p.stat.healPoint = 500;
                    }
                }
                if (NPCtype == 5)
                {
                    ItemManager.gainArmor("Boots");
                }
                if (NPCtype == 6)
                {
                    ItemManager.gainSword("Yellow");
                }
                if (NPCtype == 7)
                {
                    if (ItemManager.getCurrentArmor() == "none")
                    {
                        p.stat.healPoint = 50;
                    }
                    else if (ItemManager.getCurrentArmor() == "Armor")
                    {
                        p.stat.healPoint = 100;
                    }
                    else if (ItemManager.getCurrentArmor() == "Boots")
                    {
                        p.stat.healPoint = 200;
                    }
                    else if (ItemManager.getCurrentArmor() == "Shield")
                    {
                        p.stat.healPoint = 500;
                    }
                }
                if (NPCtype == 8)
                {
                    ItemManager.gainArmor("Shield");
                }
            }

            if (Game1.currentKeyState.IsKeyDown(Keys.Enter) && IsDrawWindow == true)
            {
                IsDrawWindow = false;
            }

            velocity = new Vector2(0, velocity.Y);

            velocity += fallSpeed;

            base.Update(gameTime);
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsDrawWindow == true)
            {
                spriteBatch.Draw(DialogWindow[NPCtype], new Rectangle(640 - 450, 50, 900, 224), Color.White);
            }
 	        base.Draw(spriteBatch);
        }
        #endregion

    }
}
