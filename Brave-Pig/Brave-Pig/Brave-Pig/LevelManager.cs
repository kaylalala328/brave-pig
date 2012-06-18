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
using Brave_Pig.Elements;
using Brave_Pig.Sound;
using Brave_Pig.Items;

namespace Brave_Pig
{

    public static class LevelManager
    {
        #region Declarations
        private static ContentManager Content;
        private static Player player;
        private static int currentLevel;
        private static bool IsleftPortal = false;
        private static List<Enemy> enemies = new List<Enemy>();
        private static List<NPC> npc = new List<NPC>();

        private static Texture2D BackGround;
        private static Texture2D BasicTiles;

        private static Portal LeftPortal;
        private static Portal RightPortal;
        private static bool IsPortal = false;

        public static bool IsDialog = false;

        public static bool monsterDamage = false;

        public static int potioning;
        public static int potionRate;
        #endregion

        #region Properties
        public static int CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
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
            
            IsPortal = false;
            LeftPortal = null;
            RightPortal = null;

            enemies.Clear();
            npc.Clear();

            for (int x = 0; x < TileMap.MapWidth; x++)
            {
                for (int y = 0; y < TileMap.MapHeight; y++)
                {
                    //맵에 enemy 추가.
                    //enemy마다 움직이는 방향은 랜덤으로
                    if (TileMap.CellCodeValue(x, y) == "ENEMY1")
                    {
                        Enemy Bluemushroom = new Enemy(Content, "파랑버섯", "bluemushroom",64,64,x,y, 50, 5);
                        enemies.Add(Bluemushroom);
                    }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY2")
                    {
                        Enemy BrownPig = new Enemy(Content, "갈색돼지", "brownpig", 70, 52, x, y, 100, 15);
                        enemies.Add(BrownPig);
                    }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY3")
                    {
                        Enemy MossMushroom = new Enemy(Content, "이끼버섯", "mossmushroom", 112, 110, x, y, 200, 25);
                        enemies.Add(MossMushroom);
                    }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY4")
                    {
                        Enemy PoisonMushroom = new Enemy(Content, "독버섯", "poisionmushroom", 78, 80, x, y, 300, 30);
                        enemies.Add(PoisonMushroom);
                    }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY5")
                    {
                        Enemy MossMushroom = new Enemy(Content, "불량버섯", "cynicalmushroom", 63, 65, x, y, 400, 40);
                        enemies.Add(MossMushroom);
                    }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY6")
                    {
                        Enemy MossMushroom = new Enemy(Content, "보라버섯", "violetmushroom", 63, 65, x, y, 500, 50);
                        enemies.Add(MossMushroom);
                    }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY7")
                    {
                        Enemy MossMushroom = new Enemy(Content, "빨간돼지", "redpig", 68, 55, x, y, 600, 70);
                        enemies.Add(MossMushroom);
                    }
                    ////////////////////////////////////////////////////
                    if (TileMap.CellCodeValue(x, y) == "NPC11") // 정기
                    {
                        NPC NPC1 = new NPC(Content, 0, "gonggong", 100, 128, x, y);
                        npc.Add(NPC1);
                    }

                    if (TileMap.CellCodeValue(x, y) == "NPC12")
                    {
                        NPC NPC2 = new NPC(Content, 1, "sly", 60, 76, x, y);
                        npc.Add(NPC2);
                    }

                    if (TileMap.CellCodeValue(x, y) == "NPC13")
                    {
                        NPC NPC3 = new NPC(Content, 2, "mos", 80, 80, x, y);
                        npc.Add(NPC3);
                    }
                    if (TileMap.CellCodeValue(x, y) == "NPC21") // 정기
                    {
                        NPC NPC4 = new NPC(Content, 3, "gonggong", 100, 128, x, y);
                        npc.Add(NPC4);
                    }

                    if (TileMap.CellCodeValue(x, y) == "NPC22")
                    {
                        NPC NPC5 = new NPC(Content, 4, "sly", 60, 76, x, y);
                        npc.Add(NPC5);
                    }

                    if (TileMap.CellCodeValue(x, y) == "NPC23")
                    {
                        NPC NPC6 = new NPC(Content, 5, "mos", 80, 80, x, y);
                        npc.Add(NPC6);
                    }
                    if (TileMap.CellCodeValue(x, y) == "NPC31") // 정기
                    {
                        NPC NPC7 = new NPC(Content, 6, "gonggong", 100, 128, x, y);
                        npc.Add(NPC7);
                    }

                    if (TileMap.CellCodeValue(x, y) == "NPC32")
                    {
                        NPC NPC8 = new NPC(Content, 7, "sly", 60, 76, x, y);
                        npc.Add(NPC8);
                    }

                    if (TileMap.CellCodeValue(x, y) == "NPC33")
                    {
                        NPC NPC9 = new NPC(Content, 8, "mos", 80, 80, x, y);
                        npc.Add(NPC9);
                    }
                    ////////////////////////////////////////////////////

                    //주인공이 어느 지점을 지나고 몇 초 후에 나타남
                    if (TileMap.CellCodeValue(x, y) == "MBOSS1")
                    {
                        Enemy Middle = new Enemy(Content, "해골장나르크", "middle1", 82, 89, x, y, 500, 30);
                        enemies.Add(Middle);
                    }

                    if (TileMap.CellCodeValue(x, y) == "MBOSS2")
                    {
                        Enemy Middle2 = new Enemy(Content, "Slime", "middle2", 218, 218, x, y, 1500, 75);
                        enemies.Add(Middle2);
                    }

                    if (TileMap.CellCodeValue(x, y) == "MBOSS3")
                    {
                        Enemy Middle3 = new Enemy(Content, "orangemushroom", "middle3", 120, 140, x, y, 3000, 100);
                        enemies.Add(Middle3);
                    }
                    ////////////////////////////////////////////////////

                    //보스맵에 들어가면 있음
                    if (TileMap.CellCodeValue(x, y) == "BOSS")
                    {
                        Enemy Boss = new Enemy(Content, "Boss", "Boss", 190, 140, x, y, 5000, 150);
                        enemies.Add(Boss);
                    }

                    //닿으면 캐릭터가 공격당했을때 모션을 하면서 HP 감소
                    if (TileMap.CellCodeValue(x, y) == "DAMAGED")
                    { 
                    }
                    if (TileMap.CellCodeValue(x, y) == "RSTART")
                    {
                        RightPortal = new Portal(Content, 100, 145, x, y);
                        IsPortal = true;
                        if(IsleftPortal == true)
                            player.WorldLocation = new Vector2(x * TileMap.TileWidth, y * TileMap.TileHeight - player.height);
                    }

                    if (TileMap.CellCodeValue(x, y) == "LSTART")
                    {
                        IsPortal = true;
                        LeftPortal = new Portal(Content, 100, 145, x, y);
                        if(IsleftPortal == false)
                           player.WorldLocation = new Vector2(x * TileMap.TileWidth, y * TileMap.TileHeight - player.height);
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
            Random nums = new Random();
            IsDialog = false;
            potionRate = nums.Next(0, 10);

            foreach (NPC n in npc)
            {
                n.Update(gameTime, player);
                IsDialog = IsDialog || n.IsDrawWindow;
            }
            
            if (IsPortal)
            {
                if(LeftPortal != null)
                    LeftPortal.Update(gameTime,player);
                if(RightPortal != null)
                    RightPortal.Update(gameTime,player);
            }

            foreach (Enemy e in enemies)
            {
                e.Update(gameTime);

                if (!e.Dead)
                {
                    if(player.CollisionRectangle.Intersects(e.CollisionRectangle))
                    {
                        if(player.WorldCenter.Y < e.WorldLocation.Y)
                        {
                            player.push();
                        }
                        else
                        {
                            player.damaged(e.Damege);
                        }
                    }
                    
                    if (player.CollisionRectangle2.Intersects(e.CollisionRectangle))
                    {
                        if (player.attackingfilp())
                        {
                            e.PlayAnimation("damage");

                            if (!player.anima())
                            {
                                monsterDamage = false;
                            }

                            if (monsterDamage == false)
                            {
                                e.healthPoint = e.healthPoint - player.getAttack();
                                monsterDamage = true;
                            }

                            if (e.healthPoint <= 0)
                            {
                                e.PlayAnimation("dead");
                                e.Dead = true;
                                if (potionRate >= 9)
                                {
                                    potioning = ItemManager.getPotion().getCount();
                                    ItemManager.getPotion().setCount(potioning + 1);
                                }
                            }
                            else
                            {
                                e.Pushed();
                            }
                        }
                    }
                    else if (player.CollisionRectangle3.Intersects(e.CollisionRectangle))
                    {
                        if (player.attacking())
                        {
                            e.PlayAnimation("damage");

                            if (!player.anima())
                            {
                                monsterDamage = false;
                            }

                            if (monsterDamage == false)
                            {
                                e.healthPoint = e.healthPoint - player.getAttack();
                                monsterDamage = true;
                            }

                            if (e.healthPoint <= 0)
                            {
                                e.PlayAnimation("dead");
                                e.Dead = true;
                                if (potionRate >= 9)
                                {
                                    potioning = ItemManager.getPotion().getCount();
                                    ItemManager.getPotion().setCount(potioning + 1);
                                }
                            }
                            else
                            {
                                e.Pushed();
                            }
                        }
                    }
                   
                    if (player.CollisionRectangle4.Intersects(e.CollisionRectangle))
                    {
                        if (player.skill2ing())
                        {
                            e.PlayAnimation("damage");
                            e.healthPoint = e.healthPoint - player.getAttack();
                            if (e.healthPoint <= 0)
                            {
                                e.PlayAnimation("dead");
                                e.Dead = true;
                                if (potionRate >= 9)
                                {
                                    potioning = ItemManager.getPotion().getCount();
                                    ItemManager.getPotion().setCount(potioning + 1);
                                }
                            }
                            else
                            {
                                e.Pushed();
                            }
                        }
                    }
                    
                    if (player.CollisionRectangle5.Intersects(e.CollisionRectangle))
                    {
                        if (player.skill3ing())
                        {
                            e.PlayAnimation("damage");
                            e.healthPoint = e.healthPoint - player.getAttack();
                            if (e.healthPoint <= 0)
                            {
                                e.PlayAnimation("dead");
                                e.Dead = true;
                                if (potionRate >= 9)
                                {
                                    potioning = ItemManager.getPotion().getCount();
                                    ItemManager.getPotion().setCount(potioning + 1);
                                }
                            }
                            else
                            {
                                e.Pushed();
                            }
                        }
                    }
                }
            }
            if (LeftPortal != null)
            {
                if (LeftPortal.IsWarp)
                {
                    IsleftPortal = true;
                    currentLevel--;
                    LoadLevel(currentLevel);

                }
            }

            if (RightPortal != null)
            {
                if (RightPortal.IsWarp)
                {
                    IsleftPortal = false;
                    currentLevel++;
                    LoadLevel(currentLevel);
                }
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
            if (IsPortal)
            {
                if (LeftPortal != null)
                    LeftPortal.Draw(spriteBatch);
                if (RightPortal != null)
                    RightPortal.Draw(spriteBatch);
            }
            foreach (Enemy e in enemies)
            {
                e.Draw(spriteBatch);
            }
            foreach (NPC n in npc)
            {
                n.Draw(spriteBatch);
            }
            //Enermy Draw

            
            
        }

        #endregion

    }
}
