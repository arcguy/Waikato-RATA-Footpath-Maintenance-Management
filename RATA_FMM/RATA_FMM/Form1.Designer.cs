namespace RATA_FMM
{
    partial class Form1
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
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBoxData = new System.Windows.Forms.ListBox();
            this.labelMap = new System.Windows.Forms.Label();

            this.listBoxDataLong = new System.Windows.Forms.ListBox();

            this.labelMaintenance = new System.Windows.Forms.Label();
            this.labelReplacement = new System.Windows.Forms.Label();
            this.buttonUpdateResults = new System.Windows.Forms.Button();
            this.pictureBoxFilter = new System.Windows.Forms.PictureBox();
            this.labelFilterResults = new System.Windows.Forms.Label();
            this.textBoxFilterDefects = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonShowAll = new System.Windows.Forms.Button();

            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1441, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.printToolStripMenuItem,
            this.toolStripMenuItem1,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.PrintToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // listBoxData
            // 
            this.listBoxData.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxData.FormattingEnabled = true;
            this.listBoxData.HorizontalScrollbar = true;
            this.listBoxData.Location = new System.Drawing.Point(10, 30);
            this.listBoxData.Name = "listBoxData";
            this.listBoxData.Size = new System.Drawing.Size(500, 680);
            this.listBoxData.TabIndex = 1;
            this.listBoxData.SelectedIndexChanged += new System.EventHandler(this.listBoxData_SelectedIndexChanged);
            // 
            // labelMap
            // 
            this.labelMap.AutoSize = true;
            this.labelMap.Location = new System.Drawing.Point(1343, 438);
            this.labelMap.Name = "labelMap";
            this.labelMap.Size = new System.Drawing.Size(80, 13);
            this.labelMap.TabIndex = 4;
            this.labelMap.Text = "Insert map here";
            // 
            // listBoxDataLong
            // 
            this.listBoxDataLong.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxDataLong.FormattingEnabled = true;
            this.listBoxDataLong.HorizontalScrollbar = true;
            this.listBoxDataLong.Location = new System.Drawing.Point(516, 30);
            this.listBoxDataLong.Name = "listBoxDataLong";
            this.listBoxDataLong.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxDataLong.Size = new System.Drawing.Size(602, 186);
            this.listBoxDataLong.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(811, 560);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Filtering options here?";
            // 
            // buttonUpdateResults
            // 
            this.buttonUpdateResults.Location = new System.Drawing.Point(1295, 203);
            this.buttonUpdateResults.Name = "buttonUpdateResults";
            this.buttonUpdateResults.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateResults.TabIndex = 7;
            this.buttonUpdateResults.Text = "Update";
            this.buttonUpdateResults.UseVisualStyleBackColor = true;
            this.buttonUpdateResults.Click += new System.EventHandler(this.ButtonUpdateResults_Click);
            // 
            // pictureBoxFilter
            // 
            this.pictureBoxFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxFilter.Location = new System.Drawing.Point(1222, 46);
            this.pictureBoxFilter.Name = "pictureBoxFilter";
            this.pictureBoxFilter.Size = new System.Drawing.Size(219, 213);
            this.pictureBoxFilter.TabIndex = 8;
            this.pictureBoxFilter.TabStop = false;
            // 
            // labelFilterResults
            // 
            this.labelFilterResults.AutoSize = true;
            this.labelFilterResults.Location = new System.Drawing.Point(1219, 30);
            this.labelFilterResults.Name = "labelFilterResults";
            this.labelFilterResults.Size = new System.Drawing.Size(67, 13);
            this.labelFilterResults.TabIndex = 9;
            this.labelFilterResults.Text = "Filter Results";
            // 
            // textBoxFilterDefects
            // 
            this.textBoxFilterDefects.Location = new System.Drawing.Point(1354, 73);
            this.textBoxFilterDefects.Name = "textBoxFilterDefects";
            this.textBoxFilterDefects.Size = new System.Drawing.Size(43, 20);
            this.textBoxFilterDefects.TabIndex = 10;
            this.textBoxFilterDefects.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1242, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Show footpaths with ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1242, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "or more defects.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(1235, 62);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(170, 58);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // buttonShowAll
            // 
            this.buttonShowAll.Location = new System.Drawing.Point(1295, 232);
            this.buttonShowAll.Name = "buttonShowAll";
            this.buttonShowAll.Size = new System.Drawing.Size(75, 23);
            this.buttonShowAll.TabIndex = 14;
            this.buttonShowAll.Text = "Show All";
            this.buttonShowAll.UseVisualStyleBackColor = true;
            this.buttonShowAll.Click += new System.EventHandler(this.ButtonShowAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
          
            this.Controls.Add(this.listBoxDataLong);

            this.ClientSize = new System.Drawing.Size(1370, 720);
            this.Controls.Add(this.buttonShowAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFilterDefects);
            this.Controls.Add(this.labelFilterResults);
            this.Controls.Add(this.buttonUpdateResults);
            this.Controls.Add(this.labelReplacement);
            this.Controls.Add(this.labelMaintenance);

            this.Controls.Add(this.labelMap);
            this.Controls.Add(this.listBoxData);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBoxFilter);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ListBox listBoxData;
        private System.Windows.Forms.Label labelMap;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;

        private System.Windows.Forms.ListBox listBoxDataLong;


        private System.Windows.Forms.Label labelMaintenance;
        private System.Windows.Forms.Label labelReplacement;
        private System.Windows.Forms.Button buttonUpdateResults;
        private System.Windows.Forms.PictureBox pictureBoxFilter;
        private System.Windows.Forms.Label labelFilterResults;
        private System.Windows.Forms.TextBox textBoxFilterDefects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonShowAll;
    }
}

