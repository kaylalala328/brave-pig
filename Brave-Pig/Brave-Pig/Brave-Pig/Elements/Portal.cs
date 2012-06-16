using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Tile_Engine;
using Brave_Pig.BasicObject;
using Brave_Pig.Items;
using Brave_Pig.Character;

namespace Brave_Pig.Elements
{
    public class Portal : GameObject
    {
        public bool IsWarp;

        #region Constructor
        public Portal(ContentManager content, int width, int height, int cellX, int cellY)
        {
            IsWarp = false;

            animations.Add("portal", new AnimationStrip(content.Load<Texture2D>("Portal/portal"),width, "portal"));
            animations["portal"].FrameLength = 0.2f;
            animations["portal"].LoopAnimation = true;

            frameWidth = width;
            frameHeight = height;

            worldLocation = new Vector2(cellX * TileMap.TileWidth - width / 2, (cellY + 1) * TileMap.TileHeight - height);
            collisionRectangle = new Rectangle(0, 0, width, height);

            enabled = true;

            codeBasedBlocks = true;
            PlayAnimation("portal");
        }
        #endregion

        #region Update
        public void Update(GameTime gameTime, Player p)
        {
            if (Game1.currentKeyState.IsKeyUp(Keys.Up) && Game1.previousKeyState.IsKeyDown(Keys.Up) && this.CollisionRectangle.Intersects(p.CollisionRectangle))
            {
                IsWarp = true;
            }

            base.Update(gameTime);
        }
        #endregion
    }
}
