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
using Brave_Pig.Items;
using Level_Editor;
using Tile_Engine;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
        Potion potion;

        /*
         * GameState를 나타냄
         */
        public enum GameStates
        {
            START,
            MENU,
            MAKE,
            LOAD,
            EXIT,
            PLAY,
            PAUSE,
            WIN,
            GAMEOVER,
        };
        public static GameStates gameState = GameStates.START;

        // 키보드 입력
        public static KeyboardState previousKeyState;
        public static KeyboardState currentKeyState;

        //객체
        Screen screen;
        MainUI mainUI;

        public Game1 ( )
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            screen = new Screen();
            mainUI = new MainUI();
        }

 
        protected override void Initialize ( )
        {
            //화면 크기 설정
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            SoundManager.Initialize();
            ItemManager.Initialize(Content);
            screen.Initialize(GraphicsDevice);
            mainUI.Initialize(GraphicsDevice);
            Camera.WorldRectangle = new Rectangle(0, 0, TileMap.TileWidth * TileMap.MapWidth, TileMap.TileHeight * TileMap.MapHeight);
            Camera.Position = Vector2.Zero;
            Camera.ViewPortWidth = 1280;
            Camera.ViewPortHeight = 720;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent ( )
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TileMap.Initialize(
                Content.Load<Texture2D>(@"Textures\Tile"));
            TileMap.spriteFont =
                Content.Load<SpriteFont>(@"Font\UI font");

            SoundManager.LoadContent(Content);
            SoundManager.PlayBackground("bossSound");

            screen.LoadContent(Content);
            mainUI.LoadContent(Content);
            menual.LoadContent(Content);
            make.LoadContent(Content);
            player = new Player(Content);
            potion = new Potion(25, 10);
            LevelManager.Initialize(Content, player);
            LevelManager.LoadLevel(0);
        }


        protected override void UnloadContent ( )
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update ( GameTime gameTime )
        {
            if ( GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed )
                this.Exit();

            

            currentKeyState = Keyboard.GetState();
            if(gameState == GameStates.PLAY)
                SoundManager.UpdateSound();

            switch (gameState)
            {
                case GameStates.START:
                    SoundManager.PlayBackground("bossSound");
                    screen.Update(gameTime);
                    break;
                case GameStates.MENU:
                    SoundManager.PlayBackground("bossSound");
                    menual.Update(gameTime);
                    break;
                case GameStates.MAKE:
                    SoundManager.PlayBackground("bossSound");
                    make.Update(gameTime);
                    break;
                case GameStates.LOAD:
                    SoundManager.PlayBackground("bossSound");
                    loadGame();
                    Game1.gameState = Game1.GameStates.PLAY;
                    break;
                case GameStates.EXIT:
                    this.Exit();
                    break;
                case GameStates.PLAY:
                    if (previousKeyState.IsKeyDown(Keys.Escape) && currentKeyState.IsKeyUp(Keys.Escape))
                    {
                        gameState = GameStates.PAUSE;
                    }

                    LevelManager.Update(gameTime);

                    if (LevelManager.IsDialog == false)
                    {
                        player.Update(gameTime);
                        mainUI.Update(gameTime, player);
                    }

                    break;
                case GameStates.PAUSE:
                    if (previousKeyState.IsKeyDown(Keys.Escape) && currentKeyState.IsKeyUp(Keys.Escape))
                    {
                        gameState = GameStates.PLAY;
                    }

                    screen.UpdatePause(gameTime, player);
                    break;
                case GameStates.WIN:
                    screen.UpdateReset(gameTime, player);
                    break;
                case GameStates.GAMEOVER:
                    screen.UpdateReset(gameTime, player);
                    break;
            }

            previousKeyState = currentKeyState;

            base.Update(gameTime);
        }
        
        protected override void Draw ( GameTime gameTime )
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch ( gameState )
            {
                case GameStates.START:
                    screen.Draw(spriteBatch);
                    break;
                case GameStates.MENU:
                    menual.Draw(spriteBatch);
                    break;
                case GameStates.MAKE:
                    make.Draw(spriteBatch);
                    break;
                case GameStates.LOAD:
                    break;
                case GameStates.PLAY:
                    TileMap.Draw(spriteBatch);
                    LevelManager.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    mainUI.Draw(spriteBatch);
                    break;
                case GameStates.PAUSE:
                    TileMap.Draw(spriteBatch);
                    LevelManager.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    mainUI.Draw(spriteBatch);
                    screen.DrawPause(spriteBatch);
                    break;
                case GameStates.WIN:
                    screen.DrawWin(spriteBatch);
                    break;
                case GameStates.GAMEOVER:
                    screen.DrawGameOver(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void loadGame ( )
        {
            ItemManager.remove();
            FileStream fs = new FileStream("file1_1.txt", FileMode.OpenOrCreate);
            StreamReader r = new StreamReader(fs);
            LevelManager.CurrentLevel = Convert.ToInt32(r.ReadLine());
            LevelManager.LoadLevel(LevelManager.CurrentLevel);
            player.stat.healPoint = Convert.ToInt32(r.ReadLine());
            player.stat.manaPoint = (float)Convert.ToDouble(r.ReadLine());
            player.stat.damage = Convert.ToInt32(r.ReadLine());
            player.stat.defense = Convert.ToInt32(r.ReadLine());
            player.stat.useSword = Convert.ToInt32(r.ReadLine());
            int X = (int)Convert.ToDouble(r.ReadLine());
            int Y = (int)Convert.ToDouble(r.ReadLine());
            player.WorldLocation = new Vector2(X, Y);
            int swordCnt = Convert.ToInt32(r.ReadLine());
            switch ( swordCnt )
            {
                case 1:
                    ItemManager.gainSword("Blue");
                    break;
                case 2:
                    ItemManager.gainSword("Blue");
                    ItemManager.gainSword("Red");
                    break;
                case 3:
                    ItemManager.gainSword("Blue");
                    ItemManager.gainSword("Red");
                    ItemManager.gainSword("Yellow");
                    break;
            }
            int armorCnt = Convert.ToInt32(r.ReadLine());
            switch ( armorCnt )
            {
                case 1:
                    ItemManager.gainArmor("Armor");
                    break;
                case 2:
                    ItemManager.gainArmor("Armor");
                    ItemManager.gainArmor("Boots");
                    break;
                case 3:
                    ItemManager.gainArmor("Armor");
                    ItemManager.gainArmor("Boots");
                    ItemManager.gainArmor("Shield");
                    break;
            }
            ItemManager.getPotion().setCount(Convert.ToInt32(r.ReadLine()));
            r.Close();
        }
    }
}