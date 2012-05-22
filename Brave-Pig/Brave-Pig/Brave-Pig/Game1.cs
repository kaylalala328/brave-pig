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
using Brave_Pig.Sound;
using Brave_Pig.UI;
using Brave_Pig.BasicObject;
using Brave_Pig.Character;
using Brave_Pig.UI;

namespace Brave_Pig
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;

        Texture2D background;

        /*
         * GameState를 나타냄
         */
        public enum GameStates
        {
            START,
            PLAY,
            GAMEOVER,
            LOAD,
            WIN,
            EXIT
        };
        public static GameStates gameState = GameStates.START;

        // 키보드 입력
        public static KeyboardState previousKeyState;
        public static KeyboardState currentKeyState;

        //객체
        Screen screen;
        SoundManager sound;
        MainUI mainUI;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            screen = new Screen();
            sound = new SoundManager();
            mainUI = new MainUI();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //화면 크기 설정
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            screen.Initialize(GraphicsDevice);
            sound.Initialize();
            sound.LoadContent(Content);
            mainUI.Initialize(GraphicsDevice);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            screen.LoadContent(Content);
            mainUI.LoadContent(Content);
            //테스트용
            background = Content.Load<Texture2D>("Backgrounds/Bg04");
            player = new Player(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            sound.PlayBackground();

            if ( gameState == GameStates.START )
            {
                screen.Update(gameTime);
            }
            else
            {
                currentKeyState = Keyboard.GetState();
                player.Update(gameTime);
                mainUI.Update(gameTime, player);
                previousKeyState = currentKeyState;
            }

            if ( gameState == GameStates.EXIT )
            {
                this.Exit();
            }


            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
          
            spriteBatch.Begin();
            if ( gameState == GameStates.START )
                screen.Draw(spriteBatch);
            if(gameState == GameStates.PLAY)
            {
                // 테스트코드 배경화면
                spriteBatch.Draw(background,
                    new Rectangle(0, 0, 
                        GraphicsDevice.Viewport.Width, 
                        GraphicsDevice.Viewport.Height),
                    Color.White);
                
                player.Draw(spriteBatch);
                mainUI.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
