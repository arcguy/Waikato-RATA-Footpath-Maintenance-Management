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
            this.listBoxData = new System.Windows.Forms.ListBox();
            this.listBoxDataLong = new System.Windows.Forms.ListBox();
            this.buttonUpdateResults = new System.Windows.Forms.Button();
            this.pictureBoxFilter = new System.Windows.Forms.PictureBox();
            this.labelFilterResults = new System.Windows.Forms.Label();
            this.textBoxFilterFaults = new System.Windows.Forms.TextBox();
            this.filterLabel4 = new System.Windows.Forms.Label();
            this.filterLabel5 = new System.Windows.Forms.Label();
            this.pictureBoxFaults = new System.Windows.Forms.PictureBox();
            this.buttonShowAll = new System.Windows.Forms.Button();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filterLabel2 = new System.Windows.Forms.Label();
            this.filterLabel1 = new System.Windows.Forms.Label();
            this.textBoxFilterCondition = new System.Windows.Forms.TextBox();
            this.pictureBoxCondition = new System.Windows.Forms.PictureBox();
            this.filterLabel3 = new System.Windows.Forms.Label();
            this.pictureBoxTown = new System.Windows.Forms.PictureBox();
            this.filterLabel6 = new System.Windows.Forms.Label();
            this.comboBoxTown = new System.Windows.Forms.ComboBox();
            this.pictureBoxSort = new System.Windows.Forms.PictureBox();
            this.labelAlgorithm = new System.Windows.Forms.Label();
            this.labelHealthMin = new System.Windows.Forms.Label();
            this.labelHealthMax = new System.Windows.Forms.Label();
            this.textBoxHealthMin = new System.Windows.Forms.TextBox();
            this.textBoxHealthMax = new System.Windows.Forms.TextBox();
            this.textBoxSchoolMax = new System.Windows.Forms.TextBox();
            this.textBoxSchoolMin = new System.Windows.Forms.TextBox();
            this.labelSchoolMax = new System.Windows.Forms.Label();
            this.labelSchoolMin = new System.Windows.Forms.Label();
            this.textBoxServiceMax = new System.Windows.Forms.TextBox();
            this.textBoxServiceMin = new System.Windows.Forms.TextBox();
            this.labelServiceMax = new System.Windows.Forms.Label();
            this.labelServiceMin = new System.Windows.Forms.Label();
            this.labelZones = new System.Windows.Forms.Label();
            this.labelPathRatings = new System.Windows.Forms.Label();
            this.labelRating1 = new System.Windows.Forms.Label();
            this.textBoxRating1 = new System.Windows.Forms.TextBox();
            this.textBoxRating2 = new System.Windows.Forms.TextBox();
            this.labelRating2 = new System.Windows.Forms.Label();
            this.textBoxRating3 = new System.Windows.Forms.TextBox();
            this.labelRating3 = new System.Windows.Forms.Label();
            this.textBoxRating4 = new System.Windows.Forms.TextBox();
            this.labelRating4 = new System.Windows.Forms.Label();
            this.textBoxRating5 = new System.Windows.Forms.TextBox();
            this.labelRating5 = new System.Windows.Forms.Label();
            this.buttonUpdateAlgorithm = new System.Windows.Forms.Button();
            this.pictureBoxZones = new System.Windows.Forms.PictureBox();
            this.pictureBoxPathRatings = new System.Windows.Forms.PictureBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFaults)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPathRatings)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxData
            // 
            this.listBoxData.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxData.FormattingEnabled = true;
            this.listBoxData.HorizontalScrollbar = true;
            this.listBoxData.Location = new System.Drawing.Point(10, 43);
            this.listBoxData.Name = "listBoxData";
            this.listBoxData.Size = new System.Drawing.Size(485, 667);
            this.listBoxData.TabIndex = 1;
            this.listBoxData.SelectedIndexChanged += new System.EventHandler(this.listBoxData_SelectedIndexChanged);
            // 
            // listBoxDataLong
            // 
            this.listBoxDataLong.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxDataLong.FormattingEnabled = true;
            this.listBoxDataLong.HorizontalScrollbar = true;
            this.listBoxDataLong.Location = new System.Drawing.Point(501, 43);
            this.listBoxDataLong.Name = "listBoxDataLong";
            this.listBoxDataLong.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxDataLong.Size = new System.Drawing.Size(549, 329);
            this.listBoxDataLong.TabIndex = 5;
            // 
            // buttonUpdateResults
            // 
            this.buttonUpdateResults.Location = new System.Drawing.Point(1078, 267);
            this.buttonUpdateResults.Name = "buttonUpdateResults";
            this.buttonUpdateResults.Size = new System.Drawing.Size(85, 23);
            this.buttonUpdateResults.TabIndex = 7;
            this.buttonUpdateResults.Text = "Update";
            this.buttonUpdateResults.UseVisualStyleBackColor = true;
            this.buttonUpdateResults.Click += new System.EventHandler(this.ButtonUpdateResults_Click);
            // 
            // pictureBoxFilter
            // 
            this.pictureBoxFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxFilter.Location = new System.Drawing.Point(1056, 43);
            this.pictureBoxFilter.Name = "pictureBoxFilter";
            this.pictureBoxFilter.Size = new System.Drawing.Size(220, 285);
            this.pictureBoxFilter.TabIndex = 8;
            this.pictureBoxFilter.TabStop = false;
            // 
            // labelFilterResults
            // 
            this.labelFilterResults.AutoSize = true;
            this.labelFilterResults.Location = new System.Drawing.Point(1075, 44);
            this.labelFilterResults.Name = "labelFilterResults";
            this.labelFilterResults.Size = new System.Drawing.Size(67, 13);
            this.labelFilterResults.TabIndex = 9;
            this.labelFilterResults.Text = "Filter Results";
            // 
            // textBoxFilterFaults
            // 
            this.textBoxFilterFaults.Location = new System.Drawing.Point(1198, 139);
            this.textBoxFilterFaults.Name = "textBoxFilterFaults";
            this.textBoxFilterFaults.Size = new System.Drawing.Size(43, 20);
            this.textBoxFilterFaults.TabIndex = 10;
            this.textBoxFilterFaults.Text = "0";
            // 
            // filterLabel4
            // 
            this.filterLabel4.AutoSize = true;
            this.filterLabel4.Location = new System.Drawing.Point(1086, 142);
            this.filterLabel4.Name = "filterLabel4";
            this.filterLabel4.Size = new System.Drawing.Size(106, 13);
            this.filterLabel4.TabIndex = 11;
            this.filterLabel4.Text = "Show footpaths with ";
            // 
            // filterLabel5
            // 
            this.filterLabel5.AutoSize = true;
            this.filterLabel5.Location = new System.Drawing.Point(1086, 167);
            this.filterLabel5.Name = "filterLabel5";
            this.filterLabel5.Size = new System.Drawing.Size(73, 13);
            this.filterLabel5.TabIndex = 12;
            this.filterLabel5.Text = "or more faults.";
            // 
            // pictureBoxFaults
            // 
            this.pictureBoxFaults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxFaults.Location = new System.Drawing.Point(1078, 132);
            this.pictureBoxFaults.Name = "pictureBoxFaults";
            this.pictureBoxFaults.Size = new System.Drawing.Size(170, 60);
            this.pictureBoxFaults.TabIndex = 13;
            this.pictureBoxFaults.TabStop = false;
            // 
            // buttonShowAll
            // 
            this.buttonShowAll.Location = new System.Drawing.Point(1169, 267);
            this.buttonShowAll.Name = "buttonShowAll";
            this.buttonShowAll.Size = new System.Drawing.Size(85, 23);
            this.buttonShowAll.TabIndex = 14;
            this.buttonShowAll.Text = "Show All";
            this.buttonShowAll.UseVisualStyleBackColor = true;
            this.buttonShowAll.Click += new System.EventHandler(this.ButtonShowAll_Click);
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
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.PrintToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1448, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filterLabel2
            // 
            this.filterLabel2.AutoSize = true;
            this.filterLabel2.Location = new System.Drawing.Point(1086, 95);
            this.filterLabel2.Name = "filterLabel2";
            this.filterLabel2.Size = new System.Drawing.Size(45, 13);
            this.filterLabel2.TabIndex = 17;
            this.filterLabel2.Text = "rating of";
            // 
            // filterLabel1
            // 
            this.filterLabel1.AutoSize = true;
            this.filterLabel1.Location = new System.Drawing.Point(1086, 70);
            this.filterLabel1.Name = "filterLabel1";
            this.filterLabel1.Size = new System.Drawing.Size(158, 13);
            this.filterLabel1.TabIndex = 16;
            this.filterLabel1.Text = "Show footpaths with a condition";
            // 
            // textBoxFilterCondition
            // 
            this.textBoxFilterCondition.Location = new System.Drawing.Point(1137, 92);
            this.textBoxFilterCondition.Name = "textBoxFilterCondition";
            this.textBoxFilterCondition.Size = new System.Drawing.Size(43, 20);
            this.textBoxFilterCondition.TabIndex = 15;
            this.textBoxFilterCondition.Text = "0";
            // 
            // pictureBoxCondition
            // 
            this.pictureBoxCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCondition.Location = new System.Drawing.Point(1078, 60);
            this.pictureBoxCondition.Name = "pictureBoxCondition";
            this.pictureBoxCondition.Size = new System.Drawing.Size(170, 60);
            this.pictureBoxCondition.TabIndex = 18;
            this.pictureBoxCondition.TabStop = false;
            // 
            // filterLabel3
            // 
            this.filterLabel3.AutoSize = true;
            this.filterLabel3.Location = new System.Drawing.Point(1186, 95);
            this.filterLabel3.Name = "filterLabel3";
            this.filterLabel3.Size = new System.Drawing.Size(55, 13);
            this.filterLabel3.TabIndex = 19;
            this.filterLabel3.Text = "or greater.";
            // 
            // pictureBoxTown
            // 
            this.pictureBoxTown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxTown.Location = new System.Drawing.Point(1078, 198);
            this.pictureBoxTown.Name = "pictureBoxTown";
            this.pictureBoxTown.Size = new System.Drawing.Size(170, 60);
            this.pictureBoxTown.TabIndex = 20;
            this.pictureBoxTown.TabStop = false;
            // 
            // filterLabel6
            // 
            this.filterLabel6.AutoSize = true;
            this.filterLabel6.Location = new System.Drawing.Point(1081, 201);
            this.filterLabel6.Name = "filterLabel6";
            this.filterLabel6.Size = new System.Drawing.Size(104, 13);
            this.filterLabel6.TabIndex = 21;
            this.filterLabel6.Text = "Show footpaths from";
            // 
            // comboBoxTown
            // 
            this.comboBoxTown.FormattingEnabled = true;
            this.comboBoxTown.Location = new System.Drawing.Point(1084, 218);
            this.comboBoxTown.Name = "comboBoxTown";
            this.comboBoxTown.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTown.TabIndex = 22;
            // 
            // pictureBoxSort
            // 
            this.pictureBoxSort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSort.Location = new System.Drawing.Point(501, 378);
            this.pictureBoxSort.Name = "pictureBoxSort";
            this.pictureBoxSort.Size = new System.Drawing.Size(440, 275);
            this.pictureBoxSort.TabIndex = 23;
            this.pictureBoxSort.TabStop = false;
            // 
            // labelAlgorithm
            // 
            this.labelAlgorithm.AutoSize = true;
            this.labelAlgorithm.Location = new System.Drawing.Point(642, 381);
            this.labelAlgorithm.Name = "labelAlgorithm";
            this.labelAlgorithm.Size = new System.Drawing.Size(135, 13);
            this.labelAlgorithm.TabIndex = 24;
            this.labelAlgorithm.Text = "Modify Algorithm Weighting";
            // 
            // labelHealthMin
            // 
            this.labelHealthMin.AutoSize = true;
            this.labelHealthMin.Location = new System.Drawing.Point(518, 432);
            this.labelHealthMin.Name = "labelHealthMin";
            this.labelHealthMin.Size = new System.Drawing.Size(110, 13);
            this.labelHealthMin.TabIndex = 25;
            this.labelHealthMin.Text = "Health Zone Minimum";
            // 
            // labelHealthMax
            // 
            this.labelHealthMax.AutoSize = true;
            this.labelHealthMax.Location = new System.Drawing.Point(720, 432);
            this.labelHealthMax.Name = "labelHealthMax";
            this.labelHealthMax.Size = new System.Drawing.Size(113, 13);
            this.labelHealthMax.TabIndex = 26;
            this.labelHealthMax.Text = "Health Zone Maximum";
            // 
            // textBoxHealthMin
            // 
            this.textBoxHealthMin.Location = new System.Drawing.Point(634, 429);
            this.textBoxHealthMin.Name = "textBoxHealthMin";
            this.textBoxHealthMin.Size = new System.Drawing.Size(80, 20);
            this.textBoxHealthMin.TabIndex = 27;
            // 
            // textBoxHealthMax
            // 
            this.textBoxHealthMax.Location = new System.Drawing.Point(839, 429);
            this.textBoxHealthMax.Name = "textBoxHealthMax";
            this.textBoxHealthMax.Size = new System.Drawing.Size(80, 20);
            this.textBoxHealthMax.TabIndex = 28;
            // 
            // textBoxSchoolMax
            // 
            this.textBoxSchoolMax.Location = new System.Drawing.Point(839, 455);
            this.textBoxSchoolMax.Name = "textBoxSchoolMax";
            this.textBoxSchoolMax.Size = new System.Drawing.Size(80, 20);
            this.textBoxSchoolMax.TabIndex = 32;
            // 
            // textBoxSchoolMin
            // 
            this.textBoxSchoolMin.Location = new System.Drawing.Point(634, 455);
            this.textBoxSchoolMin.Name = "textBoxSchoolMin";
            this.textBoxSchoolMin.Size = new System.Drawing.Size(80, 20);
            this.textBoxSchoolMin.TabIndex = 31;
            // 
            // labelSchoolMax
            // 
            this.labelSchoolMax.AutoSize = true;
            this.labelSchoolMax.Location = new System.Drawing.Point(720, 458);
            this.labelSchoolMax.Name = "labelSchoolMax";
            this.labelSchoolMax.Size = new System.Drawing.Size(115, 13);
            this.labelSchoolMax.TabIndex = 30;
            this.labelSchoolMax.Text = "School Zone Maximum";
            // 
            // labelSchoolMin
            // 
            this.labelSchoolMin.AutoSize = true;
            this.labelSchoolMin.Location = new System.Drawing.Point(518, 458);
            this.labelSchoolMin.Name = "labelSchoolMin";
            this.labelSchoolMin.Size = new System.Drawing.Size(112, 13);
            this.labelSchoolMin.TabIndex = 29;
            this.labelSchoolMin.Text = "School Zone Minimum";
            // 
            // textBoxServiceMax
            // 
            this.textBoxServiceMax.Location = new System.Drawing.Point(839, 481);
            this.textBoxServiceMax.Name = "textBoxServiceMax";
            this.textBoxServiceMax.Size = new System.Drawing.Size(80, 20);
            this.textBoxServiceMax.TabIndex = 36;
            // 
            // textBoxServiceMin
            // 
            this.textBoxServiceMin.Location = new System.Drawing.Point(634, 481);
            this.textBoxServiceMin.Name = "textBoxServiceMin";
            this.textBoxServiceMin.Size = new System.Drawing.Size(80, 20);
            this.textBoxServiceMin.TabIndex = 35;
            // 
            // labelServiceMax
            // 
            this.labelServiceMax.AutoSize = true;
            this.labelServiceMax.Location = new System.Drawing.Point(720, 484);
            this.labelServiceMax.Name = "labelServiceMax";
            this.labelServiceMax.Size = new System.Drawing.Size(118, 13);
            this.labelServiceMax.TabIndex = 34;
            this.labelServiceMax.Text = "Service Zone Maximum";
            // 
            // labelServiceMin
            // 
            this.labelServiceMin.AutoSize = true;
            this.labelServiceMin.Location = new System.Drawing.Point(518, 484);
            this.labelServiceMin.Name = "labelServiceMin";
            this.labelServiceMin.Size = new System.Drawing.Size(115, 13);
            this.labelServiceMin.TabIndex = 33;
            this.labelServiceMin.Text = "Service Zone Minimum";
            // 
            // labelZones
            // 
            this.labelZones.AutoSize = true;
            this.labelZones.Location = new System.Drawing.Point(689, 405);
            this.labelZones.Name = "labelZones";
            this.labelZones.Size = new System.Drawing.Size(37, 13);
            this.labelZones.TabIndex = 37;
            this.labelZones.Text = "Zones";
            // 
            // labelPathRatings
            // 
            this.labelPathRatings.AutoSize = true;
            this.labelPathRatings.Location = new System.Drawing.Point(674, 529);
            this.labelPathRatings.Name = "labelPathRatings";
            this.labelPathRatings.Size = new System.Drawing.Size(88, 13);
            this.labelPathRatings.TabIndex = 38;
            this.labelPathRatings.Text = "Footpath Ratings";
            // 
            // labelRating1
            // 
            this.labelRating1.AutoSize = true;
            this.labelRating1.Location = new System.Drawing.Point(580, 560);
            this.labelRating1.Name = "labelRating1";
            this.labelRating1.Size = new System.Drawing.Size(13, 13);
            this.labelRating1.TabIndex = 39;
            this.labelRating1.Text = "1";
            // 
            // textBoxRating1
            // 
            this.textBoxRating1.Location = new System.Drawing.Point(600, 557);
            this.textBoxRating1.Name = "textBoxRating1";
            this.textBoxRating1.Size = new System.Drawing.Size(60, 20);
            this.textBoxRating1.TabIndex = 40;
            // 
            // textBoxRating2
            // 
            this.textBoxRating2.Location = new System.Drawing.Point(689, 557);
            this.textBoxRating2.Name = "textBoxRating2";
            this.textBoxRating2.Size = new System.Drawing.Size(60, 20);
            this.textBoxRating2.TabIndex = 42;
            // 
            // labelRating2
            // 
            this.labelRating2.AutoSize = true;
            this.labelRating2.Location = new System.Drawing.Point(670, 560);
            this.labelRating2.Name = "labelRating2";
            this.labelRating2.Size = new System.Drawing.Size(13, 13);
            this.labelRating2.TabIndex = 41;
            this.labelRating2.Text = "2";
            // 
            // textBoxRating3
            // 
            this.textBoxRating3.Location = new System.Drawing.Point(775, 557);
            this.textBoxRating3.Name = "textBoxRating3";
            this.textBoxRating3.Size = new System.Drawing.Size(60, 20);
            this.textBoxRating3.TabIndex = 44;
            // 
            // labelRating3
            // 
            this.labelRating3.AutoSize = true;
            this.labelRating3.Location = new System.Drawing.Point(756, 560);
            this.labelRating3.Name = "labelRating3";
            this.labelRating3.Size = new System.Drawing.Size(13, 13);
            this.labelRating3.TabIndex = 43;
            this.labelRating3.Text = "3";
            // 
            // textBoxRating4
            // 
            this.textBoxRating4.Location = new System.Drawing.Point(647, 583);
            this.textBoxRating4.Name = "textBoxRating4";
            this.textBoxRating4.Size = new System.Drawing.Size(60, 20);
            this.textBoxRating4.TabIndex = 46;
            // 
            // labelRating4
            // 
            this.labelRating4.AutoSize = true;
            this.labelRating4.Location = new System.Drawing.Point(628, 586);
            this.labelRating4.Name = "labelRating4";
            this.labelRating4.Size = new System.Drawing.Size(13, 13);
            this.labelRating4.TabIndex = 45;
            this.labelRating4.Text = "4";
            // 
            // textBoxRating5
            // 
            this.textBoxRating5.Location = new System.Drawing.Point(732, 583);
            this.textBoxRating5.Name = "textBoxRating5";
            this.textBoxRating5.Size = new System.Drawing.Size(60, 20);
            this.textBoxRating5.TabIndex = 48;
            // 
            // labelRating5
            // 
            this.labelRating5.AutoSize = true;
            this.labelRating5.Location = new System.Drawing.Point(713, 586);
            this.labelRating5.Name = "labelRating5";
            this.labelRating5.Size = new System.Drawing.Size(13, 13);
            this.labelRating5.TabIndex = 47;
            this.labelRating5.Text = "5";
            // 
            // buttonUpdateAlgorithm
            // 
            this.buttonUpdateAlgorithm.Location = new System.Drawing.Point(647, 622);
            this.buttonUpdateAlgorithm.Name = "buttonUpdateAlgorithm";
            this.buttonUpdateAlgorithm.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateAlgorithm.TabIndex = 49;
            this.buttonUpdateAlgorithm.Text = "Update";
            this.buttonUpdateAlgorithm.UseVisualStyleBackColor = true;
            this.buttonUpdateAlgorithm.Click += new System.EventHandler(this.buttonUpdateAlgorithm_Click);
            // 
            // pictureBoxZones
            // 
            this.pictureBoxZones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxZones.Location = new System.Drawing.Point(508, 401);
            this.pictureBoxZones.Name = "pictureBoxZones";
            this.pictureBoxZones.Size = new System.Drawing.Size(424, 119);
            this.pictureBoxZones.TabIndex = 50;
            this.pictureBoxZones.TabStop = false;
            // 
            // pictureBoxPathRatings
            // 
            this.pictureBoxPathRatings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPathRatings.Location = new System.Drawing.Point(574, 526);
            this.pictureBoxPathRatings.Name = "pictureBoxPathRatings";
            this.pictureBoxPathRatings.Size = new System.Drawing.Size(275, 90);
            this.pictureBoxPathRatings.TabIndex = 51;
            this.pictureBoxPathRatings.TabStop = false;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(732, 622);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 52;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(948, 378);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(500, 275);
            this.gMapControl1.TabIndex = 54;
            this.gMapControl1.Zoom = 0D;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1316, 720);
            this.Controls.Add(this.gMapControl1);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.textBoxRating5);
            this.Controls.Add(this.labelRating5);
            this.Controls.Add(this.textBoxRating4);
            this.Controls.Add(this.labelRating4);
            this.Controls.Add(this.textBoxRating3);
            this.Controls.Add(this.labelRating3);
            this.Controls.Add(this.textBoxRating2);
            this.Controls.Add(this.labelRating2);
            this.Controls.Add(this.textBoxRating1);
            this.Controls.Add(this.labelRating1);
            this.Controls.Add(this.labelPathRatings);
            this.Controls.Add(this.pictureBoxPathRatings);
            this.Controls.Add(this.buttonUpdateAlgorithm);
            this.Controls.Add(this.labelZones);
            this.Controls.Add(this.textBoxServiceMax);
            this.Controls.Add(this.textBoxServiceMin);
            this.Controls.Add(this.labelServiceMax);
            this.Controls.Add(this.labelServiceMin);
            this.Controls.Add(this.textBoxSchoolMax);
            this.Controls.Add(this.textBoxSchoolMin);
            this.Controls.Add(this.labelSchoolMax);
            this.Controls.Add(this.labelSchoolMin);
            this.Controls.Add(this.textBoxHealthMax);
            this.Controls.Add(this.textBoxHealthMin);
            this.Controls.Add(this.labelHealthMax);
            this.Controls.Add(this.labelHealthMin);
            this.Controls.Add(this.labelAlgorithm);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.comboBoxTown);
            this.Controls.Add(this.filterLabel6);
            this.Controls.Add(this.filterLabel3);
            this.Controls.Add(this.filterLabel2);
            this.Controls.Add(this.filterLabel1);
            this.Controls.Add(this.textBoxFilterCondition);
            this.Controls.Add(this.buttonShowAll);
            this.Controls.Add(this.filterLabel5);
            this.Controls.Add(this.filterLabel4);
            this.Controls.Add(this.textBoxFilterFaults);
            this.Controls.Add(this.labelFilterResults);
            this.Controls.Add(this.buttonUpdateResults);
            this.Controls.Add(this.pictureBoxTown);
            this.Controls.Add(this.pictureBoxCondition);
            this.Controls.Add(this.pictureBoxFaults);
            this.Controls.Add(this.pictureBoxFilter);
            this.Controls.Add(this.pictureBoxZones);
            this.Controls.Add(this.pictureBoxSort);
            this.Controls.Add(this.listBoxData);
            this.Controls.Add(this.listBoxDataLong);
            this.Name = "Form1";
            this.Text = "Waikato RATA Footpath Maintenance Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFaults)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPathRatings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBoxData;

        private System.Windows.Forms.ListBox listBoxDataLong;
        private System.Windows.Forms.Button buttonUpdateResults;
        private System.Windows.Forms.PictureBox pictureBoxFilter;
        private System.Windows.Forms.Label labelFilterResults;
        private System.Windows.Forms.TextBox textBoxFilterFaults;
        private System.Windows.Forms.Label filterLabel4;
        private System.Windows.Forms.Label filterLabel5;
        private System.Windows.Forms.PictureBox pictureBoxFaults;
        private System.Windows.Forms.Button buttonShowAll;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label filterLabel2;
        private System.Windows.Forms.Label filterLabel1;
        private System.Windows.Forms.TextBox textBoxFilterCondition;
        private System.Windows.Forms.PictureBox pictureBoxCondition;
        private System.Windows.Forms.Label filterLabel3;
        private System.Windows.Forms.PictureBox pictureBoxTown;
        private System.Windows.Forms.Label filterLabel6;
        private System.Windows.Forms.ComboBox comboBoxTown;
        private System.Windows.Forms.PictureBox pictureBoxSort;
        private System.Windows.Forms.Label labelAlgorithm;
        private System.Windows.Forms.Label labelHealthMin;
        private System.Windows.Forms.Label labelHealthMax;
        private System.Windows.Forms.TextBox textBoxHealthMin;
        private System.Windows.Forms.TextBox textBoxHealthMax;
        private System.Windows.Forms.TextBox textBoxSchoolMax;
        private System.Windows.Forms.TextBox textBoxSchoolMin;
        private System.Windows.Forms.Label labelSchoolMax;
        private System.Windows.Forms.Label labelSchoolMin;
        private System.Windows.Forms.TextBox textBoxServiceMax;
        private System.Windows.Forms.TextBox textBoxServiceMin;
        private System.Windows.Forms.Label labelServiceMax;
        private System.Windows.Forms.Label labelServiceMin;
        private System.Windows.Forms.Label labelZones;
        private System.Windows.Forms.Label labelPathRatings;
        private System.Windows.Forms.Label labelRating1;
        private System.Windows.Forms.TextBox textBoxRating1;
        private System.Windows.Forms.TextBox textBoxRating2;
        private System.Windows.Forms.Label labelRating2;
        private System.Windows.Forms.TextBox textBoxRating3;
        private System.Windows.Forms.Label labelRating3;
        private System.Windows.Forms.TextBox textBoxRating4;
        private System.Windows.Forms.Label labelRating4;
        private System.Windows.Forms.TextBox textBoxRating5;
        private System.Windows.Forms.Label labelRating5;
        private System.Windows.Forms.Button buttonUpdateAlgorithm;
        private System.Windows.Forms.PictureBox pictureBoxZones;
        private System.Windows.Forms.PictureBox pictureBoxPathRatings;
        private System.Windows.Forms.Button buttonReset;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
    }
}

