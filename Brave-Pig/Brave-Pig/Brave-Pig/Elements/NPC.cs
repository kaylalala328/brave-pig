using Microsoft.Xna.Framework;
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
        private string[] NPCname;
        private string[] NPCdialog;
        private Texture2D DialogWindow;
        public bool IsDrawWindow;
        private SpriteFont DialogFont;

        #region constructor
        public NPC(ContentManager content, int NPCtype, string Contentname, int width, int height, int cellX, int cellY)
        {
            NPCname = new string[3];
            NPCdialog = new string[3];
            DialogWindow = content.Load<Texture2D>("NPC/dialog");
            IsDrawWindow = false;
            DialogFont = content.Load<SpriteFont>("Font/NPCfont");

            NPCname[0] = "JK";  //힐
            NPCname[1] = "도행";  //무기
            NPCname[2] = "한경";  //방어구

            NPCdialog[0] = "hello...!";
            NPCdialog[1] = "우리 가게 좋은 물건 많다네^0^";
            NPCdialog[2] = "사장님이 미쳤어요~_~";

            this.NPCtype = NPCtype;
            //idle 애니메이션
            animations.Add("idle", new AnimationStrip(content.Load<Texture2D>("NPC/" + Contentname), width, "idle"));
            animations["idle"].FrameLength = 0.5f;
            animations["idle"].LoopAnimation = true;

            frameWidth = width;
            frameHeight = height;

            worldLocation = new Vector2(cellX * TileMap.TileWidth, cellY * TileMap.TileHeight);
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

            }

            if (Game1.currentKeyState.IsKeyDown(Keys.Enter) && IsDrawWindow == true)
            {
                IsDrawWindow = false;
            }

            base.Update(gameTime);
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsDrawWindow == true)
            {
                spriteBatch.Draw(DialogWindow, new Rectangle(640 - 150, 360 - 180, 300, 150), Color.White);
                spriteBatch.DrawString(DialogFont, NPCdialog[NPCtype], new Vector2(640 - 150 + 20, 360 - 180 + 50), Color.Black);
                spriteBatch.DrawString(DialogFont, "[" + NPCname[NPCtype] + "]", new Vector2(640 - 150 + 20, 360 - 180 + 10), Color.Blue);
            }
 	        base.Draw(spriteBatch);
        }
        #endregion

    }
}
