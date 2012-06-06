using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tile_Engine;

namespace Brave_Pig.BasicObject
{
    public class GameObject
    {
        #region Declarations
        //세상 위치
        protected Vector2 worldLocation;
        //Obejct 속도
        protected Vector2 velocity;
        //Frame의 Width
        protected int frameWidth;
        //각 Frame의 Height
        protected int frameHeight;

        //?? 어떤 bool 변수?
        protected bool enabled;
        //flipped?? 어떤 flipped?
        protected bool flipped = false;

        //on ground? 땅에 있을때?
        protected bool onGround;

        //충돌 사각형
        protected Rectangle collisionRectangle;
        //충돌 width
        protected int collideWidth;
        //충동 height
        protected int collideHeight;
        //code base? block??
        protected bool codeBasedBlocks = true;

        //그림 depth?? 위치 위에서 아래로
        protected float drawDepth = 0.85f;

        //Animation Dictionary
        protected Dictionary<string, AnimationStrip> animations =
            new Dictionary<string, AnimationStrip>();

        protected Dictionary<string, AnimationStrip> animations2 =
            new Dictionary<string, AnimationStrip>();
        
        //현재 애니메이션 string
        protected string currentAnimation;
        protected string currentAnimation2;
        #endregion

        #region Properties

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public Vector2 WorldLocation
        {
            get { return worldLocation; }
            set { worldLocation = value; }
        }

        public Vector2 WorldCenter
        {
            get
            {
                return new Vector2(
                  (int)worldLocation.X + (int)(frameWidth / 2),
                  (int)worldLocation.Y + (int)(frameHeight / 2));
            }
        }

        public Rectangle WorldRectangle
        {
            get
            {
                return new Rectangle(
                    (int)worldLocation.X,
                    (int)worldLocation.Y,
                    frameWidth,
                    frameHeight);
            }
        }

        public Rectangle CollisionRectangle
        {
            //사각형 위치
            get
            {
                return new Rectangle(
                    (int)worldLocation.X + collisionRectangle.X,
                    (int)WorldRectangle.Y + collisionRectangle.Y,
                    collisionRectangle.Width,
                    collisionRectangle.Height);
            }
            set { collisionRectangle = value; }
        }
        #endregion 

        #region Helper Methods
        
        //애니메이션 업데이트
        private void updateAnimation(GameTime gameTime)
        {
            //Key를 갖고 잇어야함. 즉 애니메이션이 존재하면,
            if (animations.ContainsKey(currentAnimation))
            {
                //애니메이션이 끝났다면
                if (animations[currentAnimation].FinishedPlaying)
                {
                    //애니메이션은 다음 애니메이션을 가르키게 한다.
                    PlayAnimation(animations[currentAnimation].NextAnimation, animations2[currentAnimation2].NextAnimation);
                }
                else
                {
                    //애니메이션이 안끝낫다면
                    animations[currentAnimation].Update(gameTime);
                    animations2[currentAnimation2].Update(gameTime);
                }
            }
        }
        #endregion

        #region Map-Based Collision Detection Methods
        /// <summary>
        /// 가로 충돌 처리 함수
        /// </summary>
        /// <param name="moveAmount">예측가능한 이동량</param>
        /// <returns>변경된 이동량</returns>
        private Vector2 horizontalCollisionTest(Vector2 moveAmount)
        {
            //이동량이 0면
            if (moveAmount.X == 0)
                return moveAmount;

            //이동 후 사각형
            Rectangle afterMoveRect = CollisionRectangle;
            afterMoveRect.Offset((int)moveAmount.X, 0);
            
            //코너 계산
            Vector2 corner1, corner2;

            //마이너스로 이동하면,
            if (moveAmount.X < 0)
            {
                //coner1 설정
                corner1 = new Vector2(afterMoveRect.Left,
                                      afterMoveRect.Top + 1);
                //coner2 설정
                corner2 = new Vector2(afterMoveRect.Left,
                                      afterMoveRect.Bottom - 1);
            }
                //양으로 이동하면
            else
            {
                //coner1 설정
                corner1 = new Vector2(afterMoveRect.Right,
                                      afterMoveRect.Top + 1);
                //coner2 설정
                corner2 = new Vector2(afterMoveRect.Right,
                                      afterMoveRect.Bottom - 1);
            }

            Vector2 mapCell1 = TileMap.GetCellByPixel(corner1);
            Vector2 mapCell2 = TileMap.GetCellByPixel(corner2);

            //TileMap이 Passable하다면,
            if (!TileMap.CellIsPassable(mapCell1) ||
                !TileMap.CellIsPassable(mapCell2))
            {
                //이동과 속도 0
                moveAmount.X = 0;
                velocity.X = 0;
            }

            //CodeBaseBlocks에 관련해서, BLOCK이 있다며,
            if (codeBasedBlocks)
            {
                if (TileMap.CellCodeValue(mapCell1) == "BLOCK" ||
                    TileMap.CellCodeValue(mapCell2) == "BLOCK")
                {
                    moveAmount.X = 0;
                    velocity.X = 0;
                }
            }

            return moveAmount;
        }

