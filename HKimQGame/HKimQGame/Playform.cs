/* HKimQGame.cs 
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
using System.Windows.Forms.VisualStyles;
using static HKimQGame.DesignForm;
using static System.Net.WebRequestMethods;

namespace HKimQGame
{
    public partial class Playform : Form
    {

        // Initialize variables
        int row = 0;
        int column = 0;

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
            RED_BOX = 6,
            GREEN_BOX = 7
        }


        // Detect which button is clicked
        Button clickedBtn;

        // Detect which picturebox is clicked
        PictureBox prevClickedPictureBox;
        PictureBox clickedPictureBox;

        // A list that contains pictureboxes
        private PictureBox[,] _pictureBoxArray;


        public Playform()
        {
            InitializeComponent();

            // Default 
            txtBoxMove.Text = numOfMove.ToString();
            txtBoxRemainingBox.Text = numOfRemainingBox.ToString();
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
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Name of the file
                string fileName = openFileDialog.FileName;

                using (StreamReader _reader = new StreamReader(fileName))
                {

                    // Load the game
                    try
                    {
                        // Read two lines
                        row = int.Parse(_reader.ReadLine());
                        column = int.Parse(_reader.ReadLine());

                        int[,] pictureboxData = new int[row, column];

                        // Read the file data
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < column; j++)
                            {

                                _reader.ReadLine();
                                _reader.ReadLine();
                                pictureboxData[i, j] = int.Parse(_reader.ReadLine());

                            }
                        }

                        // Remove exist game in the panel
                        if (_pictureBoxArray != null)
                        {
                            foreach (PictureBox pBox in _pictureBoxArray)
                            {
                                backgroundPanel.Controls.Remove(pBox);
                            }

                            numOfMove = 0;
                            numOfRemainingBox = 0;
                            txtBoxMove.Text = numOfMove.ToString();
                            txtBoxRemainingBox.Text = numOfRemainingBox.ToString();

                        }

                        // Load the game
                        LoadGame(row, column, pictureboxData);

                    }
                    // Display an error message
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error when loading: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

            }

        }

        /// <summary>
        /// A method that loads games
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="pictureboxData"></param>
        /// <returns></returns>
        public Array LoadGame(int row, int column, int[,] pictureboxData)
        {

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
                        Location = new Point(j * TILE_SIZE, i * TILE_SIZE)
                    };

                    // Set image of pictureboxes
                    switch (pictureboxData[i, j])
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
                            picturebox.MouseClick += ChangeBorderStyle;
                            picturebox.Tag = "redBox";
                            numOfRemainingBox++;
                            break;
                        case ((int)TileID.GREEN_BOX):
                            picturebox.Image = Properties.Resources.greenBox;
                            picturebox.Tag = "greenBox";
                            picturebox.MouseClick += ChangeBorderStyle;
                            numOfRemainingBox++;
                            break;
                        default:
                            picturebox.Tag = null;
                            break;

                    }

                 
                    // Display the number of the boxes
                    txtBoxRemainingBox.Text = numOfRemainingBox.ToString();      

                    picturebox.SizeMode = PictureBoxSizeMode.Zoom;
                    _pictureBoxArray[i, j] = picturebox;

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
            return _pictureBoxArray;
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

                if (prevClickedPictureBox == null)
                {
                    clickedPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    prevClickedPictureBox = clickedPictureBox;
                }
                else if (clickedPictureBox != prevClickedPictureBox && prevClickedPictureBox != null)
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
            // initialize variables
            int rowOfClickedPicturebox = 0;
            int columnOfClickedPicturebox = 0;

            // Button that user clicked
            clickedBtn = (Button)sender;

            if (clickedBtn != null)
            {
                // Display error message when user click a button before selecting a box
                if (clickedPictureBox == null)
                {
                    MessageBox.Show($"Select which box to move first.", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    // Count how many times they moved
                    numOfMove++;
                    txtBoxMove.Text = numOfMove.ToString();

                    // Get the index of the selected box
                    for (int i = 0; i < _pictureBoxArray.GetLength(0); i++)
                    {

                        for (int j = 0; j < _pictureBoxArray.GetLength(1); j++)
                        {
                            if (_pictureBoxArray[i, j].Equals(clickedPictureBox))
                            {
                                rowOfClickedPicturebox = i;
                                columnOfClickedPicturebox = j;
                            }
                        }
                    }
                   
                    // A variable that detects empty pictureboxes
                    bool tileMoved = false;

                    switch (clickedBtn.Name)
                    {

                        case "btnUp":

                            // detect if the pictureboxes in the row are empty
                            while (_pictureBoxArray[rowOfClickedPicturebox - 1, columnOfClickedPicturebox].Tag == null)
                            {
                                rowOfClickedPicturebox--;
                                tileMoved = true;
                            }
                            // Put the image and tag of the clicked box in the new picturebox
                            if (tileMoved)
                            {
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].Image = clickedPictureBox.Image;
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].Tag = clickedPictureBox.Tag;
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].BorderStyle = clickedPictureBox.BorderStyle;
                                clickedPictureBox.Image = null;
                                clickedPictureBox.Tag = null;
                                clickedPictureBox.BorderStyle = BorderStyle.None;

                                clickedPictureBox = _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox];
                                clickedPictureBox.BringToFront();
                            }

                            // if the box meets a door, make it invisible
                            if (_pictureBoxArray[rowOfClickedPicturebox - 1, columnOfClickedPicturebox].Tag == "greenDoor" && clickedPictureBox.Tag == "greenBox" || _pictureBoxArray[rowOfClickedPicturebox - 1, columnOfClickedPicturebox].Tag == "redDoor" && clickedPictureBox.Tag == "redBox")
                            {
                                clickedPictureBox.Image = null;
                                clickedPictureBox.Tag = null;
                                clickedPictureBox.BorderStyle = BorderStyle.None;

                                // Check the remaining box
                                CheckRemainingBox(numOfRemainingBox);

                            }
                            break;


                        case "btnDown":

                            // Detect if the pictureboxes in the row are empty
                            while (_pictureBoxArray[rowOfClickedPicturebox + 1, columnOfClickedPicturebox].Tag == null)
                            {
                                rowOfClickedPicturebox++;
                                tileMoved = true;
                            }
                            // Put the image and tag of the clicked box in the new picturebox
                            if (tileMoved)
                            {
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].Image = clickedPictureBox.Image;
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].Tag = clickedPictureBox.Tag;
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].BorderStyle = clickedPictureBox.BorderStyle;
                                clickedPictureBox.Image = null;
                                clickedPictureBox.Tag = null;
                                clickedPictureBox.BorderStyle = BorderStyle.None;

                                clickedPictureBox = _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox];
                                clickedPictureBox.BringToFront();
                            }

                            // if the box meets a door, make it invisible
                            if (_pictureBoxArray[rowOfClickedPicturebox + 1, columnOfClickedPicturebox].Tag == "greenDoor" && clickedPictureBox.Tag == "greenBox" || _pictureBoxArray[rowOfClickedPicturebox + 1, columnOfClickedPicturebox].Tag == "redDoor" && clickedPictureBox.Tag == "redBox")
                            {
                                clickedPictureBox.Image = null;
                                clickedPictureBox.Tag = null;
                                clickedPictureBox.BorderStyle = BorderStyle.None;

                                // Check the remaining box
                                CheckRemainingBox(numOfRemainingBox);

                            }
                            break;


                        case "btnLeft":

                            // Detevt if the pictureboxes in the column are empty
                            while (_pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox - 1].Tag == null)
                            {
                                columnOfClickedPicturebox--;
                                tileMoved = true;
                            }
                            // Put the image and tag of the clicked box in the new picturebox
                            if (tileMoved)
                            {
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].Image = clickedPictureBox.Image;
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].Tag = clickedPictureBox.Tag;
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].BorderStyle = clickedPictureBox.BorderStyle;
                                clickedPictureBox.Image = null;
                                clickedPictureBox.Tag = null;
                                clickedPictureBox.BorderStyle = BorderStyle.None;

                                clickedPictureBox = _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox];
                                clickedPictureBox.BringToFront();
                            }

                            // if the box meets a door, make it invisible
                            if (_pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox - 1].Tag == "greenDoor" && clickedPictureBox.Tag == "greenBox" || _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox - 1].Tag == "redDoor" && clickedPictureBox.Tag == "redBox")
                            {
                                clickedPictureBox.Image = null;
                                clickedPictureBox.Tag = null;
                                clickedPictureBox.BorderStyle = BorderStyle.None;

                                // Check the remaining box
                                CheckRemainingBox(numOfRemainingBox);

                            }
                            break;


                        case "btnRight":

                            // Detect if the pictureboxes in the column are empty
                            while (_pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox + 1].Tag == null)
                            {
                                columnOfClickedPicturebox++;
                                tileMoved = true;
                            }
                            // Put the image and tag of the clicked box in the new picturebox
                            if (tileMoved)
                            {
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].Image = clickedPictureBox.Image;
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].Tag = clickedPictureBox.Tag;
                                _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox].BorderStyle = clickedPictureBox.BorderStyle;
                                clickedPictureBox.Image = null;
                                clickedPictureBox.Tag = null;
                                clickedPictureBox.BorderStyle = BorderStyle.None;

                                clickedPictureBox = _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox];
                                clickedPictureBox.BringToFront();
                            }

                            // if the box meets a door, make it invisible
                            if (_pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox + 1].Tag == "greenDoor" && clickedPictureBox.Tag == "greenBox" || _pictureBoxArray[rowOfClickedPicturebox, columnOfClickedPicturebox + 1].Tag == "redDoor" && clickedPictureBox.Tag == "redBox")
                            {
                                clickedPictureBox.Image = null;
                                clickedPictureBox.Tag = null;
                                clickedPictureBox.BorderStyle = BorderStyle.None;

                                // Check the remaining box
                                CheckRemainingBox(numOfRemainingBox);

                            }
                            break;

                    }

                    

                }

            }

        }

        /// <summary>
        /// Count the number of remaining boxes and reset the form if it's 0
        /// </summary>
        /// <param name="numOfRemainingBox"></param>
        public void CheckRemainingBox(int numOfRemainingBox)
        {
            // Count remaining boxes
            txtBoxRemainingBox.Text = (int.Parse(txtBoxRemainingBox.Text) - 1).ToString();
            numOfRemainingBox = int.Parse(txtBoxRemainingBox.Text);

            // If there's no box
            if (numOfRemainingBox == 0)
            {
                // Display a message
                MessageBox.Show("Congratulations!\nGame end", "Qgame", MessageBoxButtons.OKCancel, MessageBoxIcon.None);

                // Reset the form
                foreach (PictureBox pbox in _pictureBoxArray)
                {
                    backgroundPanel.Controls.Remove(pbox);
                }
                _pictureBoxArray = null;

                numOfMove = 0;
                txtBoxMove.Text = numOfMove.ToString();
            }


        }
    }
}
