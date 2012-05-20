using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brave_Pig.BasicObject
{
    public class AnimationStrip
    {
        #region Declarations
        //애니메이션 텍스쳐
        private Texture2D texture;

        //애니메이션 frame width
        private int frameWidth;
        //애니메이션 frame height
        private int frameHeight;

        //frame Timer
        private float frameTimer = 0f;
        //frame delay
        private float frameDelay = 0.05f;

        //현재 애니메이션 frme
        private int currentFrame;

        //애니메이션을 반복할 것인가
        private bool loopAnimation = true;
        //애니메이션이 끝났는가?
        private bool finishedPlaying = false;

        //애니메이션 이름
        private string name;
        //다음 애니메이션 이름
        private string nextAnimation;
        #endregion

        #region Properties
        public int FrameWidth
        {
            get { return frameWidth; }
            set { frameWidth = value; }
        }

        public int FrameHeight
        {
            get { return frameHeight; }
            set { frameHeight = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string NextAnimation
        {
            get { return nextAnimation; }
            set { nextAnimation = value; }
        }

        public bool LoopAnimation
        {
            get { return loopAnimation; }
            set { loopAnimation = value; }
        }

        public bool FinishedPlaying
        {
            get { return finishedPlaying; }
        }

        public int FrameCount
        {
            get { return texture.Width / frameWidth; }
        }

        public float FrameLength
        {
            get { return frameDelay; }
            set { frameDelay = value; }
        }

        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(
                    currentFrame * frameWidth,
                    0,
                    frameWidth,
                    frameHeight);
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Animation Stip 생성자
        /// </summary>
        /// <param name="texture">애니메이션 Texture 파일(Spritesheet)</param>
        /// <param name="frameWidth">Frame의 총 Wdith</param>
        /// <param name="name">애니메이션 이름</param>
        public AnimationStrip(Texture2D texture, int frameWidth, string name)
        {
            this.texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = texture.Height;
            this.name = name;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 애니메이션의 시작을 알려줌 FinishPlaying을 false로 바꿈
        /// </summary>
        public void Play()
        {
            currentFrame = 0;
            finishedPlaying = false;
        }

        /// <summary>
        /// 애니메이션 업데이트 함수
        /// </summary>
        /// <param name="gameTime">현재 게임 시간</param>
        public void Update(GameTime gameTime)
        {
            //elapsed time
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Timer에 elapsed를 더함
            frameTimer += elapsed;

            //FrameTimer >= Delay 일정 시간이 지나면, 
            if (frameTimer >= frameDelay)
            {
                //현재 프레임을 더함
                currentFrame++;
                //Framecount
                if (currentFrame >= FrameCount)
                {
                    //반복
                    if (loopAnimation)
                    {
                        currentFrame = 0;
                    }
                    //반복 아니라면
                    else
                    {
                        currentFrame = FrameCount - 1;
                        finishedPlaying = true;
                    }
                }

                frameTimer = 0f;
            }
        }
        #endregion

    }
}
