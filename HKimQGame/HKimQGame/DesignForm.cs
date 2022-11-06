/* HKimQGame.cs 
 * Assignment 2
 * Revision History 
 * Hyunjin Kim, 2010.10.31: Created 
 * Hyunjin Kim, 2022.11.2: Added code
 * Hyunjin Kim, 2022.11.5: Comments added
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKimQGame
{
    public partial class DesignForm : Form
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

        // An array that has generated pictureboxes
        private PictureBox[,] _pictureBoxArray;

        public DesignForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// A button that generates the boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // Validate text boxes
            if (int.TryParse(txtBoxRow.Text, out int row) && int.TryParse(txtBoxColumn.Text, out int column) && row > 0 && column > 0)
            {
                    // Generate picturebox
                    GeneratePictureBox(row, column);
            }
            else
            {
                // Show error message box when user entered invalid data for row and column
                MessageBox.Show("Please provie valid data for rows and columns\n(Both must be positive integers)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// When the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateClickedButton(object sender, EventArgs e)
        {
            clickedBtn = (Button)sender;
        }


        /// <summary>
        /// Save the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stripMenuSave_Click(object sender, EventArgs e)
        {

            // Set default file location, name and extention
            saveFileDialog.InitialDirectory = @"C:\Users\Desktop";
            saveFileDialog.FileName = "savegame1";
            saveFileDialog.DefaultExt = "qgame";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Name of the file
                string fileName = saveFileDialog.FileName;

                try
                {
                    using (StreamWriter sw = new StreamWriter(fileName))
                    {
                        // Write metadata
                        sw.WriteLine($"{_pictureBoxArray.GetLength(0)}|{_pictureBoxArray.GetLength(1)}");

                        // Write IDs of pictureboxes
                        foreach (PictureBox pBox in _pictureBoxArray)
                        {
                            if (pBox.Image != null)
                            {

                                switch (pBox.Tag)
                                {
                                    case "none":
                                        sw.WriteLine((int)TileID.NONE);
                                        break;

                                    case "wall":
                                        sw.WriteLine((int)TileID.WALL);
                                        break;

                                    case "redDoor":
                                        sw.WriteLine((int)TileID.RED_DOOR);
                                        break;

                                    case "greenDoor":
                                        sw.WriteLine((int)TileID.GREEN_DOOR);
                                        break;

                                    case "redBox":
                                        sw.WriteLine((int)TileID.RED_BOX);
                                        break;

                                    case "greenBox":
                                        sw.WriteLine((int)TileID.GREEN_BOX);
                                        break;
                                }

                            }
                            else
                            {
                                sw.WriteLine((int)TileID.NONE);
                            }
                        }
                    }

                    // Display a message when the file is successfully added
                    MessageBox.Show($"File saved successfully\n{CountPictureBox(_pictureBoxArray)}", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                // Show a message if saving is failed
                catch(Exception ex)
                {
                    MessageBox.Show($"Error when saving: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               

            }
        }

        /// <summary>
        /// A method that creates the pictureboxes
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Array GeneratePictureBox(int row, int column)
        {
            PictureBox[,] pictureBoxArray = new PictureBox[row, column];

            // Remove exist pictureboxes 
            if (_pictureBoxArray != null)
            {
                foreach (PictureBox pBox in _pictureBoxArray)
                {
                    panelGrid.Controls.Remove(pBox);
                }
            }

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

                    picturebox.MouseClick += ClickPictureBox;
                    picturebox.SizeMode = PictureBoxSizeMode.Zoom;
                    _pictureBoxArray[i, j] = picturebox;

                    // Add picture boxes in the panel
                    panelGrid.Controls.Add(_pictureBoxArray[i, j]);

                }

            }

            return pictureBoxArray;
        }


        /// <summary>
        /// When a picturebox is clicked, insert an image and a tag of the clicked button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickPictureBox(object sender, EventArgs e)
        {
            PictureBox pBox = sender as PictureBox;

            if(clickedBtn != null)
            {
                pBox.Image = clickedBtn.Image;
                pBox.Tag = clickedBtn.Tag;
            }
        }

        /// <summary>
        /// A method that counts the number of pictureboxes that have images
        /// </summary>
        /// <param name="pictureBoxArray"></param>
        /// <returns></returns>
        public string CountPictureBox(Array pictureBoxArray)
        {
            // Initialize variables
            int countWall = 0;
            int countDoor = 0;
            int countBox = 0;
            string msg = "";

            // Count the number of wall, door, and box
            foreach (PictureBox pBox in pictureBoxArray)
            {
                if(pBox.Image != null)
                {
                    string pboxTag = pBox.Tag.ToString() ?? "";

                    switch (pboxTag)
                    {
                        case "wall":
                            countWall++;
                            break;

                        case "redDoor":
                        case "greenDoor":
                            countDoor++;
                            break;

                        case "redBox":
                        case "greenBox":
                            countBox++;
                            break;
                    }

                }
            }

            // Return the message
            msg = $"Total number of wall: {countWall}\nTotal number of door: {countDoor}\nTotal number of box: {countBox}";
            return msg;

        }


    }
}
