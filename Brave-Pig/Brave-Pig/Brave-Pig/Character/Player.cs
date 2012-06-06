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

namespace Brave_Pig.Character
{
    public class Player : GameObject
    {
        private Vector2 fallSpeed = new Vector2(0,20);
        private float moveScale = 180.0f;
        Random num = new Random();
        private int direct = 0;
        Status stat = new Status();
        private bool finish = false;
        
        public Player(ContentManager content)
        {

            animations.Add("normal", new AnimationStrip(content.Load<Texture2D>("Player/normal"), 179, "normal"));
            animations["normal"].LoopAnimation = true;

            animations.Add("normal2", new AnimationStrip(content.Load<Texture2D>("Player/normal2"), 179, "normal2"));
            animations["normal2"].LoopAnimation = true;

            animations.Add("normal3", new AnimationStrip(content.Load<Texture2D>("Player/normal3"), 179, "normal3"));
            animations["normal3"].LoopAnimation = true;

            animations.Add("normal4", new AnimationStrip(content.Load<Texture2D>("Player/normal4"), 179, "normal4"));
            animations["normal4"].LoopAnimation = true;

            animations.Add("attack1", new AnimationStrip(content.Load<Texture2D>("Player/attack1"), 179, "attack1"));
            animations["attack1"].LoopAnimation = false;

            animations.Add("attack2", new AnimationStrip(content.Load<Texture2D>("Player/attack2"), 179, "attack2"));
            animations["attack2"].LoopAnimation = false;

            animations.Add("attack21", new AnimationStrip(content.Load<Texture2D>("Player/attack21"), 179, "attack21"));
            animations["attack21"].LoopAnimation = false;

            animations.Add("attack31", new AnimationStrip(content.Load<Texture2D>("Player/attack31"), 179, "attack31"));
            animations["attack31"].LoopAnimation = false;

            animations.Add("attack22", new AnimationStrip(content.Load<Texture2D>("Player/attack22"), 179, "attack22"));
            animations["attack22"].LoopAnimation = false;

            animations.Add("attack32", new AnimationStrip(content.Load<Texture2D>("Player/attack32"), 179, "attack32"));
            animations["attack32"].LoopAnimation = false;

            animations.Add("attack23", new AnimationStrip(content.Load<Texture2D>("Player/attack23"), 179, "attack23"));
            animations["attack23"].LoopAnimation = false;

            animations.Add("attack33", new AnimationStrip(content.Load<Texture2D>("Player/attack33"), 179, "attack33"));
            animations["attack33"].LoopAnimation = false;

            animations.Add("move", new AnimationStrip(content.Load<Texture2D>("Player/move"), 179, "move"));
            animations["move"].LoopAnimation = true;

            animations.Add("move32", new AnimationStrip(content.Load<Texture2D>("Player/move32"), 179, "move32"));
            animations["move32"].LoopAnimation = true;

            animations.Add("move33", new AnimationStrip(content.Load<Texture2D>("Player/move33"), 179, "move33"));
            animations["move33"].LoopAnimation = true;

            animations.Add("move34", new AnimationStrip(content.Load<Texture2D>("Player/move34"), 179, "move34"));
            animations["move34"].LoopAnimation = true;

            animations.Add("charge", new AnimationStrip(content.Load<Texture2D>("Player/charge"), 179, "charge"));
            animations["charge"].LoopAnimation = false;

            animations.Add("charge21", new AnimationStrip(content.Load<Texture2D>("Player/charge21"), 179, "charge21"));
            animations["charge21"].LoopAnimation = false;

            animations.Add("charge22", new AnimationStrip(content.Load<Texture2D>("Player/charge22"), 179, "charge22"));
            animations["charge21"].LoopAnimation = false;

            animations.Add("charge23", new AnimationStrip(content.Load<Texture2D>("Player/charge23"), 179, "charge23"));
            animations["charge23"].LoopAnimation = false;

            animations.Add("skill1", new AnimationStrip(content.Load<Texture2D>("Player/skill1"), 179, "skill1"));
            animations["skill1"].LoopAnimation = false;

            animations.Add("skill2", new AnimationStrip(content.Load<Texture2D>("Player/skill2"), 179, "skill2"));
            animations["skill2"].LoopAnimation = false;

            animations.Add("skill3", new AnimationStrip(content.Load<Texture2D>("Player/skill3"), 179, "skill3"));
            animations["skill3"].LoopAnimation = false;

            animations.Add("skill4", new AnimationStrip(content.Load<Texture2D>("Player/skill4"), 179, "skill4"));
            animations["skill4"].LoopAnimation = false;

            animations.Add("skill22", new AnimationStrip(content.Load<Texture2D>("Player/skill22"), 179, "skill22"));
            animations["skill22"].LoopAnimation = false;

            animations.Add("skill32", new AnimationStrip(content.Load<Texture2D>("Player/skill32"), 179, "skill32"));
            animations["skill32"].LoopAnimation = false;

            animations.Add("skill33", new AnimationStrip(content.Load<Texture2D>("Player/skill33"), 179, "skill33"));
            animations["skill33"].LoopAnimation = false;

            animations.Add("damage", new AnimationStrip(content.Load<Texture2D>("Player/damage"), 179, "damage"));
            animations["damage"].LoopAnimation = false;

            animations.Add("damage2", new AnimationStrip(content.Load<Texture2D>("Player/damage2"), 179, "damage2"));
            animations["damage2"].LoopAnimation = false;

            animations.Add("damage3", new AnimationStrip(content.Load<Texture2D>("Player/damage3"), 179, "damage3"));
            animations["damage3"].LoopAnimation = false;

            animations.Add("damage4", new AnimationStrip(content.Load<Texture2D>("Player/damage4"), 179, "damage4"));
            animations["damage4"].LoopAnimation = false;

            animations2.Add("skillmap", new AnimationStrip(content.Load<Texture2D>("Player/skillmap"), 140, "skillmap"));
            animations2["skillmap"].LoopAnimation = false;

            animations2.Add("default", new AnimationStrip(content.Load<Texture2D>("Player/default"), 77, "default"));
            animations2["default"].LoopAnimation = false;

            /*
            animations.Add("jump", new AnimationStrip(content.Load<Texture2D>("Player/jump"), 179, "jump"));
            animations["jump"].LoopAnimation = false;
            animations["jump"].FrameLength = 0.08f;
            animations["jump"].NextAnimation = "normal";*/

            frameWidth = 179;
            frameHeight = 77;
            CollisionRectangle = new Rectangle(9, 1, 161, 75);

            drawDepth = 0f;

            currentAnimation = "normal";
            currentAnimation2 = "default";
            enabled = true;
            codeBasedBlocks = true;

            PlayAnimation("normal", "default");
            //worldLocation = new Vector2(350,300);   //플레이어 위치
        }

