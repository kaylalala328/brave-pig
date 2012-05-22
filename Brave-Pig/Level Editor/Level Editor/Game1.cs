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
using Tile_Engine;

namespace Level_Editor
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// GraphicsDevice
        /// </summary>
        GraphicsDeviceManager graphics;

        /// <summary>
        /// Sprite Batch
        /// </summary>
        SpriteBatch spriteBatch;

        /// <summary>
        /// Integer Pointer
        /// </summary>
        IntPtr drawSurface;

        /// <summary>
        /// Windows Form Variable
        /// </summary>
        System.Windows.Forms.Form parentForm;
        System.Windows.Forms.PictureBox pictureBox;
        System.Windows.Forms.Control gameForm;

        /// <summary>
        /// Layer 변수
        /// </summary>
        public int DrawLayer = 0;
        
        /// <summary>
        /// Layer 타일
        /// </summary>
        public int DrawTile = 0;

        /// <summary>
        /// Back 이미지 파일
        /// </summary>
        Texture2D BackgroundImage;

        /// <summary>
        /// Fore 이미지 파일
        /// </summary>
        Texture2D ForegroundImage;

        MapEditor MapEdit;

        public bool EditingCode = false;
        public string CurrentCodeValue = "";
        public string HoverCodeValue = "";

        public MouseState lastMouseState;
        System.Windows.Forms.VScrollBar vscroll;
        System.Windows.Forms.HScrollBar hscroll;

        public Game1(IntPtr drawSurface,
                    System.Windows.Forms.Form parentForm,
                    System.Windows.Forms.PictureBox surfacePictureBox)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.drawSurface = drawSurface;
            this.parentForm = parentForm;
            this.pictureBox = surfacePictureBox;

            graphics.PreparingDeviceSettings +=
                new EventHandler<PreparingDeviceSettingsEventArgs>(
                graphics_PreparingDeviceSettings);

            Mouse.WindowHandle = drawSurface;

            gameForm =
                System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            gameForm.VisibleChanged += new EventHandler(gameForm_VisibleChanged);
            pictureBox.SizeChanged += new EventHandler(pictureBox_SizeChanged);

            vscroll =
                (System.Windows.Forms.VScrollBar)parentForm.Controls["vScrollBar1"];
            hscroll =
                (System.Windows.Forms.HScrollBar)parentForm.Controls["hScrollBar1"];

            MapEdit = parentForm as MapEditor;

        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {

            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = drawSurface;

        }

        /// <summary>
        /// XNA의 기본 윈도우를 강제로 숨긴다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameForm_VisibleChanged(object sender, EventArgs e)
        {
            if (gameForm.Visible == true)
                gameForm.Visible = false;
        }

        void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            if (parentForm.WindowState != System.Windows.Forms.FormWindowState.Minimized)
            {
                graphics.PreferredBackBufferWidth = pictureBox.Width;
                graphics.PreferredBackBufferHeight = pictureBox.Height;
                Camera.ViewPortWidth = pictureBox.Width;
                Camera.ViewPortHeight = pictureBox.Height;
                graphics.ApplyChanges();
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Camera.ViewPortWidth = pictureBox.Width;
            Camera.ViewPortHeight = pictureBox.Height;

            Camera.WorldRectangle =
                new Rectangle(
                    0,
                    0,
                    TileMap.TileWidth * TileMap.MapWidth,
                    TileMap.TileHeight * TileMap.MapHeight
                );

            TileMap.Initialize(
                Content.Load<Texture2D>(@"Textures\TileA"));

            TileMap.spriteFont =
                Content.Load<SpriteFont>(@"Fonts\Pericles8");

            lastMouseState = Mouse.GetState();

            pictureBox_SizeChanged(null, null);
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
            if (MapEdit.Click_Accept == true)
            {
                try
                {
                    BackgroundImage = Content.Load<Texture2D>(@"Textures\" + MapEdit.BackgroundFile());
                    ForegroundImage = Content.Load<Texture2D>(@"Textures\" + MapEdit.ForegroundFile());
                }
                catch
                {

                }

            }
            Camera.Position = new Vector2(hscroll.Value, vscroll.Value);

            
            MouseState ms = Mouse.GetState();

            
            if ((ms.X > 0) && (ms.Y > 0) &&
                (ms.X < Camera.ViewPortWidth) &&
                (ms.Y < Camera.ViewPortHeight))
            {
                Vector2 mouseLoc = Camera.ScreenToWorld(
                    new Vector2(ms.X, ms.Y));

                if (Camera.WorldRectangle.Contains(
                    (int)mouseLoc.X, (int)mouseLoc.Y))
                {
                    if (ms.LeftButton == ButtonState.Pressed)
                    {
                        TileMap.SetTileAtCell(
                          TileMap.GetCellByPixelX((int)mouseLoc.X),
                          TileMap.GetCellByPixelY((int)mouseLoc.Y),
                          3);
                    }
                    
                    if ((ms.RightButton == ButtonState.Pressed) &&
                        (lastMouseState.RightButton == ButtonState.Released))
                    {
                        if (EditingCode)
                        {
                            TileMap.GetMapSquareAtCell(
                              TileMap.GetCellByPixelX((int)mouseLoc.X),
                              TileMap.GetCellByPixelY((int)mouseLoc.Y)
                            ).CodeValue = CurrentCodeValue;
                        }
                        else
                        {
                            TileMap.GetMapSquareAtCell(
                              TileMap.GetCellByPixelX((int)mouseLoc.X),
                              TileMap.GetCellByPixelY((int)mouseLoc.Y)
                            ).TogglePassable();
                        }
                    }

                    HoverCodeValue =
                            TileMap.GetMapSquareAtCell(
                                TileMap.GetCellByPixelX(
                                    (int)mouseLoc.X),
                                TileMap.GetCellByPixelY(
                                    (int)mouseLoc.Y)).CodeValue;
                }
            }

            lastMouseState = ms;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend);

            try
            {

                spriteBatch.Draw(BackgroundImage, Camera.WorldRectangle, Camera.ViewPort, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            }
            catch
            { 
            }
            try
            {
                spriteBatch.Draw(ForegroundImage, Camera.WorldRectangle, Camera.ViewPort, Color.White,0.0f,Vector2.Zero, SpriteEffects.None,0.3f);
            }
            catch
            {

            }

            TileMap.Draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

    }
}
