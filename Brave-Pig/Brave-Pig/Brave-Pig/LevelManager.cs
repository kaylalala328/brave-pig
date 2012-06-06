using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Level_Editor;
using Tile_Engine;
using Brave_Pig.BasicObject;
using Brave_Pig.Monsters;
using Brave_Pig.Character;
namespace Brave_Pig
{

    public static class LevelManager
    {
        #region Declarations
        private static ContentManager Content;
        private static Player player;
        private static int currentLevel;
        private static bool Isleft = true;
        private static List<Enemy> enemies = new List<Enemy>();

        private static Texture2D BackGround;
        private static Texture2D BasicTiles;
        private static Texture2D ForeGround;

    
        #endregion

        #region Properties
        public static int CurrentLevel
        {
            get { return currentLevel; }
        }

        #endregion

        #region Initialization
        public static void Initialize(ContentManager content, Player gamePlayer)
        {
            Content = content;
            player = gamePlayer;
        }
        #endregion

        #region Helper Methods
        private static void checkCurrentCellCode()
        {
            string code = TileMap.CellCodeValue(TileMap.GetCellByPixel(player.WorldCenter));

            if (code == "DEAD")
            {
                //player.Kill();
            }
        }
        #endregion

        #region Public Methods
        public static void LoadLevel(int levelNumber)
        {
            TileMap.LoadMap((System.IO.FileStream)TitleContainer.OpenStream("Content/Maps/MAP" + levelNumber.ToString().PadLeft(3, '0') + ".MAP"));
            BackGround = Content.Load<Texture2D>("Textures/back" + levelNumber.ToString().PadLeft(3, '0'));
            BasicTiles = Content.Load<Texture2D>("Textures/BasicTiles" + levelNumber.ToString().PadLeft(3, '0'));
            ForeGround = Content.Load<Texture2D>("Textures/ForeTiles" + levelNumber.ToString().PadLeft(3, '0'));
            enemies.Clear();

            for (int x = 0; x < TileMap.MapWidth; x++)
            {
                for (int y = 0; y < TileMap.MapHeight; y++)
                {
                    //맵에 enemy 추가.
                    //enemy마다 움직이는 방향은 랜덤으로
                    if (TileMap.CellCodeValue(x, y) == "ENEMY1")
                    { 
                        Enemy Bluemushroom = new Enemy(Content, "파랑버섯", "bluemushroom",64,x,y, 200);
                        enemies.Add(Bluemushroom);
                    }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY2")
                    { }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY3")
                    { }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY4")
                    { }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY5")
                    { }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY6")
                    { }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY7")
                    { }
                    ////////////////////////////////////////////////////

                    //주인공이 어느 지점을 지나고 몇 초 후에 나타남
                    if (TileMap.CellCodeValue(x, y) == "MBOSS1")
                    { }

                    if (TileMap.CellCodeValue(x, y) == "MBOSS2")
                    { }
                    ////////////////////////////////////////////////////

                    //보스맵에 들어가면 있음
                    if (TileMap.CellCodeValue(x, y) == "BOSS")
                    { }

                    //닿으면 캐릭터가 공격당했을때 모션을 하면서 HP 감소
                    if (TileMap.CellCodeValue(x, y) == "DAMAGED")
                    { 
                    }
                    if (TileMap.CellCodeValue(x, y) == "RSTART")
                    {
                        if(Isleft == true)
                            player.WorldLocation = new Vector2(x * TileMap.TileWidth, y * TileMap.TileHeight);
                    }

                    if (TileMap.CellCodeValue(x, y) == "LSTART")
                    {
                        if(Isleft == false)
                            player.WorldLocation = new Vector2(x * TileMap.TileWidth, y * TileMap.TileHeight);
                    }

                    /*
                    if (TileMap.CellCodeValue(x, y) == "START")
                    {
                        player.WorldLocation = new Vector2(
                            x * TileMap.TileWidth,
                            y * TileMap.TileHeight);
                    }
                     */
                }
            }

            currentLevel = levelNumber;
        }

        public static void Update(GameTime gameTime)
        {

            foreach (Enemy e in enemies)
            {
                e.Update(gameTime);
            }
            //Monster update
            //Character Update
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                spriteBatch.Draw(BackGround, Vector2.Zero, Camera.ViewPort, Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
            }
            catch
            { }
            try
            {
                spriteBatch.Draw(BasicTiles, Vector2.Zero, Camera.ViewPort, Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
            }
            catch
            { }
            try
            {
                spriteBatch.Draw(ForeGround, Vector2.Zero, Camera.ViewPort, Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
            }
            catch
            { }
            foreach (Enemy e in enemies)
            {
                e.Draw(spriteBatch);
            }
            //Enermy Draw
            
        }

        #endregion

    }
}
