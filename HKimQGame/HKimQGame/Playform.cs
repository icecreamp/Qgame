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
        const int TILE_SIZE = 50;

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

        // An array that contains pictureboxes
        private PictureBox[,] _pictureBoxArray;

        public Playform()
        {
            InitializeComponent();
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
            openFileDialog.DefaultExt = "qgame";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Name of the file
                string fileName = openFileDialog.FileName;

                using (StreamReader _reader = new StreamReader(fileName))
                    {

                    int[] fileData = System.IO.File.ReadLines(fileName).Select(line => int.Parse(line)).ToArray();

                    int row = fileData[0];
                    int column = fileData[1];
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
                                BorderStyle = BorderStyle.Fixed3D,
                                Location = new Point(i * TILE_SIZE, j * TILE_SIZE)
                            };

                            switch (fileData[i])
                            {
                                case 0:
                                    picturebox.Image = null;
                                    break;
                                case 1:
                                    picturebox.Image = Properties.Resources.wall;
                                    break;
                                case 2:
                                    picturebox.Image = Properties.Resources.redDoor;
                                    break;
                                case 3:
                                    picturebox.Image = Properties.Resources.greenDoor;
                                    break;
                                case 4:
                                    picturebox.Image = Properties.Resources.redBox;
                                    break;
                                case 5:
                                    picturebox.Image = Properties.Resources.greenBox;
                                    break;

                            }

                            //picturebox.MouseClick += ClickPictureBox;
                            picturebox.SizeMode = PictureBoxSizeMode.Zoom;
                            _pictureBoxArray[i, j] = picturebox;

                            // Add picture boxes in the panel
                            backgroundPanel.Controls.Add(_pictureBoxArray[i, j]);
                        }
                    }
                    
                }

            }

        }

    }
}
