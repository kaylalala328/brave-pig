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

/*
 * 게임 시작 화면
 * Start, Load, Exit 선택 화면
 */
namespace Brave_Pig
{
    class Screen
    {
        #region Declaration
        Texture2D screen;
        Texture2D menu;
        Texture2D start;
        Texture2D load;
        Texture2D exit;
        Texture2D pause;
        Texture2D resume;
        Texture2D save;
        Texture2D load2;
        Texture2D exit2;
        
        GraphicsDevice graphics;

        int width, height;
        int cursorX, cursorY;
        int cursorWidth, cursorHeight;
        //시작 화면 선택
        private enum SelectMode
        {
            START,
            LOAD,
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

        public void Initialize(GraphicsDevice Graphics)
        {
            graphics = Graphics;
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;
            cursorX = width / 3;
            cursorY = height / 2;
            cursorWidth = width / 3;
            cursorHeight = height / 4;
        }
        public void LoadContent(ContentManager content)
        {
            screen = content.Load<Texture2D>("Screen/Screen");
            menu = content.Load<Texture2D>("Screen/menu");
            start = content.Load<Texture2D>("Screen/start");
            load = content.Load<Texture2D>("Screen/load");
            exit = content.Load<Texture2D>("Screen/exit");
            pause = content.Load<Texture2D>("Screen/pause");
            resume = content.Load<Texture2D>("Screen/resume");
            save = content.Load<Texture2D>("Screen/save");
            load2 = content.Load<Texture2D>("Screen/load2");
            exit2 = content.Load<Texture2D>("Screen/exit2");
        }
        public void Update(GameTime gameTime)
        {
            //키보드 입력
            Game1.currentKeyState = Keyboard.GetState();

            if ( Game1.previousKeyState.IsKeyDown(Keys.Down) &&
                Game1.currentKeyState.IsKeyUp(Keys.Down))
            {
                switch(select)
                {
                    case SelectMode.START:
                        select = SelectMode.LOAD;
                        break;
                    case SelectMode.LOAD:
                        select = SelectMode.EXIT;
                        break;
                    case SelectMode.EXIT:
                        select = SelectMode.START;
                        break;
                }
            }
            else if ( Game1.previousKeyState.IsKeyDown(Keys.Up) &&
                Game1.currentKeyState.IsKeyUp(Keys.Up))
            {
                switch ( select )
                {
                    case SelectMode.START:
                        select = SelectMode.EXIT;
                        break;
                    case SelectMode.LOAD:
                        select = SelectMode.START;
                        break;
                    case SelectMode.EXIT:
                        select = SelectMode.LOAD;
                        break;
                }
            }
            else if ( Game1.previousKeyState.IsKeyDown(Keys.Enter) &&
                Game1.currentKeyState.IsKeyUp(Keys.Enter))
            {
                switch ( select )
                {
                    case SelectMode.START:
                        Game1.gameState = Game1.GameStates.PLAY;
                        break;
                    case SelectMode.LOAD:
                        Game1.gameState = Game1.GameStates.LOAD;
                        break;
                    case SelectMode.EXIT:
                        Game1.gameState = Game1.GameStates.EXIT;
                        break;
                }
            }
            Game1.previousKeyState = Game1.currentKeyState;
        }
        public void UpdatePause(GameTime gameTime)
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
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(screen,
                new Rectangle(0, 0, width, height),
                Color.White);

            switch(select)
            {
                case SelectMode.START:
                    spriteBatch.Draw(start,
                        new Rectangle(cursorX - 10, 
                            cursorY - 10, 
                            cursorWidth + 20, 
                            cursorHeight / 3 + 20),
                            null,
                        Color.White, 0.07f, Vector2.Zero, SpriteEffects.None, 0f);
                    spriteBatch.Draw(load,
                        new Rectangle(cursorX,
                            cursorY + cursorHeight / 3,
                            cursorWidth,
                            cursorHeight / 3),
                        Color.White);
                    spriteBatch.Draw(exit,
                        new Rectangle(cursorX,
                            cursorY + (cursorHeight / 3)*2,
                            cursorWidth,
                            cursorHeight / 3),
                        Color.White);
                    break;
                case SelectMode.LOAD:
                    spriteBatch.Draw(start,
                        new Rectangle(cursorX, 
                            cursorY, 
                            cursorWidth, 
                            cursorHeight / 3),
                        Color.White);
                    spriteBatch.Draw(load,
                        new Rectangle(cursorX - 10,
                            cursorY + cursorHeight / 3 - 10,
                            cursorWidth + 20,
                            cursorHeight / 3 + 20),
                            null,
                        Color.White, 0.07f, Vector2.Zero, SpriteEffects.None, 0f);
                    spriteBatch.Draw(exit,
                        new Rectangle(cursorX,
                            cursorY + (cursorHeight / 3)*2,
                            cursorWidth,
                            cursorHeight / 3),
                        Color.White);
                    break;
                case SelectMode.EXIT:
                    spriteBatch.Draw(start,
                        new Rectangle(cursorX, 
                            cursorY, 
                            cursorWidth, 
                            cursorHeight / 3),
                        Color.White);
                    spriteBatch.Draw(load,
                        new Rectangle(cursorX,
                            cursorY + cursorHeight / 3,
                            cursorWidth,
                            cursorHeight / 3),
                        Color.White);
                    spriteBatch.Draw(exit,
                        new Rectangle(cursorX - 10,
                            cursorY + (cursorHeight / 3)*2 - 10,
                            cursorWidth + 20,
                            cursorHeight / 3 + 20),
                            null,
                        Color.White, 0.07f, Vector2.Zero, SpriteEffects.None, 0f);
                    break;
            }
        }
        public void DrawPause(SpriteBatch spriteBatch)
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
