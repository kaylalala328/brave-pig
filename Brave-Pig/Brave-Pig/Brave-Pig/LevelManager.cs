using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private static Vector2 respawnLocation;

        private static List<Enemy> enemies = new List<Enemy>();
        #endregion

        #region Properties
        public static int CurrentLevel
        {
            get { return currentLevel; }
        }

        public static Vector2 RespawnLocation
        {
            get { return respawnLocation; }
            set { respawnLocation = value; }
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

            enemies.Clear();

            for (int x = 0; x < TileMap.MapWidth; x++)
            {
                for (int y = 0; y < TileMap.MapHeight; y++)
                {
                    //맵에 enemy 추가.
                    //enemy마다 움직이는 방향은 랜덤으로
                    if (TileMap.CellCodeValue(x, y) == "ENEMY1")
                    { }

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
                    { }

                    //enemy가 움직이는 범위 설정
                    //이 타일을 찍는 
                    if (TileMap.CellCodeValue(x, y) == "EBLOCK")
                    { }

                    if (TileMap.CellCodeValue(x, y) == "BLOCK")
                    { 

                    }

                    if (TileMap.CellCodeValue(x, y) == "RSTART")
                    {
                        //player.WorldLocation = new Vector2(x * TileMap.TileWidth, y * TileMap.TileHeight);
                    }

                    if (TileMap.CellCodeValue(x, y) == "LSTART")
                    { }

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
            respawnLocation = player.WorldLocation;
        }

        public static void Update(GameTime gameTime)
        {
            /*
            if (!player.Dead)
            {
                checkCurrentCellCode(); 

                
                for (int x = enemies.Count - 1; x >= 0; x--)
                {
                    enemies[x].Update(gameTime);
                    if (!enemies[x].Dead)
                    {
                        if (player.CollisionRectangle.Intersects(
                            enemies[x].CollisionRectangle))
                        {
                            if (player.WorldCenter.Y < enemies[x].WorldLocation.Y)
                            {
                                player.Jump();
                                player.Score += 5;
                                enemies[x].PlayAnimation("die");
                                enemies[x].Dead = true; ;
                            }
                            else
                            {
                                player.Kill();
                            }
                        }
                    }
                    else
                    {
                        if (!enemies[x].Enabled)
                        {
                            enemies.RemoveAt(x);
                        }
                    }
                }
            }
           */
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            /*
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);
            */
        }

        #endregion

    }
}
