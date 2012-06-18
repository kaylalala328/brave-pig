using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.IO;
using Tile_Engine;

namespace Level_Editor
{
    public partial class MapEditor : Form
    {
        public Game1 game;                  //타일을 로드하기 위한 경로(filepath)를 설정할 때 필요한 Game1 객체
        public bool Click_Accept = false;   //background와 foreground 파일을 적용하는 버튼 클릭을 bool 변수로 나타낸 것
        public bool scroll = false;         //이거 안하면 타일 안찍힘 ㅡㅡ

        string path;
        string NewPath;

        public MapEditor()
        {
            InitializeComponent();
            //map 파일을 저장하는 경로
            //Application.StartupPath는 프로젝트 실행 경로
            path = Application.StartupPath;
            //Level Editor를 처음 발견한 부분의 index 반환받아서 그 이후를 지움
            NewPath = path.Remove(path.IndexOf("Level Editor"));

            NewPath += "Brave-Pig/Brave-PigContent/Maps/MAP";
        }
        
        public void LoadImageList()
        {
            string filepath = game.Content.RootDirectory;
            filepath += @"\Textures\Tile.png";
            
            Bitmap tileSheet = new Bitmap(filepath);

            for (int y = 0; y < tileSheet.Height / TileMap.TileHeight; y++)
            {
                for (int x = 0; x < tileSheet.Width / TileMap.TileWidth; x++)
                {
                    Bitmap newBitmap = tileSheet.Clone(new
                        System.Drawing.Rectangle(x * TileMap.TileWidth, y * TileMap.TileHeight, TileMap.TileWidth, TileMap.TileHeight), 
                                                 System.Drawing.Imaging.PixelFormat.DontCare);
                }
            }
            FixScrollBarScales();
        }

        private void FixScrollBarScales()
        {
            Camera.ViewPortWidth = pctSurface.Width;
            Camera.ViewPortHeight = pctSurface.Height;
            Camera.Move(Vector2.Zero);

            vScrollBar1.Minimum = 0;
            vScrollBar1.Maximum = Camera.WorldRectangle.Height - Camera.ViewPortHeight;

            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = Camera.WorldRectangle.Width - Camera.ViewPortWidth;
        }

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TileMap.LoadMap(new FileStream(NewPath + cboMapNumber.Items[cboMapNumber.SelectedIndex] + ".MAP", FileMode.Open));
                
            }
            catch
            {
                System.Diagnostics.Debug.Print("Unable to load map file");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.Exit();
            Application.Exit();
        }

        private void MapEditor_Load(object sender, EventArgs e)
        {
            cboCodeValues.Items.Clear();
            cboCodeValues.Items.Add("Enemy1");
            cboCodeValues.Items.Add("Enemy2");
            cboCodeValues.Items.Add("Enemy3");
            cboCodeValues.Items.Add("Enemy4");
            cboCodeValues.Items.Add("Enemy5");
            cboCodeValues.Items.Add("Enemy6");
            cboCodeValues.Items.Add("Enemy7");
            cboCodeValues.Items.Add("MiddleBoss1");
            cboCodeValues.Items.Add("MiddleBoss2");
            cboCodeValues.Items.Add("Boss");
            cboCodeValues.Items.Add("NPC11");
            cboCodeValues.Items.Add("NPC12");
            cboCodeValues.Items.Add("NPC13");
            cboCodeValues.Items.Add("NPC21");
            cboCodeValues.Items.Add("NPC22");
            cboCodeValues.Items.Add("NPC23");
            cboCodeValues.Items.Add("NPC31");
            cboCodeValues.Items.Add("NPC32");
            cboCodeValues.Items.Add("NPC33");

            cboCodeValues.Items.Add("EnemyBlocking");
            cboCodeValues.Items.Add("Blocking");
            cboCodeValues.Items.Add("UpperBlocking");
            
            cboCodeValues.Items.Add("RightStart");
            cboCodeValues.Items.Add("LeftStart");

            for (int x = 0; x < 100; x++)
            {
                cboMapNumber.Items.Add(x.ToString().PadLeft(3, '0'));
            }

            cboMapNumber.SelectedIndex = 0;

            TileMap.EditorMode = true;
        }

        private void MapEditor_Resize(object sender, EventArgs e)
        {
            FixScrollBarScales();
        }

