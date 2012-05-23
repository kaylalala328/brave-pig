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
        private bool finish = false;
        
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

            animations.Add("skill1", new AnimationStrip(content.Load<Texture2D>("Player/skill1"), 179, "skill1"));
            animations["skill1"].LoopAnimation = false;

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
            if (animations["skill1"].FinishedPlaying)
            {
                finish = false;
            } //애니메이션 디폴트 값 입력 전에 스킬 사용여부 체크
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


            } //공격 애니메이션

            if (keyState.IsKeyDown(Keys.Left))
            {
                direct = 0;
                flipped = false;
                newAnimation = "move";
                velocity = new Vector2(0, velocity.Y);

                if (keyState.IsKeyDown(Keys.Space))
                {
                    int attack_num = num.Next(0, 10);
                    if (direct == 0)
                    {
                        flipped = false;
                    }
                    else
                    {
                        flipped = true;
                    }

                    if (attack_num < 5)
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
            } // 좌측이동 애니메이션 + 이동중 공격

            if (keyState.IsKeyDown(Keys.Right))
            {
                direct = 1;
                flipped = true;
                newAnimation = "move";
                velocity = new Vector2(0, velocity.Y);

                if (keyState.IsKeyDown(Keys.Space))
                {
                    int attack_num = num.Next(0, 10);
                    if (direct == 0)
                    {
                        flipped = false;
                    }
                    else
                    {
                        flipped = true;
                    }

                    if (attack_num < 5)
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
            } //우측이동 애니메이션 + 이동중 공격

            if (keyState.IsKeyDown(Keys.Z))
            {
                if (stat.manaPoint < 3)
                {
                    stat.manaPoint = stat.manaPoint + 0.005f;
                }
                else
                {
                    stat.manaPoint = stat.manaPoint + 0.0f;
                } // 게이지 값 연산

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
            } // 게이지 모으기 애니메이션

            if (keyState.IsKeyDown(Keys.S))
            {
                if (stat.manaPoint >= 1)
                {
                    if (stat.useSword == 0)
                    {
                        if (!finish)
                        {
                            finish = true;
                            stat.manaPoint = stat.manaPoint - 1.0f;
                        } // 스킬 사용시 마나 포인트 변경
                        if (direct == 0)
                        {
                            flipped = false;
                        }
                        else
                        {
                            flipped = true;
                        }

                        newAnimation = "skill1";
                        velocity = new Vector2(0, velocity.Y);
                    }
                    else
                    {
                        //다른 무기일 경우 추가
                    }
                }
                
            } // 기본무기 스킬(1개 중 1번)

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
                if (animations[currentAnimation].FinishedPlaying == true || animations[currentAnimation].LoopAnimation == true || newAnimation == "skill1")
                    PlayAnimation(newAnimation);
            } //다른 입력값이 들어왔을때 처리방법
            else
            {
                if (animations[currentAnimation].FinishedPlaying == true)
                    PlayAnimation(newAnimation);
            } //지속 입력값이 들어왔을때 처리방법
            

            velocity += fallSpeed;

            //repositionCamera();
            base.Update(gameTime);
        }

        public void Jump()
        {
            velocity.Y = -500;
        }

        #region UIstatus
        public float getMana()
        {
            return stat.manaPoint;
        }
        public float getHeal()
        {
            return stat.healPoint;
        }
        public int getMaxHeal()
        {
            return stat.getMaxHeal();
        }
        public int getAttack()
        {
            return stat.damage;
        }
        public int getDefense()
        {
            return stat.defense;
        }
        #endregion //UI쪽으로 넘기는 status값

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
