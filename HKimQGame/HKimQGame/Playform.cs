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
        PictureBox prevClickedPictureBox;
        PictureBox clickedPictureBox;

        // An array that contains pictureboxes
        private PictureBox[,] _pictureBoxArray;

        public Playform()
        {
            InitializeComponent();
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
                    // Read the file and save in an array
                    int[] fileData = System.IO.File.ReadLines(fileName).Select(line => int.Parse(line)).ToArray();

                    // Get the length from the file
                    int row = fileData[0];
                    int column = fileData[1];

                    // skip the two lines of the file
                    int index = 2;

                    // Set length in picturebox array
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
                                case 0:
                                    picturebox.Image = null;
                                    picturebox.Tag = null;
                                    break;
                                case 1:
                                    picturebox.Image = Properties.Resources.wall;
                                    picturebox.Tag = "wall";
                                    break;
                                case 2:
                                    picturebox.Image = Properties.Resources.redDoor;
                                    picturebox.Tag = "redDoor";
                                    break;
                                case 3:
                                    picturebox.Image = Properties.Resources.greenDoor;
                                    picturebox.Tag = "greenDoor";
                                    break;
                                case 4:
                                    picturebox.Image = Properties.Resources.redBox;
                                    picturebox.Tag = "redBox";
                                    break;
                                case 5:
                                    picturebox.Image = Properties.Resources.greenBox;
                                    picturebox.Tag = "greenBox";
                                    break;

                            }

                            if(picturebox.Tag == "redBox" || picturebox.Tag == "greenBox")
                            {

                                 picturebox.MouseClick += ChangeBorderStyle;

                            }

                            picturebox.SizeMode = PictureBoxSizeMode.Zoom;
                            _pictureBoxArray[i, j] = picturebox;

                            // Add picture boxes in the panel
                            backgroundPanel.Controls.Add(_pictureBoxArray[i, j]);
                            index++;
                        }
                    }

                    txtBoxMove.Enabled = true;
                    txtBoxRemainingBox.Enabled = true;
                    btnDown.Enabled = true;
                    btnUp.Enabled = true;
                    btnLeft.Enabled = true;
                    btnRight.Enabled = true;
                }

            }

        }

        private void ChangeBorderStyle(object sender, EventArgs e)
        {

            clickedPictureBox = (PictureBox)sender;        

            if (clickedPictureBox != null && clickedPictureBox.Image != null && clickedPictureBox.Tag == "greenBox" || clickedPictureBox.Tag == "redBox")
            {


                if(prevClickedPictureBox != null && prevClickedPictureBox != clickedPictureBox)
                {
                    clickedPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    prevClickedPictureBox.BorderStyle = BorderStyle.None;
                }
                else if(prevClickedPictureBox == clickedPictureBox || prevClickedPictureBox == null)
                {
                    clickedPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    prevClickedPictureBox = clickedPictureBox;
                    clickedPictureBox = null;

                }

                
            }

            //if(prevClickedPictureBox != null && prevClickedPictureBox != clickedPictureBox)
            //{
            //    prevClickedPictureBox.BorderStyle = BorderStyle.None;
            //}

          
            //if (prevClickedPictureBox != null && clickedBtn == null)
            //{

            //    prevClickedPictureBox.BorderStyle = BorderStyle.None;

            //}
            //clickedBtn = (Button)sender;
        }

    }
}
