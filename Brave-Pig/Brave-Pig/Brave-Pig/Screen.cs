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
        Texture2D cursor;
        Texture2D menu;

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
        #endregion

        public void Initialize(GraphicsDevice Graphics)
        {
            graphics = Graphics;
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;
            cursorX = width / 3;
            cursorY = height / 2;
            cursorWidth = width / 14;
            cursorHeight = height / 15;
        }
        public void LoadContent(ContentManager content)
        {
            screen = content.Load<Texture2D>("Screen/Screen");
            cursor = content.Load<Texture2D>("Screen/cursor");
            menu = content.Load<Texture2D>("Screen/menu");
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
                        cursorY = cursorY + cursorWidth;
                        break;
                    case SelectMode.LOAD:
                        select = SelectMode.EXIT;
                        cursorY = cursorY + cursorWidth;
                        break;
                    case SelectMode.EXIT:
                        select = SelectMode.START;
                        cursorY = cursorY - 2 * cursorWidth;
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
                        cursorY = cursorY + 2 * cursorWidth;
                        break;
                    case SelectMode.LOAD:
                        select = SelectMode.START;
                        cursorY = cursorY - cursorWidth;
                        break;
                    case SelectMode.EXIT:
                        select = SelectMode.LOAD;
                        cursorY = cursorY - cursorWidth;
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
        /*
         * 시작 화면 출력
         */
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(screen,
                new Rectangle(0, 0, width, height),
                Color.White);
            spriteBatch.Draw(menu,
                new Rectangle(width / 3 + cursorWidth, 
                    height / 2, 
                    width / 3, 
                    height / 3),
                Color.White);

            switch(select)
            {
                case SelectMode.START:
                    spriteBatch.Draw(cursor,
                        new Rectangle(cursorX, cursorY, cursorWidth, cursorHeight),
                        Color.White);
                    break;
                case SelectMode.LOAD:
                    spriteBatch.Draw(cursor,
                        new Rectangle(cursorX, cursorY, cursorWidth, cursorHeight),
                        Color.White);
                    break;
                case SelectMode.EXIT:
                    spriteBatch.Draw(cursor,
                        new Rectangle(cursorX, cursorY, cursorWidth, cursorHeight),
                        Color.White);
                    break;
            }
        }
    }
}
