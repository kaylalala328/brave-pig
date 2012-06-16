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
        private Vector2 fallSpeed = new Vector2(0,30);
        private float moveScale = 180.0f;
        Random num = new Random();
        private int direct = 0;
        Status stat = new Status();
        private bool finish = false;
        private bool heal = false;

        public int height;
        
        int returnValue;
        protected bool isAttack = false;
        protected bool isAttackflip = false;

        public Player(ContentManager content)
        {
            #region addAnimation
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
            animations["charge"].FrameLength = 0.07f;
            animations["charge"].NextAnimation = "normal";

            animations.Add("charge21", new AnimationStrip(content.Load<Texture2D>("Player/charge21"), 179, "charge21"));
            animations["charge21"].LoopAnimation = false;
            animations["charge21"].FrameLength = 0.07f;
            animations["charge21"].NextAnimation = "normal";

            animations.Add("charge22", new AnimationStrip(content.Load<Texture2D>("Player/charge22"), 179, "charge22"));
            animations["charge22"].LoopAnimation = false;
            animations["charge22"].FrameLength = 0.07f;
            animations["charge22"].NextAnimation = "normal";

            animations.Add("charge23", new AnimationStrip(content.Load<Texture2D>("Player/charge23"), 179, "charge23"));
            animations["charge23"].LoopAnimation = false;
            animations["charge23"].FrameLength = 0.07f;
            animations["charge23"].NextAnimation = "normal";

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

            animations.Add("jump", new AnimationStrip(content.Load<Texture2D>("Player/jump"), 179, "jump"));
            animations["jump"].LoopAnimation = false;
            animations["jump"].FrameLength = 0.07f;
            animations["jump"].NextAnimation = "normal";

            animations.Add("jump2", new AnimationStrip(content.Load<Texture2D>("Player/jump2"), 179, "jump2"));
            animations["jump2"].LoopAnimation = false;
            animations["jump2"].FrameLength = 0.07f;
            animations["jump2"].NextAnimation = "normal";

            animations.Add("jump3", new AnimationStrip(content.Load<Texture2D>("Player/jump3"), 179, "jump3"));
            animations["jump3"].LoopAnimation = false;
            animations["jump3"].FrameLength = 0.07f;
            animations["jump3"].NextAnimation = "normal";

            animations.Add("jump4", new AnimationStrip(content.Load<Texture2D>("Player/jump4"), 179, "jump4"));
            animations["jump4"].LoopAnimation = false;
            animations["jump4"].FrameLength = 0.07f;
            animations["jump4"].NextAnimation = "normal";

            animations.Add("dead", new AnimationStrip(content.Load<Texture2D>("Player/dead"), 179, "dead"));
            animations["dead"].LoopAnimation = false;
            animations["dead"].FrameLength = 0.5f;

            animations.Add("dead2", new AnimationStrip(content.Load<Texture2D>("Player/dead2"), 179, "dead2"));
            animations["dead2"].LoopAnimation = false;
            animations["dead2"].FrameLength = 0.5f;

            animations.Add("dead3", new AnimationStrip(content.Load<Texture2D>("Player/dead3"), 179, "dead3"));
            animations["dead3"].LoopAnimation = false;
            animations["dead3"].FrameLength = 0.5f;

            animations.Add("dead4", new AnimationStrip(content.Load<Texture2D>("Player/dead4"), 179, "dead4"));
            animations["dead4"].LoopAnimation = false;
            animations["dead4"].FrameLength = 0.5f;
            #endregion

            frameWidth = 179;
            frameHeight = 77;

            height = frameHeight;

            CollisionRectangle = new Rectangle(52, 5, 66, 71);
            CollisionRectangle2 = new Rectangle(118, 5, 59, 71);
            CollisionRectangle3 = new Rectangle(0, 5, 51, 71);

            drawDepth = 0.1f;

            currentAnimation = "normal";
            currentAnimation2 = "default";
            enabled = true;
            codeBasedBlocks = true;

            IsPlayer = true;
            PlayAnimation("normal", "default");
            worldLocation = new Vector2(20,500);   //플레이어 위치
        }

        public override void Update(GameTime gameTime)
        {
            if (animations["skill1"].FinishedPlaying || animations["skill2"].FinishedPlaying || animations["skill3"].FinishedPlaying || animations["skill4"].FinishedPlaying || animations["skill22"].FinishedPlaying || animations["skill32"].FinishedPlaying || animations["skill33"].FinishedPlaying)
            {
                finish = false;
            } //애니메이션 디폴트 값 입력 전에 스킬 사용여부 체크

            if(animations["damage"].FinishedPlaying || animations["damage2"].FinishedPlaying || animations["damage3"].FinishedPlaying || animations["damage4"].FinishedPlaying)
            {
                heal = false;
            }

            string newAnimation = "";
            string newAnimation2 = "default";

            if ( ItemManager.getCurrentSword() == "Basic" )
            {
                newAnimation = "normal";
                newAnimation2 = "default";
            }
            else if ( ItemManager.getCurrentSword() == "Blue" )
            {
                newAnimation = "normal2";
                newAnimation2 = "default";
            }
            else if (ItemManager.getCurrentSword() == "Red")
            {
                newAnimation = "normal3";
                newAnimation2 = "default";
            }
            else if (ItemManager.getCurrentSword() == "Yellow")
            {
                newAnimation = "normal4";
                newAnimation2 = "default";
            }

            velocity = new Vector2(0, velocity.Y);
            //GamePadState gamePad = GamePad.GetState(PlayerIndex.One); //xbox 패드 입력값


            #region swordselect
            if (Game1.currentKeyState.IsKeyDown(Keys.D0) && ItemManager.haveSwords.Contains("Basic"))
            {
                ItemManager.setCurrentSword("Basic");
            }
            if ( Game1.currentKeyState.IsKeyDown(Keys.D1) && ItemManager.haveSwords.Contains("Blue"))
            {
                ItemManager.setCurrentSword("Blue");
            }
            if ( Game1.currentKeyState.IsKeyDown(Keys.D2) && ItemManager.haveSwords.Contains("Red"))
            {
                ItemManager.setCurrentSword("Red");
            }
            if ( Game1.currentKeyState.IsKeyDown(Keys.D3) && ItemManager.haveSwords.Contains("Yellow"))
            {
                ItemManager.setCurrentSword("Yellow");
            }            
            #endregion

            #region attack
            if ( Game1.currentKeyState.IsKeyDown(Keys.Space))
            {
                if (ItemManager.getCurrentSword() == "Basic")
                {
                    int attack_num = num.Next(0, 10);
                    if (direct == 0)
                    {
                        flipped = false;
                        isAttack = true;
                    }
                    else
                    {
                        flipped = true;
                        isAttackflip = true;
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
                        isAttack = true;
                    }
                    else
                    {
                        flipped = true;
                        isAttackflip = true;
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
                        isAttack = true;
                    }
                    else
                    {
                        flipped = true;
                        isAttackflip = true;
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
                        isAttack = true;
                    }
                    else
                    {
                        flipped = true;
                        isAttackflip = true;
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
            if ( Game1.currentKeyState.IsKeyDown(Keys.Left))
            {
                direct = 0;
                flipped = false;
                velocity = new Vector2(-moveScale, velocity.Y);
                if (ItemManager.getCurrentSword() == "Basic")
                {
                    newAnimation = "move";

                    if ( Game1.currentKeyState.IsKeyDown(Keys.Space))
                    {
                        int attack_num = num.Next(0, 10);
                        flipped = false;
                        isAttack = true;

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

                    if (Game1.currentKeyState.IsKeyDown(Keys.Space))
                    {
                        int attack_num = num.Next(0, 10);
                        flipped = false;
                        isAttack = true;

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

                    if (Game1.currentKeyState.IsKeyDown(Keys.Space))
                    {
                        int attack_num = num.Next(0, 10);
                        flipped = false;
                        isAttack = true;

                        if (attack_num < 5)
                        {
                            newAnimation = "attack22";
                        }
                        else
                        {
                            newAnimation = "attack32";
                        }
                    }
                }// 2번째 무기 좌측 이동 + 공격
                else if (ItemManager.getCurrentSword() == "Yellow")
                {
                    newAnimation = "move34";

                    if (Game1.currentKeyState.IsKeyDown(Keys.Space))
                    {
                        int attack_num = num.Next(0, 10);
                        flipped = false;
                        isAttack = true;

                        if (attack_num < 5)
                        {
                            newAnimation = "attack23";
                        }
                        else
                        {
                            newAnimation = "attack33";
                        }
                    }
                }// 3번째 무기 좌측 이동 + 공격
            } // 좌측이동 애니메이션 + 이동중 공격

            if ( Game1.currentKeyState.IsKeyDown(Keys.Right))
            {
                direct = 1;
                flipped = true;
                velocity = new Vector2(moveScale, velocity.Y);
                if (ItemManager.getCurrentSword() == "Basic")
                {
                    newAnimation = "move";

                    if (Game1.currentKeyState.IsKeyDown(Keys.Space))
                    {
                        int attack_num = num.Next(0, 10);
                        flipped = true;
                        isAttack = true;
                        
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

                    if (Game1.currentKeyState.IsKeyDown(Keys.Space))
                    {
                        int attack_num = num.Next(0, 10);
                        flipped = true;
                        isAttackflip = true;

                        if (attack_num < 5)
                        {
                            newAnimation = "attack21";
                        }
                        else
                        {
                            newAnimation = "attack31";
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
                        isAttackflip = true;

                        if (attack_num < 5)
                        {
                            newAnimation = "attack22";
                        }
                        else
                        {
                            newAnimation = "attack32";
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
                        isAttackflip = true;

                        if (attack_num < 5)
                        {
                            newAnimation = "attack23";
                        }
                        else
                        {
                            newAnimation = "attack33";
                        }
                    }
                }// 3번째 무기 우측 이동 + 공격
            } //우측이동 애니메이션 + 이동중 공격
            #endregion

            #region charge

            if ( Game1.currentKeyState.IsKeyDown(Keys.Z))
            {
                if (ItemManager.getCurrentSword() == "Basic" && newAnimation != "move")
                {
                    if (currentAnimation != "jump")
                    {
                        if (stat.manaPoint < 2)
                        {
                            stat.manaPoint = stat.manaPoint + 0.005f;
                        }
                        else if (stat.manaPoint < 3)
                        {
                            stat.manaPoint = stat.manaPoint + 0.002f;
                        }
                        else
                        {
                            stat.manaPoint = stat.manaPoint + 0.0f;
                        } // 게이지 값 연산
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
                }//기본무기 게이지
                else if (ItemManager.getCurrentSword() == "Blue" && newAnimation != "move32")
                {
                    if (currentAnimation != "jump2")
                    {
                        if (stat.manaPoint < 2)
                        {
                            stat.manaPoint = stat.manaPoint + 0.005f;
                        }
                        else if (stat.manaPoint < 3)
                        {
                            stat.manaPoint = stat.manaPoint + 0.002f;
                        }
                        else
                        {
                            stat.manaPoint = stat.manaPoint + 0.0f;
                        } // 게이지 값 연산
                    }

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
                else if (ItemManager.getCurrentSword() == "Red" && newAnimation != "move33")
                {
                    if (currentAnimation != "jump3")
                    {
                        if (stat.manaPoint < 2)
                        {
                            stat.manaPoint = stat.manaPoint + 0.005f;
                        }
                        else if (stat.manaPoint < 3)
                        {
                            stat.manaPoint = stat.manaPoint + 0.002f;
                        }
                        else
                        {
                            stat.manaPoint = stat.manaPoint + 0.0f;
                        } // 게이지 값 연산
                    }

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
                else if (ItemManager.getCurrentSword() == "Yellow" && newAnimation != "move34")
                {
                    if (currentAnimation != "jump4")
                    {
                        if (stat.manaPoint < 2)
                        {
                            stat.manaPoint = stat.manaPoint + 0.005f;
                        }
                        else if (stat.manaPoint < 3)
                        {
                            stat.manaPoint = stat.manaPoint + 0.002f;
                        }
                        else
                        {
                            stat.manaPoint = stat.manaPoint + 0.0f;
                        } // 게이지 값 연산
                    }

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
            if ((Game1.previousKeyState.IsKeyUp(Keys.S) && Game1.currentKeyState.IsKeyDown(Keys.S)))
            {
                if (stat.manaPoint >= 1)
                {
                    if (ItemManager.getCurrentSword() == "Basic")
                    {
                        if (currentAnimation != "jump")
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
                        }
                    } // 기본무기 스킬(1개 중 1번)
                    else if ( ItemManager.getCurrentSword() == "Blue" )
                    {
                        if (currentAnimation != "jump2")
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
                        }
                    } // 1번째 무기 스킬(1개 중 1번)
                    else if (ItemManager.getCurrentSword() == "Red")
                    {
                        if (currentAnimation != "jump3")
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
                        }
                    } // 2번째 무기 스킬(1개 중 1번)
                    else if (ItemManager.getCurrentSword() == "Yellow")
                    {
                        if (currentAnimation != "jump4")
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
                        }
                    } // 3번째 무기 스킬(1개 중 1번)
                }
            } // 1번스킬 계열 애니메이션
            #endregion

            #region skill2
            if ( (Game1.previousKeyState.IsKeyUp(Keys.D) && Game1.currentKeyState.IsKeyDown(Keys.D)))
            {
                if (stat.manaPoint >= 2)
                {
                    if (ItemManager.getCurrentSword() == "Red")
                    {
                        if (currentAnimation != "jump3")
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
                        }
                    } // 2번째 무기 스킬(2개 중 2번)
                    else if (ItemManager.getCurrentSword() == "Yellow")
                    {
                        if (currentAnimation != "jump4")
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
                        }
                    } // 3번째 무기 스킬(3개 중 2번)
                }
            } // 2번스킬 계열 애니메이션
            #endregion

            #region skill3
            if ( Game1.previousKeyState.IsKeyUp(Keys.F) && Game1.currentKeyState.IsKeyDown(Keys.F))
            {
                if (stat.manaPoint == 3)
                {
                    if (ItemManager.getCurrentSword() == "Yellow")
                    {
                        if (currentAnimation != "jump4")
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
                        }
                    } // 3번째 무기 스킬(3개 중 3번)
                }
            } // 3번스킬 계열 애니메이션
            #endregion

            #region jump
            if (Game1.currentKeyState.IsKeyDown(Keys.LeftAlt))
            {
                if (ItemManager.getCurrentSword() == "Basic")
                {
                    newAnimation = "jump";
                    if (onGround)
                    {
                        Jump();
                        newAnimation = "jump";
                    }
                }
                else if (ItemManager.getCurrentSword() == "Blue")
                {
                    newAnimation = "jump2";
                    if (onGround)
                    {
                        Jump();
                        newAnimation = "jump2";
                    }
                }
                else if (ItemManager.getCurrentSword() == "Red")
                {
                    newAnimation = "jump3";
                    if (onGround)
                    {
                        Jump();
                        newAnimation = "jump3";
                    }
                }
                else if (ItemManager.getCurrentSword() == "Yellow")
                {
                    newAnimation = "jump4";
                    if (onGround)
                    {
                        Jump();
                        newAnimation = "jump4";
                    }
                }
            }
            #endregion

            #region potion
            if ((Game1.previousKeyState.IsKeyUp(Keys.D4) && Game1.currentKeyState.IsKeyDown(Keys.D4)))
            {
                if (ItemManager.getPotion().getCount() > 0)
                {
                    ItemManager.getPotion().usePotion();
                    stat.healPoint = stat.healPoint + ItemManager.getPotion().getHeal();
                    if ( stat.healPoint > stat.maxHeal )
                    {
                        stat.healPoint = stat.maxHeal;
                    }
                }
            }   
            #endregion

            if (newAnimation != currentAnimation)
            {
                isAttack = false;
                isAttackflip = false;
                if (currentAnimation != "jump" && currentAnimation != "jump2" && currentAnimation != "jump3" && currentAnimation != "jump4")
                {
                    if (animations[currentAnimation].FinishedPlaying == true || animations[currentAnimation].LoopAnimation == true || newAnimation == "skill1" || newAnimation == "skill2" || newAnimation == "skill3" || newAnimation == "skill4" || newAnimation == "skill32" || newAnimation == "skill22" || newAnimation == "skill33")
                        PlayAnimation(newAnimation, newAnimation2);
                }
                else
                {
                    if (animations[currentAnimation].FinishedPlaying == true || animations[currentAnimation].LoopAnimation == true || newAnimation == "attack2" || newAnimation == "attack1" || newAnimation == "attack21" || newAnimation == "attack31" || newAnimation == "attack22" || newAnimation == "attack32" || newAnimation == "attack23" || newAnimation == "attack33")
                    PlayAnimation(newAnimation, newAnimation2);
                }
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
            velocity.Y = -900;
        }

        public void damaged()
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

                if (currentAnimation != "damage")
                {
                    PlayAnimation("damage");
                }

                if (!flipped)
                {
                    velocity = new Vector2(3000, velocity.Y);
                }
                else
                {
                    velocity = new Vector2(-3000, velocity.Y);
                }

                if (!heal)
                {
                    heal = true;
                    if (stat.healPoint > 0)
                    {
                        stat.healPoint = stat.healPoint - 5;
                    }
                    else
                    {
                        PlayAnimation("dead");
                    }
                }
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

                if (currentAnimation != "damage2")
                {
                    PlayAnimation("damage2");
                }

                if (!flipped)
                {
                    velocity = new Vector2(300, 0);
                }
                else
                {
                    velocity = new Vector2(-300, 0);
                }

                if (!heal)
                {
                    heal = true;
                    if (stat.healPoint > 0)
                    {
                        stat.healPoint = stat.healPoint - 5;
                    }
                    else
                    {
                        PlayAnimation("dead2");
                    }
                }
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

                if (currentAnimation != "damage3")
                {
                    PlayAnimation("damage3");
                }

                if (!flipped)
                {
                    velocity = new Vector2(300, 0);
                }
                else
                {
                    velocity = new Vector2(-300, 0);
                }

                if (!heal)
                {
                    heal = true;
                    if (stat.healPoint > 0)
                    {
                        stat.healPoint = stat.healPoint - 5;
                    }
                    else
                    {
                        PlayAnimation("dead3");
                    }
                }
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

                if (currentAnimation != "damage4")
                {
                    PlayAnimation("damage4");
                }

                if (!flipped)
                {
                    velocity = new Vector2(300, 0);
                }
                else
                {
                    velocity = new Vector2(-300, 0);
                }

                if (!heal)
                {
                    heal = true;
                    if (stat.healPoint > 0)
                    {
                        stat.healPoint = stat.healPoint - 5;
                    }
                    else
                    {
                        PlayAnimation("dead4");
                    }
                }
            }
        }

        public bool attacking()
        {
            return isAttack;
        }
        public bool attackingfilp()
        {
            return isAttackflip;
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
            if (ItemManager.getCurrentSword() == "Basic")
            {
                returnValue = stat.damage;
            }
            else if (ItemManager.getCurrentSword() == "Blue")
            {
                returnValue = (stat.damage * 12) / 10;
            }
            else if (ItemManager.getCurrentSword() == "Red")
            {
                returnValue = (stat.damage * 15) / 10;
            }
            else if (ItemManager.getCurrentSword() == "Yellow")
            {
                returnValue = (stat.damage * 20) / 10;
            }
            return returnValue;
        }
        public int getDefense()
        {
            return stat.defense;
        }
        #endregion //UI쪽으로 넘기는 status값


        private void repositionCamera()
        {
            int screenLocX = (int)Camera.WorldToScreen(worldLocation).X;

            if (screenLocX > 600)
            {
                Camera.Move(new Vector2(screenLocX - 600, 0));
            }

            if (screenLocX < 400)
            {
                Camera.Move(new Vector2(screenLocX - 400, 0));
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
