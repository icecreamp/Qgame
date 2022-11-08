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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HKimQGame.DesignForm;

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

                try
                {
                    using (StreamReader _reader = new StreamReader(fileName))
                    {
                        if (File.Exists(fileName) && Path.GetExtension(fileName) == ".qgame")
                        {
                            string[] arrayLength = _reader.ReadLine().Split('|');

                            if (int.TryParse(arrayLength[0], out int row) && int.TryParse(arrayLength[1], out int column) && row > 0 && column > 0)
                            {
                                // Generate game
                                GenerateGame(row, column);
                            }
                            else
                            {
                                // Show error message box when user entered invalid data for row and column
                                MessageBox.Show("Please provie valid data for rows and columns\n(Both must be positive integers)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                        // Write metadata
                        //string arrayLength = _reader.ReadLine(fileName).First();

                        //// Write IDs of pictureboxes
                        //foreach (PictureBox pBox in _pictureBoxArray)
                        //{
                        //    if (pBox.Image != null)
                        //    {

                        //        switch (pBox.Tag)
                        //        {
                        //            case "none":
                        //                sw.WriteLine((int)TileID.NONE);
                        //                break;

                        //            case "wall":
                        //                sw.WriteLine((int)TileID.WALL);
                        //                break;

                        //            case "redDoor":
                        //                sw.WriteLine((int)TileID.RED_DOOR);
                        //                break;

                        //            case "greenDoor":
                        //                sw.WriteLine((int)TileID.GREEN_DOOR);
                        //                break;

                        //            case "redBox":
                        //                sw.WriteLine((int)TileID.RED_BOX);
                        //                break;

                        //            case "greenBox":
                        //                sw.WriteLine((int)TileID.GREEN_BOX);
                        //                break;
                        //        }

                        //    }
                        //    else
                        //    {
                        //        sw.WriteLine((int)TileID.NONE);
                        //    }
                        // }
                    }

                }
                // Show a message if saving is failed
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when opening: Please check if it is the right file\nex) (file name).qgame:", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        public Array GenerateGame(int row, int column)
        {
            PictureBox[,] pictureBoxArray = new PictureBox[row, column];

            return pictureBoxArray;
        }

    }
}