        private void cboCodeValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNewCode.Enabled = false;
            switch (cboCodeValues.Items[cboCodeValues.SelectedIndex].ToString())
            {
                case "Enemy1":
                    txtNewCode.Text = "ENEMY1";
                    game.DrawTile = 3;
                    break;

                case "Enemy2":
                    txtNewCode.Text = "ENEMY2";
                    game.DrawTile = 4;
                    break;

                case "Enemy3":
                    txtNewCode.Text = "ENEMY3";
                    game.DrawTile = 5;
                    break;

                case "Enemy4":
                    txtNewCode.Text = "ENEMY4";
                    game.DrawTile = 6;
                    break;

                case "Enemy5":
                    txtNewCode.Text = "ENEMY5";
                    game.DrawTile = 7;
                    break;

                case "Enemy6":
                    txtNewCode.Text = "ENEMY6";
                    game.DrawTile = 8;
                    break;

                case "Enemy7":
                    txtNewCode.Text = "ENEMY7";
                    game.DrawTile = 9;
                    break;

                case "MiddleBoss1":
                    txtNewCode.Text = "MBOSS1";
                    game.DrawTile = 10;
                    break;

                case "MiddleBoss2":
                    txtNewCode.Text = "MBOSS2";
                    game.DrawTile = 11;
                    break;

                case "Boss":
                    txtNewCode.Text = "BOSS";
                    game.DrawTile = 12;
                    break;
                case "NPC11":
                    txtNewCode.Text = "NPC11";
                    game.DrawTile = 3;
                    break;
                case "NPC12":
                    txtNewCode.Text = "NPC12";
                    game.DrawTile = 4;
                    break;
                case "NPC13":
                    txtNewCode.Text = "NPC13";
                    game.DrawTile = 5;
                    break;
                case "NPC21":
                    txtNewCode.Text = "NPC21";
                    game.DrawTile = 3;
                    break;
                case "NPC22":
                    txtNewCode.Text = "NPC22";
                    game.DrawTile = 4;
                    break;
                case "NPC23":
                    txtNewCode.Text = "NPC23";
                    game.DrawTile = 5;
                    break;
                case "NPC31":
                    txtNewCode.Text = "NPC31";
                    game.DrawTile = 3;
                    break;
                case "NPC32":
                    txtNewCode.Text = "NPC32";
                    game.DrawTile = 4;
                    break;
                case "NPC33":
                    txtNewCode.Text = "NPC33";
                    game.DrawTile = 5;
                    break;
                case "EnemyBlocking":
                    txtNewCode.Text = "EBLOCK";
                    game.DrawTile = 2;
                    break;

                case "Blocking":
                    txtNewCode.Text = "BLOCK";
                    game.DrawTile = 1;
                    break;

                case "UpperBlocking":
                    txtNewCode.Text = "UBLOCK";
                    game.DrawTile = 1;
                    break;

                case "RightStart":
                    txtNewCode.Text = "RSTART";
                    game.DrawTile = 14;
                    break;

                case "LeftStart":
                    txtNewCode.Text = "LSTART";
                    game.DrawTile = 15;
                    break;
            }
        }

        private void txtNewCode_TextChanged(object sender, EventArgs e)
        {
            game.CurrentCodeValue = txtNewCode.Text;
        }

        private void timerGameUpdate_Tick(object sender, EventArgs e)
        {
            if (hScrollBar1.Maximum < 0)
            {
                FixScrollBarScales();
            }
            game.Tick();           
        }

        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileMap.SaveMap(new FileStream(NewPath + cboMapNumber.Items[cboMapNumber.SelectedIndex] + ".MAP", FileMode.Create));
        }

        private void clearMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileMap.ClearMap();
        }

        private void MapEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            game.Exit();
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Click_Accept == false)
            {
                Click_Accept = true;
                button4.Text = "적용 취소";
                BackGroundFileLocation.Text = "back" + cboMapNumber.Text.ToString();
                ForeGroundFileLocation.Text = "BasicTiles" + cboMapNumber.Text.ToString();
            }
            else
            {
                button4.Text = "적용";
                Click_Accept = false;
                BackGroundFileLocation.Text = "";
                ForeGroundFileLocation.Text = "";
            }
        }

        public string BackgroundFile()
        {
            return BackGroundFileLocation.Text;
        }

        public string ForegroundFile()
        {
            return ForeGroundFileLocation.Text;
        }

        private void vScrollBar1_KeyPress(object sender, KeyPressEventArgs e)
        {
            scroll = true;
        }

        private void hScrollBar1_Leave(object sender, EventArgs e)
        {
            scroll = false;
        }
    }
}
