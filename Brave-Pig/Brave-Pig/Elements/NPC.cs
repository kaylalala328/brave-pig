using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Tile_Engine;
using Brave_Pig.BasicObject;
using Brave_Pig.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brave_Pig.Elements
{
    class NPC : GameObject
    {
        int NPCtype;

        #region constructor
        public NPC(ContentManager content, string NPCName, string Contentname, int width, int height, int cellX, int cellY)
        { 
            IsEnemy = true;

            //idle 애니메이션
            animations.Add("idle", new AnimationStrip(content.Load<Texture2D>("NPC/" + Contentname), width, "idle"));
            animations["idle"].FrameLength = 0.5f;
            animations["idle"].LoopAnimation = true;

            frameWidth = width;
            frameHeight = height;

            worldLocation = new Vector2(cellX * TileMap.TileWidth, cellY * TileMap.TileHeight);

            enabled = true;

            codeBasedBlocks = true;
            PlayAnimation("idle");
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        #endregion
    }
}
