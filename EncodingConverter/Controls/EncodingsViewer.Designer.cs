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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddToFavorites = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tstbSearchEncodings = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblSelectedEncoding = new System.Windows.Forms.ToolStripLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbRemoveFavoriteEncoding = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tstbSearchFavorites = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.lblSelectedEncodingFavorites = new System.Windows.Forms.ToolStripLabel();
            this.lvAllEncodings = new EncodingConverter.Controls.SearchableEncodingListView();
            this.lvFavoriteEncodings = new EncodingConverter.Controls.SearchableEncodingListView();
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
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddToFavorites,
            this.toolStripSeparator1,
            this.tstbSearchEncodings,
            this.toolStripSeparator3,
            this.lblSelectedEncoding});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(518, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddToFavorites
            // 
            this.tsbAddToFavorites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddToFavorites.Image = global::EncodingConverter.Properties.Resources.star;
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
            // lblSelectedEncoding
            // 
            this.lblSelectedEncoding.Name = "lblSelectedEncoding";
            this.lblSelectedEncoding.Size = new System.Drawing.Size(0, 22);
            this.lblSelectedEncoding.ToolTipText = "Selected encoding name";
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
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRemoveFavoriteEncoding,
            this.toolStripSeparator2,
            this.tstbSearchFavorites,
            this.toolStripSeparator4,
            this.lblSelectedEncodingFavorites});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(518, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbRemoveFavoriteEncoding
            // 
            this.tsbRemoveFavoriteEncoding.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemoveFavoriteEncoding.Image = global::EncodingConverter.Properties.Resources.delete;
            this.tsbRemoveFavoriteEncoding.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveFavoriteEncoding.Name = "tsbRemoveFavoriteEncoding";
            this.tsbRemoveFavoriteEncoding.Size = new System.Drawing.Size(23, 22);
            this.tsbRemoveFavoriteEncoding.Text = "toolStripButton1";
            this.tsbRemoveFavoriteEncoding.ToolTipText = "Remove selected";
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
            // lblSelectedEncodingFavorites
            // 
            this.lblSelectedEncodingFavorites.Name = "lblSelectedEncodingFavorites";
            this.lblSelectedEncodingFavorites.Size = new System.Drawing.Size(0, 22);
            this.lblSelectedEncodingFavorites.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSelectedEncodingFavorites.ToolTipText = "Selected encoding name";
            // 
            // lvAllEncodings
            // 
            this.lvAllEncodings.CheckBoxes = true;
            this.lvAllEncodings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAllEncodings.GridLines = true;
            this.lvAllEncodings.HideSelection = false;
            this.lvAllEncodings.Location = new System.Drawing.Point(3, 28);
            this.lvAllEncodings.MultiSelect = false;
            this.lvAllEncodings.Name = "lvAllEncodings";
            this.lvAllEncodings.ShowItemToolTips = true;
            this.lvAllEncodings.Size = new System.Drawing.Size(518, 256);
            this.lvAllEncodings.TabIndex = 2;
            this.lvAllEncodings.UseCompatibleStateImageBehavior = false;
            this.lvAllEncodings.View = System.Windows.Forms.View.Details;
            // 
            // lvFavoriteEncodings
            // 
            this.lvFavoriteEncodings.CheckBoxes = true;
            this.lvFavoriteEncodings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFavoriteEncodings.GridLines = true;
            this.lvFavoriteEncodings.HideSelection = false;
            this.lvFavoriteEncodings.Location = new System.Drawing.Point(3, 28);
            this.lvFavoriteEncodings.MultiSelect = false;
            this.lvFavoriteEncodings.Name = "lvFavoriteEncodings";
            this.lvFavoriteEncodings.ShowItemToolTips = true;
            this.lvFavoriteEncodings.Size = new System.Drawing.Size(518, 256);
            this.lvFavoriteEncodings.TabIndex = 3;
            this.lvFavoriteEncodings.UseCompatibleStateImageBehavior = false;
            this.lvFavoriteEncodings.View = System.Windows.Forms.View.Details;
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddToFavorites;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox tstbSearchEncodings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel lblSelectedEncoding;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbRemoveFavoriteEncoding;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox tstbSearchFavorites;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel lblSelectedEncodingFavorites;
        private SearchableEncodingListView lvAllEncodings;
        private SearchableEncodingListView lvFavoriteEncodings;
    }
}
