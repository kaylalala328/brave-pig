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
    class Boss : GameObject
    {
        private Vector2 fallSpeed;
        private int HealthPoint { get; set; }
        bool IsBoss;
        public bool Dead;
        private bool facingLeft;
        private float walkSpeed;
        private Vector2 TargetVector;
        #region Constructor
        public Boss(ContentManager content, string MonsterNmae, string Contentname, int width, int height, int cellX, int cellY, int HP)
        {
            Dead = false;
            facingLeft = true;
            walkSpeed = 60.0f;
            fallSpeed = new Vector2(0, 20);
            IsBoss = true;

            //idle 애니메이션
            animations.Add("idle", new AnimationStrip(content.Load<Texture2D>(
                "Monsters/" + Contentname + "_idle"), width, "idle"));
            animations["idle"].FrameLength = 0.3f;
            animations["idle"].LoopAnimation = true;

            //attack 애니메이션
            animations.Add("attack", new AnimationStrip(content.Load<Texture2D>(
                "Monsters/" + Contentname + "_attack"), width, "attack"));
            animations["attack"].FrameLength = 0.3f;
            animations["attack"].LoopAnimation = true;

            //dead 애니메이션
            animations.Add("dead", new AnimationStrip(content.Load<Texture2D>(
                "Monsters/" + Contentname + "_dead"), width, "dead"));
            animations["dead"].FrameLength = 0.3f;
            animations["dead"].LoopAnimation = true;

            HealthPoint = HP;

            frameWidth = width;
            frameHeight = height;
            CollisionRectangle = new Rectangle(3, 3, frameWidth - 6, frameHeight - 6);

            worldLocation = new Vector2(
                cellX * TileMap.TileWidth,
                cellY * TileMap.TileHeight);

            enabled = true;

            codeBasedBlocks = true;
            PlayAnimation("idle");
        }
        #endregion

        #region Update
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
        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        #endregion
    }
}
