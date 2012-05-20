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

namespace Brave_Pig.Character
{
    public class Player : GameObject
    {
        private Vector2 fallSpeed = new Vector2(0,20);
        
        public Player(ContentManager content)
        {
            animations.Add("normal", new AnimationStrip(content.Load<Texture2D>("Player/normal"), 139, "normal"));
            animations["normal"].LoopAnimation = true;

            animations.Add("attack", new AnimationStrip(content.Load<Texture2D>("Player/attack"), 139, "attack"));
            animations["attack"].LoopAnimation = false;

            frameWidth = 139;
            frameHeight = 77;
            //CollisionRectangle = new Rectangle(9, 1, 30, 46);

            drawDepth = 0f;

            currentAnimation = "normal";
            enabled = true;
            codeBasedBlocks = false;
            
            PlayAnimation("normal");
            worldLocation = Vector2.Zero;   //플레이어 위치
        }

        public override void Update(GameTime gameTime)
        {
            string newAnimation = "normal";
            velocity = new Vector2(0, velocity.Y);
            //xbox 패드용 변수
            //GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Space))
            {
                flipped = false;
                newAnimation = "attack";
                velocity = new Vector2(0, velocity.Y);
            }

            if (newAnimation != currentAnimation)
            {

                if(animations[currentAnimation].LoopAnimation == true)

                    PlayAnimation(newAnimation);
                else if(animations[currentAnimation].FinishedPlaying == true)
                    PlayAnimation(newAnimation);
            }

            velocity += fallSpeed;

            //repositionCamera();
            base.Update(gameTime);
        }

        /*private void repositionCamera()
        {
            int screenLocX = (int)Camera.WorldToScreen(worldLocation).X;

            if (screenLocX > 500)
            {
                Camera.Move(new Vector2(screenLocX - 500, 0));
            }

            if (screenLocX < 200)
            {
                Camera.Move(new Vector2(screenLocX - 200, 0));
            }
        }*/
    }
}
