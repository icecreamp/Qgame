﻿/* HKimQGame.cs 
 * Assignment 2
 * Revision History 
 * Hyunjin Kim, 2010.11.06: Created 
 * Hyunjin Kim, 2022.11.6: Added code
 * Hyunjin Kim, 2022.11.31: Comments added
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HKimQGame.DesignForm;
using static System.Net.WebRequestMethods;

namespace HKimQGame
{
    public partial class Playform : Form
    {
        // Count the number of moves
        int numOfMove = 0;
        int numOfRemainingBox = 0;

        // Size of the pictureboxes
        const int TILE_SIZE = 55;

        // IDs for picturebox items
        public enum TileID
        {
            NONE = 0,
            WALL = 1,
            RED_DOOR = 2,
            GREEN_DOOR = 3,
            RED_BOX = 4,
            GREEN_BOX = 5
        }


        // Detect which button is clicked
        Button clickedBtn;

        // Detect which picturebox is clicked
        PictureBox prevClickedPictureBox;
        PictureBox clickedPictureBox;

        // An array that contains pictureboxes
        private PictureBox[,] _pictureBoxArray;

        
        public Playform()
        {
            InitializeComponent();

            // Default 
            txtBoxMove.Text = numOfMove.ToString();
            txtBoxRemainingBox.Text = "0";
            btnDown.Enabled = false;
            btnUp.Enabled = false;
            btnLeft.Enabled = false;
            btnRight.Enabled = false;
        }

        /// <summary>
        /// Load existing game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set default file location
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Name of the file
                string fileName = openFileDialog.FileName;

                using (StreamReader _reader = new StreamReader(fileName))
                {
                    // file error try catch !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    // Read the file and save in an array
                    int[] fileData = System.IO.File.ReadLines(fileName).Select(line => int.Parse(line)).ToArray();

                    // Get the length from the file
                    int row = fileData[0];
                    int column = fileData[1];

                    // Remove exist game in the panel
                    if (_pictureBoxArray != null)
                    {
                        foreach (PictureBox pBox in _pictureBoxArray)
                        {
                            backgroundPanel.Controls.Remove(pBox);
                        }
                    }

                    // Load the game
                    LoadGame(row, column, fileData);
                    
                }

            }

        }

        /// <summary>
        /// A method that loads games
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public Array LoadGame(int row, int column, int[] fileData)
        {
            // Skip the two lines of the file
            int index = 2;

            // Set length in picturebox array
            PictureBox[,] pictureBoxArray = new PictureBox[row, column];

            _pictureBoxArray = new PictureBox[row, column];

            // Generate pictureboxes
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    PictureBox picturebox = new PictureBox
                    {
                        Name = $"pictureBox{i}",
                        Size = new Size(TILE_SIZE, TILE_SIZE),
                        Location = new Point(i * TILE_SIZE, j * TILE_SIZE)
                    };

                    // Set image of pictureboxes
                    switch (fileData[index])
                    {

                        case ((int)TileID.WALL):
                            picturebox.Image = Properties.Resources.wall;
                            picturebox.Tag = "wall";
                            break;
                        case ((int)TileID.RED_DOOR):
                            picturebox.Image = Properties.Resources.redDoor;
                            picturebox.Tag = "redDoor";
                            break;
                        case ((int)TileID.GREEN_DOOR):
                            picturebox.Image = Properties.Resources.greenDoor;
                            picturebox.Tag = "greenDoor";
                            break;
                        case ((int)TileID.RED_BOX):
                            picturebox.Image = Properties.Resources.redBox;
                            picturebox.Tag = "redBox"; 
                            picturebox.MouseClick += ChangeBorderStyle;
                            numOfRemainingBox++;
                            break;
                        case ((int)TileID.GREEN_BOX):
                            picturebox.Image = Properties.Resources.greenBox;
                            picturebox.Tag = "greenBox";
                            picturebox.BringToFront();
                            picturebox.MouseClick += ChangeBorderStyle;
                            numOfRemainingBox++;
                            break;

                    }

                    txtBoxRemainingBox.Text = numOfRemainingBox.ToString();

                    picturebox.SizeMode = PictureBoxSizeMode.Zoom;
                    _pictureBoxArray[i, j] = picturebox;
                    index++;

                    // Add picture boxes in the panel
                    backgroundPanel.Controls.Add(_pictureBoxArray[i, j]);
                }
            }

            // Enable the textboxes and buttons
            btnDown.Enabled = true;
            btnUp.Enabled = true;
            btnLeft.Enabled = true;
            btnRight.Enabled = true;

            // Return the picturebox array
            return pictureBoxArray;
        }

        /// <summary>
        /// A method that changes border style of a clicked picturebox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeBorderStyle(object sender, EventArgs e)
        {

            // Get clicked picturebox           
            clickedPictureBox = (PictureBox)sender;
            
            // Set borderstyle
            if (clickedPictureBox != null)
            {

                if(prevClickedPictureBox == null)
                {
                    clickedPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    prevClickedPictureBox = clickedPictureBox;
                }
                else if(clickedPictureBox != prevClickedPictureBox && prevClickedPictureBox != null)
                {
                    clickedPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    prevClickedPictureBox.BorderStyle = BorderStyle.None;
                    prevClickedPictureBox = clickedPictureBox;
                }

            }

        }

        /// <summary>
        /// A method that moves the boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MoveBox(object sender, EventArgs e)
        {
            clickedBtn = (Button)sender;

            int rowIndex = 0;
            int columnIndex = 0;

            if (clickedBtn != null)
            {
                // Display error message when user click a button before selecting a box
                if (clickedPictureBox == null)
                {
                    MessageBox.Show($"Select which box to move first.", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // MessageBox.Show($"Congratulations\nGame end", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {

                   if(clickedBtn == btnUp)
                    {
                        clickedPictureBox.Location = new Point(clickedPictureBox.Left, clickedPictureBox.Top - TILE_SIZE);
                    }
                   if(clickedBtn == btnDown)
                    {
                        clickedPictureBox.Location = new Point(clickedPictureBox.Left, clickedPictureBox.Top + TILE_SIZE);
                    }
                    if (clickedBtn == btnLeft)
                    {
                        clickedPictureBox.Location = new Point(clickedPictureBox.Left - TILE_SIZE, clickedPictureBox.Top);
                    }
                    if (clickedBtn == btnRight)
                    {
                        clickedPictureBox.Location = new Point(clickedPictureBox.Left + TILE_SIZE, clickedPictureBox.Top);
                    }
                }

            }


        }

    }
}
