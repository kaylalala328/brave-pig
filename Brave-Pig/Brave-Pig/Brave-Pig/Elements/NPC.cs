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
        private Texture2D[] DialogWindow;
        public bool IsDrawWindow;
        private SpriteFont DialogFont;
        private Vector2 fallSpeed;

        #region constructor
        public NPC(ContentManager content, int NPCtype, string Contentname, int width, int height, int cellX, int cellY)
        {
            DialogWindow = new Texture2D[3];
            IsDrawWindow = false;
            DialogFont = content.Load<SpriteFont>("Font/NPCfont");
            fallSpeed = new Vector2(0, 20);

            this.NPCtype = NPCtype;

            DialogWindow[0] = content.Load<Texture2D>("NPC/JK");    //무기
            DialogWindow[1] = content.Load<Texture2D>("NPC/DH");    //힐
            DialogWindow[2] = content.Load<Texture2D>("NPC/HK");    //방어구

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
