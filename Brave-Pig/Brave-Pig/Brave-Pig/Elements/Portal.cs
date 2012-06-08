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

namespace Brave_Pig.Elements
{
    public class Portal : GameObject
    {
        #region Constructor
        public Portal(ContentManager content, int width, int height, int cellX, int cellY)
        {
            IsEnemy = true;

            animations.Add("portal", new AnimationStrip(content.Load<Texture2D>("Portal/portal"),width, "portal"));
            animations["portal"].FrameLength = 0.2f;
            animations["portal"].LoopAnimation = true;

            frameWidth = width;
            frameHeight = height;

            worldLocation = new Vector2(cellX * TileMap.TileWidth, cellY * TileMap.TileHeight);

            enabled = true;

            codeBasedBlocks = true;
            PlayAnimation("portal");
        }
        #endregion
    }
}
