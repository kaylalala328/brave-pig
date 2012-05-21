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
        //private float moveScale = 180.0f;
        Random num = new Random();
        private int direct = 0;
        Status stat = new Status();
        
        public Player(ContentManager content)
        {

            animations.Add("normal", new AnimationStrip(content.Load<Texture2D>("Player/normal"), 179, "normal"));
            animations["normal"].LoopAnimation = true;

            animations.Add("attack1", new AnimationStrip(content.Load<Texture2D>("Player/attack1"), 179, "attack1"));
            animations["attack1"].LoopAnimation = false;

            animations.Add("attack2", new AnimationStrip(content.Load<Texture2D>("Player/attack2"), 179, "attack2"));
            animations["attack2"].LoopAnimation = false;

            animations.Add("move", new AnimationStrip(content.Load<Texture2D>("Player/move"), 179, "move"));
            animations["move"].LoopAnimation = true;

            animations.Add("charge", new AnimationStrip(content.Load<Texture2D>("Player/charge"), 179, "charge"));
            animations["charge"].LoopAnimation = false;

            /*animations.Add("jump", new AnimationStrip(content.Load<Texture2D>("Player/jump"), 179, "jump"));
            animations["jump"].LoopAnimation = false;
            animations["jump"].FrameLength = 0.08f;
            animations["jump"].NextAnimation = "normal";*/

            frameWidth = 179;
            frameHeight = 77;
            //CollisionRectangle = new Rectangle(9, 1, 30, 46);

            drawDepth = 0f;

            currentAnimation = "normal";
            enabled = true;
            codeBasedBlocks = false;
            
            PlayAnimation("normal");
            worldLocation = new Vector2(350,300);   //플레이어 위치
        }

        public override void Update(GameTime gameTime)
        {
            string newAnimation = "normal";
            velocity = new Vector2(0, velocity.Y);
            //GamePadState gamePad = GamePad.GetState(PlayerIndex.One); //xbox 패드 입력값
            KeyboardState keyState = Keyboard.GetState(); //키보드 입력값

            if (keyState.IsKeyDown(Keys.Space))
            {
                int attack_num = num.Next(0,10);
                if (direct == 0)
                {
                    flipped = false;
                }
                else
                {
                    flipped = true;
                }
                
                if(attack_num < 5)
                {
                    newAnimation = "attack1";
                    velocity = new Vector2(0, velocity.Y);
                }
                else
                {
                    newAnimation = "attack2";
                    velocity = new Vector2(0, velocity.Y);
                }
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                direct = 0;
                flipped = false;
                newAnimation = "move";
                velocity = new Vector2(0, velocity.Y);
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                direct = 1;
                flipped = true;
                newAnimation = "move";
                velocity = new Vector2(0, velocity.Y);
            }

            if (keyState.IsKeyDown(Keys.Z))
            {
                if (stat.manaPoint < 3)
                {
                    stat.manaPoint = stat.manaPoint + 0.005f;
                }
                else
                {
                    stat.manaPoint = stat.manaPoint + 0.0f;
                }

                if (direct == 0)
                {
                    flipped = false;
                }
                else
                {
                    flipped = true;
                }

                newAnimation = "charge";
                velocity = new Vector2(0, velocity.Y);
            }
            /*if (keyState.IsKeyDown(Keys.Z))
            {
                newAnimation = "jump";
                velocity = new Vector2(0, velocity.Y);
                if (onGround)
                {
                    Jump();
                    newAnimation = "jump";
                }             
            }*/

            if (newAnimation != currentAnimation)
            {
                if (animations[currentAnimation].FinishedPlaying == true || animations[currentAnimation].LoopAnimation == true)
                    PlayAnimation(newAnimation);
            }
            else
            {
                if (animations[currentAnimation].FinishedPlaying == true)
                    PlayAnimation(newAnimation);
            }
            

            velocity += fallSpeed;

            //repositionCamera();
            base.Update(gameTime);
        }

        public void Jump()
        {
            velocity.Y = -500;
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
