/* HKimQGame.cs 
 * Assignment 2
 * Revision History 
 * Hyunjin Kim, 2010.11.06: Created 
 * Hyunjin Kim, 2022.11.6: Added code
 * Hyunjin Kim, 2022.11.: Comments added
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
            txtBoxMove.Text = "0";
            txtBoxRemainingBox.Text = "0";
            txtBoxMove.Enabled = false;
            txtBoxRemainingBox.Enabled = false;
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
            // Set default file location, name and extention
            openFileDialog.InitialDirectory = @"C:\Users\Desktop";
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
            // skip the two lines of the file
            int index = 2;

            // Set length in picturebox array
            PictureBox[,] pictureBoxArray = new PictureBox[row, column];

            _pictureBoxArray = new PictureBox[row, column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    // Generate pictureboxes
                    PictureBox picturebox = new PictureBox
                    {
                        Name = $"pictureBox{i}",
                        Size = new Size(TILE_SIZE, TILE_SIZE),
                        Location = new Point(i * TILE_SIZE, j * TILE_SIZE)
                    };

                    // Set image of pictureboxes
                    switch (fileData[index])
                    {
                        case ((int)TileID.NONE):
                            picturebox.Image = null;
                            picturebox.Tag = null;
                            break;
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
                            break;
                        case ((int)TileID.GREEN_BOX):
                            picturebox.Image = Properties.Resources.greenBox;
                            picturebox.Tag = "greenBox";
                            break;

                    }

                    // Add a click event to red box and green box
                    if (picturebox.Tag == "redBox" || picturebox.Tag == "greenBox")
                    {

                        picturebox.MouseClick += ChangeBorderStyle;

                    }

                    picturebox.SizeMode = PictureBoxSizeMode.Zoom;
                    _pictureBoxArray[i, j] = picturebox;
                    index++;

                    // Add picture boxes in the panel
                    backgroundPanel.Controls.Add(_pictureBoxArray[i, j]);
                }
            }

            // Enable the textboxes and buttons
            txtBoxMove.Enabled = true;
            txtBoxRemainingBox.Enabled = true;
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

        public void MoveBox(object sender, EventArgs e)
        {
            //clickedPictureBox = (PictureBox)sender;
            clickedPictureBox.MouseClick += MoveBox;

            int rowIndex;
            int columnIndex;
            int changeImg;

            if (clickedBtn != null)
            {
                for (int i = 0; i < _pictureBoxArray.GetLength(0); i++)
                {
                    for (int j = 0; j < _pictureBoxArray.GetLength(1); j++)
                    {
                        if (_pictureBoxArray[i, j].Equals(clickedBtn))
                        {
                            rowIndex = i;
                            columnIndex = j;
                            changeImg = rowIndex - _pictureBoxArray.GetLength(0);

                            if (clickedBtn == btnUp)
                            {

                                _pictureBoxArray[changeImg, columnIndex].Image = Properties.Resources.redBox;
                                clickedPictureBox.Image = null;
                            }
                        }
                    }
                }
            }


        }

    }
}
