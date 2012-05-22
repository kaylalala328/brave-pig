﻿using System;
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
        public Game1 game;
        public bool Click_Accept = false;

        public MapEditor()
        {

            InitializeComponent();             
        }
        
        public void LoadImageList()
        {
            string filepath = game.Content.RootDirectory;
            filepath += @"\Textures\TileA.png";
            
            Bitmap tileSheet = new Bitmap(filepath);
            //int tilecount = 0;
            for (int y = 0; y < tileSheet.Height / TileMap.TileHeight; y++)
            {
                for (int x = 0; x < tileSheet.Width / TileMap.TileWidth; x++)
                {
                    Bitmap newBitmap = tileSheet.Clone(new
                        System.Drawing.Rectangle(
                            x * TileMap.TileWidth,
                            y * TileMap.TileHeight,
                            TileMap.TileWidth,
                            TileMap.TileHeight),
                            System.Drawing.Imaging.PixelFormat.DontCare);
                    /*
                    imgListTiles.Images.Add(newBitmap);
                    string itemName = "";
                    if (tilecount == 0)
                    {
                        itemName = "Empty";
                    }
                    if (tilecount == 1)
                    {
                        itemName = "White";
                    }
                    listTiles.Items.Add(new
                        ListViewItem(itemName, tilecount++));
                    */
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
                TileMap.LoadMap(new FileStream(
                    Application.StartupPath + @"\MAP" +
                    cboMapNumber.Items[cboMapNumber.SelectedIndex] + ".MAP",
                    FileMode.Open));
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

            cboCodeValues.Items.Add("EnemyBlocking");
            
            cboCodeValues.Items.Add("RightStart");
            cboCodeValues.Items.Add("LeftStart");

            cboCodeValues.Items.Add("Port");

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
                    break;

                case "Enemy2":
                    txtNewCode.Text = "ENEMY2";
                    break;

                case "Enemy3":
                    txtNewCode.Text = "ENEMY3";
                    break;

                case "Enemy4":
                    txtNewCode.Text = "ENEMY4";
                    break;

                case "Enemy5":
                    txtNewCode.Text = "ENEMY5";
                    break;

                case "Enemy6":
                    txtNewCode.Text = "ENEMY6";
                    break;

                case "Enemy7":
                    txtNewCode.Text = "ENEMY7";
                    break;

                case "MiddleBoss1":
                    txtNewCode.Text = "MBOSS1";
                    break;
                case "MiddleBoss2":
                    txtNewCode.Text = "MBOSS2";
                    break;
                case "Boss":
                    txtNewCode.Text = "BOSS";
                    break;

                case "Lethal":
                    txtNewCode.Text = "DAMAGED";
                    break;

                case "EnemyBlocking":
                    txtNewCode.Text = "BLOCK";
                    break;

                case "RightStart":
                    txtNewCode.Text = "RSTART";
                    break;

                case "LeftStart":
                    txtNewCode.Text = "LSTART";
                    break;

                case "Port":
                    txtNewCode.Text = "PORT";
                    break;
            }
        }
        /*
        private void listTiles_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (listTiles.SelectedIndices.Count > 0)
            {
                game.DrawTile =
                    listTiles.SelectedIndices[0];
            }
        }
        */
        private void radioPassable_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPassable.Checked)
            {
                game.EditingCode = false;
            }
            else
            {
                game.EditingCode = true;
            }
        }

        private void radioCode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPassable.Checked)
            {
                game.EditingCode = false;
            }
            else
            {
                game.EditingCode = true;
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

            if (game.HoverCodeValue != lblCurrentCode.Text)
                lblCurrentCode.Text = game.HoverCodeValue;
        }

        private void saveMapToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            TileMap.SaveMap(new FileStream(
                Application.StartupPath + @"\MAP" +
                cboMapNumber.Items[cboMapNumber.SelectedIndex] + ".MAP",
                FileMode.Create));
        }

        private void clearMapToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            TileMap.ClearMap();
        }

        private void MapEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            game.Exit();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BackGroundFileLocation.Text = openFileDialog1.SafeFileName.Remove(openFileDialog1.SafeFileName.Length-4);
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ForeGroundFileLocation.Text = openFileDialog1.SafeFileName.Remove(openFileDialog1.SafeFileName.Length - 4);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Click_Accept == false)
            {
                Click_Accept = true;
                button4.Text = "적용 취소";
            }
            else
            {
                button4.Text = "적용";
                Click_Accept = false;
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
    }
}
