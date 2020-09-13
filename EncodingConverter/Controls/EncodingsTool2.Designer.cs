namespace EncodingConverter.Controls
{
    partial class EncodingsViewer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncodingsViewer));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lvAllEncodings = new System.Windows.Forms.ListView();
            this.columnHeader1_encodingName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2_body = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6_codepage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddToFavorites = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tstbSearchEncodings = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1_selectedEncoding = new System.Windows.Forms.ToolStripLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvFavoriteEncodings = new System.Windows.Forms.ListView();
            this.columnHeader3_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4_body = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5_codepage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tstbSearchFavorites = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1_selectedEncodingFavorites = new System.Windows.Forms.ToolStripLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(532, 313);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvAllEncodings);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(524, 287);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "All encodings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lvAllEncodings
            // 
            this.lvAllEncodings.CheckBoxes = true;
            this.lvAllEncodings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1_encodingName,
            this.columnHeader2_body,
            this.columnHeader6_codepage});
            this.lvAllEncodings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAllEncodings.FullRowSelect = true;
            this.lvAllEncodings.GridLines = true;
            this.lvAllEncodings.HideSelection = false;
            this.lvAllEncodings.Location = new System.Drawing.Point(3, 28);
            this.lvAllEncodings.MultiSelect = false;
            this.lvAllEncodings.Name = "lvAllEncodings";
            this.lvAllEncodings.ShowItemToolTips = true;
            this.lvAllEncodings.Size = new System.Drawing.Size(518, 256);
            this.lvAllEncodings.TabIndex = 0;
            this.lvAllEncodings.UseCompatibleStateImageBehavior = false;
            this.lvAllEncodings.View = System.Windows.Forms.View.Details;
            this.lvAllEncodings.VirtualMode = true;
            // 
            // columnHeader1_encodingName
            // 
            this.columnHeader1_encodingName.Text = "Name";
            this.columnHeader1_encodingName.Width = 212;
            // 
            // columnHeader2_body
            // 
            this.columnHeader2_body.Text = "Body Name";
            this.columnHeader2_body.Width = 91;
            // 
            // columnHeader6_codepage
            // 
            this.columnHeader6_codepage.Text = "Code Page";
            this.columnHeader6_codepage.Width = 64;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddToFavorites,
            this.toolStripSeparator1,
            this.tstbSearchEncodings,
            this.toolStripSeparator3,
            this.toolStripLabel1_selectedEncoding});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(518, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddToFavorites
            // 
            this.tsbAddToFavorites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddToFavorites.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddToFavorites.Image")));
            this.tsbAddToFavorites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddToFavorites.Name = "tsbAddToFavorites";
            this.tsbAddToFavorites.Size = new System.Drawing.Size(23, 22);
            this.tsbAddToFavorites.Text = "toolStripButton1";
            this.tsbAddToFavorites.ToolTipText = "Add selected to favorite";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tstbSearchEncodings
            // 
            this.tstbSearchEncodings.Name = "tstbSearchEncodings";
            this.tstbSearchEncodings.Size = new System.Drawing.Size(150, 25);
            this.tstbSearchEncodings.ToolTipText = "Search";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1_selectedEncoding
            // 
            this.toolStripLabel1_selectedEncoding.Name = "toolStripLabel1_selectedEncoding";
            this.toolStripLabel1_selectedEncoding.Size = new System.Drawing.Size(0, 22);
            this.toolStripLabel1_selectedEncoding.ToolTipText = "Selected encoding name";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvFavoriteEncodings);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(524, 287);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Favorite encodings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lvFavoriteEncodings
            // 
            this.lvFavoriteEncodings.CheckBoxes = true;
            this.lvFavoriteEncodings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3_name,
            this.columnHeader4_body,
            this.columnHeader5_codepage});
            this.lvFavoriteEncodings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFavoriteEncodings.GridLines = true;
            this.lvFavoriteEncodings.HideSelection = false;
            this.lvFavoriteEncodings.Location = new System.Drawing.Point(3, 28);
            this.lvFavoriteEncodings.MultiSelect = false;
            this.lvFavoriteEncodings.Name = "lvFavoriteEncodings";
            this.lvFavoriteEncodings.ShowItemToolTips = true;
            this.lvFavoriteEncodings.Size = new System.Drawing.Size(518, 256);
            this.lvFavoriteEncodings.TabIndex = 1;
            this.lvFavoriteEncodings.UseCompatibleStateImageBehavior = false;
            this.lvFavoriteEncodings.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3_name
            // 
            this.columnHeader3_name.Text = "Name";
            this.columnHeader3_name.Width = 225;
            // 
            // columnHeader4_body
            // 
            this.columnHeader4_body.Text = "Body Name";
            this.columnHeader4_body.Width = 93;
            // 
            // columnHeader5_codepage
            // 
            this.columnHeader5_codepage.Text = "Code Page";
            this.columnHeader5_codepage.Width = 70;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.tstbSearchFavorites,
            this.toolStripSeparator4,
            this.toolStripLabel1_selectedEncodingFavorites});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(518, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton1";
            this.toolStripButton3.ToolTipText = "Remove selected";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tstbSearchFavorites
            // 
            this.tstbSearchFavorites.Name = "tstbSearchFavorites";
            this.tstbSearchFavorites.Size = new System.Drawing.Size(150, 25);
            this.tstbSearchFavorites.ToolTipText = "Search";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1_selectedEncodingFavorites
            // 
            this.toolStripLabel1_selectedEncodingFavorites.Name = "toolStripLabel1_selectedEncodingFavorites";
            this.toolStripLabel1_selectedEncodingFavorites.Size = new System.Drawing.Size(0, 22);
            this.toolStripLabel1_selectedEncodingFavorites.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripLabel1_selectedEncodingFavorites.ToolTipText = "Selected encoding name";
            // 
            // EncodingsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "EncodingsViewer";
            this.Size = new System.Drawing.Size(532, 313);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView lvAllEncodings;
        private System.Windows.Forms.ColumnHeader columnHeader1_encodingName;
        private System.Windows.Forms.ColumnHeader columnHeader2_body;
        private System.Windows.Forms.ColumnHeader columnHeader6_codepage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddToFavorites;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox tstbSearchEncodings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1_selectedEncoding;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvFavoriteEncodings;
        private System.Windows.Forms.ColumnHeader columnHeader3_name;
        private System.Windows.Forms.ColumnHeader columnHeader4_body;
        private System.Windows.Forms.ColumnHeader columnHeader5_codepage;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox tstbSearchFavorites;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1_selectedEncodingFavorites;
    }
}