        private Vector2 verticalCollisionTest(Vector2 moveAmount)
        {
            if (moveAmount.Y == 0)
                return moveAmount;

            Rectangle afterMoveRect = CollisionRectangle;
            afterMoveRect.Offset((int)moveAmount.X, (int)moveAmount.Y);
            Vector2 corner1, corner2;

            if (moveAmount.Y < 0)
            {
                corner1 = new Vector2(afterMoveRect.Left + 1,
                                      afterMoveRect.Top);
                corner2 = new Vector2(afterMoveRect.Right - 1,
                                      afterMoveRect.Top);
            }
            else
            {
                corner1 = new Vector2(afterMoveRect.Left + 1,
                                      afterMoveRect.Bottom);
                corner2 = new Vector2(afterMoveRect.Right - 1,
                                      afterMoveRect.Bottom);
            }

            Vector2 mapCell1 = TileMap.GetCellByPixel(corner1);
            Vector2 mapCell2 = TileMap.GetCellByPixel(corner2);

            if (!TileMap.CellIsPassable(mapCell1) ||
                !TileMap.CellIsPassable(mapCell2))
            {
                if (moveAmount.Y > 0)
                    onGround = true;
                moveAmount.Y = 0;
                velocity.Y = 0;
            }

            if (codeBasedBlocks)
            {
                if (TileMap.CellCodeValue(mapCell1) == "BLOCK" ||
                    TileMap.CellCodeValue(mapCell2) == "BLOCK")
                {
                    if (moveAmount.Y > 0)
                        onGround = true;
                    moveAmount.Y = 0;
                    velocity.Y = 0;
                }
            }

            return moveAmount;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Animation 입력이 들어오면 애니메이션을 Play
        /// </summary>
        /// <param name="name"></param>
        public void PlayAnimation(string name, string name2)
        {
            if (!(name == null) && animations.ContainsKey(name))
            {
                currentAnimation = name;
                currentAnimation2 = name2;
                animations[name].Play();
                animations2[name2].Play();
            }
        }

        /// <summary>
        /// 업데이트 함수
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            //가능하다면,
            if (!enabled)
                return;

            //현재 elapsed time
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //애니메이션을 업데이트함.
            updateAnimation(gameTime);

            //속도의 Y가 0이 아니라면, ground false 즉 점프 될 수 있음.
            if (velocity.Y != 0)
            {
                onGround = false;
            }

            //이동량을 속도 * elapsed
            Vector2 moveAmount = velocity * elapsed;

            //충돌 관련함수
            moveAmount = horizontalCollisionTest(moveAmount);
            moveAmount = verticalCollisionTest(moveAmount);

            //moveAmount를 더한 새로운 포지션을 가짐
            Vector2 newPosition = worldLocation + moveAmount;

            //*****check 무엇을 clamp하는지
            //새로운 포지션(clamp 함수 이용)
            newPosition = new Vector2(
                MathHelper.Clamp(newPosition.X, 0,
                  Camera.WorldRectangle.Width - frameWidth),
                MathHelper.Clamp(newPosition.Y, 2 * (-TileMap.TileHeight),
                  Camera.WorldRectangle.Height - frameHeight));

            //newposition을 worldlocation에 저장함.
            //worldLocation = newPosition;
        }

        /// <summary>
        /// 그리기 함수
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //가능하지 않다면 return
            if (!enabled)
                return;

            //애니메이션을 갖고 잇으면
            if (animations.ContainsKey(currentAnimation))
            {
                //spriteeffect 좌우 대칭을 위한 Effect 변수
                SpriteEffects effect = SpriteEffects.None;

                //flip 시킬 것인가?
                if (flipped)
                {
                    effect = SpriteEffects.FlipHorizontally;
                }

                //현재 spriteBatch를 그린다.
                spriteBatch.Draw(
                    animations[currentAnimation].Texture,
                    Camera.WorldToScreen(WorldRectangle),
                    animations[currentAnimation].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
                spriteBatch.Draw(
                    animations2[currentAnimation2].Texture,
                    new Rectangle(200, 300, 140, 140),
                    animations2[currentAnimation2].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
                spriteBatch.Draw(
                    animations2[currentAnimation2].Texture,
                    new Rectangle(1000, 250, 140, 140),
                    animations2[currentAnimation2].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
                spriteBatch.Draw(
                    animations2[currentAnimation2].Texture,
                    new Rectangle(600, 100, 140, 140),
                    animations2[currentAnimation2].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
                spriteBatch.Draw(
                    animations2[currentAnimation2].Texture,
                    new Rectangle(400, 400, 140, 140),
                    animations2[currentAnimation2].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
                spriteBatch.Draw(
                    animations2[currentAnimation2].Texture,
                    new Rectangle(800, 50, 140, 140),
                    animations2[currentAnimation2].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
                spriteBatch.Draw(
                    animations2[currentAnimation2].Texture,
                    new Rectangle(300, 70, 140, 140),
                    animations2[currentAnimation2].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
                spriteBatch.Draw(
                    animations2[currentAnimation2].Texture,
                    new Rectangle(700, 350, 140, 140),
                    animations2[currentAnimation2].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
                spriteBatch.Draw(
                    animations2[currentAnimation2].Texture,
                    new Rectangle(50, 130, 140, 140),
                    animations2[currentAnimation2].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
                spriteBatch.Draw(
                    animations2[currentAnimation2].Texture,
                    new Rectangle(1100, 100, 140, 140),
                    animations2[currentAnimation2].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
                spriteBatch.Draw(
                    animations2[currentAnimation2].Texture,
                    new Rectangle(450, 250, 140, 140),
                    animations2[currentAnimation2].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
            }
        }

        #endregion

    }
}
