using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Brave_Pig.BasicObject;
/*
 * 게임 시작 화면
 * Start, Load, Exit 선택 화면
 */
namespace Brave_Pig
{
    class Screen : GameObject
    {
        #region Declaration
        Texture2D screen;
        Texture2D menu;
        Texture2D pause;
        Texture2D resume;
        Texture2D save;
        Texture2D load2;
        Texture2D exit2;
        Texture2D screen1;
        Texture2D[] screens = new Texture2D[5];

        GraphicsDevice graphics;

        int[] screenX = new int[7] { 0, 256, 512, 768, 1024, 0, 1024 };
        bool[] check = new bool[5] { false, false, false, false, false };
        bool leftTurn = false;
        bool rightTurn = false;
        int time;
        bool start = true;

        int width, height;
        int cursorX, cursorY;
        int cursorWidth, cursorHeight;
        //시작 화면 선택
        private enum SelectMode
        {
            START,
            LOAD,
            MENU,
            MAKE,
            EXIT
        };
        private SelectMode select = SelectMode.START;

        private enum PauseMenu
        {
            RESUME,
            SAVE,
            LOAD,
            EXIT
        }
        private PauseMenu pauseMenu = PauseMenu.RESUME;
        #endregion

        public void Initialize ( GraphicsDevice Graphics )
        {
            graphics = Graphics;
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;
            cursorX = width / 3;
            cursorY = height / 2;
            cursorWidth = width / 3;
            cursorHeight = height / 4;

            IsEnemy = false;
        }
        public void LoadContent ( ContentManager content )
        {
            screen = content.Load<Texture2D>("Screen/Screen");
            menu = content.Load<Texture2D>("Screen/menu");
            pause = content.Load<Texture2D>("Screen/pause");
            resume = content.Load<Texture2D>("Screen/resume");
            save = content.Load<Texture2D>("Screen/save");
            load2 = content.Load<Texture2D>("Screen/load2");
            exit2 = content.Load<Texture2D>("Screen/exit2");
            screen1 = content.Load<Texture2D>("Screen/screen1");
            screens[0] = content.Load<Texture2D>("Screen/screen2");
            screens[1] = content.Load<Texture2D>("Screen/screen3");
            screens[2] = content.Load<Texture2D>("Screen/screen4");
            screens[3] = content.Load<Texture2D>("Screen/screen5");
            screens[4] = content.Load<Texture2D>("Screen/screen6");

            animations.Add("normal", new AnimationStrip(content.Load<Texture2D>("Player/normal3"), 179, "normal3"));
            animations["normal"].LoopAnimation = true;
            animations["normal"].FrameLength = 0.3f;
            animations.Add("attack1", new AnimationStrip(content.Load<Texture2D>("Player/attack32"), 179, "attack32"));
            animations["attack1"].LoopAnimation = false;
            animations["attack1"].FrameLength = 0.7f;
            animations.Add("attack2", new AnimationStrip(content.Load<Texture2D>("Player/attack33"), 179, "attack33"));
            animations["attack2"].LoopAnimation = false;
            animations["attack2"].FrameLength = 0.7f;
            animations2.Add("mon", new AnimationStrip(content.Load<Texture2D>("Monsters/bluemushroom_attack"), 64, "bluemushroom_attack"));
            animations2["mon"].LoopAnimation = false;
            animations2["mon"].FrameLength = 0.7f;
            animations2.Add("normalMon", new AnimationStrip(content.Load<Texture2D>("Monsters/bluemushroom_idle"), 64, "bluemushroom_idle"));
            animations2["normalMon"].LoopAnimation = false;
            animations2["normalMon"].FrameLength = 0.3f;

            currentAnimation = "attack1";
            currentAnimation2 = "mon";
            PlayAnimation("attack1", "mon");
        }
        public override void Update ( GameTime gameTime )
        {
            //키보드 입력
            Game1.currentKeyState = Keyboard.GetState();

            if ( (int)( gameTime.TotalGameTime.TotalSeconds % 10 ) == 9 )
            {
                if ( !start )
                {
                    PlayAnimation("attack1", "mon");
                    start = true;
                }
            }

            for ( int i = 0 ; i < 5 ; i++ )
            {
                if ( screenX[i] == 512 )
                {
                    switch ( i )
                    {
                        case 0:
                            select = SelectMode.START;
                            break;
                        case 1:
                            select = SelectMode.LOAD;
                            break;
                        case 2:
                            select = SelectMode.MENU;
                            break;
                        case 3:
                            select = SelectMode.MAKE;
                            break;
                        case 4:
                            select = SelectMode.EXIT;
                            break;
                    }
                }
            }
            if ( Game1.previousKeyState.IsKeyDown(Keys.Enter) &&
                Game1.currentKeyState.IsKeyUp(Keys.Enter) )
            {
                if ( !leftTurn && !rightTurn )
                {
                    switch ( select )
                    {
                        case SelectMode.START:
                            Game1.gameState = Game1.GameStates.PLAY;
                            break;
                        case SelectMode.LOAD:
                            Game1.gameState = Game1.GameStates.LOAD;
                            break;
                        case SelectMode.MENU:
                            Game1.gameState = Game1.GameStates.MENU;
                            break;
                        case SelectMode.MAKE:
                            Game1.gameState = Game1.GameStates.MAKE;
                            break;
                        case SelectMode.EXIT:
                            Game1.gameState = Game1.GameStates.EXIT;
                            break;
                    }
                }
            }
            else if ( Game1.previousKeyState.IsKeyDown(Keys.Left) &&
                Game1.currentKeyState.IsKeyUp(Keys.Left) )
            {
                if ( !rightTurn )
                {
                    leftTurn = true;
                }
            }
            else if ( Game1.previousKeyState.IsKeyDown(Keys.Right) &&
                Game1.currentKeyState.IsKeyUp(Keys.Right) )
            {
                if ( !leftTurn )
                {
                    rightTurn = true;
                }
            }
            Game1.previousKeyState = Game1.currentKeyState;

            updateAnimation(gameTime);
        }
        public void UpdatePause ( GameTime gameTime )
        {
            if ( Game1.previousKeyState.IsKeyDown(Keys.Down) &&
                Game1.currentKeyState.IsKeyUp(Keys.Down) )
            {
                switch ( pauseMenu )
                {
                    case PauseMenu.RESUME:
                        pauseMenu = PauseMenu.SAVE;
                        break;
                    case PauseMenu.SAVE:
                        pauseMenu = PauseMenu.LOAD;
                        break;
                    case PauseMenu.LOAD:
                        pauseMenu = PauseMenu.EXIT;
                        break;
                    case PauseMenu.EXIT:
                        pauseMenu = PauseMenu.RESUME;
                        break;
                }
            }
            else if ( Game1.previousKeyState.IsKeyDown(Keys.Up) &&
                Game1.currentKeyState.IsKeyUp(Keys.Up) )
            {
                switch ( pauseMenu )
                {
                    case PauseMenu.RESUME:
                        pauseMenu = PauseMenu.EXIT;
                        break;
                    case PauseMenu.SAVE:
                        pauseMenu = PauseMenu.RESUME;
                        break;
                    case PauseMenu.LOAD:
                        pauseMenu = PauseMenu.SAVE;
                        break;
                    case PauseMenu.EXIT:
                        pauseMenu = PauseMenu.LOAD;
                        break;
                }
            }
            else if ( Game1.previousKeyState.IsKeyDown(Keys.Enter) &&
                Game1.currentKeyState.IsKeyUp(Keys.Enter) )
            {
                switch ( pauseMenu )
                {
                    case PauseMenu.RESUME:
                        Game1.gameState = Game1.GameStates.PLAY;
                        break;
                    case PauseMenu.SAVE:
                        break;
                    case PauseMenu.LOAD:
                        break;
                    case PauseMenu.EXIT:
                        Game1.gameState = Game1.GameStates.EXIT;
                        break;
                }
            }
        }
        /*
         * 시작 화면 출력
         */
        public override void Draw ( SpriteBatch spriteBatch )
        {
            spriteBatch.Draw(screen,
                new Rectangle(0, 0, width, height),
                Color.White);

            DrawAnimation(spriteBatch);

            for ( int i = 0 ; i < 5 ; i++ )
            {
                spriteBatch.Draw(screens[i], new Rectangle(screenX[i], 576, 256, 144), Color.White);
            }

            if ( leftTurn )
            {
                DrawLeftTurn(spriteBatch);
            }
            if ( rightTurn )
            {
                DrawRightTurn(spriteBatch);
            }

            spriteBatch.Draw(screen1, new Rectangle(0, 576, 1280, 144), Color.White * 0.4f);
        }
        public void DrawAnimation ( SpriteBatch spriteBatch )
        {
            int animationX = (int)( width * 600 / 1280 );
            int animationY = (int)( height * 250 / 720 );
            int aniWidth = (int)( width * 300 / 1280 );
            int aniHeight = (int)( height * 300 / 720 );

            if ( animations2.ContainsKey(currentAnimation2) )
            {
                spriteBatch.Draw(
                animations2[currentAnimation2].Texture,
                new Rectangle((int)( width * 1 / 3 ), (int)( height * 100 / 720 ) + (int)( height * 250 / 720 ), (int)( width * 200 / 1280 ), (int)( height * 200 / 720 )),
                animations2[currentAnimation2].FrameRectangle,
                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0f);
                //animation이 끝났을 경우
                if ( animations2[currentAnimation2].FinishedPlaying == true )
                {
                    PlayAnimation("normal", "normalMon");
                    start = false;
                }
            }
            if ( animations.ContainsKey(currentAnimation) )
            {
                spriteBatch.Draw(
                animations[currentAnimation].Texture,
                new Rectangle(animationX, animationY, aniWidth, aniHeight),
                animations[currentAnimation].FrameRectangle,
                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0f);
                //animation이 끝났을 경우
                if ( animations[currentAnimation].FinishedPlaying == true )
                {
                    PlayAnimation("normal", "normalMon");
                    start = false;
                }
            }
        }
        public void DrawLeftTurn ( SpriteBatch spriteBatch )
        {
            for ( int i = 0 ; i < 5 ; i++ )
            {
                screenX[i] -= 2;
                if ( screenX[i] < 0 )
                {
                    screenX[i] = 1278;
                    check[i] = true;
                }
                if ( check[i] )
                {
                    spriteBatch.Draw(screens[i], new Rectangle(screenX[5], 576, 256, 144), Color.White);
                    screenX[5] -= 2;
                    if ( screenX[5] < -255 )
                    {
                        check[i] = false;
                        leftTurn = false;
                        screenX[5] = 0;
                    }
                }
            }
        }
        public void DrawRightTurn ( SpriteBatch spriteBatch )
        {
            for ( int i = 0 ; i < 5 ; i++ )
            {
                screenX[i] += 2;
                if ( screenX[i] > 1024 )
                {
                    screenX[i] = -254;
                    check[i] = true;
                }
                if ( check[i] )
                {
                    spriteBatch.Draw(screens[i], new Rectangle(screenX[6], 576, 256, 144), Color.White);
                    screenX[6] += 2;
                    if ( screenX[6] > 1279 )
                    {
                        check[i] = false;
                        rightTurn = false;
                        screenX[6] = 1024;
                    }
                }
            }
        }
        public void DrawPause ( SpriteBatch spriteBatch )
        {
            int[] num = new int[4] { 0, 0, 0, 0 };
            switch ( pauseMenu )
            {
                case PauseMenu.RESUME:
                    num[0] = 20;
                    break;
                case PauseMenu.SAVE:
                    num[1] = 20;
                    break;
                case PauseMenu.LOAD:
                    num[2] = 20;
                    break;
                case PauseMenu.EXIT:
                    num[3] = 20;
                    break;
            }

            spriteBatch.Draw(pause,
                new Rectangle((int)( width / 8 ) * 3, height / 4, width / 4, height / 2),
                Color.White);
            spriteBatch.Draw(resume,
                new Rectangle((int)( width / 8 ) * 3 - num[0] / 2, (int)( height / 3 ) - num[0] / 2, 320 + num[0], 70 + num[0]),
                Color.White);
            spriteBatch.Draw(save,
                new Rectangle((int)( width / 8 ) * 3 - num[1] / 2, (int)( height / 3 ) + 70 - num[1] / 2, 320 + num[1], 70 + num[1]),
                Color.White);
            spriteBatch.Draw(load2,
                new Rectangle((int)( width / 8 ) * 3 - num[2] / 2, (int)( height / 3 ) + 140 - num[2] / 2, 320 + num[2], 70 + num[2]),
                Color.White);
            spriteBatch.Draw(exit2,
                new Rectangle((int)( width / 8 ) * 3 - num[3] / 2, (int)( height / 3 ) + 210 - num[3] / 2, 320 + num[3], 70 + num[3]),
                Color.White);
        }
    }
}
