namespace HKimQGame
{
    partial class Playform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.lblNumOfMove = new System.Windows.Forms.Label();
            this.lblNumOfBox = new System.Windows.Forms.Label();
            this.txtBoxMove = new System.Windows.Forms.TextBox();
            this.txtBoxRemainingBox = new System.Windows.Forms.TextBox();
            this.backgroundPicBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1219, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadGameToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadGameToolStripMenuItem
            // 
            this.loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            this.loadGameToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.loadGameToolStripMenuItem.Text = "Load game";
            this.loadGameToolStripMenuItem.Click += new System.EventHandler(this.loadGameToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(1036, 549);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(72, 66);
            this.btnUp.TabIndex = 2;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(948, 625);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(72, 66);
            this.btnLeft.TabIndex = 3;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(1036, 625);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(72, 66);
            this.btnDown.TabIndex = 4;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(1123, 625);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(72, 66);
            this.btnRight.TabIndex = 5;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            // 
            // lblNumOfMove
            // 
            this.lblNumOfMove.AutoSize = true;
            this.lblNumOfMove.Location = new System.Drawing.Point(945, 58);
            this.lblNumOfMove.Name = "lblNumOfMove";
            this.lblNumOfMove.Size = new System.Drawing.Size(116, 16);
            this.lblNumOfMove.TabIndex = 6;
            this.lblNumOfMove.Text = "Number of moves:";
            // 
            // lblNumOfBox
            // 
            this.lblNumOfBox.Location = new System.Drawing.Point(945, 132);
            this.lblNumOfBox.Name = "lblNumOfBox";
            this.lblNumOfBox.Size = new System.Drawing.Size(213, 16);
            this.lblNumOfBox.TabIndex = 7;
            this.lblNumOfBox.Text = "Number of remaining boxes:";
            // 
            // txtBoxMove
            // 
            this.txtBoxMove.Location = new System.Drawing.Point(948, 88);
            this.txtBoxMove.Name = "txtBoxMove";
            this.txtBoxMove.Size = new System.Drawing.Size(210, 22);
            this.txtBoxMove.TabIndex = 8;
            // 
            // txtBoxRemainingBox
            // 
            this.txtBoxRemainingBox.Location = new System.Drawing.Point(948, 161);
            this.txtBoxRemainingBox.Name = "txtBoxRemainingBox";
            this.txtBoxRemainingBox.Size = new System.Drawing.Size(210, 22);
            this.txtBoxRemainingBox.TabIndex = 9;
            // 
            // backgroundPicBox
            // 
            this.backgroundPicBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.backgroundPicBox.Location = new System.Drawing.Point(21, 58);
            this.backgroundPicBox.Name = "backgroundPicBox";
            this.backgroundPicBox.Size = new System.Drawing.Size(909, 633);
            this.backgroundPicBox.TabIndex = 10;
            this.backgroundPicBox.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // Playform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 716);
            this.Controls.Add(this.backgroundPicBox);
            this.Controls.Add(this.txtBoxRemainingBox);
            this.Controls.Add(this.txtBoxMove);
            this.Controls.Add(this.lblNumOfBox);
            this.Controls.Add(this.lblNumOfMove);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Playform";
            this.Text = "Playform";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Label lblNumOfMove;
        private System.Windows.Forms.Label lblNumOfBox;
        private System.Windows.Forms.TextBox txtBoxMove;
        private System.Windows.Forms.TextBox txtBoxRemainingBox;
        private System.Windows.Forms.PictureBox backgroundPicBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}