        public override void Update(GameTime gameTime)
        {
            if (animations["skill1"].FinishedPlaying || animations["skill2"].FinishedPlaying || animations["skill3"].FinishedPlaying || animations["skill4"].FinishedPlaying || animations["skill22"].FinishedPlaying || animations["skill32"].FinishedPlaying || animations["skill33"].FinishedPlaying)
            {
                finish = false;
            } //애니메이션 디폴트 값 입력 전에 스킬 사용여부 체크
            string newAnimation = "";
            string newAnimation2 = "default";

            if ( ItemManager.getCurrentSword() == "Basic" )
            {
                newAnimation = "normal";
            }
            else if ( ItemManager.getCurrentSword() == "Blue" )
            {
                newAnimation = "normal2";
            }
            else if (ItemManager.getCurrentSword() == "Red")
            {
                newAnimation = "normal3";
            }
            else if (ItemManager.getCurrentSword() == "Yellow")
            {
                newAnimation = "normal4";
            }

            velocity = new Vector2(0, velocity.Y);
            //GamePadState gamePad = GamePad.GetState(PlayerIndex.One); //xbox 패드 입력값


            #region swordselect
            if (Game1.currentKeyState.IsKeyDown(Keys.D0) && ItemManager.haveSwords.Contains("Basic"))
            {
                ItemManager.setCurrentSword("Basic");
            }
            if ( Game1.currentKeyState.IsKeyDown(Keys.D1) && ItemManager.haveSwords.Contains("Blue") )
            {
                ItemManager.setCurrentSword("Blue");
            }
            if ( Game1.currentKeyState.IsKeyDown(Keys.D2) && ItemManager.haveSwords.Contains("Red") )
            {
                ItemManager.setCurrentSword("Red");
            }
            if ( Game1.currentKeyState.IsKeyDown(Keys.D3) && ItemManager.haveSwords.Contains("Yellow") )
            {
                ItemManager.setCurrentSword("Yellow");
            }            
            #endregion

            #region attack
            if ( Game1.currentKeyState.IsKeyDown(Keys.Space) )
            {
                if (ItemManager.getCurrentSword() == "Basic")
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
                    }
                    else
                    {
                        newAnimation = "attack2";
                    }
                }// 기본 무기 공격 애니메이션
                else if (ItemManager.getCurrentSword() == "Blue")
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
                        newAnimation = "attack21";
                    }
                    else
                    {
                        newAnimation = "attack31";
                    }
                }// 1번째 무기 공격 애니메이션
                else if (ItemManager.getCurrentSword() == "Red")
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
                        newAnimation = "attack22";
                    }
                    else
                    {
                        newAnimation = "attack32";
                    }
                }// 2번째 무기 공격 애니메이션
                else if (ItemManager.getCurrentSword() == "Yellow")
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
                        newAnimation = "attack23";
                    }
                    else
                    {
                        newAnimation = "attack33";
                    }
                }// 3번째 무기 공격 애니메이션
            } //공격 애니메이션
            #endregion

            #region move
            if ( Game1.currentKeyState.IsKeyDown(Keys.Left) )
            {

                direct = 0;
                flipped = false;
                velocity = new Vector2(-moveScale, velocity.Y);
                if (ItemManager.getCurrentSword() == "Basic")
                {
                    newAnimation = "move";

                    if ( Game1.currentKeyState.IsKeyDown(Keys.Space) )
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
                        }
                        else
                        {
                            newAnimation = "attack2";
                        }
                    }
                } // 기본 무기 좌측 이동 + 공격
                else if (ItemManager.getCurrentSword() == "Blue")
                {
                    newAnimation = "move32";

                    if ( Game1.currentKeyState.IsKeyDown(Keys.Space) )
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
                            newAnimation = "attack21";
                        }
                        else
                        {
                            newAnimation = "attack31";
                        }
                    }
                }// 1번째 무기 좌측 이동 + 공격
                else if (ItemManager.getCurrentSword() == "Red")
                {
                    newAnimation = "move33";

                    if ( Game1.currentKeyState.IsKeyDown(Keys.Space) )
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
                            newAnimation = "attack22";
                            velocity = new Vector2(0, velocity.Y);
                        }
                        else
                        {
                            newAnimation = "attack32";
                            velocity = new Vector2(0, velocity.Y);
                        }
                    }
                }// 2번째 무기 좌측 이동 + 공격
                else if (ItemManager.getCurrentSword() == "Yellow")
                {
                    newAnimation = "move34";

                    if ( Game1.currentKeyState.IsKeyDown(Keys.Space) )
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
                            newAnimation = "attack23";
                            velocity = new Vector2(0, velocity.Y);
                        }
                        else
                        {
                            newAnimation = "attack33";
                            velocity = new Vector2(0, velocity.Y);
                        }
                    }
                }// 3번째 무기 좌측 이동 + 공격
            } // 좌측이동 애니메이션 + 이동중 공격

            if ( Game1.currentKeyState.IsKeyDown(Keys.Right) )
            {
                direct = 1;
                flipped = true;
                velocity = new Vector2(moveScale, velocity.Y);
                if (ItemManager.getCurrentSword() == "Basic")
                {
                    newAnimation = "move";

                    if ( Game1.currentKeyState.IsKeyDown(Keys.Space) )
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
                        }
                        else
                        {
                            newAnimation = "attack2";
                        }
                    }
                } // 기본 무기 우측 이동 + 공격
                else if (ItemManager.getCurrentSword() == "Blue")
                {
                    newAnimation = "move32";

                    if ( Game1.currentKeyState.IsKeyDown(Keys.Space) )
                    {
                        int attack_num = num.Next(0, 10);
                        flipped = true;

                        if (attack_num < 5)
                        {
                            newAnimation = "attack21";
                            velocity = new Vector2(0, velocity.Y);
                        }
                        else
                        {
                            newAnimation = "attack31";
                            velocity = new Vector2(0, velocity.Y);
                        }
                    }
                }// 1번째 무기 우측 이동 + 공격
                else if (ItemManager.getCurrentSword() == "Red")
                {
                    newAnimation = "move33";

                    if (Game1.currentKeyState.IsKeyDown(Keys.Space))
                    {
                        int attack_num = num.Next(0, 10);
                        flipped = true;

                        if (attack_num < 5)
                        {
                            newAnimation = "attack22";
                            velocity = new Vector2(0, velocity.Y);
                        }
                        else
                        {
                            newAnimation = "attack32";
                            velocity = new Vector2(0, velocity.Y);
                        }
                    }
                }// 2번째 무기 우측 이동 + 공격
                else if (ItemManager.getCurrentSword() == "Yellow")
                {
                    newAnimation = "move34";

                    if (Game1.currentKeyState.IsKeyDown(Keys.Space))
                    {
                        int attack_num = num.Next(0, 10);
                        flipped = true;

                        if (attack_num < 5)
                        {
                            newAnimation = "attack23";
                            velocity = new Vector2(0, velocity.Y);
                        }
                        else
                        {
                            newAnimation = "attack33";
                            velocity = new Vector2(0, velocity.Y);
                        }
                    }
                }// 3번째 무기 우측 이동 + 공격
            } //우측이동 애니메이션 + 이동중 공격
            #endregion

            #region charge

            if ( Game1.currentKeyState.IsKeyDown(Keys.Z) )
            {
                if (ItemManager.getCurrentSword() == "Basic")
                {
                    if (stat.manaPoint < 3)
                    {
                        stat.manaPoint = stat.manaPoint + 0.5f;
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
                }//기본무기 게이지
                else if (ItemManager.getCurrentSword() == "Blue")
                {
                    if (stat.manaPoint < 3)
                    {
                        stat.manaPoint = stat.manaPoint + 0.5f;
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

                    newAnimation = "charge21";
                }//1번째무기 게이지
                else if (ItemManager.getCurrentSword() == "Red")
                {
                    if (stat.manaPoint < 3)
                    {
                        stat.manaPoint = stat.manaPoint + 0.5f;
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

                    newAnimation = "charge22";
                }//2번째무기 게이지
                else if (ItemManager.getCurrentSword() == "Yellow")
                {
                    if (stat.manaPoint < 3)
                    {
                        stat.manaPoint = stat.manaPoint + 0.5f;
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

                    newAnimation = "charge23";
                }//3번째무기 게이지
            } // 게이지 모으기 애니메이션
            #endregion

            #region skill1
            if (Game1.previousKeyState.IsKeyUp(Keys.S) && Game1.currentKeyState.IsKeyDown(Keys.S))
            {
                if (stat.manaPoint >= 1)
                {
                    if (ItemManager.getCurrentSword() == "Basic")
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
                    } // 기본무기 스킬(1개 중 1번)
                    else if ( ItemManager.getCurrentSword() == "Blue" )
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

                        newAnimation = "skill2";
                    } // 1번째 무기 스킬(1개 중 1번)
                    else if (ItemManager.getCurrentSword() == "Red")
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

                        newAnimation = "skill3";
                    } // 2번째 무기 스킬(1개 중 1번)
                    else if (ItemManager.getCurrentSword() == "Yellow")
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

                        newAnimation = "skill4";
                    } // 3번째 무기 스킬(1개 중 1번)
                }
            } // 1번스킬 계열 애니메이션
            #endregion

            #region skill2
            if ( Game1.previousKeyState.IsKeyUp(Keys.D) && Game1.currentKeyState.IsKeyDown(Keys.D) )
            {
                if (stat.manaPoint >= 2)
                {
                    if (ItemManager.getCurrentSword() == "Red")
                    {
                        if (!finish)
                        {
                            finish = true;
                            stat.manaPoint = stat.manaPoint - 2.0f;
                        } // 스킬 사용시 마나 포인트 변경
                        if (direct == 0)
                        {
                            flipped = false;
                        }
                        else
                        {
                            flipped = true;
                        }

                        newAnimation = "skill22";
                    } // 2번째 무기 스킬(2개 중 2번)
                    else if (ItemManager.getCurrentSword() == "Yellow")
                    {
                        if (!finish)
                        {
                            finish = true;
                            stat.manaPoint = stat.manaPoint - 2.0f;
                        } // 스킬 사용시 마나 포인트 변경
                        if (direct == 0)
                        {
                            flipped = false;
                        }
                        else
                        {
                            flipped = true;
                        }

                        newAnimation = "skill32";
                    } // 3번째 무기 스킬(3개 중 2번)
                }
            } // 2번스킬 계열 애니메이션
            #endregion

            #region skill3
            if ( Game1.previousKeyState.IsKeyUp(Keys.F) && Game1.currentKeyState.IsKeyDown(Keys.F) )
            {
                if (stat.manaPoint >= 2)
                {
                    if (ItemManager.getCurrentSword() == "Yellow")
                    {
                        if (!finish)
                        {
                            finish = true;
                            stat.manaPoint = stat.manaPoint - 3.0f;
                        } // 스킬 사용시 마나 포인트 변경
                        if (direct == 0)
                        {
                            flipped = false;
                        }
                        else
                        {
                            flipped = true;
                        }

                        newAnimation = "skill33";
                        newAnimation2 = "skillmap";
                    } // 3번째 무기 스킬(3개 중 3번)
                }
            } // 3번스킬 계열 애니메이션
            #endregion

            #region damage
            if (Game1.currentKeyState.IsKeyDown(Keys.L))
            {
                if (ItemManager.getCurrentSword() == "Basic")
                {                    
                    if (direct == 0)
                    {
                        flipped = false;
                    }
                    else
                    {
                        flipped = true;
                    }

                    newAnimation = "damage";
                }
                else if (ItemManager.getCurrentSword() == "Blue")
                {
                    if (direct == 0)
                    {
                        flipped = false;
                    }
                    else
                    {
                        flipped = true;
                    }

                    newAnimation = "damage2";
                }
                else if (ItemManager.getCurrentSword() == "Red")
                {
                    if (direct == 0)
                    {
                        flipped = false;
                    }
                    else
                    {
                        flipped = true;
                    }

                    newAnimation = "damage3";
                }
                else if (ItemManager.getCurrentSword() == "Yellow")
                {
                    if (direct == 0)
                    {
                        flipped = false;
                    }
                    else
                    {
                        flipped = true;
                    }

                    newAnimation = "damage4";
                }
            }
            #endregion

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
                if (animations[currentAnimation].FinishedPlaying == true || animations[currentAnimation].LoopAnimation == true || newAnimation == "skill1" || newAnimation == "skill2" || newAnimation == "skill3" || newAnimation == "skill4" || newAnimation == "skill32" || newAnimation == "skill22" || newAnimation == "skill33")
                    PlayAnimation(newAnimation, newAnimation2);
            } //다른 입력값이 들어왔을때 처리방법
            else
            {
                if (animations[currentAnimation].FinishedPlaying == true)
                    PlayAnimation(newAnimation, newAnimation2);
            } //지속 입력값이 들어왔을때 처리방법
            

            velocity += fallSpeed;

            repositionCamera();
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


        private void repositionCamera()
        {
            int screenLocX = (int)Camera.WorldToScreen(worldLocation).X;

            if (screenLocX > 800)
            {
                Camera.Move(new Vector2(screenLocX - 800, 0));
            }

            if (screenLocX < 300)
            {
                Camera.Move(new Vector2(screenLocX - 300, 0));
            }

            int screenLocY = (int)Camera.WorldToScreen(worldLocation).Y;

            if (screenLocY > 200)
            {
                Camera.Move(new Vector2(0, screenLocY - 200));
            }

            if (screenLocY < 100)
            {
                Camera.Move(new Vector2( 0, screenLocY - 100));
            }
        }

        private void checkLevelTransition()
        {
            Vector2 centerCell = TileMap.GetCellByPixel(WorldCenter);
            if (TileMap.CellCodeValue(centerCell).StartsWith("T_"))
            {
                string[] code = TileMap.CellCodeValue(centerCell).Split('_');

                if (code.Length != 4)
                    return;

                LevelManager.LoadLevel(int.Parse(code[1]));

                WorldLocation = new Vector2(
                    int.Parse(code[2]) * TileMap.TileWidth,
                    int.Parse(code[3]) * TileMap.TileHeight);


                velocity = Vector2.Zero;
            }
        }
    }
}
