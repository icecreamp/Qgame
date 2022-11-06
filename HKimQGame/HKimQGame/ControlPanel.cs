/* HKimQGame.cs 
 * Assignment 2
 * Revision History 
 * Hyunjin Kim, 2010.10.31: Created 
 * Hyunjin Kim, 2022.11.1: Added code
 * Hyunjin Kim, 2022.11.1: Comments added
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKimQGame
{
    public partial class ControlPanel : Form
    {
        private DesignForm designform;
        private Playform playform;

        public ControlPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A button that leads users to the design form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnDesign_Click(object sender, EventArgs e)
        {
            designform = new DesignForm();
            designform.Show();
        }

        /// <summary>
        /// A button that open a game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            playform = new Playform();
            playform.Show();
        }

        /// <summary>
        /// A button that exits the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
